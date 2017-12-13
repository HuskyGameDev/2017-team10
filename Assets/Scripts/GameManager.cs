using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

	public GameObject UIMan;
	public GameObject PauseMenu;
	public GameObject Character;
    
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
//            mainCam.GetChild(0).GetChild(0).GetComponent<GrenadeThrow>().SetPause();
            PauseMenu.SetActive(false);
			Time.timeScale = 1.0f;
            //GameObject.Find ("GrenadeInv").GetComponent<GrenadeThrow> ().enabled = true;
            //Character.GetComponent<FirstPersonController>().enabled = true;
        } else {
			PauseMenu.SetActive(true);
            mainCam.GetComponentInParent<FirstPersonController>().SetPause();
            mainCam.GetComponent<LeanScript>().paused = true;
//            mainCam.GetChild(0).GetChild(0).GetComponent<GrenadeThrow>().SetPause();
            Time.timeScale = 0f;
            
            //GameObject.Find ("GrenadeInv").GetComponent<GrenadeThrow> ().enabled = false;
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

	public void GameOver(){
		PauseMenu.SetActive(true);
	}
}
