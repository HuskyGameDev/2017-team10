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
    ItemSwitch fub;

    void Start() {
        grenadeAmmo = gameObject.GetComponent<ItemHeld>();
        fub = gameObject.GetComponentInParent<ItemSwitch>();
        playerT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update() {
        if (paused) {
            Throw();
        }
    }

    public void SetPause() {
        paused = !paused;
    }

    private void Throw() {
        if (Input.GetMouseButtonDown(0)) {
            grenadeAmmo.SetAmmo(grenadeAmmo.GetAmmo() - 1);
            fub.recheckItems = true;
            GameObject grenade = Instantiate(grenadePre);
            grenade.transform.position = playerT.position + playerT.forward * 1.06125f;
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.velocity = playerT.forward * gThrowSpeed;

//            if(grenadeAmmo.GetAmmo() <= 0) {
//               transform.parent.GetComponent<ItemSwitch>().recheckItems = true;
//            }
        }
    }
}
