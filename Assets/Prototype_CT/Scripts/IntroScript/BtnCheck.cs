using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCheck : MonoBehaviour {

	public Image img;
	private Button button;
	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		button.GetComponent<Button> ().onClick.AddListener (CheckImage);
		img.gameObject.SetActive (false);
	}

	private void CheckImage () {
		img.gameObject.SetActive (!img.gameObject.activeSelf);
	}

	// Update is called once per frame
	void Update () {

	}
}