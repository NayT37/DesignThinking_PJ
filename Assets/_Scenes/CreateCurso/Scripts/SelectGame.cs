using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class SelectGame : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private Image newGameBtn, loadGameBtn, syncBtn;
    private DOTweenAnimation newGameCtrl;

    private CourseServices _courseServices;

    private SyncServices _syncServices;
    
	public GameObject _notData, _syncprefab, _syncSucc, _noConn, _syncNoSucc;

	public Transform _TextTransform, _syncTransform;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {

        DataBaseParametersCtrl.Ctrl.isQueryOk = false;

        _courseServices = new CourseServices();

        var goSync = gameObject.AddComponent<SyncServices>();
        _syncServices = goSync.GetComponent<SyncServices>();

        DOTween.Init();
        // EXAMPLE B: initialize with custom settings, and set capacities immediately
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);

        newGameBtn = GameObject.Find("NewGameBtn").GetComponent<Image>();
        loadGameBtn = GameObject.Find("LoadGameBtn").GetComponent<Image>();
        newGameBtn.raycastTarget = false;
        loadGameBtn.raycastTarget = false;

        newGameCtrl = GameObject.Find("BtnNewGame").GetComponent<DOTweenAnimation>();

        StartCoroutine(WaitAnimationTime());
    }

    public void syncBtnBhvr(){
        Button[] _btns = new Button[2];
        GameObject obj = Instantiate(_syncprefab, _syncTransform);
        _btns = obj.GetComponentsInChildren<Button>();
        _btns[0].onClick.AddListener(delegate {ValidationSyncBtnBhvr(_btns[0].name, obj);});
        _btns[1].onClick.AddListener(delegate {ValidationSyncBtnBhvr(_btns[1].name, obj);});
    }

    public void ValidationSyncBtnBhvr(string res, GameObject obj){

        int r = int.Parse(res);
        bool isConn = DataBaseParametersCtrl.Ctrl.doConnection();
        if (r!=1){
            if (isConn)
            {
                DOTween.Play("bg_outSyncYes");
                DOTween.Play("bg_outSyncYes2");
                StartCoroutine(waitForYesValidation(obj));
            } else {
                DOTween.Play("bg_syncExit");
                Debug.Log("No validation");
                StartCoroutine(waitForExitValidation(obj, true, false));
            }
        }else{
            DOTween.Play("bg_syncExit");
            Debug.Log("No validation");
            StartCoroutine(waitForExitValidation(obj, false, false));
        }
    }
	public void NewGameBtnBhvr() { 

        string name = "CreateCurso";

        StartCoroutine(ChangeScene(name));
	}

    public void LoadGameBtnBhvr() { 

        string name = "LoadGame";
        int count = _courseServices.GetCoursesCount();

        if (count!=0)
        {
            StartCoroutine(ChangeScene(name));
        } else {
            GameObject obj = Instantiate(_notData, _TextTransform);
			StartCoroutine(DeletePrefab(obj));
        }

        }
    
    public void backToScene(){
		StartCoroutine (BackOne ());
	}
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES

    
	IEnumerator BackOne(){
		DOTween.Play("bg_transition");
        //DestroyImmediate(DataBaseParametersCtrl.Ctrl.transform.gameObject);
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene ("RegisterUser");
	}
    private IEnumerator WaitAnimationTime()
    {
        yield return new WaitForSeconds(newGameCtrl.duration * 3);
        newGameBtn.raycastTarget = true;
        loadGameBtn.raycastTarget = true;
    }

    private IEnumerator waitForExitValidation(GameObject go, bool isSync, bool isConn)
    {
        yield return new WaitForSeconds(0.5f);
        
        if (isSync)
        {
            if (isConn)
            {
                _syncServices.sendDataToSync();
                StopAllCoroutines();
                StartCoroutine(waitToSync(go));
                StartCoroutine(waitToSyncNot(go));
            }else {
                GameObject obj = Instantiate(_noConn, _TextTransform);
                StartCoroutine(DeletePrefab(obj));
            }
           
        } else{
            DestroyImmediate(go);
        }
    }

    private IEnumerator waitToSyncNot(GameObject go)
    {
        yield return new WaitUntil(()=> DataBaseParametersCtrl.Ctrl.isSyncNot == true);
        DataBaseParametersCtrl.Ctrl.isSyncNot = false; 
        Debug.Log("No sincronizó");
        StartCoroutine(DeleteMsg(go, _syncNoSucc)); 
    }

    private IEnumerator waitToSync(GameObject go)
    {
        yield return new WaitUntil(()=> DataBaseParametersCtrl.Ctrl.isQueryOk == true);
        DataBaseParametersCtrl.Ctrl.isQueryOk = false; 
        Debug.Log("Sincronización correcta");
        StartCoroutine(DeleteMsg(go, _syncSucc)); 
    }

    private IEnumerator DeleteMsg(GameObject go, GameObject pref)
    {
        DOTween.Play("bg_syncExit");
        yield return new WaitForSeconds(0.5f);	
        DestroyImmediate(go);
        GameObject obj = Instantiate(pref, _TextTransform);
        StartCoroutine(DeletePrefab(obj));
    }

    private IEnumerator waitForYesValidation(GameObject go)
    {
        //aquí debo calcular el tiempo de esperar de la respuesta del servidor 
        //e implementar el método de envio de datos al servicio remoto
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(waitForExitValidation(go, true, true));
    }

    private IEnumerator DeletePrefab(GameObject obj)
    {
        yield return new WaitForSeconds(4.0f);	
        DestroyImmediate(obj);
    }

    private IEnumerator ChangeScene(string name)
    {
        DOTween.Play("bg_transition");
        yield return new WaitForSeconds(1.0f);
        Main_Ctrl.instance.GoToScene(name);
    }

     
    #endregion
}