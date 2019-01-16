using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class MainTab : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES
    //Public Variables
    public delegate void TabChangeAction();
    public static event TabChangeAction OnTabChange;
    //Private Variables
    [SerializeField]
    private int _selectedTab;
    private SubTab[] _childsArray;
    private Text _internalTxt;
    private bool _showTabs;
    private int _tabsToShowCounter;
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
        _showTabs = true;
        _tabsToShowCounter = 3; // Delete this line
        ShowHideTabs();
        _tabsToShowCounter = 1; //Change this line to show all childs
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
            else
            {
                _childsArray[i].SetStateTo(true);
            }
        }
    }

    public void ShowHideTabs()
    {
        SearchChilds();
        _showTabs = !_showTabs;
        for (int i = 0; i < _tabsToShowCounter; i++)
        {
            _childsArray[i].gameObject.SetActive(_showTabs);
        }
    }

    public void HideTabs()
    {
        _showTabs = false;
        for (int i = 0; i < 3; i++)
        {
            _childsArray[i].gameObject.SetActive(false);
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
    public void OnPointerClick(PointerEventData eventData)
    {
        ShowHideTabs();
    }
    #endregion


    #region GETTERS_AND_SETTERS
    public int GetSelectedTab()
    {
        return _selectedTab;
    }
    public void SetSelectedTab(int value)
    {
        SearchChilds();
        _selectedTab = value;
        _internalTxt.text = "IDEA " + _selectedTab;
        foreach (SubTab child in _childsArray)
        {
            child.SetStateTo(false);
        }
        _childsArray[value - 1].SetStateTo(true);
        HideTabs();
        OnTabChange();
    }
    public void SetTabsToShowCouner(int value)
    {
        _tabsToShowCounter = value;
    }
    #endregion


    #region COROUTINES
    #endregion
}
