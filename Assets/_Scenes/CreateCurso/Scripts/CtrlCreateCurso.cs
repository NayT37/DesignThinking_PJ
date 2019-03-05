using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.XR;
using Vuforia;

public class CtrlCreateCurso : MonoBehaviour
{


    #region Variables
    public InputField NameCourse;
    private string tmp;
    private CourseServices _courseServices;
    #endregion

    void Start()
    {
        XRSettings.enabled = false;
        VuforiaBehaviour.Instance.enabled = false;

        _courseServices = new CourseServices();
    }

    void Update()
    {
    }

    public void Close()
    {
        StartCoroutine(Back());
    }

    public void SaveData()
    {
        tmp = NameCourse.text;
        StartCoroutine(SaveNameCourse());
    }

    IEnumerator Back()
    {
        DOTween.Play("bg_transition");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("SelectGame");
    }

    IEnumerator SaveNameCourse()
    {
        Main_Ctrl.instance.NameCourse = NameCourse.text.ToUpper();
        var result = _courseServices.CreateCourse(NameCourse.text.ToUpper());
        if (result.id != 0)
        {
            DataBaseParametersCtrl.Ctrl._courseLoaded = result;
            DOTween.Play("bg_transition");
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("CreateGroup");
        }
        else
        {
            DOTween.Play("7");
        }

    }

}
