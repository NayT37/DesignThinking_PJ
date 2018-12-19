using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReflexionCtrl : MonoBehaviour
{

    #region VARIABLES
    //Public Variables
    public int itemsQuantity;
    //Private Variables
    private GameObject[] _itemList;
    private Button _nextBtn;
    #endregion


    #region SYSTEM_METHODS
    private void Start()
    { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _nextBtn = GameObject.Find("Next_Btn").GetComponent<Button>();
        _nextBtn.onClick.AddListener(NextStage);
        _itemList = new GameObject[itemsQuantity];
        for (int i = 0; i < _itemList.Length; i++)
        {
            _itemList[i] = GameObject.Find("Intro_" + (i + 1));
            if (i != 0)
            {
                //     _itemList[i].SetActive(false);
            }
        }
    }

    private void NextStage()
    {
        print("something");
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
