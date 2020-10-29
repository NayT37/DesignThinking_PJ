using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

public class MainTab : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES
    //Public Variables
    public delegate void TabChangeAction();
    public static event TabChangeAction OnTabChange;
    public static MainTab instance = null;
    //Private Variables
    [SerializeField]
    private int _selectedTab;
    private SubTab[] _childsArray;
    private Text _internalTxt;
    private bool _showTabs;
    [SerializeField]
    private int _tabsToShowCounter;
    [SerializeField]
    private string _textToShow;
    #endregion

    #region SYSTEM_METHODS
    public void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        _childsArray = new SubTab[3];
    }
    public void Start() { Initializate(); }
    public void OnEnable() { SubTab.OnStateChange += SetChildStateTofalse; }
    public void OnDisable() { SubTab.OnStateChange -= SetChildStateTofalse; }
    #endregion


    #region CREATED_METHODS
    public void Initializate()
    {
        //Initial();
        _selectedTab = 1;
        _internalTxt = transform.GetChild(0).GetComponent<Text>();
        _showTabs = true;
        // _tabsToShowCounter = 1;
        HideTabs();
    }

    private void Initial()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        _childsArray = new SubTab[3];
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
        SearchChilds();
        _showTabs = false;
        for (int i = 0; i < 3; i++)
        {
            _childsArray[i].gameObject.SetActive(false);
        }
    }

    private void SearchChilds()
    {
        try
        {
            if (_childsArray[0] == null)
            {
                for (int i = 0; i < _childsArray.Length; i++)
                {
                    _childsArray[i] = transform.GetChild(i + 1).GetComponent<SubTab>();
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("There was an error " + e);
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
        if (!_internalTxt)
        {
            _internalTxt = transform.GetChild(0).GetComponent<Text>();
        }
        _internalTxt.text = (_textToShow + " " + value);
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
