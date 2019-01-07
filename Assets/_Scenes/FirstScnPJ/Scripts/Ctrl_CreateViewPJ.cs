using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_CreateViewPJ : MonoBehaviour
{

    public Button _btnCreateProject;
    public Button _btnLoadProjects;

    private bool _doDataToLoad;

    public GameObject _notData;

    public Transform _TextTransform;

    private ProjectServices _projectServices;

    // Use this for initialization
    void Start()
    {

        _doDataToLoad = false;
        _btnCreateProject.onClick.AddListener(delegate { eventClick(_btnCreateProject.name); });
        _btnLoadProjects.onClick.AddListener(delegate { eventClick(_btnLoadProjects.name); });
        _projectServices = new ProjectServices();

    }

    private void eventClick(string name)
    {
        bool isChange = false;
        // int groupId = DataBaseParametersCtrl.Ctrl._groupLoaded.id;

        int counterProjects = 0;//_projectServices.GetProjectsCounter(groupId);

        if (counterProjects != 0)
        {
            _doDataToLoad = true;
        }

        string newSceneToLoad = "";

        if (name.Equals("NewProjectBtn"))
        {
            var project = _projectServices.CreateProject("");
            newSceneToLoad = "Challenge_HUD";
            isChange = true;
        }
        else
        {
            if (_doDataToLoad)
            {
                newSceneToLoad = "ViewPJs";
                isChange = true;
            }
            else
            {
                GameObject obj = Instantiate(_notData, _TextTransform);
                StartCoroutine(DeletePrefab(obj));
                Debug.Log("No hay resultados para cargar");
            }
        }

        if (isChange)
        {
            DOTween.Play(name);
            DOTween.Play("bg_transition");
            StartCoroutine(ChangeScene(newSceneToLoad));
        }


    }

    private IEnumerator DeletePrefab(GameObject obj)
    {
        yield return new WaitForSeconds(4.0f);
        DestroyImmediate(obj);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region COROUTINES
    private IEnumerator ChangeScene(string newSceneToLoad)
    {

        yield return new WaitForSeconds(2.0f);
        /*
		It doesn't need to load asyncrhonous...

		SceneManager.LoadScene(newSceneToLoad, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("M_5A"); 
		*/
        SceneManager.LoadScene(newSceneToLoad);
    }
    #endregion
}
