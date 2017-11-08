using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetect : MonoBehaviour {

    public bool trigger = false;
    public GameObject player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 relPos = player.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, relPos, out hit))
                if (hit.collider.gameObject.CompareTag("Player"))
                    trigger = true;
        }
    }
}
