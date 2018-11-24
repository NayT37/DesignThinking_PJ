using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriviaCtrl : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    [HideInInspector]
    public bool isTriviaFinished;
    public TriviaTxt[] triviaTxtArray;
    //Private Variables
    [SerializeField]
    private int _answersQuantity = 4;
    private Transform[] _btnsPosArray;
    private Vector3[] _btnStoredPosArray;
    #endregion


    #region SYSTEM_METHODS
    private void Awake()
    {

    }
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _btnsPosArray = new Transform[_answersQuantity];
        _btnStoredPosArray = new Vector3[_answersQuantity];
        for (int i = 0; i < _answersQuantity; i++)
        {
            _btnsPosArray[i] = GameObject.Find("BtnOption_" + (i + 1)).GetComponent<Transform>();
            print(_btnsPosArray[i].localPosition);
            _btnStoredPosArray[i] = _btnsPosArray[i].localPosition;
        }
        ShuffleTransformArray(_btnStoredPosArray);
        ReassingBtnPos();
    }

    private void ReassingBtnPos()
    {
        for (int i = 0; i < _answersQuantity; i++)
        {
            print("Before " + _btnsPosArray[i].localPosition);
            _btnsPosArray[i].localPosition = _btnStoredPosArray[i];
            print("After " + _btnsPosArray[i].localPosition);
        }
    }

    private void ShuffleTransformArray(Vector3[] tArray)
    {
        Vector3 temp;
        for (int i = 0; i < tArray.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0, tArray.Length);
            temp = tArray[rnd];
            tArray[rnd] = tArray[i];
            tArray[i] = temp;
            tArray[i] = tArray[i];
        }
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}

[System.Serializable]
public class TriviaTxt : System.Object
{
    public string questionTxt;
    public string[] answersArray;
}