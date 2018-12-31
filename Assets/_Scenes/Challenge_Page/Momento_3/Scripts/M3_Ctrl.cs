using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

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
    private Transform[] _dropZonesArray;

    private NoteServices _noteServices;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _noteServices = new NoteServices();

        _mainPanel = GameObject.Find("Main_Panel");
        _addPostItPanel = GameObject.Find("AddPostIt_Panel");
        _contentHolder = GameObject.Find("Content").transform;
        _editableItem = GameObject.Find("EditableItem").GetComponent<PostIt>();

        _dropZonesArray = new Transform[6];

        for (int i = 0; i < 6; i++)
        {
            _dropZonesArray[i] = GameObject.Find("DropZone_" + (i + 1)).transform;
        }

        int counternotes = _noteServices.GetNotesCounter();

        var notes = _noteServices.GetNotes();

        _addPostItPanel.SetActive(false);
        _postItQntt = 0;
        _mainTab = GameObject.Find("Btn_1").GetComponent<Button>();
        _mainTab.onClick.AddListener(ShowHideTabs);
        _showTabs = true;
        ShowHideTabs();
        GeneratePostIts(counternotes, notes);
    }

    public void AddNewPostIt()
    {
        _addPostItPanel.SetActive(true);
    }

    public void SavePostIt()
    {
        string text = _editableItem.GetInternalInput().text;
        if (text != "")
        {
            _postItQntt++;
            GameObject temp = Instantiate(_postIt, new Vector3(0, 0, 0), Quaternion.identity);
            Drag_M3_Item drag = temp.GetComponent<Drag_M3_Item>();
            temp.transform.SetParent(_contentHolder);
            temp.transform.localScale = new Vector3(1, 1, 1);
            drag.ChangeText(text);
            var note = _noteServices.CreateNote(text);
            drag._note = note;



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

    private void GeneratePostIts(int number, IEnumerable<Note> notes)
    {
        int counter = 0;

        foreach (var item in notes)
        {
            GameObject temp = Instantiate(_postIt, new Vector2(0, 0), Quaternion.identity);
            Drag_M3_Item drag = temp.GetComponent<Drag_M3_Item>();

            //DB
            drag.ChangeText(item.description);
            drag.internalID = item.position;
            try
            {
                drag.internalID = 1;
                temp.transform.SetParent(_dropZonesArray[drag.internalID - 1]);
                drag.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                drag.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                temp.transform.localScale = new Vector3(1, 1, 1);
                _dropZonesArray[drag.internalID - 1].GetComponent<Drop_M3_Zone>().CheckForChild();
            }
            catch (IndexOutOfRangeException e)
            {
                temp.transform.SetParent(_contentHolder);
                temp.transform.localScale = new Vector3(1, 1, 1);
            }
            counter++;
        }


        for (int i = 0; i < number; i++)
        {
            GameObject temp = Instantiate(_postIt, new Vector2(0, 0), Quaternion.identity);
            Drag_M3_Item drag = temp.GetComponent<Drag_M3_Item>();

            //DB
            //   drag.ChangeText(_editableItem.GetInternalInput().text);
            drag.internalID = 0;
            try
            {
                drag.internalID = 1;
                temp.transform.SetParent(_dropZonesArray[drag.internalID - 1]);
                drag.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                drag.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                temp.transform.localScale = new Vector3(1, 1, 1);
                _dropZonesArray[drag.internalID - 1].GetComponent<Drop_M3_Zone>().CheckForChild();
            }
            catch (IndexOutOfRangeException e)
            {
                temp.transform.SetParent(_contentHolder);
                temp.transform.localScale = new Vector3(1, 1, 1);
            }
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
