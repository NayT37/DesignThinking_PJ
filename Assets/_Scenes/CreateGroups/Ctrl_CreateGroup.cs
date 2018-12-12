using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Ctrl_CreateGroup : MonoBehaviour {


	public Text TitleCurse;
	[SerializeField]
	public GameObject[] array_NumberPerson;
	public Text numberPerson;
	private int tmp = 0;
	private InputField groupName;
	private GameObject saveCheck;
	private GameObject inputUserGroup;
	private string saveData = "data.json";

	void Start () {
		TitleCurse.text = Main_Ctrl.instance.NameCourse;
		groupName = GameObject.Find ("IFNameGroup").GetComponent<InputField> ();


		saveCheck = GameObject.Find ("CuadroShowSave");
		saveCheck.SetActive (false);
		inputUserGroup = GameObject.Find ("CuadroMenor");

		for (int i = 0; i <= array_NumberPerson.Length; i++) {
			array_NumberPerson [i].GetComponent<Button> ().interactable = true;
		}
	}



	#region Finalizar
	// Metodo finalizar, captura el nombre del los grupos y el No de personas en dichos grupos,
	// toma todos los grupos que previamente hayan sido guardados. 




	#endregion


	#region añadir grupos
	// Metodo guardar, toma el nombre proporcionado para el grupo y el número de estudiantes en el,
	// limpia la interfaz para que el usuario digite el proximo grupo hasta que seleccione finalizar.





	public void GoToSelect(){

//		if (groupName.text.Equals ("") && numberPerson.text.Equals("0")) {
//			
//		} else {
//			//Activar gameobject que contiene el check
//			saveCheck.SetActive (true);
//			inputUserGroup.SetActive (false);
//		}

		saveCheck.SetActive (true);
		inputUserGroup.SetActive (false);

//		StartCoroutine (waitSecondsForchangeSquad ());

		new {items = new [] {
			new {name = "command" , index = "X"}, 
		}};


		Debug.Log ("Este es el nombre del grupo " + groupName.text);

//		StartCoroutine (SelectUser ());
	} 

	#endregion
 

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

//	IEnumerator waitSecondsForchangeSquad(){
//		yield return new WaitForSeconds (saveCheck.SetActive(true));
//	}

}
