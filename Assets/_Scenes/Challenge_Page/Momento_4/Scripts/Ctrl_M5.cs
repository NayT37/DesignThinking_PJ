using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_M5 : MonoBehaviour {


	public GameObject panelUI;
	public GameObject panelShadow;
	public GameObject panelMiddle;
	public GameObject squadLeft;
	public GameObject squadRigth;
	private GameObject[] left;
	private GameObject[] right;
	private GameObject[] squadL;
	private GameObject[] squadR;
	private GameObject[] addCheck;
	public GameObject PanelFull;


	void Start () {
	}
	

	void Update () {

		
	}

	public void MainIdea(){
		panelUI.SetActive (true);
		panelShadow.SetActive (false);

	}
	public void CloseIdea(){
		panelShadow.SetActive (true);
		panelMiddle.SetActive (true);
		panelUI.SetActive (false);
	}

	public void AddIdea(){
		left = GameObject.FindGameObjectsWithTag ("left");
		right = GameObject.FindGameObjectsWithTag ("Rigth");

		foreach (var item in right) {
			if (item.tag.Equals("Rigth")) {
				Debug.Log ("algo");
				squadRigth.SetActive (true);
			}
		}
		foreach (var itemL in left) {
			if (itemL.tag.Equals("left")) {
				Debug.Log ("some");
				squadLeft.SetActive (true);
			}
		}
	}

	public void SquadBtn(){
		squadL = GameObject.FindGameObjectsWithTag ("left");
		squadR = GameObject.FindGameObjectsWithTag ("Rigth");

		foreach (var item in squadL) {
			if (item.tag.Equals("left")) {
				PanelFull.SetActive (true);
			}
		}
		foreach (var item in squadR) {
			if (item.tag.Equals("Rigth")) {
				PanelFull.SetActive (true);
			}
		}
	}

	public void CheckIdea(){
		addCheck = GameObject.FindGameObjectsWithTag ("add");

		for (int i = 0 ; i < addCheck.Length; i++) {
			Debug.Log (addCheck [i] + "hola");
		}

//		foreach (var item in addCheck) {
//			if (item.tag.Equals("add")) {
//				Debug.Log ("aqui " + item);
//			}
//		}
	}
}
