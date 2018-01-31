using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRedButton : MonoBehaviour {

	public void InteractAct() {
		Debug.Log ("HELP");
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GameWin();
	}
}
