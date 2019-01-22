using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class Ctrl_CreateGroup : MonoBehaviour {


	public Text TitleCurse;
	[SerializeField]
	public Text numberPerson;
	private int tmp = 0;
	private InputField groupName;
	private GameObject saveCheck;
	private GameObject inputUserGroup;
	private GameObject _feedbackGroup;
	private Button addBtn;
	private Button subBtn;
	private int contador = 0;

	private GroupServices _groupServices;




	void Start () {
		TitleCurse.text = "CURSO: "+ Main_Ctrl.instance.NameCourse.ToUpper();
		groupName = GameObject.Find ("IFNameGroup").GetComponent<InputField> ();

		saveCheck = GameObject.Find ("CuadroShowSave");
		saveCheck.SetActive (false);
		inputUserGroup = GameObject.Find ("CuadroMenor");

		_feedbackGroup = GameObject.Find ("CuadroFeedback");
		_feedbackGroup.SetActive (false);

		addBtn = GameObject.Find ("AddBtn").GetComponent<Button> ();
		subBtn = GameObject.Find ("RestBtn").GetComponent<Button> ();

		_groupServices = new GroupServices ();


	}



	#region Finalizar
	/* Metodo finalizar, captura el nombre del los grupos y el No de personas en dichos grupos,
	 * toma todos los grupos que previamente hayan sido guardados. 
	 */

	public void GotoScene(){
		
		if (contador >= 1) {
			StartCoroutine (GoScene());	
		}
	} 
	#endregion


	#region añadir grupos
	/* Metodo guardar, toma el nombre proporcionado para el grupo y el número de estudiantes en el,
	 *  limpia la interfaz para que el usuario digite el proximo grupo hasta que seleccione finalizar.
	 */ 
	public void SaveDataGroup(){
		//Cuando no haya escrito el nombre del grupo o el número de personas iniciara la animación para 
		//mostrar que debe llenar los campos
		if (groupName.text.Equals ("") || numberPerson.text.Equals("0")) {
			//Inicia corrutina para mostrar un feedback de lo que debe llenar
			StartCoroutine(feedback());
		} else {
			//Enviar a base de datos el nuevo grupo creado con dos parametros, el nombre y el número de estudiantes por grupo
			var group = _groupServices.CreateGroup (groupName.text.ToUpper(),Convert.ToInt32(numberPerson.text.ToString()));
			//StartCoroutine(getIsQueryCreateGroup());
			contador += 1;
			Debug.Log (contador + " contador");
			//Se devuelve el Id del grupo desde la base de datos, cuando sea 1 se guardara y cuando sea 0 es porque ese grupo
			//ya existe
			Debug.Log (group);
			if (group == 1) {
				//Iniciar corrutina, donde muestra el feedback del grupo creado
				StartCoroutine (waitSecondsForchangeSquad ());
			} else {
				StartCoroutine (UserExist ());
			}
		}
	}
	#endregion


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
		

	public void backToScene(){
		StartCoroutine (backScene());
	} 

	/* Corrutina cambio de escena */
	IEnumerator GoScene(){
		DOTween.Play("bg_transition");
		yield return new WaitForSeconds(1.0f);	
		SceneManager.LoadScene ("Edit_Curse");
	}

	IEnumerator backScene(){
		DOTween.Play("bg_transition");
		yield return new WaitForSeconds(1.0f);	
		SceneManager.LoadScene ("CreateCurso");
	}

	/* Corrutina para el cambio de gameobject sobre el check */
	IEnumerator waitSecondsForchangeSquad(){
		//Activar gameobject que contiene el check
		saveCheck.SetActive (true);
		inputUserGroup.SetActive (false);
		yield return new WaitForSeconds (1f);
		inputUserGroup.SetActive (true);
		saveCheck.SetActive (false);
		//Setiar los valores en predeterminado para un nuevo grupo
		groupName.text = "";
		numberPerson.text = "0";
		tmp = 0;
		DOTween.Play ("8");
	}
	//Corrutina donde se ejecuta la animación se espera un tiempo determinado y se pausa
	IEnumerator feedback(){
		DOTween.Play ("6");
		yield return new WaitForSeconds (3f);
		DOTween.Pause ("6");
	}

	IEnumerator UserExist(){
		_feedbackGroup.SetActive (true);
		inputUserGroup.SetActive (false);
		yield return new WaitForSeconds (1.2f);
		inputUserGroup.SetActive (true);
		_feedbackGroup.SetActive (false);
		groupName.text = "";
		numberPerson.text = "0";
		tmp = 0;
	}

}
