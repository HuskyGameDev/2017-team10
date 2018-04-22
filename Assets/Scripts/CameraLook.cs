using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour {

    //The rotation point for the camera
    //public GameObject focusPoint;

    //the rotateSpeed multiplier, while rots is the pitch and yaw of the camera.
    public float rotateSpeedx = 2.5f,rotateSpeedy = 1.5f, rotY, rotX;


    public bool negMove = false;

    //cursorLock checks if the cursor is locked, 
    //invertedLook checks if the y input axis needs to be switched.
    public bool cursorLock, invertedLook = false;

    private float InputX, InputY;

	// Use this for initialization
	void Start () {
        cursorLock = true;
        InputX = Input.GetAxis("Mouse X");
	}
	
	// Update is called once per frame
	void Update () {
        InputX = Input.GetAxis("Mouse X");
        InputY = Input.GetAxis("Mouse Y");

        
        rotX += rotateSpeedx * InputX;
        //rotX = Mathf.Clamp(rotX, -40f, 40f);

        
        if (invertedLook)
            rotY += rotateSpeedy * Input.GetAxis("Mouse Y");
        else
            rotY += -rotateSpeedy * Input.GetAxis("Mouse Y");


        //rotY = Mathf.Clamp(rotY, -15f, 45); 

        transform.localEulerAngles = new Vector3(rotY, rotX);

        if (cursorLock)
            //Cursor.lockState = CursorLockMode.Locked;

		if (Input.GetKeyDown (KeyCode.Escape)) {
		}
            //cursorLock = !cursorLock;

	}
}
