using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.XR;
using System;

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
    [SerializeField]
    private int counterMindmaps, _actualTab;
    //private MainTab _mainTab;
    private Button _addIdea, _detIdea;
    private bool _showTabs;

    //Panel image variables
    private PanelImage _panelImg;
    private Button _imgPanelBtn;
    private int _storyTellingVersion;
    private VersionTab _versionTab;
    private int _changeTo;
    [SerializeField]
    private Text _mainIdeaTxt;
    [SerializeField]
    private GameObject _feedbackImg;
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

        var goServiceM = gameObject.AddComponent<MindmapServices>();
        _mindmapServices = goServiceM.GetComponent<MindmapServices>();
        var goServiceS = gameObject.AddComponent<SectionServices>();
        _sectionServices = goServiceS.GetComponent<SectionServices>();
        var goServiceN = gameObject.AddComponent<NodeServices>();
        _nodeServices = goServiceN.GetComponent<NodeServices>();

        _arraySections = new Section[6] { null, null, null, null, null, null };

        _arrayNodes = new Node[18] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };

        PanelSaveIdea.instance.ClosePanel();
        _titleTxt = "Idea Principal";
        _subMainIdeasArray = new SubMainIdea[6];
        for (int i = 0; i < 6; i++)
        {
            _subMainIdeasArray[i] = GameObject.Find("SubIdea_" + (i + 1)).GetComponent<SubMainIdea>();
        }
        _panelFeedback = GameObject.FindObjectOfType<PanelSaveFeedback>();
        _panelFeedback.gameObject.SetActive(false);


        _addIdea = GameObject.Find("AddIdea_Btn").GetComponent<Button>();
        _detIdea = GameObject.Find("DetIdea_Btn").GetComponent<Button>();
        _addIdea.onClick.AddListener(CreateNewIdea);
        _detIdea.onClick.AddListener(DeleteCurrentIdea);

        //Panel Image Stuff
        _panelImg = GameObject.FindObjectOfType<PanelImage>();
        _imgPanelBtn = GameObject.Find("OpenImgPanel_Btn").GetComponent<Button>();
        _imgPanelBtn.onClick.AddListener(OpenImgPanel);
        _panelImg.gameObject.SetActive(false);

        //DB Validation stuff
        var mmCounter = _mindmapServices.GetMindmapsCounter();
        switch (mmCounter)
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
        MainTab.instance.SetTabsToShowCouner(mmCounter);

        _internalTxt = "";
        _panelImg.SetInternalImg(null); //DB here to get the image
        ChMainHUD.instance.SetLimitCtrl(5); //If there is a Mindmap available

        _storyTellingVersion = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.version;
        _versionTab = FindObjectOfType<VersionTab>();
        _versionTab.SetInternalText(_storyTellingVersion);

        _actualTab = 1;
        _changeTo = 0;
        //ChargeNodesMindmap();
        ChangeMindmapVersion(1);

        //Habilitar o no los hijos
        var textIdea = DataBaseParametersCtrl.Ctrl._mindMapLoaded.ideaDescription;
        _internalTxt = textIdea;
        _feedbackImg.SetActive(false);
        ValidateInternalTxt();
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
        _actualTab = version; //is this necesary?
        setArraySections();
        setArrayNodes();
        setTextNodes();
        loadImage();
        ValidateInternalTxt();
    }

    public void setArraySections()
    {
        Int64 mindmapid = DataBaseParametersCtrl.Ctrl._mindMapLoaded.id;
        var sections = _sectionServices.GetSections(mindmapid);

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
                _arrayNodes[count] = item;
                count++;
            }

        }
    }

    private void loadImage()
    {

        var imageToBase64 = DataBaseParametersCtrl.Ctrl._mindMapLoaded.image;

        if (imageToBase64.Length > 10)
        {
            var b64_bytes = System.Convert.FromBase64String(imageToBase64);

            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(b64_bytes);
            _panelImg.SetInternalImg(tex);
        }
    }

    public void createMindmap()
    {
        int version = 0;
        if (counterMindmaps == 2)
        {
            version = 2;
        }
        else
        {
            version = 3;
        }

        _mindmapServices.CreateMindMap(version);

        //ChargeNodesMindmap();
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
    }

    private void setTextNodes()
    {

        for (int i = 0; i < _arraySections.Length; i++)
        {
            _subMainIdeasArray[i].SetInternalTxt(_arrayNodes[6 + i].description);
            _subMainIdeasArray[i].SetChildsOportunityTxt(_arrayNodes[i].description);
            _subMainIdeasArray[i].SetChildsRiskTxt(_arrayNodes[12 + i].description);
        }

        //Habilitar o no los hijos
        var textIdea = DataBaseParametersCtrl.Ctrl._mindMapLoaded.ideaDescription;
        _internalTxt = textIdea;
        ValidateInternalTxt();
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
            PanelSaveIdea.instance.ctrlTxtObj.SetInternalTxt(text);
            //If is NOT the main idea
            if (position != 18)
            {
                _nodeServices.UpdateNode(_arrayNodes[position], text);
            }
            else
            {
                var mindmaptem = DataBaseParametersCtrl.Ctrl._mindMapLoaded;
                mindmaptem.ideaDescription = text;
                _mindmapServices.UpdateMindmap(mindmaptem, 0);
                ValidateInternalTxt();
            }

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
    }

    public void EraseInfoAtPanel()
    {
        for (int i = 0; i < 6; i++)
        {
            _subMainIdeasArray[i].SetInternalTxt("");
            _subMainIdeasArray[i].SetChildsText("", "");
        }
        this.SetInternalTxt("");
    }

    public void AddVersion()
    {
        EraseInfoAtPanel();
    }

    private void MainTabChanged()
    {
        ChangeMindmapVersion(MainTab.instance.GetSelectedTab());
    }

    public void CreateNewIdea()
    {
        if (counterMindmaps < 2)
        {
            counterMindmaps++;
            _detIdea.gameObject.SetActive(true);
            _changeTo = 2;
        }
        else if (_addIdea.gameObject.activeInHierarchy)
        {
            counterMindmaps++;
            _addIdea.gameObject.SetActive(false);
            _changeTo = 3;
        }
        createMindmap();
        MainTab.instance.SetTabsToShowCouner(counterMindmaps);
        MainTab.instance.SetSelectedTab(_changeTo);
        _showTabs = false;
        MainTab.instance.HideTabs();
    }

    public void DeleteCurrentIdea()
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
        deleteMindmap();
        _actualTab = 1;
        MainTab.instance.SetTabsToShowCouner(counterMindmaps);
        MainTab.instance.HideTabs();
        MainTab.instance.SetSelectedTab(1);
        _showTabs = false;
    }

    public void OpenImgPanel()
    {
        _panelImg.gameObject.SetActive(true);
    }

    public void DeleteImgFromDB()
    {
        //DB changes here
        _mindmapServices.UpdateMindmap("");
        _panelImg.SetInternalImg(null);
    }

    public void UpdateImgFromDB(string imageToUpdate)
    {
        _mindmapServices.UpdateMindmap(imageToUpdate);
        _panelImg.SetDetBtn(true);
    }

    private void ValidateInternalTxt()
    {
        if (!_internalTxt.Equals(""))
        {
            foreach (SubMainIdea smi in _subMainIdeasArray)
            {
                smi.SetCanWrite(true);
            }
            _feedbackImg.SetActive(true);
            _mainIdeaTxt.gameObject.SetActive(false);
        }
        else
        {
            foreach (SubMainIdea smi in _subMainIdeasArray)
            {
                smi.SetCanWrite(false);
            }
            _feedbackImg.SetActive(false);
            _mainIdeaTxt.gameObject.SetActive(true);
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
