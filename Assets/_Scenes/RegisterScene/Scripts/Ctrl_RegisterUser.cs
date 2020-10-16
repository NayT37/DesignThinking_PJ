using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.XR;
using Image = UnityEngine.UI.Image;

public class Ctrl_RegisterUser : MonoBehaviour
{

    #region  variables
    public InputField userName;
    public InputField passName;

    public Transform _parentText;

    public GameObject _prefabText, _noUserPrefab, _userNoExistPrefab, _loadUser;
    public Toggle _checkFirstTime;

    public GameObject _panelBlock;
    private DOTweenAnimation animationGame;

    private TeacherServices _teacherServices;

    private CourseServices courseS;
    public bool Testing;
    #endregion

    public void Start()
    {
        _panelBlock.SetActive(false);
        var goCourses = gameObject.AddComponent<CourseServices>();
        courseS = goCourses.GetComponent<CourseServices>();

        var teacher = DataBaseParametersCtrl.Ctrl._teacherLoggedIn;

        if (teacher != null)
        {
            userName.text = teacher.email;
        }

    }
    public void GoUser()
    {
        var goTeacher = gameObject.AddComponent<TeacherServices>();
        _teacherServices = goTeacher.GetComponent<TeacherServices>();

        string name = userName.text;
        string password = passName.text;

        if(Testing){
            userName.text = name = "talentum@test.com";
            passName.text = password = "123456";
        }
        

        //Debug.Log(passName.text+ " .... "+ password);
        bool isFirstTime = _checkFirstTime.isOn;
        if (!name.Equals("") && !passName.Equals(""))
        {
            _panelBlock.SetActive(true);
            bool isConn = DataBaseParametersCtrl.Ctrl.doConnection();
            bool doOtherMethod = false;
            var teacher = new Teacher();
            //GameObject objLoad = Instantiate(_loadUser, _parentText);
            if (isFirstTime)
            {
                if (isConn)
                {
                    teacher = _teacherServices.GetTeacherNamed(name, password, isFirstTime);
                    StartCoroutine(getIsQueryOk(teacher));
                    
                    //doOtherMethod = true;
                }
                else
                {
                    GameObject obj = Instantiate(_prefabText, _parentText);
                    StartCoroutine(DeletePrefab(obj));
                    Debug.Log("No tiene conexión a internet...");
                }

            }
            else
            {
                teacher = _teacherServices.GetTeacherNamed(name, password, isFirstTime);
                doOtherMethod = true;
            }

            if (doOtherMethod)
            {

                Debug.Log(teacher);
                if (teacher.identityCard.Equals("null"))
                {
                    //DOTween.Play("7");
                    userName.text = "";
                    passName.text = "";

                    if (DataBaseParametersCtrl.Ctrl.isNotTeacherExist)
                    {
                       GameObject obj = Instantiate(_userNoExistPrefab, _parentText);
                       StartCoroutine(DeletePrefab(obj)); 
                    } else{
                        GameObject obj = Instantiate(_noUserPrefab, _parentText);
                       StartCoroutine(DeletePrefab(obj));
                    }

                }
                else
                {
                    DOTween.Play("bg_transition");
                    StartCoroutine(ResgisterUser());
                }

             
            }
        }
        else
        {
            DOTween.Play("7");
            userName.text = "";
            passName.text = "";
            /* Debug.Log("inicio");
			courseS.GetCourses();
			StartCoroutine(getIsQueryOk()); */

        }
    }

    private IEnumerator getIsQueryOk(Teacher teacher)
    {
        GameObject objLoad = Instantiate(_loadUser, _parentText);
        Debug.Log("Waiting to get Teacher...");
        yield return new WaitUntil(() => DataBaseParametersCtrl.Ctrl.isQueryOk == true);
        DataBaseParametersCtrl.Ctrl.isQueryOk = false;
        DestroyImmediate(objLoad);
        _panelBlock.SetActive(false);
        teacher = DataBaseParametersCtrl.Ctrl._teacherLoggedIn;
         if (teacher.identityCard.Equals("null"))
                {
                    //DOTween.Play("7");
                    userName.text = "";
                    passName.text = "";
                    if (DataBaseParametersCtrl.Ctrl.isNotTeacherExist)
                    {
                       GameObject obj = Instantiate(_userNoExistPrefab, _parentText);
                       StartCoroutine(DeletePrefab(obj)); 
                    } else{
                        GameObject obj = Instantiate(_noUserPrefab, _parentText);
                       StartCoroutine(DeletePrefab(obj));
                    }
                    

                }
                else
                {
                    Debug.Log(teacher.ToString());
                    DOTween.Play("bg_transition");
                    StartCoroutine(ResgisterUser());
                }
    }

    IEnumerator ResgisterUser()
    {
        _panelBlock.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("SelectGame");
        userName.text = "";
        passName.text = "";
    }

    private IEnumerator DeletePrefab(GameObject obj)
    {
        yield return new WaitForSeconds(3.0f);
        DestroyImmediate(obj);
        _panelBlock.SetActive(false);
    }


}
