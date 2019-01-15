using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainTab : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    [SerializeField]
    private int _selectedTab;
    private SubTab[] _childsArray;
    private Text _internalTxt;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void OnEnable() { SubTab.OnStateChange += SetChildStateTofalse; }
    private void OnDisable() { SubTab.OnStateChange -= SetChildStateTofalse; }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _selectedTab = 1;
        _childsArray = new SubTab[3];
        _internalTxt = GetComponentInChildren<Text>();
        ShowTabs(false);
    }

    private void SetChildStateTofalse()
    {
        SearchChilds();
        for (int i = 0; i < _childsArray.Length; i++)
        {
            if ((i + 1) != _selectedTab)
            {
                _childsArray[i].SetStateTo(false);
            }
        }
    }

    public void ShowTabs(bool value)
    {
        SearchChilds();
        for (int i = 0; i < _childsArray.Length; i++)
        {
            _childsArray[i].gameObject.SetActive(value);
        }
    }

    private void SearchChilds()
    {
        if (!_childsArray[0])
        {
            for (int i = 0; i < _childsArray.Length; i++)
            {
                _childsArray[i] = transform.GetChild(i + 1).GetComponent<SubTab>();
            }
        }
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    public int GetSelectedTab()
    {
        return _selectedTab;
    }
    public void SetSelectedTab(int value)
    {
        _selectedTab = value;
        _internalTxt.text = "IDEA " + _selectedTab;
    }
    #endregion


    #region COROUTINES
    #endregion
}
