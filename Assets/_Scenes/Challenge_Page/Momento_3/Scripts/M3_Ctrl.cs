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
    private Button _mainTab, _addIdea, _detIdea;
    private bool _showTabs;
    private Transform[] _dropZonesArray;

    private NoteServices _noteServices;

    private StorytellingServices _storytellingServices;

    private StoryTelling[] _arraystorytellings;

    [SerializeField]
    private int counterstorytelling, _actualTab;

    private List<GameObject> _arrayPostit;
    TabBehaviour[] _tabsArray;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _noteServices = new NoteServices();

        _storytellingServices = new StorytellingServices();

        _mainPanel = GameObject.Find("Main_Panel");
        _addPostItPanel = GameObject.Find("AddPostIt_Panel");
        _contentHolder = GameObject.Find("Content").transform;
        _editableItem = GameObject.Find("EditableItem").GetComponent<PostIt>();

        _dropZonesArray = new Transform[6];

        for (int i = 0; i < 6; i++)
        {
            _dropZonesArray[i] = GameObject.Find("DropZone_" + (i + 1)).transform;
        }

        _actualTab = 1;

        _addPostItPanel.SetActive(false);
        _postItQntt = 0;

        _tabsArray = new TabBehaviour[3];
        for (int i = 0; i < _tabsArray.Length; i++)
        {
            _tabsArray[i] = GameObject.Find("Btn_" + (i + 1)).GetComponent<TabBehaviour>();

        }
        _mainTab = _tabsArray[0].GetComponent<Button>();

        _addIdea = GameObject.Find("AddIdea_Btn").GetComponent<Button>();
        _detIdea = GameObject.Find("DetIdea_Btn").GetComponent<Button>();
        _mainTab.onClick.AddListener(ShowHideTabs);
        _addIdea.onClick.AddListener(CreateNewIdea);
        _detIdea.onClick.AddListener(DeleteCurrentIdea);
        _detIdea.gameObject.SetActive(false);
        _showTabs = false;
        HideTabs();
        //       _arrayPostit = new List<GameObject>(); //Delete this line
        //      counterstorytelling = 1; //Delete this line

        ChargeNotesToStoryTelling();
        ChangeStoryTellingVersion(1);
    }

    public void ChargeNotesToStoryTelling()
    {

        counterstorytelling = _storytellingServices.GetStoryTellingsCounters();

        _arraystorytellings = new StoryTelling[counterstorytelling];

        setArrayStoryTellings();

        _arrayPostit = new List<GameObject>();

    }

    public void ChangeStoryTellingVersion(int version)
    {

        deleteNotesPrefab();

        DataBaseParametersCtrl.Ctrl._storyTellingLoaded = _arraystorytellings[version - 1];

        int counternotes = _noteServices.GetNotesCounter();

        var notes = _noteServices.GetNotes();

        GeneratePostIts(counternotes, notes);

    }

    public void setArrayStoryTellings()
    {

        var storytellings = _storytellingServices.GetStoryTellings();

        int counter = 0;

        foreach (var item in storytellings)
        {
            _arraystorytellings[counter] = item;
            counter++;
        }
    }

    public void createStoryTelling()
    {
        int version = 0;
        if (counterstorytelling == 1)
        {
            version = 2;
        }
        else
        {
            version = 3;
        }

        _storytellingServices.CreateStoryTelling(version);

        ChargeNotesToStoryTelling();
    }

    public void deleteStorytelling()
    {

        int versionToDelete = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.version;

        if (versionToDelete == 1)
        {
            _storytellingServices.UpdateStoryTelling(_arraystorytellings[1], 1);
            _storytellingServices.UpdateStoryTelling(_arraystorytellings[2], 2);
        }
        else if (versionToDelete == 2)
        {
            _storytellingServices.UpdateStoryTelling(_arraystorytellings[2], 2);
        }

        _storytellingServices.DeleteStoryTelling();

        ChargeNotesToStoryTelling();

        ChangeStoryTellingVersion(1);
    }

    public void deleteNotesPrefab()
    {
        if (_arrayPostit.Count != 0)
        {
            for (int i = 0; i < _arrayPostit.Count; i++)
            {
                DestroyImmediate(_arrayPostit[i]);
            }
            _arrayPostit.Clear();
        }
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
            _arrayPostit.Add(temp);
        }
        _mainPanel.SetActive(true);
        _addPostItPanel.SetActive(false);
        _editableItem.GetInternalInput().text = "";
    }

    public void ShowHideTabs()
    {
        if (counterstorytelling != 1)
        {
            _showTabs = !_showTabs;
            for (int i = 1; i < counterstorytelling; i++)
            {
                _mainTab.transform.GetChild(i).gameObject.SetActive(_showTabs);
            }
        }
    }

    private void GeneratePostIts(int number, IEnumerable<Note> notes)
    {
        int counter = 0;

        foreach (var item in notes)
        {
            //  var temp = _arrayPostit[counter];
            var temp = Instantiate(_postIt, new Vector2(0, 0), Quaternion.identity);
            Drag_M3_Item drag = temp.GetComponent<Drag_M3_Item>();
            _arrayPostit.Add(temp);
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
    }

    public void CreateNewIdea()
    {
        if (counterstorytelling < 2)
        {
            counterstorytelling++;
            _detIdea.gameObject.SetActive(true);
        }
        else if (_addIdea.gameObject.activeInHierarchy)
        {
            counterstorytelling++;
            _addIdea.gameObject.SetActive(false);
        }
        HideTabs();
        _showTabs = false;
    }

    public void DeleteCurrentIdea()
    {
        if (counterstorytelling > 2)
        {
            counterstorytelling--;
            _addIdea.gameObject.SetActive(true);
        }
        else if (_detIdea.gameObject.activeInHierarchy)
        {
            counterstorytelling--;
            _detIdea.gameObject.SetActive(false);
        }
        _actualTab = 1;
        for (int i = 0; i < _tabsArray.Length; i++)
        {
            _tabsArray[i].SetInernalID(i + 1);
        }
        HideTabs();
        _showTabs = false;
        deleteNotesPrefab();
        ChargeNotesToStoryTelling();
        ChangeStoryTellingVersion(1);
    }

    public void HideTabs()
    {
        _showTabs = false;
        for (int i = 1; i < 3; i++)
        {
            _mainTab.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    public void SetActualTab(int value)
    {
        _actualTab = value;
        deleteNotesPrefab();
        ChargeNotesToStoryTelling();
        ChangeStoryTellingVersion(1);
    }
    #endregion


    #region COROUTINES
    #endregion
}
