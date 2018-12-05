using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ctrl_CreateGroup : MonoBehaviour {


	public Text TitleCurse;
	[SerializeField]
	public GameObject[] array_NumberPerson;
	public Text numberPerson;
	private int tmp = 0;

	void Start () {
		TitleCurse.text = Main_Ctrl.instance.NameCourse;

		for (int i = 0; i <= array_NumberPerson.Length; i++) {
			array_NumberPerson [i].GetComponent<Button> ().interactable = true;
		}
	}

	public void GoToSelect(){
		StartCoroutine (SelectUser ());
	}  

	public void AddPerson(){
		tmp += 1;
		numberPerson.text = tmp.ToString();
		Debug.Log (tmp);
		CalculateNumberPerson ();
	}

	public void SubPerson(){
		tmp -= 1;
		numberPerson.text = tmp.ToString();
		CalculateNumberPerson ();
	}

	void CalculateNumberPerson(){
//		numberPerson.text = array_NumberPerson [tmp].ToString();
		if (tmp == 0) {
			numberPerson.GetComponent<Button> ().interactable = false;
			Debug.Log ("una persona calculada");
		}else if(tmp == 10){
//			array_NumberPerson[tmp].GetComponent<Button> ().interactable = false;
			numberPerson.GetComponent<Button> ().interactable = false;
		}

	}

	IEnumerator SelectUser(){
		SceneManager.LoadScene ("ChoiseUser", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("CreateGroup");
	}

}
