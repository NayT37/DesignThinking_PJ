using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class M3_Ctrl : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    [SerializeField]
    private GameObject _postIt;
    private GameObject _mainPanel, _addPostItPanel;
    private Transform _contentHolder;
    private int _postItQntt;
    private PostIt _editableItem;
    private Button _mainTab;
    private bool _showTabs;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _mainPanel = GameObject.Find("Main_Panel");
        _addPostItPanel = GameObject.Find("AddPostIt_Panel");
        _contentHolder = GameObject.Find("Content").transform;
        _editableItem = GameObject.Find("EditableItem").GetComponent<PostIt>();
        _addPostItPanel.SetActive(false);
        _postItQntt = 0;
        _mainTab = GameObject.Find("Btn_1").GetComponent<Button>();
        _mainTab.onClick.AddListener(ShowHideTabs);
        _showTabs = true;
        ShowHideTabs();
    }

    public void AddNewPostIt()
    {
        _addPostItPanel.SetActive(true);
    }

    public void SavePostIt()
    {
        if (_editableItem.GetInternalInput().text != "")
        {
            _postItQntt++;
            GameObject temp = Instantiate(_postIt, new Vector3(0, 0, 0), Quaternion.identity);
            Drag_M3_Item drag = temp.GetComponent<Drag_M3_Item>();
            temp.transform.SetParent(_contentHolder);
            temp.transform.localScale = new Vector3(1, 1, 1);
            drag.ChangeText(_editableItem.GetInternalInput().text);
        }
        _mainPanel.SetActive(true);
        _addPostItPanel.SetActive(false);
        _editableItem.GetInternalInput().text = "";
    }

    public void ShowHideTabs()
    {
        _showTabs = !_showTabs;
        for (int i = 1; i < _mainTab.transform.childCount; i++)
        {
            _mainTab.transform.GetChild(i).gameObject.SetActive(_showTabs);
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
