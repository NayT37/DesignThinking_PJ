using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SubTab : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES
    //Public Variables
    public delegate void StateChangedAction();
    public static event StateChangedAction OnStateChange;
    //Private Variables
    private MainTab _mainTab;
    private Color32 _activeClr, _deactiveClr;
    private Image _internalImg;
    private bool _isActiveState;
    private int _internalID;
    private Text _internalTxt;

    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _internalID = int.Parse(name.Split('_')[1]);
        _internalTxt = transform.GetChild(0).GetComponent<Text>();
        _activeClr = new Color32(255, 255, 255, 255);
        _deactiveClr = new Color32(200, 200, 200, 200);
        _internalImg = GetComponent<Image>();
        _isActiveState = false;
        _internalImg.color = _deactiveClr;
    }

    private void ChangeState()
    {
        if (!_mainTab)
        {
            _mainTab = GameObject.FindObjectOfType<MainTab>();
        }
        if (_mainTab.GetSelectedTab() != _internalID)
        {
            _mainTab.SetSelectedTab(_internalID);
            OnStateChange();
            if (_isActiveState) { _internalImg.color = _activeClr; } else { _internalImg.color = _deactiveClr; }
        }
    }

    public void SetStateTo(bool value)
    {
        _isActiveState = value;
        if (_isActiveState)
        { _internalImg.color = _activeClr; }
        else { _internalImg.color = _deactiveClr; }

    }
    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData eventData)
    {
        _isActiveState = !_isActiveState;
        ChangeState();
    }
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
