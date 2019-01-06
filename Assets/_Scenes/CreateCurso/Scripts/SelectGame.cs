using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class SelectGame : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private Image newGameBtn, loadGameBtn, syncBtn;
    private DOTweenAnimation newGameCtrl;

    private CourseServices _courseServices;
    
	public GameObject _notData, _syncprefab, _syncSucc;

	public Transform _TextTransform, _syncTransform;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {

        _courseServices = new CourseServices();

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
        Debug.Log("Sync DataBase...");
        GameObject obj = Instantiate(_syncprefab, _syncTransform);
        _btns = obj.GetComponentsInChildren<Button>();
        _btns[0].onClick.AddListener(delegate {ValidationSyncBtnBhvr(_btns[0].name, obj);});
        _btns[1].onClick.AddListener(delegate {ValidationSyncBtnBhvr(_btns[1].name, obj);});
    }

    public void ValidationSyncBtnBhvr(string res, GameObject obj){

        int r = int.Parse(res);
        if (r!=1){
            DOTween.Play("bg_outSyncYes");
            StartCoroutine(waitForYesValidation(obj));
            Debug.Log("Yes validation");
        }else{
            DOTween.Play("bg_syncExit");
            Debug.Log("No validation");
            StartCoroutine(waitForExitValidation(obj, false));
        }
    }
	public void NewGameBtnBhvr() { 

        string name = "CreateCurso";

        StartCoroutine(ChangeScene(name));
		Debug.Log ("hola");}

    public void LoadGameBtnBhvr() { 

        string name = "LoadGame";
        int count = _courseServices.GetCoursesCount();

        if (count!=0)
        {
            StartCoroutine(ChangeScene(name));
        } else {
            GameObject obj = Instantiate(_notData, _TextTransform);
			StartCoroutine(DeletePrefab(obj));
			Debug.Log("No hay resultados para cargar");
        }

        }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    private IEnumerator WaitAnimationTime()
    {
        yield return new WaitForSeconds(newGameCtrl.duration * 3);
        newGameBtn.raycastTarget = true;
        loadGameBtn.raycastTarget = true;
    }

    private IEnumerator waitForExitValidation(GameObject go, bool isSync)
    {
        yield return new WaitForSeconds(0.5f);
        DestroyImmediate(go);
        if (isSync)
        {
           GameObject obj = Instantiate(_syncSucc, _TextTransform);
		   StartCoroutine(DeletePrefab(obj));
        }
    }

    private IEnumerator waitForYesValidation(GameObject go)
    {
        //aquí debo calcular el tiempo de esperar de la respuesta del servidor 
        //e implementar el método de envio de datos al servicio remoto
        yield return new WaitForSeconds(2.0f);
        DOTween.Play("bg_syncExit");
        Debug.Log("Sync Finished");
        StartCoroutine(waitForExitValidation(go, true));
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