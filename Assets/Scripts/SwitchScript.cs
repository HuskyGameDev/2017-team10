using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {

    public Transform interactableObj; //This is the transform for the interactable object that we want to affect with the switch

	// Use this for initialization
	void Start () {
		if(interactableObj == null) {
            Debug.Log(transform.name + " DOESN'T HAVE A THING THAT IT AFFECTS!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InteractAct() {
        interactableObj.SendMessage("InteractAct");
    }
}
