using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Ctrl_M4 : CtrlInternalText
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private SubMainIdea[] _subMainIdeasArray;
    private PanelSaveFeedback _panelFeedback;

    private MindmapServices _mindmapServices;

    private SectionServices _sectionServices;

    private NodeServices _nodeServices;

    private Mindmap[] _arrayMindmaps;

    private Section[] _arraySections;

    private Node[] _arrayNodes;

    private int counterMindmaps, _actualTab;
    private MainTab _mainTab;
    private Button _addIdea, _detIdea;
    private bool _showTabs;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void OnEnable() { MainTab.OnTabChange += MainTabChanged; }
    private void OnDisable() { MainTab.OnTabChange -= MainTabChanged; }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        counterMindmaps = 0;
        _mindmapServices = new MindmapServices();
        _sectionServices = new SectionServices();
        _nodeServices = new NodeServices();

        _arraySections = new Section[6] { null, null, null, null, null, null };

        _arrayNodes = new Node[18] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };

        PanelSaveIdea.instance.ClosePanel();
        _titleTxt = "Idea Principal";
        _subMainIdeasArray = new SubMainIdea[6];
        for (int i = 0; i < 6; i++)
        {
            _subMainIdeasArray[i] = GameObject.Find("SubIdea_" + (i + 1)).GetComponent<SubMainIdea>();
        }
        //DB stuff
        _internalTxt = "";
        _panelFeedback = GameObject.FindObjectOfType<PanelSaveFeedback>();
        _panelFeedback.gameObject.SetActive(false);

        _actualTab = 1;
        _addIdea = GameObject.Find("AddIdea_Btn").GetComponent<Button>();
        _detIdea = GameObject.Find("DetIdea_Btn").GetComponent<Button>();
        _addIdea.onClick.AddListener(CreateNewIdea);
        _detIdea.onClick.AddListener(deleteMindmap);
        //DB Validation here
        _detIdea.gameObject.SetActive(false);

        ChargeNodesMindmap();
        ChangeMindmapVersion(1);
    }

    public void ChargeNodesMindmap()
    {

        counterMindmaps = _mindmapServices.GetMindmapsCounter();

        _arrayMindmaps = new Mindmap[counterMindmaps];

        setArrayMindmaps();

        //_arrayPostit = new List<GameObject>();

    }

    public void setArrayMindmaps()
    {

        var mindmaps = _mindmapServices.GetMindmaps();

        int counter = 0;

        foreach (var item in mindmaps)
        {
            _arrayMindmaps[counter] = item;
            counter++;
        }
    }

    public void ChangeMindmapVersion(int version)
    {
        ChargeNodesMindmap();

        DataBaseParametersCtrl.Ctrl._mindMapLoaded = _arrayMindmaps[version - 1];

        setArraySections();
        setArrayNodes();
        setTextNodes();


    }

    public void setArraySections()
    {

        var sections = _sectionServices.GetSections();

        int counter = 0;

        foreach (var item in sections)
        {
            _arraySections[counter] = item;
            counter++;
        }
    }

    public void setArrayNodes()
    {
        int count = 0;
        for (int i = 0; i < _arraySections.Length; i++)
        {

            var nodes = _nodeServices.GetNodes(_arraySections[i].id);

            foreach (var item in nodes)
            {
                _arrayNodes[i + count] = item;
                count++;
            }

        }
    }

    public void createMindmap()
    {
        int version = 0;
        if (counterMindmaps == 1)
        {
            version = 2;
        }
        else
        {
            version = 3;
        }

        _mindmapServices.CreateMindMap(version);

        ChargeNodesMindmap();
        ChangeMindmapVersion(version);
    }

    public void deleteMindmap()
    {
        var mindmaptodelete = DataBaseParametersCtrl.Ctrl._mindMapLoaded;
        int versionToDelete = mindmaptodelete.version;
        int lengthStorys = _arrayMindmaps.Length;

        if (versionToDelete == 1)
        {
            _mindmapServices.UpdateMindmap(_arrayMindmaps[1], 1);
            if (lengthStorys == 3)
            {
                _mindmapServices.UpdateMindmap(_arrayMindmaps[2], 2);
            }
        }
        else if (versionToDelete == 2)
        {
            if (lengthStorys == 3)
            {
                _mindmapServices.UpdateMindmap(_arrayMindmaps[2], 2);
            }

        }

        _mindmapServices.DeleteMindmap(mindmaptodelete);

        if (counterMindmaps > 2)
        {
            counterMindmaps--;
            _addIdea.gameObject.SetActive(true);
        }
        else if (_detIdea.gameObject.activeInHierarchy)
        {
            counterMindmaps--;
            _detIdea.gameObject.SetActive(false);
        }
        UpdateDetBtn();
        ChargeNodesMindmap();
        ChangeMindmapVersion(1);
    }

    private void setTextNodes()
    {

        for (int i = 0; i < _arraySections.Length; i++)
        {
            _subMainIdeasArray[6 + i].SetInternalTxt(_arrayNodes[6 + i].description);
            _subMainIdeasArray[i].SetChildsOportunityTxt(_arrayNodes[i].description);
            _subMainIdeasArray[i].SetChildsRiskTxt(_arrayNodes[12 + i].description);
        }
    }
    public void OpenSavePanel()
    {
        //DB stuff
        PanelSaveIdea.instance.OpenPanel(this);
    }
    public void CloseSavePanel()
    {
        PanelSaveIdea.instance.ClosePanel();
    }

    public void SaveInfoPanel()
    {
        string text = PanelSaveIdea.instance.inputTxt.text;
        if (text != "")
        {
            int position = PanelSaveIdea.instance.ctrlTxtObj.databaseID;
            _nodeServices.UpdateNode(_arrayNodes[position], text);
            PanelSaveIdea.instance.ctrlTxtObj.SetInternalTxt(text);
            //DB stuff
            PanelSaveIdea.instance.ClosePanel();
        }
    }

    private void SetSubMainIdeaText()
    {
        //DB stuff
        for (int i = 0; i < 6; i++)
        {
            _subMainIdeasArray[i].SetInternalTxt("Hola soy " + (i + 1));
            _subMainIdeasArray[i].SetChildsText("Oportunidad " + i, "Riesgo " + (i + 1));
        }
    }

    public void SavePrototypeVersion()
    {
        //DBStuff
        _panelFeedback.OpenPanel(1, 1);
    }

    public void ChangeActiveVersion()
    {
        EraseInfoAtPanel();
        //DBStuff
    }

    public void EraseInfoAtPanel()
    {
        for (int i = 0; i < 6; i++)
        {
            _subMainIdeasArray[i].SetInternalTxt("");
            _subMainIdeasArray[i].SetChildsText("", "");
        }
    }

    public void AddVersion()
    {
        EraseInfoAtPanel();
    }

    private void MainTabChanged()
    {
        if (!_mainTab)
        {
            _mainTab = GameObject.FindObjectOfType<MainTab>();
        }

        ChangeMindmapVersion(_mainTab.GetSelectedTab());
    }

    public void CreateNewIdea()
    {
        if (counterMindmaps < 2)
        {
            counterMindmaps++;
            _detIdea.gameObject.SetActive(true);
        }
        else if (_addIdea.gameObject.activeInHierarchy)
        {
            counterMindmaps++;
            _addIdea.gameObject.SetActive(false);
        }
        //  HideTabs();
        if (!_mainTab)
        {
            _mainTab = GameObject.FindObjectOfType<MainTab>();
        }
        _mainTab.SetTabsToShowCouner(counterMindmaps);
        _mainTab.HideTabs();
        _showTabs = false;
    }

    public void UpdateDetBtn()
    {
        if (counterMindmaps > 2)
        {
            counterMindmaps--;
            _addIdea.gameObject.SetActive(true);
        }
        else if (_detIdea.gameObject.activeInHierarchy)
        {
            counterMindmaps--;
            _detIdea.gameObject.SetActive(false);
        }
        _actualTab = 1;
        _mainTab.SetTabsToShowCouner(counterMindmaps);
        _mainTab.HideTabs();
        _mainTab.SetSelectedTab(1);
        _showTabs = false;
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
