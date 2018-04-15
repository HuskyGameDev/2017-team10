using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionD2 : MonoBehaviour {

    public GameObject player; //The player
    public bool playerSeen = false;
    public float counter;
    public bool bark1 = false, bark2 = false; //bark1 is for when the player is seen, bark2 ensures that there won't be multiple barks.
    public AudioClip enemydetected;
    public float timerLimit = 2f; //How long it takes for the player to be spotted.

    private AudioSource source;
    public GameObject enemy;
    private float timer = 0; //For detection
    private float timer2 = 0; //For forgetting the player

    // Use this for initialization
    void Start () {
        enemy = transform.parent.parent.gameObject;
        player = Camera.main.transform.parent.gameObject;
        
	}
	
	// Update is called once per frame
	void Update () {
        if(bark1)
            if (!bark2) {
                source.PlayOneShot(enemydetected);
                bark2 = true;
            }

        if (playerSeen) {
            timer2 = 0;
            timer += Time.deltaTime;

            if(timer > timerLimit) {
                enemy.GetComponent<AIMovement>().OnDetect();

            }
        } else {
            timer = 0;
            timer2 += Time.deltaTime;

            if(timer2 > 3) {
                enemy.GetComponent<AIMovement>().Patrol();
                bark1 = false;
                bark2 = false;
            }
        }
	}

    
    //This is for when the player is in the vision cone, but can't be seen.
    private void OnTriggerStay(Collider other) {

        RaycastHit hit;
        Vector3 playerToBot = player.transform.position - transform.position;
        Physics.Raycast(transform.position, playerToBot, out hit);

        if (hit.transform.tag != "Player") { //If the player can be spotted
            playerSeen = false;
            bark1 = false;
            bark2 = false;
        } else {
            playerSeen = true;
            bark1 = true;
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") { //If the player enters the vision cone

            RaycastHit hit;
            Vector3 playerToBot = player.transform.position - transform.position;
            Physics.Raycast(transform.position, playerToBot, out hit);

            if (hit.transform.tag == "Player") { //If the player can be spotted
                playerSeen = true;
                bark1 = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            playerSeen = false;
            bark1 = false;
            bark2 = false;
        }
    }

}
