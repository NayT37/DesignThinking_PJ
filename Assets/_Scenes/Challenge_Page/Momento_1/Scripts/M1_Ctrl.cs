using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Vuforia;
public class M1_Ctrl : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    [SerializeField]
    private int _panelQuantity;
    private EmpathyPanel[] _panelsArray;
    private GameObject[] _checkByPanel;
    [SerializeField]
    private int _activePanel;

    private SectorServices _sectorServices;

    private Sector[] _arraySectors;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        XRSettings.enabled = false;
        VuforiaBehaviour.Instance.enabled = false;

        _sectorServices = new SectorServices();

        _arraySectors = new Sector[6];

        Int64 empathymapid = DataBaseParametersCtrl.Ctrl._empathyMapLoaded.id;

        var sectors = _sectorServices.GetSectors(empathymapid);

        int counter = 0;

        _panelsArray = new EmpathyPanel[_panelQuantity];
        _checkByPanel = new GameObject[_panelQuantity - 1];

        bool isCheck = true;
        foreach (var item in sectors)
        {
            _arraySectors[counter] = item;
            Debug.Log(_arraySectors[counter].ToString());
            _checkByPanel[counter] = GameObject.Find("Zones").transform.GetChild(counter).Find("CheckObj").gameObject;
            if (item.description.Equals(""))
            {
                if (isCheck)
                {
                    ChMainHUD.instance.SetLimitCtrl(2);
                    isCheck = false;
                }
                _checkByPanel[counter].SetActive(false);
            }
            counter++;
        }

        // DataBaseParametersCtrl.Ctrl._sectorLoaded = _arraySectors[0];
        //  _sectorServices.UpdateSector("");


        for (int i = 0; i < _panelsArray.Length; i++)
        {
            _panelsArray[i] = GameObject.Find("Panel_" + i).GetComponent<EmpathyPanel>();
            if (i > 0)
            {
                _panelsArray[i].SetActivePanel(false);
            }
        }
        _activePanel = 0;
        // for (int i = 0; i < _checkByPanel.Length; i++)
        // {
        //     _checkByPanel[i] = GameObject.Find("Zones").transform.GetChild(i).Find("CheckObj").gameObject;
        //     _checkByPanel[i].SetActive(false);
        // }

    }

    public void activePanel(int panelNumber)
    {
        ActivePanelByNumber(panelNumber, true);
    }

    public void ActivePanelByNumber(int panelNumber, bool isActive)
    {
        print("Active is: " + panelNumber);

        if (panelNumber != 0)
        {
            if (isActive)
            {
                DataBaseParametersCtrl.Ctrl._sectorLoaded = _arraySectors[panelNumber - 1];
                _panelsArray[panelNumber].UpdateText(_arraySectors[panelNumber - 1].description);
            }

        }
        // else
        // {
        //     DataBaseParametersCtrl.Ctrl._sectorLoaded = _arraySectors[_activePanel - 1];
        //     if (isActive)
        //     {
        //       _panelsArray[_activePanel - 1].UpdateText(_arraySectors[_activePanel - 1].description);  
        //     } 
        // }



        if (panelNumber != _activePanel)
        {
            _panelsArray[_activePanel].SetActivePanel(false);
            _panelsArray[panelNumber].SetActivePanel(true);
            _activePanel = panelNumber;
        }

        //When some limit is cleared...
        ChMainHUD.instance.SetLimitCtrl(2);
    }

    public void OnPanelTextChanged(int panelId, string value)
    {
        _checkByPanel[panelId - 1].SetActive(true);
        var s = _sectorServices.UpdateSector(value);
        _arraySectors[panelId - 1] = s;
    }

    public void ClosePanel(int panelNumber)
    {
        if (panelNumber != _activePanel)
        {
            _panelsArray[_activePanel].SetActivePanel(false);
            _panelsArray[panelNumber].SetActivePanel(true);
            _activePanel = panelNumber;
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
