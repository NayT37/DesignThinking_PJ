using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EmpathyPanel : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    [SerializeField]
    private bool _isMainPanel;
    private bool _isActivePanel;
    private bool _isInfoEmpty;
    private string _internalText;
    private InputField _internalInput;
    private Button _saveInfoBtn;
    private M1_Ctrl _levelCtrl;
    private int _panelId;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _isActivePanel = false;
        _isInfoEmpty = false;
        _internalText = "";
        _panelId = int.Parse(name.Split('_')[1]);
        if (!_isMainPanel)
        {
            _internalInput = transform.Find("InputTextHolder").GetChild(0).GetComponent<InputField>();
            _saveInfoBtn = transform.Find("BtnGuardar").GetComponent<Button>();
            _saveInfoBtn.onClick.AddListener(SaveInfo);
        }
    }

    private void SaveInfo()
    {
        if (_levelCtrl == null)
        {
            _levelCtrl = GameObject.FindObjectOfType<M1_Ctrl>();
        }
        if (_internalInput.text != "" && _internalInput.text != _internalText)
        {
            _internalText = _internalInput.text;
            _isInfoEmpty = false;
            _levelCtrl.OnPanelTextChanged(_panelId);
            //Send internalText to DB
        }
        _levelCtrl.ActivePanelByNumber(0);
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    public void SetActivePanel(bool value)
    {
        _isActivePanel = value;
        this.gameObject.SetActive(_isActivePanel);
    }
    #endregion


    #region COROUTINES
    #endregion
}
