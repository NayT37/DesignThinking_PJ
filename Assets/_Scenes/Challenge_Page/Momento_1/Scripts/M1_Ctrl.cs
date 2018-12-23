using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_Ctrl : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    [SerializeField]
    private int _panelQuantity;
    private EmpathyPanel[] _panelsArray;
    private GameObject[] _checkByPanel;
    private int _activePanel;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _panelsArray = new EmpathyPanel[_panelQuantity];
        _checkByPanel = new GameObject[_panelQuantity - 1];

        for (int i = 0; i < _panelsArray.Length; i++)
        {
            _panelsArray[i] = GameObject.Find("Panel_" + i).GetComponent<EmpathyPanel>();
            if (i > 0)
            {
                _panelsArray[i].SetActivePanel(false);
            }
        }
        _activePanel = 0;
        for (int i = 0; i < _checkByPanel.Length; i++)
        {
            _checkByPanel[i] = GameObject.Find("Zones").transform.GetChild(i).Find("CheckObj").gameObject;
            _checkByPanel[i].SetActive(false);
        }

    }

    public void ActivePanelByNumber(int panelNumber)
    {
        if (panelNumber != _activePanel)
        {
            _panelsArray[_activePanel].SetActivePanel(false);
            _panelsArray[panelNumber].SetActivePanel(true);
            _activePanel = panelNumber;
        }
    }

    public void OnPanelTextChanged(int panelId)
    {
        _checkByPanel[panelId - 1].SetActive(true);
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
