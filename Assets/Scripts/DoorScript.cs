using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public bool open = false; //Whether or not the door is open
    public float speed = 10;

    public  Vector3 openPosition;
    public Vector3 closePosition;

	// Use this for initialization
	void Start () {
        openPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z) - transform.right * transform.localScale.x;
        closePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;

        if (open && transform.position != openPosition) {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, step);
        } else if(!open && transform.position != closePosition) {
            transform.position = Vector3.MoveTowards(transform.position, closePosition, step);
        }
	}

    public void InteractAct() {
        open = !open;
    }
}
