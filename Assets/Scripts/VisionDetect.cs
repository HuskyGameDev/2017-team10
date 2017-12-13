using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetect : MonoBehaviour {
    
    public GameObject player;
    public GameObject enemy;

    private bool detected = false;
    private bool mia = false;
    private bool inCone = false;

    private float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(detected && mia && !inCone)
        {
            Timer();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inCone = true;
            Vector3 relPos = player.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, relPos, out hit))
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    if (!detected)
                    {
                        enemy.GetComponent<AIMovement>().OnDetect();
                        detected = true;
                    }
                    mia = false;
                    timer = 0;
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inCone = false;
            if(detected && !mia)
            {
                mia = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 relPos = player.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, relPos, out hit))
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    if (!detected)
                    {
                        enemy.GetComponent<AIMovement>().OnDetect();
                        detected = true;
                    }
                    mia = false;
                    timer = 0;
                }
                else
                {
                    Timer();
                }
        }
    }

    /*
     * Tracks how long the enemy has lost vision of the player
     */
    private void Timer()
    {
        timer += Time.deltaTime;
        if(timer >= 5.0)
        {
            detected = false;
            enemy.GetComponent<AIMovement>().Patrol();
            timer = 0;
        }
    }
}
