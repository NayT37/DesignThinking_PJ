using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CtrlCreateCurso : MonoBehaviour
{


    #region Variables
    public InputField NameCourse;
    private string tmp;
    private CourseServices _courseServices;
    public GameObject _feedbackGroup;
    #endregion

    void Start()
    {
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
        if(!string.IsNullOrEmpty(NameCourse.text)){
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
                StartCoroutine(UserExist());
                DOTween.Play("7");
            }
        }else{
            DOTween.Play("7");
        }
    }
    IEnumerator UserExist(){
		_feedbackGroup.SetActive (true);
        NameCourse.text = "";
        NameCourse.placeholder.color = new Color(255,255,255,0);
        DOTween.Restart("exist");
		yield return new WaitForSeconds (2.8f);
        NameCourse.placeholder.color = new Color(255,255,255,1);
		_feedbackGroup.SetActive (false);
		NameCourse.text = "";
		tmp = "";
	}
}
