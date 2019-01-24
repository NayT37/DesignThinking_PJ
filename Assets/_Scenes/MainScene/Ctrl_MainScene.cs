using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using Vuforia;


public class Ctrl_MainScene : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        XRSettings.enabled = false;
        VuforiaBehaviour.Instance.enabled = false;
    }


    public void GoUser()
    {
        StartCoroutine(ResgisterUser());
    }

    IEnumerator ResgisterUser()
    {

        yield return null;
        SceneManager.LoadScene("RegisterUser");
    }
}
