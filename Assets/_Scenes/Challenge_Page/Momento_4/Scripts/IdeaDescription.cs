using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdeaDescription : MonoBehaviour {

	private Text temp;
	private Text TextEmpty;
	private Ctrl_M4 ctrlM4;
	public Text[] _arrayTextDescription;

	void Start () {
		_arrayTextDescription = new Text[10];
		TextEmpty = GameObject.Find ("TextEmpty").GetComponent<Text> ();
	}
		

	public void getTextDescription(){
		temp = GameObject.Find("TextIdea").GetComponent<Text>();
		TextEmpty.text = temp.text;
		Debug.Log (temp.text + " valor digitado");
		Debug.Log (TextEmpty.text + " valor actual");


//
//		for (int i = 0; i < _arrayTextDescription.Length; i++) {
//			Debug.Log ("texto array" + _arrayTextDescription [i]);
//			_arrayTextDescription[0].text = TextEmpty.text;
//			Debug.Log (_arrayTextDescription[i].text + " Este es el aaray");	
//		}
			

//		int counter = 0;
//		foreach (var item in _arrayTextDescription) {
//			_arrayTextDescription [counter] = item.GetComponent<Text> ().text;
//			counter++;
//			Debug.Log ("esto es lo que se agrega al array " + _arrayTextDescription [item.text]);
//		}

		ctrlM4.panelMain.SetActive (true);
		ctrlM4._panelUI.SetActive (false);
//		Destroy (ctrlM4._panelUI.gameObject);
	}
}
