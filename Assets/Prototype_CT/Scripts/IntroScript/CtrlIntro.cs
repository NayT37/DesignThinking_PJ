using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CtrlIntro : MonoBehaviour {

	public GameObject[] panels;
	// Use this for initialization
	void Start () {
		Default();
	}
	
	// Update is called once per frame
	void Update () {
	}
	

	public void Default () {
		if (panels.Length > 0) {
			foreach (GameObject item in panels) {
				item.SetActive (false);
			}
			panels[0].SetActive (true);
		}
	}
	public void ActivateNextPanel () {
		if (panels.Length > 0) {
			for (int i = 0; i < panels.Length; i++) {
				if (!panels[i].activeSelf) {
					panels[i].SetActive (true);
					break;
				}
			}
		}
	}

	public void NextScene () {
		SceneManager.LoadScene ("Empatizar");
	}
}
