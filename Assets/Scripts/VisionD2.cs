using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionD2 : MonoBehaviour {

    public GameObject player; //The player
    public bool playerSeen = false;
    public float counter;

    private IEnumerator timer;

	// Use this for initialization
	void Start () {
        player = Camera.main.transform.parent.gameObject;
        timer = Timer();
	}
	
	// Update is called once per frame
	void Update () {

	}

    
    //This is for when the player is in the vision cone, but can't be seen.
    private void OnTriggerStay(Collider other) {

        RaycastHit hit;
        Vector3 playerToBot = player.transform.position - transform.position;
        Physics.Raycast(transform.position, playerToBot, out hit);

        if (hit.transform.tag != "Player") { //If the player can be spotted
            playerSeen = false;
        } else {
            playerSeen = true;
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") { //If the player enters the vision cone

            RaycastHit hit;
            Vector3 playerToBot = player.transform.position - transform.position;
            Physics.Raycast(transform.position, playerToBot, out hit);

            if (hit.transform.tag == "Player") { //If the player can be spotted
                playerSeen = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            playerSeen = false;
        }
    }

    IEnumerator Timer() {
        while (playerSeen) {
            counter += Time.deltaTime;
            yield return null;
        }
    }

}
