using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeDet : MonoBehaviour {

    GameObject blast;
    bool goneOff = false;

    private void Start() {
        blast = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!goneOff) {
            blast.SetActive(true);
        }
    }

}
