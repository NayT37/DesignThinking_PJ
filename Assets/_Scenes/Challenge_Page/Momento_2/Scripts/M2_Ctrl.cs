using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class M2_Ctrl : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private InputField _txtField1, _txtField2, _txtField3;

    private ProblemServices _problemServices;

    private FieldServices _fieldServices;

    private string[] _arrayResults;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _problemServices = new ProblemServices();

        _fieldServices = new FieldServices();

        _arrayResults = new string[3];

        int counterProblem = _problemServices.GetProblemsCounter();

        if (counterProblem==0){
            var problem =_problemServices.CreateProblem(_arrayResults);
        }

        _txtField1 = GameObject.Find("FieldHolder_1").GetComponentInChildren<InputField>();
        _txtField2 = GameObject.Find("FieldHolder_2").GetComponentInChildren<InputField>();
        _txtField3 = GameObject.Find("FieldHolder_3").GetComponentInChildren<InputField>();
    }

    public void SendText()
    {
        if (_txtField1.text != "") { }
        if (_txtField2.text != "") { }
        if (_txtField3.text != "") { }
    }

    public void UpdateProblem(){

        _fieldServices.UpdateFields(_arrayResults);

    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
