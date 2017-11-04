using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    public int itemRep; //The prefab for the object to be picked up
    private GameObject inventNode; //The node to put prefabs

	// Use this for initialization
	void Start () {
        inventNode = GameObject.FindWithTag("InventoryNode");
	}

    //Use this for when the player interacts with this item
    public void PickUp() {
        //Adding it to the simple inventory
        ItemHeld fub = inventNode.transform.GetChild(itemRep).GetComponent<ItemHeld>();
        fub.SetAmmo(fub.GetAmmo() + 1);
        Destroy(gameObject);

    }
}
