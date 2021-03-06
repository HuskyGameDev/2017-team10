﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerDie()
    {
        gameObject.SetActive(false);
        GameObject[] sentries = GameObject.FindGameObjectsWithTag("SentryEnemy");
        foreach(GameObject go in sentries)
        {
            go.GetComponent<AIMovement>().Patrol();
        }

        GameObject[] attackers = GameObject.FindGameObjectsWithTag("AttackEnemy");
        foreach(GameObject go in attackers)
        {
            go.GetComponent<AIMovement>().Patrol();
        }
    }
}
