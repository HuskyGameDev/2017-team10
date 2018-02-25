using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetect : MonoBehaviour {
    
    public GameObject player;
    public GameObject enemy;
    public AudioClip enemydetected;


    private AudioSource source;
    public bool detected = false;
    public bool mia = false;
    public bool inCone = false;
    public bool barked = false; //This is to control the bark given when the player is seen

    private float timer;

	// Use this for initialization
	void Start () {
        player = Camera.main.transform.parent.gameObject;
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
                        if (!barked) { //When the player is first spotted
                            if(enemydetected != null) //Make sure there's an audio clip to play.
                                source.PlayOneShot(enemydetected);
                            barked = true;
                        }
                        
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
                barked = false; //When the player is lost
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

                        if (!barked) {
                            source.PlayOneShot(enemydetected);
                            barked = true;
                        }
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
        if(timer >= 10.0)
        {
            detected = false;
            enemy.GetComponent<AIMovement>().Patrol();
            timer = 0;
        }
    }
}
