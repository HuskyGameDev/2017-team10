using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject UIMan;
	public GameObject PauseMenu;
	public GameObject Character;

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
			GameObject.Find ("FirstPersonCharacter").GetComponent<LeanScript>().enabled = true;
			GameObject.Find ("Grenade").GetComponent<GrenadeThrow> ().enabled = true;
			//Character.GetComponent<FirstPersonController>().enabled = true;
		} else {
			PauseMenu.SetActive(true);
			Time.timeScale = 0f;
			GameObject.Find ("FirstPersonCharacter").GetComponent<LeanScript>().enabled = false;
			GameObject.Find ("Grenade").GetComponent<GrenadeThrow> ().enabled = false;
			//GameObject.Find ("FPSController").GetComponent<PlayerController>
//			GameObject.Find ("FPSController").GetComponent ().enabled = false;
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
		PauseMenu.SetActive (false);
		Time.timeScale = 0f;
	}
}
