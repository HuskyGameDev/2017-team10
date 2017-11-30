//GrenadeThrow handles the input and launching of the grenade
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour {

    public float gThrowSpeed = 10;
    public GameObject grenadePre;
    public bool getRid = false, paused = false;

    private Transform playerT;
    private bool oneGrenadeOnly = false; //Makes sure a player is throwing one grenade at a time

    ItemHeld grenadeAmmo;

    void Start() {
        grenadeAmmo = gameObject.GetComponent<ItemHeld>();
        playerT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update() {
        Throw();
    }

    private void Throw() {
        if (Input.GetMouseButtonDown(0) && !paused) {
            grenadeAmmo.SetAmmo(grenadeAmmo.GetAmmo() - 1);
            GameObject grenade = Instantiate(grenadePre);
            grenade.transform.position = playerT.position + playerT.forward * 1.06125f;
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.velocity = playerT.forward * gThrowSpeed;

            if(grenadeAmmo.GetAmmo() <= 0) {
                transform.parent.GetComponent<ItemSwitch>().recheckItems = true;
            }
        }
    }
}
