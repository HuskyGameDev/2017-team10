using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class AIMove : MonoBehaviour {
    private Rigidbody rb;
    private Stopwatch timer = new Stopwatch();
    private float speed;
    public float speedMod;

	// Use this for initialization
	void Start () {
        timer.Start();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        // Varies the speed as a sin function with cycle of 10 seconds
        float time = timer.ElapsedMilliseconds / 1000.0f;
        float speed = Mathf.Sin(Mathf.PI * time);

        Vector3 movement = new Vector3(speed * speedMod, 0.0f, 0.0f);
        
    }
}
