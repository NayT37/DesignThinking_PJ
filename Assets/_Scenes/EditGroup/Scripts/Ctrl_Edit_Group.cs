using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Ctrl_Edit_Group : MonoBehaviour {


	private Button addBtn;
	private Button subBtn;
	public Text numberPerson;

	public GameObject _validationDelete, _msgDelete;

	public Transform _parentValidation, _textParent;
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

		Button[] _btns = new Button[2];
        GameObject obj = Instantiate(_validationDelete, _parentValidation);
        _btns = obj.GetComponentsInChildren<Button>();
        _btns[0].onClick.AddListener(delegate {ValidationSyncBtnBhvr(_btns[0].name, obj);});
        _btns[1].onClick.AddListener(delegate {ValidationSyncBtnBhvr(_btns[1].name, obj);});
		
	}

	public void ValidationSyncBtnBhvr(string res, GameObject obj){

        int r = int.Parse(res);
        if (r!=1){
            DOTween.Play("bg_outSyncYes");
            StartCoroutine(waitForExitValidation(obj, true));
            Debug.Log("Yes validation");   
            
        }else{
            DOTween.Play("bg_syncExit");
            Debug.Log("No validation");
            StartCoroutine(waitForExitValidation(obj, false));
        }
    }

    private IEnumerator DeletePrefab(GameObject obj)
    {
        yield return new WaitForSeconds(4.0f);	
        DestroyImmediate(obj);
    }

	private IEnumerator waitForExitValidation(GameObject go, bool isDelete)
    {
        yield return new WaitForSeconds(0.5f);
        DestroyImmediate(go);
        if (isDelete)
        {
            GameObject obj = Instantiate(_msgDelete, _textParent);
            StartCoroutine(DeletePrefab(obj));
			var result = _groupServices.DeleteGroup ();
			if (result != 0) {
				SceneManager.LoadScene ("Edit_Curse");
			}
        }
    }



}
