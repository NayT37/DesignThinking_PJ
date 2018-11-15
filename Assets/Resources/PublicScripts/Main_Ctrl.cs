using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Main_Ctrl : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Static instance of GameManager which allows it to be accessed by any other script.
    public static Main_Ctrl instance = null;
    //Private Variables
    private bool isPaused;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    private void Start() { }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        isPaused = false;
    }

    public void GoToScene(int number)
    {
        SceneManager.LoadScene(number);
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
