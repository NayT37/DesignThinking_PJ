using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Ctrl_Edit_Group : MonoBehaviour {


	private Button addBtn;
	private Button subBtn;
	public Text numberPerson;
	private int tmp = 0;
	private InputField _nameGroupUpdate;


	private GroupServices _groupServices;

	void Start () {
		addBtn = GameObject.Find ("MasIcon").GetComponent<Button> ();
		subBtn = GameObject.Find ("MenosIcon").GetComponent<Button> ();

		_nameGroupUpdate = GameObject.Find ("IFNameGroup").GetComponent<InputField> ();
		_groupServices = new GroupServices ();

		var groups = _groupServices.GetGroups ();
		Debug.Log ("grupos " + groups);
		var nameGroups = _groupServices.GetGroupNamed ("Test");
		Debug.Log ("hay uno" + nameGroups);


	}

	void Update () {
		
	}

	public void AddPerson(){
		tmp += 1;
		subBtn.GetComponent<Button> ().interactable = true;
		if (tmp == 10) {
			addBtn.GetComponent<Button> ().interactable = false;
		}
		numberPerson.text = tmp.ToString();
	}

	public void SubPerson(){
		tmp -= 1;
		addBtn.GetComponent<Button> ().interactable = true;
		if (tmp == 0) {
			subBtn.GetComponent<Button> ().interactable = false;
		} 
		numberPerson.text = tmp.ToString();
	}

	public void saveDataUpdate(){

		StartCoroutine (GotoScene());
	}

	public void backScene(){
		StartCoroutine (GobackScene ());
	}

	IEnumerator GotoScene(){
		var groupUpdate = _groupServices.GetGroupId (1);
		var result = _groupServices.UpdateGroup (groupUpdate ,_nameGroupUpdate.text, Convert.ToInt32((numberPerson.ToString())));
		groupUpdate = _groupServices.GetGroupId (1);
		if (result != 0) {
			DataBaseParametersCtrl.Ctrl._groupLoaded = groupUpdate;
			SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
			yield return null;
			SceneManager.UnloadSceneAsync ("Edit_Group");
		}
	}

	IEnumerator GobackScene(){
		SceneManager.LoadScene ("Edit_Curse", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Edit_Group");
	}

}
