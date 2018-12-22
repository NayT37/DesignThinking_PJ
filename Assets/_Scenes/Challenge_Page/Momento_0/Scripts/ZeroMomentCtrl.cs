using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ZeroMomentCtrl : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private GameObject _sectorObj, _publicObj;
    private GameObject _btnProductivo, _btnEducativo, _btnSalud;
    private Image _imgProductivo, _imgEducativo, _imgSalud;
    private Color32 _neutralClr, _selectedClr;
    private bool _isSectorSelected, _isGenderSelected, _isAgeSelected;
    private int _pageCtrl;
    private Image[] _ageImgsArray, _genderImgsArray;
    private GameObject _genderHolderObj, _ageHolderObj;
    private GameObject _genderItemHolder, _ageItemHolder;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _sectorObj = GameObject.Find("SectoresObj");
        _publicObj = GameObject.Find("PublicoObj");
        //Get Buttons
        _btnProductivo = GameObject.Find("Productivo_Holder");
        _btnEducativo = GameObject.Find("Educativo_Holder");
        _btnSalud = GameObject.Find("Salud_Holder");
        //Get Images
        _imgProductivo = _btnProductivo.GetComponent<Image>();
        _imgEducativo = _btnEducativo.GetComponent<Image>();
        _imgSalud = _btnSalud.GetComponent<Image>();
        //Adding methods to the button
        _btnProductivo.GetComponent<Button>().onClick.AddListener(PressProductivo);
        _btnEducativo.GetComponent<Button>().onClick.AddListener(PressEducativo);
        _btnSalud.GetComponent<Button>().onClick.AddListener(PressSalud);
        //Color
        _neutralClr = new Color32(255, 255, 255, 255);
        _selectedClr = new Color32(0, 255, 170, 255);
        //Page number
        _pageCtrl = 1;
        _genderHolderObj = GameObject.Find("Genero_Holder");
        _ageHolderObj = GameObject.Find("Edad_Holder");

        _genderItemHolder = _genderHolderObj.transform.Find("Item_Holder").gameObject;
        _ageItemHolder = _ageHolderObj.transform.Find("Item_Holder").gameObject;

        _genderImgsArray = new Image[_genderItemHolder.transform.childCount];
        _ageImgsArray = new Image[_ageItemHolder.transform.childCount];

        for (int i = 0; i < _genderImgsArray.Length; i++)
        {
            _genderImgsArray[i] = _genderItemHolder.transform.GetChild(i).GetComponent<Image>();

        }
        for (int i = 0; i < _ageImgsArray.Length; i++)
        {
            _ageImgsArray[i] = _ageItemHolder.transform.GetChild(i).GetComponent<Image>();
        }

        _isSectorSelected = false;
        _isGenderSelected = false;
        _isAgeSelected = false;

        //Active and deactivate panels
        _sectorObj.SetActive(true);
        _publicObj.SetActive(false);
    }

    private void PressProductivo()
    {
        _isSectorSelected = true;
        _imgProductivo.color = _selectedClr;
        _imgEducativo.color = _neutralClr;
        _imgSalud.color = _neutralClr;
    }
    private void PressEducativo()
    {
        _isSectorSelected = true;
        _imgProductivo.color = _neutralClr;
        _imgEducativo.color = _selectedClr;
        _imgSalud.color = _neutralClr;
    }
    private void PressSalud()
    {
        _isSectorSelected = true;
        _imgProductivo.color = _neutralClr;
        _imgEducativo.color = _neutralClr;
        _imgSalud.color = _selectedClr;
    }

    public void Next()
    {

        if (_isSectorSelected && !_isGenderSelected && !_isAgeSelected)
        {
            _sectorObj.SetActive(false);
            _publicObj.SetActive(true);
        }
        else
        {
            print("Select another thing first");
        }

        if (_isGenderSelected && _isAgeSelected)
        {
            //Load scene M_1
            print("go next");
        }
        else
        {
            print("Select another thing first");
        }

        _pageCtrl++;
    }

    public void GenderSelection(string value)
    {
        _isGenderSelected = true;
        for (int i = 0; i < _genderImgsArray.Length; i++)
        {
            _genderImgsArray[i].color = _neutralClr;
        }
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = _selectedClr;
    }
    public void AgeSelection(string value)
    {
        _isAgeSelected = true;
        for (int i = 0; i < _ageImgsArray.Length; i++)
        {
            _ageImgsArray[i].color = _neutralClr;
        }
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = _selectedClr;
    }
    #endregion



    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
