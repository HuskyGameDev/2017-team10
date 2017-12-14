﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

	public GameObject UIMan;
	public GameObject PauseMenu;
	public GameObject DeathMenu;
    
    private Transform mainCam;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TogglePauseMenu(){
		if (PauseMenu.activeSelf) {
            mainCam.GetComponentInParent<FirstPersonController>().SetPause();
            mainCam.GetComponent<LeanScript>().paused = false;
            PauseMenu.SetActive(false);
			Time.timeScale = 1.0f;
        } else {
			PauseMenu.SetActive(true);
            mainCam.GetComponentInParent<FirstPersonController>().SetPause();
            mainCam.GetComponent<LeanScript>().paused = true;
            Time.timeScale = 0f;
		}
		Debug.Log ("GAMEMANAGER:: TimeScale: " + Time.timeScale);
	}

	public void NewGameBtn(string newGameLevel){
		SceneManager.LoadScene (newGameLevel);
	}

	public void ExitGameBtn(){
//		UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit ();
	}

	public void ResumeBtn(){
		TogglePauseMenu ();
	}

	public void GameOver(){
		DeathMenu.SetActive(true);
	}
}
