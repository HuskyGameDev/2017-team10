//InteractScript handles interactions player has with items and triggers in the worldspace
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {

    public float interactRange = 2.5f; //The player's "reach"
    GameObject toBeAdded; //This will come in handy when an item must be added to player's inventory.

    private Transform CamT; //To shoot the raycast out from

    // Use this for initialization
    void Start() {
        CamT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;

        if (Input.GetButtonDown("Interact")) { //This will be f as standard
            Physics.Raycast(CamT.position, CamT.forward, out hit, interactRange); //Reach out
            if (hit.transform != null) {
                if (hit.transform.tag == "Item") { //Item is anything that goes into inventory
                    if (hit.transform.GetComponent<ItemScript>() != null) {
                        hit.transform.GetComponent<ItemScript>().PickUp();
                        transform.GetComponent<ItemSwitch>().recheckItems = true; //Recheck the items to see if more ammo has been added that would affect what's being shown
                    }else {
                        Debug.LogError(hit.transform.name + " NEEDS ITEM SCRIPT AT " + hit.transform.position);
                    }
                }
                else if (hit.transform.tag == "Interactable") { //Interactable is anything in the world, such as buttons or notes
                    hit.transform.SendMessage("InteractAct");
                }
            }
        }
    }
}
