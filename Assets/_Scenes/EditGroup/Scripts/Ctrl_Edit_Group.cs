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

	private Text TextGroup;


	private GroupServices _groupServices;

	void Start () {
		addBtn = GameObject.Find ("MasIcon").GetComponent<Button> ();
		subBtn = GameObject.Find ("MenosIcon").GetComponent<Button> ();

		_nameGroupUpdate = GameObject.Find ("NameGroup").GetComponent<InputField> ();
		_groupServices = new GroupServices ();

		TextGroup = GameObject.Find ("TitleGroup").GetComponent<Text> ();
		TextGroup.text = Main_Ctrl.instance.NameCourse;

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

		var result = _groupServices.UpdateGroup (_nameGroupUpdate.text, Convert.ToInt32((numberPerson.text.ToString())));
		if (result != 0) {
			SceneManager.LoadScene ("Edit_Curse");
		}
	}

	public void backScene(){
		SceneManager.LoadScene ("Edit_Curse");
	}

	public void deleteGroup(){

		var result = _groupServices.DeleteGroup ();
		if (result != 0) {
			SceneManager.LoadScene ("Edit_Curse");
		}
		
	}



}
