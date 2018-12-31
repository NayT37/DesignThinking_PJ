using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Ctrl_M4 : MonoBehaviour {


	public GameObject PrefabObject;
	public GameObject parentLeft;
	public GameObject parentRigth;
	public GameObject parentPanelUI;
	public GameObject _panelUI;
	public GameObject panelMain;
	private GameObject MainIdeaText;

	private IdeaDescription[] _TextIdeaDescription;


	void Start () {

		_TextIdeaDescription = new IdeaDescription[18];

		MainIdeaText = GameObject.Find ("MainIdea");
		_panelUI.SetActive (false);



//		Instantiate (PrefbaObject, parentLeft.transform);
//		Instantiate (PrefbaObject, parentLeft.transform);
//		Instantiate (PrefbaObject, parentLeft.transform);

//		Instantiate (PrefbaObject, parentRigth.transform);
//		Instantiate (PrefbaObject, parentRigth.transform);
//		Instantiate (PrefbaObject, parentRigth.transform);
	}
	void Update () {
		
	}
	public void IdeaMethod(){
		DOTween.Pause ("5");
		_panelUI.SetActive (true);
		panelMain.SetActive (false);
	}

//	public void getTextDescription(){
//		temp = GameObject.Find ("TextIdea").GetComponent<Text> ();
//		TextEmpty = GameObject.Find ("TextEmpty").GetComponent<Text> ();
//		TextEmpty.text = temp.text;
//		Debug.Log (temp.text + " valor digitado");
//		Debug.Log (TextEmpty.text + " valor actual");
//
//
//
//		for (int i = 0; i < _TextIdeaDescription.Length; i++) {
//			_TextIdeaDescription [0].text = TextEmpty.text;
//			Debug.Log (_TextIdeaDescription [i].text + " Este es el aaray");	
//		}
//	}
}
