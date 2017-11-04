using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public GameObject GM;
	public GameObject MM;
	public GameObject PauseMenu;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		ScanForKeyStroke ();
	}

	void ScanForKeyStroke(){
		if (Input.GetKeyDown ("escape")) {
			GM.GetComponent<GameManager>().TogglePauseMenu ();
		}
	}

	public void OptionSliderUpdate(float val){

	}

	public void MusicSliderUpdate(float val){
//		MM.GetComponent<MusicManager>().SetVolume (val);
	}

	public void MusicToggle(bool val){

	}
}
