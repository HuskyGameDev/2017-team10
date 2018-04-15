using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerCamMove : MonoBehaviour {

    Vector3 iV;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    private void Movement() {
        //iV = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * .5f;
        iV = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.LeftControl)) {
            //iV += new Vector3(0, 1, 0);
        } else if (Input.GetKey(KeyCode.Space)) {

        }
        //iV = Vector3.Normalize(iV);
        
        transform.localPosition += iV;
    }
}
