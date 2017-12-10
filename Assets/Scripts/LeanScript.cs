//LeanScript handles leaning the main camera to create the effect of the character leaning over.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanScript : MonoBehaviour {

    enum Leaning { Right, Center, Left };
    Leaning curLeaning;

    public float speed = 5;
    public float leanx = .3f;
    private float step, step2;

    public bool paused = false;

    // Use this for initialization
    void Start() {
        curLeaning = Leaning.Center;
        step = speed * Time.deltaTime;
        step2 = speed * 1.5f * Time.deltaTime;
        //leany = .25f;
    }

    // Update is called once per frame
    void Update() {
        if (!paused) {
            LeanInput();
            LeanMove();
        }
    }

    void LeanInput() { //Handles the input, which then goes
        if (Input.GetButton("LeanLeft"))
            curLeaning = Leaning.Left;
        else if (Input.GetButton("LeanRight"))
            curLeaning = Leaning.Right;
        else if (Input.GetButton("LeanLeft") && Input.GetButton("LeanRight"))
            curLeaning = Leaning.Center;
        else
            curLeaning = Leaning.Center;
    }

    void LeanMove() { //Handles actual movement

        if (curLeaning == Leaning.Center) { //Moving to center

            if (transform.localPosition.x != 0) {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, transform.localPosition.y), step2);
            }

        }
        else if (curLeaning == Leaning.Left) { //Leaning Left

            if (transform.localPosition.x != -leanx)
                if (!Physics.Raycast(transform.localPosition, transform.transform.right * -1, leanx - transform.localPosition.x)) {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(-leanx, transform.localPosition.y), step);
                }
        }
        else if (curLeaning == Leaning.Right) { //Leaning Right

            if (transform.localPosition.x != leanx)
                if (!Physics.Raycast(transform.localPosition, transform.transform.right, leanx - transform.localPosition.x)) {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(leanx, transform.localPosition.y), step);
                }
        }
    }
}