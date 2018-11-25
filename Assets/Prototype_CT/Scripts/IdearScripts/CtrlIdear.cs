using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CtrlIdear : MonoBehaviour {

	public Button btnAddCard;
	public Button btnMenu;
	public Button btnVersion;
	public Button btnSave;
	public Button btnNext;
	public Button btnShowFases;
	public Button btnHideFases;
	public Image card;
	public Image menu;
	public Image fases;
	public GameObject panelVersion;
	[Header ("Versionamiento")]
	public Button btnClose;
	public Button btnSaveVer;

	// Use this for initialization
	void Start () {
		btnAddCard.onClick.AddListener (ShowCard);
		btnMenu.onClick.AddListener (DisplayMenu);
		btnVersion.onClick.AddListener (DisplayPanelVersion);
		btnSave.onClick.AddListener (NextScene);
		btnNext.onClick.AddListener (NextScene);
		btnShowFases.onClick.AddListener (DisplayFases);
		btnHideFases.onClick.AddListener (DisplayFases);
		btnClose.onClick.AddListener (HidePanelVersion);
		btnSaveVer.onClick.AddListener (DisabledBtn);
		panelVersion.SetActive (false);
	}

	private void DisabledBtn () {
		btnSaveVer.interactable = false;
	}

	private void HidePanelVersion () {
		panelVersion.SetActive (false);
		menu.gameObject.SetActive(false);
	}

	private void DisplayFases () {
		fases.gameObject.SetActive (!fases.gameObject.activeSelf);
	}

	private void DisplayPanelVersion () {
		panelVersion.SetActive (true);
	}

	private void NextScene () {
		// SceneManager.LoadScene("");

	}

	private void DisplayMenu () {
		menu.gameObject.SetActive (!menu.gameObject.activeSelf);
	}

	private void ShowCard () {
		card.gameObject.SetActive (true);
	}

	// Update is called once per frame
	void Update () {

	}
}