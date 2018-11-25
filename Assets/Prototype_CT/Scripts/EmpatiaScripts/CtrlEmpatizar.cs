using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CtrlEmpatizar : MonoBehaviour {

	public GameObject[] panels;
	public Button[] btnsUserProfile;
	public Button showFase1;
	public Button hideFase1;
	public Image fase1;
	public Button showFase2;
	public Button hideFase2;
	public Image fase2;

	// Use this for initialization
	void Start () {
		Default ();
	}

	// Update is called once per frame
	void Update () {
		showFase1.onClick.AddListener (DisplayFase1);
		hideFase1.onClick.AddListener (DisplayFase1);
		showFase2.onClick.AddListener (DisplayFase2);
		hideFase2.onClick.AddListener (DisplayFase2);
	}

	private void DisplayFase2 () {
		fase2.gameObject.SetActive (!fase2.gameObject.activeSelf);
	}

	private void DisplayFase1 () {
		fase1.gameObject.SetActive (!fase1.gameObject.activeSelf);
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
					if (i == panels.Length - 1) {
						showFase1.gameObject.SetActive (false);
						showFase2.gameObject.SetActive (true);
					}
					break;
				}
			}
		}
	}

	private bool _isAge, _isGen = false;
	public void SetProfile (string nameBtn) {
		foreach (Button btn in btnsUserProfile) {
			if (btn.name.Contains ("Age") && nameBtn.Contains ("Age")) {
				btn.interactable = true;
				if (btn.name.Contains (nameBtn)) {
					btn.interactable = false;
					_isAge = true;
				}
			} else if (btn.name.Contains ("Gen") && nameBtn.Contains ("Gen")) {
				btn.interactable = true;
				if (btn.name.Contains (nameBtn)) {
					btn.interactable = false;
					_isGen = true;
				}
			}
		}
		if (_isAge && _isGen)
			ActivateNextPanel ();
	}

	public void NextScene () {
		SceneManager.LoadScene ("Definir");
	}
}