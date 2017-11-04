using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject pauseMenu;

	public void NewGameBtn(string newGameLevel){
		SceneManager.LoadScene (newGameLevel);
	}

	public void ExitGameBtn(){
		UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit ();
	}

	public void ResumeBtn(){
		pauseMenu.SetActive (false);
		Time.timeScale = 0f;
	}
}
