using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Ctrl_M4 : MonoBehaviour {


	public GameObject panelUI;
	public GameObject panelShadow;
	public GameObject panelMiddle;
	public GameObject squadLeft;
	public GameObject squadRigth;
	private InputField title;
	private GameObject[] left;
	private GameObject[] right;
	private GameObject[] squadL;
	private GameObject[] squadR;
	private GameObject[] addCheck;
	public GameObject PanelFull;
	public GameObject panelUi2;
	public Image check;
	public Image addNew;



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
		panelUI.SetActive (false);
	}
	public void close(){
		panelShadow.SetActive (true);
		panelMiddle.SetActive (true);
		PanelFull.SetActive (true);
		panelUi2.SetActive (false);
	}
	public void saveDataNew(){
		check.gameObject.SetActive (true);
		addNew.gameObject.SetActive (false);
		panelUi2.SetActive (false);
		DOTween.Play ("1");
	}

	public void MoveesceneNew(){
		StartCoroutine (NewScene ());
	}
	public void SaveData(){
		
		var st = panelUI.GetComponentsInChildren<InputField> ();
		Debug.Log (st[0].text);

		if (st[0].text.Equals("")) {
			Debug.Log ("claro");
		} else {
			panelShadow.SetActive (true);
			panelMiddle.SetActive (true);
			panelUI.SetActive (false);	
		}
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
		panelUi2.SetActive (true);
	}

	IEnumerator NewScene(){
		SceneManager.LoadScene ("M_5", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("M_4");
	}
}
