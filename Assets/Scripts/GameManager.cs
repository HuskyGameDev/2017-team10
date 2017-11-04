using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject UIMan;
	public GameObject PauseMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TogglePauseMenu(){
		if (PauseMenu.activeSelf) {
			PauseMenu.SetActive(false);
			Time.timeScale = 1.0f;
		} else {
			PauseMenu.SetActive(true);
			Time.timeScale = 0f;
		}

		Debug.Log ("GAMEMANAGER:: TimeScale: " + Time.timeScale);
	}

	public void NewGameBtn(string newGameLevel){
		SceneManager.LoadScene (newGameLevel);
	}

	public void ExitGameBtn(){
		UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit ();
	}

	public void ResumeBtn(){
		PauseMenu.SetActive (false);
		Time.timeScale = 0f;
	}
}
