using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.XR;
using Vuforia;

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
    //private Button _mainTab, _addIdea, _detIdea;
    private Button _addIdea, _detIdea;

    private bool _showTabs;
    private Transform[] _dropZonesArray;

    private NoteServices _noteServices;

    private StorytellingServices _storytellingServices;

    private StoryTelling[] _arraystorytellings;

    [SerializeField]
    private int counterstorytelling, _actualTab;

    private List<GameObject> _arrayPostit;
    //TabBehaviour[] _tabsArray;
    // private MainTab _mainTab;
    private int _changeTo;

    //Feedback text to show
    private Text _feedbackTxt;
    private GameObject _blockPanel;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void OnEnable() { MainTab.OnTabChange += MainTabChanged; }
    private void OnDisable() { MainTab.OnTabChange -= MainTabChanged; }

    private void Update()
    {
    }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        XRSettings.enabled = false;
        VuforiaBehaviour.Instance.enabled = false;
        _changeTo = 0;
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

        _blockPanel = GameObject.Find("BlockPanel");
        _blockPanel.SetActive(false);

        _addIdea = GameObject.Find("AddIdea_Btn").GetComponent<Button>();
        _detIdea = GameObject.Find("DetIdea_Btn").GetComponent<Button>();
        _addIdea.onClick.AddListener(CreateNewIdea);
        _detIdea.onClick.AddListener(DeleteCurrentIdea);
        //DB validation here
        var stCounter = _storytellingServices.GetStoryTellingsCounters();
        switch (stCounter)
        {
            case 1:
                _addIdea.gameObject.SetActive(true);
                _detIdea.gameObject.SetActive(false);
                break;
            case 2:
                _addIdea.gameObject.SetActive(true);
                _detIdea.gameObject.SetActive(true);
                break;
            case 3:
                _addIdea.gameObject.SetActive(false);
                _detIdea.gameObject.SetActive(true);
                break;
        }
        MainTab.instance.SetTabsToShowCouner(stCounter);
        _feedbackTxt = GameObject.Find("FeedbackText").GetComponent<Text>();
        FeedbackTextValidation();
        _showTabs = false;
        _arrayPostit = new List<GameObject>();
        ChMainHUD.instance.SetLimitCtrl(4); //If there is a StoryTelling available
        counterstorytelling = 1;
        // ChargeNotesToStoryTelling();
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
        ChargeNotesToStoryTelling();
        DataBaseParametersCtrl.Ctrl._storyTellingLoaded = _arraystorytellings[version - 1];

        int counternotes = _noteServices.GetNotesCounter();

        var notes = _noteServices.GetNotes();

        GeneratePostIts(counternotes, notes);
        FeedbackTextValidation();
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
        if (counterstorytelling == 2)
        {
            version = 2;
        }
        else
        {
            version = 3;
        }

        var s = _storytellingServices.CreateStoryTelling(version);

        print(s.ToString());
        StartCoroutine(waitToStorytelling(version));
        // ChargeNotesToStoryTelling();

    }

    public void deleteStorytelling()
    {

        int versionToDelete = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.version;
        Debug.Log("version To delete ..." + versionToDelete);
        int lengthStorys = _arraystorytellings.Length;
        Debug.Log("versions counter ..." + lengthStorys);

        if (versionToDelete == 1)
        {
            _storytellingServices.UpdateStoryTelling(_arraystorytellings[1], 1);
            if (lengthStorys == 3)
            {
                _storytellingServices.UpdateStoryTelling(_arraystorytellings[2], 2);
            }
        }
        else if (versionToDelete == 2)
        {
            if (lengthStorys == 3)
            {
                _storytellingServices.UpdateStoryTelling(_arraystorytellings[2], 2);
            }

        }

        _storytellingServices.DeleteStoryTelling();
    }




    public void deleteNotesPrefab()
    {
        if (_arrayPostit.Count != 0)
        {
            for (int i = 0; i < _arrayPostit.Count; i++)
            {
                //_arrayPostit.Remove(_arrayPostit[i]);
                print("Destroying " + _arrayPostit[i]);
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
            GameObject temp = Instantiate(_postIt, new Vector2(0, 0), Quaternion.identity, _contentHolder);
            Drag_M3_Item drag = temp.GetComponent<Drag_M3_Item>();
            temp.transform.localScale = new Vector3(1, 1, 1);
            drag.ChangeText(text);
            var note = _noteServices.CreateNote(text);
            drag._note = note;
            _arrayPostit.Add(temp);
            drag.internalID = 0;
        }
        _feedbackTxt.gameObject.SetActive(false);
        _mainPanel.SetActive(true);
        _addPostItPanel.SetActive(false);
        _editableItem.GetInternalInput().text = "";
    }

    /*     public void ShowHideTabs()
        {
            if (counterstorytelling != 1)
            {
                _showTabs = !_showTabs;
                for (int i = 1; i < counterstorytelling; i++)
                {
                    _mainTab.transform.GetChild(i).gameObject.SetActive(_showTabs);
                }
            }
        } */

    private void GeneratePostIts(int number, IEnumerable<Note> notes)
    {
        int counter = 0;

        foreach (var item in notes)
        {
            //  var temp = _arrayPostit[counter];
            // var temp = Instantiate(_postIt, new Vector2(0, 0), Quaternion.identity);
            GameObject temp = null;
            if (item.position != 0)
            {
                temp = Instantiate(_postIt, new Vector2(0, 0), Quaternion.identity, _dropZonesArray[item.position - 1]);
            }
            else
            {
                temp = Instantiate(_postIt, new Vector2(0, 0), Quaternion.identity, _contentHolder);
            }
            Drag_M3_Item drag = temp.GetComponent<Drag_M3_Item>();
            drag._note = item;
            _arrayPostit.Add(temp);
            //DB
            drag.ChangeText(item.description);
            drag.internalID = item.position;
            counter++;
        }
    }

    public void CreateNewIdea()
    {
        /*         if (_mainTab == null)
                {
                    print("searching maintab");
                    _mainTab = GameObject.FindObjectOfType<MainTab>();
                } */
        if (counterstorytelling < 2)
        {
            counterstorytelling++;
            _detIdea.gameObject.SetActive(true);
            _changeTo = 2;
        }
        else if (_addIdea.gameObject.activeInHierarchy)
        {
            counterstorytelling++;
            _addIdea.gameObject.SetActive(false);
            _changeTo = 3;
        }
        createStoryTelling();
        print("Adding story telling now counter is " + counterstorytelling);
        MainTab.instance.SetTabsToShowCouner(counterstorytelling);
        MainTab.instance.SetSelectedTab(_changeTo);
        _showTabs = false;
        MainTab.instance.HideTabs();
    }

    public void DeleteCurrentIdea()
    {
        /*         if (!_mainTab)
                {
                    _mainTab = GameObject.FindObjectOfType<MainTab>();
                } */
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
        print("Deleting story telling now counter is " + counterstorytelling);

        deleteStorytelling();
        _actualTab = 1;
        MainTab.instance.SetTabsToShowCouner(counterstorytelling);
        MainTab.instance.HideTabs();
        MainTab.instance.SetSelectedTab(1);
        _showTabs = false;
    }

    private void MainTabChanged()
    {
        print("Trying to change to : " + MainTab.instance.GetSelectedTab());
        ChangeStoryTellingVersion(MainTab.instance.GetSelectedTab());
    }

    private void FeedbackTextValidation()
    {
        if (_contentHolder.childCount == 0) _feedbackTxt.gameObject.SetActive(true); else _feedbackTxt.gameObject.SetActive(false);
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    public void SetActualTab(int value)
    {
        _actualTab = value;
        ChangeStoryTellingVersion(value);
    }
    #endregion


    #region COROUTINES
    IEnumerator waitToStorytelling(int version)
    {
        //active panel
        _blockPanel.SetActive(true);
        Debug.Log("waitToStory start...");
        yield return new WaitUntil(() => DataBaseParametersCtrl.Ctrl.isQueryOk == true);
        DataBaseParametersCtrl.Ctrl.isQueryOk = false;
        //deactive
        _blockPanel.SetActive(false);
        Debug.Log("waitToStory finish...");
        ChangeStoryTellingVersion(version);
    }
    #endregion
}
