using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MindMap_Ctrl : MonoBehaviour
{

    #region VARIABLES
    //Public Variables
    public string[] mainIdeasArray;
    public string[] subIdeasArray1, subIdeasArray2, subIdeasArray3;
    //Private Variables
    [SerializeField]
    private Drag_MindMap _dragItem;
    private Button _arrowLeft, _arrowRight;
    private int _ideaIndexCtrl;
    private enum IdeasMoment
    {
        main, first, second, third
    }
    private IdeasMoment _actualMoment;
    private Transform _prefabParent;
    [SerializeField]
    private List<MindMapItem> _mmItemsList;
    private GameObject[] _blockerObjsArray;
    private GameObject _finalPanel;
    private MomentServices _momentServices;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    private void Start() { }

    private void OnEnable()
    {
        Drop_MindMap.OnDropFinish += DropWasOk;
    }
    private void OnDisable()
    {
        Drop_MindMap.OnDropFinish -= DropWasOk;
    }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _momentServices = new MomentServices();

        _arrowLeft = GameObject.Find("mm_ArrowL").GetComponent<Button>();
        _arrowRight = GameObject.Find("mm_ArrowR").GetComponent<Button>();
        _arrowLeft.onClick.AddListener(ChangeBackwards);
        _arrowRight.onClick.AddListener(ChangeForward);
        _arrowLeft.gameObject.SetActive(false);
        _arrowRight.gameObject.SetActive(false);
        _ideaIndexCtrl = 0;
        _actualMoment = IdeasMoment.main;
        _prefabParent = GameObject.Find("DragItem_Holder").GetComponent<Transform>();
        _mmItemsList = new List<MindMapItem>();
        _blockerObjsArray = new GameObject[3];
        for (int i = 0; i < _blockerObjsArray.Length; i++)
        {
            _blockerObjsArray[i] = GameObject.Find("Blocker_" + (i + 1));
        }
        _finalPanel = GameObject.Find("Panel_Final");
        _finalPanel.SetActive(false);
        AddDataToList();
        DropWasOk();
    }

    private void AddDataToList()
    {
        for (int i = 0; i < mainIdeasArray.Length; i++)
        {
            MindMapItem temp = new MindMapItem();
            temp.internalText = mainIdeasArray[i];
            temp.ideaType = 0;
            temp.mainIdeaNumber = i + 1;
            _mmItemsList.Add(temp);
            //            print(temp);
        }
        for (int i = 0; i < subIdeasArray1.Length; i++)
        {
            MindMapItem temp = new MindMapItem();
            temp.internalText = subIdeasArray1[i];
            temp.ideaType = 1;
            temp.mainIdeaNumber = 0;
            _mmItemsList.Add(temp);
        }
        for (int i = 0; i < subIdeasArray2.Length; i++)
        {
            MindMapItem temp = new MindMapItem();
            temp.internalText = subIdeasArray2[i];
            temp.ideaType = 2;
            temp.mainIdeaNumber = 0;
            _mmItemsList.Add(temp);
        }
        for (int i = 0; i < subIdeasArray3.Length; i++)
        {
            MindMapItem temp = new MindMapItem();
            temp.internalText = subIdeasArray3[i];
            temp.ideaType = 3;
            temp.mainIdeaNumber = 0;
            _mmItemsList.Add(temp);
        }
    }

    private void PrintList()
    {
        foreach (MindMapItem item in _mmItemsList)
        {
            print(item + " Text: " + item.internalText + " Idea type: " + item.ideaType + " MainIdea: " + item.mainIdeaNumber);
        }
    }

    private void ChangeForward()
    {
        _ideaIndexCtrl++;
        if (_ideaIndexCtrl > mainIdeasArray.Length)
        {
            _ideaIndexCtrl = 0;
        }
    }
    private void ChangeBackwards()
    {
        _ideaIndexCtrl--;
        if (_ideaIndexCtrl < 0)
        {
            _ideaIndexCtrl = mainIdeasArray.Length;
        }
    }

    private void UpdateContent()
    {
        switch (_actualMoment)
        {
            case IdeasMoment.main:
                //  mainIdeasArray[_ideaIndexCtrl];
                break;
            case IdeasMoment.first:
                //   subIdeasArray1[_ideaIndexCtrl];
                break;
            case IdeasMoment.second:
                //    subIdeasArray2[_ideaIndexCtrl];
                break;
            case IdeasMoment.third:
                //   subIdeasArray3[_ideaIndexCtrl];
                break;
        }
    }

    private void DropWasOk()
    {
        if (_mmItemsList.Count > 0)
        {
            Drag_MindMap dmm = (Drag_MindMap)Instantiate(_dragItem);
            dmm.transform.SetParent(_prefabParent);
            dmm.transform.localPosition = new Vector3(0, 0, 0);
            dmm.UpdateContent(_mmItemsList[0].internalText, _mmItemsList[0].ideaType, _mmItemsList[0].mainIdeaNumber);
            _mmItemsList.Remove(_mmItemsList[0]);
        }
        else
        {
            _finalPanel.SetActive(true);

            //DB
            _momentServices.UpdateMoment(100);
        }

        if (_mmItemsList.Count < 10)
        {
            for (int i = 0; i < _blockerObjsArray.Length; i++)
            {
                _blockerObjsArray[i].SetActive(false);
            }
        }

        switch (_actualMoment)
        {
            case IdeasMoment.main:
                //  mainIdeasArray[_ideaIndexCtrl];
                break;
            case IdeasMoment.first:
                //   subIdeasArray1[_ideaIndexCtrl];
                break;
            case IdeasMoment.second:
                //    subIdeasArray2[_ideaIndexCtrl];
                break;
            case IdeasMoment.third:
                //   subIdeasArray3[_ideaIndexCtrl];
                break;
        }
    }

    private void CreateNewDragItem() { }

    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}

[System.Serializable]
public class MindMapItem : System.Object
{
    public string internalText;
    public int ideaType;
    public int mainIdeaNumber;
}
