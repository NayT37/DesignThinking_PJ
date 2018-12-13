using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


public class Ctrl_MainScene : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        XRSettings.enabled = false;
    }

    public void GoUser()
    {
        StartCoroutine(ResgisterUser());
    }

    IEnumerator ResgisterUser()
    {
        SceneManager.LoadScene("RegisterUser", LoadSceneMode.Additive);
        yield return null;
        SceneManager.UnloadSceneAsync("Main_Scene");
    }
}
