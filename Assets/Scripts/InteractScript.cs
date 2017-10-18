using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {

    public float interactRange = 2.5f; //The player's "reach"
    GameObject toBeAdded; //This will come in handy when an item must be added to player's inventory.
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        if (Input.GetButtonDown("Interact")) { //This will be f as standard
            Physics.Raycast(transform.position, transform.forward, out hit, interactRange); //Reach out

            if(hit.transform.tag == "Item") { //Item is anything that goes into inventory
                toBeAdded = hit.transform.GetComponent<ItemHolder>().inventoryItem;
                Instantiate(toBeAdded, transform.GetChild(0).localPosition, transform.rotation, transform.GetChild(0));
                Destroy(hit.transform.gameObject);
            } else if (hit.transform.tag == "Interactable") { //Interactable is anything in the world, such as buttons or notes

            }
        }
	}
}
