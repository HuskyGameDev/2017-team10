//GrenadeBlast handles the actual explosion caused by the grenade.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBlast : MonoBehaviour {

    public float upScale; //This will be the new size of the sphere.

    public float multi = 1.025f, bounds = 25f; //This will be by how fast it grows.

    private bool detonated = false;

    private void OnEnable() {
        detonated = true;
    }

    private void OnTriggerEnter(Collider other) { 
        if(other.transform.GetComponent<GrenadeHit>() != null) {
            other.transform.SendMessage("GHit");
        }
    }

    // Update is called once per frame
    void Update () {
        if (detonated) {
            if (transform.localScale.x > bounds) {
                Destroy(gameObject);
            } else {
                upScale += multi * Time.deltaTime * 100;
                transform.localScale = new Vector3(upScale, upScale, upScale);
                transform.GetComponent<SphereCollider>().radius = transform.localScale.x * .125f/6.75f;

                Mathf.Clamp(transform.GetComponent<SphereCollider>().radius, 0, bounds * .125f/6.75f);
                Mathf.Clamp(transform.localScale.x, 0, bounds);
                Mathf.Clamp(transform.localScale.y, 0, bounds);
                Mathf.Clamp(transform.localScale.z, 0, bounds);
            }
        }
	}
}
