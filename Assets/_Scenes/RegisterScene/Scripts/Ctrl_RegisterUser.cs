using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class Ctrl_RegisterUser : MonoBehaviour {

	#region  variables
	public InputField userName;
	public InputField passName;	

	public Transform _parentText;

	public GameObject _prefabText;
	public Toggle _checkFirstTime;
	private DOTweenAnimation animationGame;
	
	private TeacherServices _teacherServices;
	#endregion

	public void Start(){	

		var teacher = DataBaseParametersCtrl.Ctrl._teacherLoggedIn;

		if (teacher != null)
		{
			userName.text = teacher.email;
		}
	
	}
	public void GoUser(){

		_teacherServices = new TeacherServices();

		string name = userName.text;
		string password = DataBaseParametersCtrl.Ctrl.GenerateSHA512String(passName.text);

		//Debug.Log(passName.text+ " .... "+ password);
		bool isFirstTime = _checkFirstTime.isOn;
		if(!name.Equals("") && !passName.Equals("")){

			bool isConn = DataBaseParametersCtrl.Ctrl.doConnection();
			bool doOtherMethod = false;
			var teacher = new Teacher();

			if (isFirstTime)
			{
				if (isConn)
				{
					teacher = _teacherServices.GetTeacherNamed(name, password, isFirstTime);
					doOtherMethod = true;	
				} else{
					GameObject obj = Instantiate(_prefabText, _parentText);
					StartCoroutine(DeletePrefab(obj));
					Debug.Log("No tiene conexión a internet...");
				}
				
			} else{
					teacher = _teacherServices.GetTeacherNamed(name, password, isFirstTime);	
					doOtherMethod = true;
			}
				
			if (doOtherMethod)
			{
					

				if (teacher.identityCard.Equals("null"))
				{
					DOTween.Play ("7");
				} else {
					
					DOTween.Play("bg_transition");
					userName.text = "";
					passName.text = "";		
					StartCoroutine (ResgisterUser ());
				}
			} 
		}else{
//			userName.GetComponent<InputField> ();
//			userName.placeholder.transform.localScale = new Vector3 (1.5f,1.5f,1);
			DOTween.Play ("7");
		}
	}

	IEnumerator ResgisterUser(){
		
		yield return new WaitForSeconds(1.0f);	
		SceneManager.LoadScene ("SelectGame");
	}

	private IEnumerator DeletePrefab(GameObject obj)
    {
        yield return new WaitForSeconds(3.0f);	
        DestroyImmediate(obj);
    }
}
