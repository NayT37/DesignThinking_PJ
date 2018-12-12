using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DG.Tweening;

public class Ctrl_CreateGroup : MonoBehaviour {


	public Text TitleCurse;
	[SerializeField]
	public GameObject[] array_NumberPerson;
	public Text numberPerson;
	private int tmp = 0;
	private InputField groupName;
	private GameObject saveCheck;
	private GameObject inputUserGroup;

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
	public void GotoScene(){
		StartCoroutine (GoScene());
	} 

	#endregion


	#region añadir grupos
	// Metodo guardar, toma el nombre proporcionado para el grupo y el número de estudiantes en el,
	// limpia la interfaz para que el usuario digite el proximo grupo hasta que seleccione finalizar.
	public void SaveDataGroup(){

		if (groupName.text.Equals ("") && numberPerson.text.Equals("0")) {
			DOTween.Play ("6");
		} else {
			StartCoroutine (waitSecondsForchangeSquad ());
		}
		Debug.Log ("Este es el nombre del grupo " + groupName.text);
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

	public void backToScene(){
		StartCoroutine (backScene());
	} 

	/* Corrutina cambio de escena */
	IEnumerator GoScene(){
		SceneManager.LoadScene ("ChoiseUser", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("CreateGroup");
	}

	IEnumerator backScene(){
		SceneManager.LoadScene ("CreateCurso", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("CreateGroup");
	}

	/* Corrutina para el cambio de gameobject sobre el check */
	IEnumerator waitSecondsForchangeSquad(){
		//Activar gameobject que contiene el check
		saveCheck.SetActive (true);
		inputUserGroup.SetActive (false);
		yield return new WaitForSeconds (1.2f);
		inputUserGroup.SetActive (true);
		saveCheck.SetActive (false);

		//Setiar los valores en predeterminado para un nuevo grupo
		groupName.text = "";
		numberPerson.text = "0";
	}

}
