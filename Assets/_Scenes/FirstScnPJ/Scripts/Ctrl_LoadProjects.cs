using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Ctrl_LoadProjects : MonoBehaviour
{


    public GameObject prefab_project;
    public GameObject parent_project;

    public GameObject _validationDelete, _msgDelete;

    public Transform _parentValidation, _textParent;

    private ProjectServices _projectServices;
    private Text _nameConstant;

    private Project[] _projects;

    private GameObject[] _prefabsProjects;

    private int groupid;


    // Use this for initialization
    void Start()
    {


        // for (int i = 0; i < 8; i++)
        // {
        // 	Instantiate (prefab_project, parent_project.transform);
        // }

        //_projectServices = new ProjectServices();
        groupid = DataBaseParametersCtrl.Ctrl._groupLoaded.id;

        _prefabsProjects = new GameObject[0];

        LoadAllPrefabs();
    }


    void Update()
    {


    }

    void LoadAllPrefabs()
    {

        int lengthProjects = _prefabsProjects.Length;

        if (lengthProjects != 0)
        {
            DeletePrefabs(lengthProjects);
        }

        int counterProjects = _projectServices.GetProjectsCounter(groupid);

        _projects = new Project[counterProjects];

        _prefabsProjects = new GameObject[counterProjects];

        var projects = _projectServices.GetProjects();

        int counter = 0;
        foreach (var item in projects)
        {
            var SetName = Instantiate(prefab_project, parent_project.transform);
            SetName.name = counter.ToString();
            _projects[counter] = item;
            counter++;
            SetName.GetComponentInChildren<Text>().text = item.name;
            SetName.transform.GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(delegate { GetProjectPressed(SetName.name, item.name); });
            SetName.transform.GetChild(1).GetComponentInChildren<Button>().onClick.AddListener(delegate { DeleteProjectPressed(SetName.name); });

            Debug.Log("name" + item.name);
        }
    }

    void DeletePrefabs(int count)
    {

        for (int i = 0; i < count; i++)
        {
            DestroyImmediate(_prefabsProjects[i]);
        }
    }

    void GetProjectPressed(string positionInToArray, string nameCourse)
    {

        int value = int.Parse(positionInToArray);
        Debug.Log("position " + positionInToArray);

        DataBaseParametersCtrl.Ctrl._projectLoaded = _projects[value];

        DOTween.Play("bg_transition");
        StartCoroutine(goToScene());
    }

    public void DeleteProjectPressed(string positionInToArray)
    {

        int value = int.Parse(positionInToArray);
        //DataBaseParametersCtrl.Ctrl._projectLoaded = _projects[value];

        Button[] _btns = new Button[2];
        GameObject obj = Instantiate(_validationDelete, _parentValidation);
        _btns = obj.GetComponentsInChildren<Button>();
        _btns[0].onClick.AddListener(delegate { ValidationSyncBtnBhvr(_btns[0].name, obj, value); });
        _btns[1].onClick.AddListener(delegate { ValidationSyncBtnBhvr(_btns[1].name, obj, value); });

    }

    public void ValidationSyncBtnBhvr(string res, GameObject obj, int value)
    {

        int r = int.Parse(res);
        if (r != 1)
        {
            DOTween.Play("bg_outSyncYes");
            LoadAllPrefabs();
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
            GameObject obj = Instantiate(_msgDelete, _textParent);
            StartCoroutine(DeletePrefab(obj));
            var result = _projectServices.DeleteProject(_projects[value]);
        }
    }

    public void backToScene()
    {
        StartCoroutine(BackOne());
    }

    IEnumerator BackOne()
    {
        DOTween.Play("bg_transition");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("CreateViewPj");
    }

    public IEnumerator goToScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Challenge_HUD");
    }

}
