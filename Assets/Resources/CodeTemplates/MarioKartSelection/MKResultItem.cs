using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MKResultItem : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    public MKItem[] _MKItemsArray;
    public int[] _obtainedResultsArray;
    #endregion
    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    void OnEnable()
    {
        MKItem.OnClicked += ValidateAnswer;
    }

    void OnDisable()
    {
        MKItem.OnClicked -= ValidateAnswer;
    }
    #endregion
    #region CREATED_METHODS
    private void Initializate()
    {
        _MKItemsArray = FindObjectsOfType<MKItem>();
        _obtainedResultsArray = new int[_MKItemsArray.Length];
        //Order the array
        Array.Sort(_MKItemsArray);
        UpdateObtainedResults();
    }

    private void ValidateAnswer()
    {
        int temp = 0;
        foreach (MKItem item in _MKItemsArray)
        {
            if (item.isCorrect)
            {
                temp++;
            }
        }
        UpdateObtainedResults();
        if (temp != 0 && temp == _MKItemsArray.Length)
        {
            FinishGame();
        }
    }

    private void UpdateObtainedResults()
    {
        for (int i = 0; i < _MKItemsArray.Length; i++)
        {
            _obtainedResultsArray[i] = _MKItemsArray[i].GetImgCtrl();
        }
        UpdateContent();
    }

    public virtual void UpdateContent() { }
    public virtual void FinishGame() { print("Game was finished"); }
    #endregion
    #region COROUTINES
    #endregion
}
