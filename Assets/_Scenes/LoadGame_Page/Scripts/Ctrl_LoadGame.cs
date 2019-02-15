using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.XR;
using Vuforia;
public class Ctrl_LoadGame : MonoBehaviour
{


    public GameObject prefab_Curse;
    public GameObject parent_group, _validationDelete, _msgDelete, _loadCourses, cloneLoadCourses;

	public Transform _parentValidation, _textParent;
    private Slider _sliderCurses;
    private SliderHandler slider_Handler;

    private float max;
    private float min;

    private CourseServices _courseServices;
    private Text _nameConstant;

    private Course[] _courses;

    private IEnumerable<Course> courses;

    private  int countercourses = 0;

	private GameObject[] _prefabsCourses;


    // Use this for initialization
    void Start()
    {
        XRSettings.enabled = false;
        VuforiaBehaviour.Instance.enabled = false;

        var goCourses = gameObject.AddComponent<CourseServices>();
        _courseServices = goCourses.GetComponent<CourseServices>();

        _prefabsCourses = new GameObject[0];
		cloneLoadCourses = new GameObject();

        callToGetCourses();

        
    }

     private IEnumerator waitToDeleteLoadCourse()
    {
        yield return new WaitForSeconds(1.0f);
		DestroyImmediate(cloneLoadCourses);
    }

    void GetCoursePressed(string positionInToArray, string nameCourse)
    {

        Main_Ctrl.instance.NameCourse = nameCourse;

        int value = int.Parse(positionInToArray);
        Debug.Log("position " + positionInToArray);

        DataBaseParametersCtrl.Ctrl._courseLoaded = _courses[value];


        goToScene();
    }

    private void DeleteCoursePressed(string name)
    {
        int value = int.Parse(name);
        DataBaseParametersCtrl.Ctrl._courseLoaded = _courses[value];

        Button[] _btns = new Button[2];
        GameObject obj = Instantiate(_validationDelete, _parentValidation);
        _btns = obj.GetComponentsInChildren<Button>();
        _btns[0].onClick.AddListener(delegate { ValidationSyncBtnBhvr(_btns[0].name, obj, value); });
        _btns[1].onClick.AddListener(delegate { ValidationSyncBtnBhvr(_btns[1].name, obj, value); });
    }

    void DeletePrefabs(int count)
    {

        for (int i = 0; i < count; i++)
        {
            DestroyImmediate(_prefabsCourses[i]);
        }
    }

	public void ValidationSyncBtnBhvr(string res, GameObject obj, int value)
    {

        int r = int.Parse(res);
        if (r != 1)
        {
            DOTween.Play("bg_outSyncYes");
            // courses = _courseServices.GetCourses();
            StartCoroutine(waitForExitValidation(obj, true, value));
            Debug.Log("Yes validation");

        }
        else
        {
            DOTween.Play("bg_syncExit");
            Debug.Log("No validation");
            StartCoroutine(waitForExitValidation(obj, false, value));
        }
    }

	 private IEnumerator DeletePrefab(GameObject obj)
    {
        yield return new WaitForSeconds(4.0f);
        DestroyImmediate(obj);
    }

    private IEnumerator waitForExitValidation(GameObject go, bool isDelete, int value)
    {
        yield return new WaitForSeconds(0.5f);
        DestroyImmediate(go);
        if (isDelete)
        {
			
			int lengthCourses = _prefabsCourses.Length;
			DeletePrefabs(lengthCourses);
            GameObject obj = Instantiate(_msgDelete, _textParent);
            StartCoroutine(DeletePrefab(obj));
            _courseServices.DeleteCourse();
            StartCoroutine(waitToDeleteCourse());
        }
    }

    private IEnumerator waitToDeleteCourse()
    {
        yield return new WaitUntil(()=> DataBaseParametersCtrl.Ctrl.isQueryOk == true);
        DataBaseParametersCtrl.Ctrl.isQueryOk = false;  
        Debug.Log("Curso eliminado");
		callToGetCourses();
    }

	private void callToGetCourses(){
		
		cloneLoadCourses = Instantiate(_loadCourses, _textParent);
		CreatePrefabs();
		
	}

    void Update()
    {


    }

    public void goToScene()
    {
        StartCoroutine(GoScene());
    }

    public void backToScene()
    {
        StartCoroutine(BackOne());
    }

    IEnumerator GoScene()
    {
        DOTween.Play("bg_transition");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Edit_Curse");
    }

    IEnumerator BackOne()
    {
        DOTween.Play("bg_transition");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("SelectGame");
    }


    void CreatePrefabs()
    {
        courses = _courseServices.GetCourses();
        int countercourses = _courseServices.GetCoursesCount();

        _prefabsCourses = new GameObject[countercourses];
		_courses = new Course[countercourses];
        
        int counter = 0;

        DOTween.Play("bgLoadCourses");
		StartCoroutine(waitToDeleteLoadCourse());

        foreach (var item in courses)
        {
            Debug.Log(item.name);
            var SetName = Instantiate(prefab_Curse, parent_group.transform);
            SetName.name = counter.ToString();
            _courses[counter] = item; 
			_prefabsCourses[counter] = SetName;
            counter++;
            SetName.GetComponentInChildren<Text>().text = item.name;
            SetName.GetComponentInChildren<Button>().onClick.AddListener(delegate { GetCoursePressed(SetName.name, item.name); });
            SetName.transform.GetChild(1).GetComponentInChildren<Button>().onClick.AddListener(delegate { DeleteCoursePressed(SetName.name); });
            Debug.Log("name" + item.name);
        }
    }
}
