using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlEmpatizar : MonoBehaviour {

	public GameObject[] panels;
	public Button[] btnsUserProfile;

	// Use this for initialization
	void Start () {
		Default ();
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
			foreach (GameObject item in panels) {
				if (!item.activeSelf) {
					item.SetActive (true);
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
				Debug.Log (btn.name);
				if (btn.name.Contains (nameBtn)) {
					btn.interactable = false;
					_isAge = true;
				}
			} else if (btn.name.Contains ("Gen") && nameBtn.Contains ("Gen")) {
				btn.interactable = true;
				Debug.Log (btn.name);
				if (btn.name.Contains (nameBtn)) {
					btn.interactable = false;
					_isGen = true;
				}
			}
		}
		if (_isAge && _isGen)
			ActivateNextPanel ();
	}
}