using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanScript : MonoBehaviour {

    enum Leaning { Right, Center, Left };
    Leaning curLeaning;
    CharacterController mainCont;
    Camera fpsCam;

    public float speed = 5;
    public float leanx = .3f;
    private float step, step2;

    // Use this for initialization
    void Start() {
        fpsCam = Camera.main;
        curLeaning = Leaning.Center;
        step = speed * Time.deltaTime;
        step2 = speed * 1.5f * Time.deltaTime;
        //leany = .25f;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButton("LeanLeft"))
            curLeaning = Leaning.Left;
        else if (Input.GetButton("LeanRight"))
            curLeaning = Leaning.Right;
        else if (Input.GetButton("LeanLeft") && Input.GetButton("LeanRight"))
            curLeaning = Leaning.Center;
        else
            curLeaning = Leaning.Center;

        LeanMove();
    }

    void LeanMove() {

        if (curLeaning == Leaning.Center) {

            if (transform.localPosition.x != 0) {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, transform.localPosition.y), step2);
            }

        }
        else if (curLeaning == Leaning.Left) {

            if (transform.localPosition.x != -leanx)
                if (!Physics.Raycast(transform.localPosition, fpsCam.transform.right * -1, leanx - transform.localPosition.x)) {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(-leanx, transform.localPosition.y), step);
                }
        }
        else if (curLeaning == Leaning.Right) {

            if (transform.localPosition.x != leanx)
                if (!Physics.Raycast(transform.localPosition, fpsCam.transform.right, leanx - transform.localPosition.x)) {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(leanx, transform.localPosition.y), step);
                }
        }
    }
}