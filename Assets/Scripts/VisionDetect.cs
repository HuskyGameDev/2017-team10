using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetect : MonoBehaviour {

    public bool trigger = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        trigger = true;
        if (other.gameObject.CompareTag("Player"))
        {
            other.enabled = false;
        }
    }
}
