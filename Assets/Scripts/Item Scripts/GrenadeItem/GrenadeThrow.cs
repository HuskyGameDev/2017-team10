using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour {

    public float gThrowSpeed = 10;

    private GameObject grenadePre;
    private Transform playerT;
    private bool oneGrenadeOnly = false; //Makes sure a player is throwing one grenade at a time

    void Start() {
        grenadePre = Resources.Load("GrenadeThrown.prefab") as GameObject;
        playerT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void Throw() {
        if(Input.GetMouseButtonDown(0) && !oneGrenadeOnly) {
            GameObject grenade = Instantiate(grenadePre);
            grenade.transform.position = transform.position + playerT.forward * 1.5f;
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.velocity = playerT.forward * gThrowSpeed;
        } 

        if (Input.GetMouseButtonUp(0) && oneGrenadeOnly) {
            GameObject grenade = Instantiate(grenadePre);
            grenade.transform.position = transform.position + playerT.forward * 1.5f;
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.velocity = playerT.forward * gThrowSpeed;

            oneGrenadeOnly = false;
        }
    }
}
