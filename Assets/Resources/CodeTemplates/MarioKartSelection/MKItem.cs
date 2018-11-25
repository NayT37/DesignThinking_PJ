using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class MKItem : MonoBehaviour, IComparable<MKItem>
{
    #region VARIABLES
    //Public Variables
    //This array it's supposed to be filled via code
    private GameObject[] _objectsToDisplayArray;
    //Which is the right answer? (from 1 to array's length)
    public int correctSpriteNumber;
    //This bool is for autoevaluate the Item and know when is correct
    [HideInInspector]
    public bool isCorrect;

    //Event's variables
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    //Private Variables
    private Transform _internalHolder;
    //This int is very important because it's the main control of images an autoevaluate function
    private int _objCtrl;
    //This int lets this items to be sorted in MKResultItem
    private int _internalID;
    #endregion


    #region SYSTEM_METHODS
    //Called in Awake to avoid some problems
    private void Awake() { Initializate(); }
    private void Start() { ValidateItemFirstTime(); } //Validate to know if it is correct
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _objectsToDisplayArray = new GameObject[3];
        //Get Image
        _internalHolder = transform.Find("Changing_Holder");
        //Create a random value to display a random image of the array
        _objCtrl = UnityEngine.Random.Range(0, _objectsToDisplayArray.Length);
        //Change the getted image's sprite
        //    _objectsToDisplayArray[_objCtrl].transform.SetParent(_internalHolder);
        //This item is not correct yet
        isCorrect = false;
        //And get the ID with the number
        _internalID = int.Parse(name.Split('_')[1]);

        for (int i = 0; i < _objectsToDisplayArray.Length; i++)
        {
            _objectsToDisplayArray[i] = _internalHolder.GetChild(i).gameObject;
            _objectsToDisplayArray[i].SetActive(false);
        }
        _objectsToDisplayArray[_objCtrl].SetActive(true);
        //Validate to know if it is correct
        //   ValidateItem();
    }

    //Go forward in the sprite's array
    public void ShowNextImage()
    {
        _objectsToDisplayArray[_objCtrl].SetActive(false);
        _objCtrl++;
        if (_objCtrl > _objectsToDisplayArray.Length - 1)
        {
            _objCtrl = 0;
        }
        _objectsToDisplayArray[_objCtrl].SetActive(true);
        ValidateItem();
    }

    //Go backwards in the sprite's array
    public void ShowPreviousImage()
    {
        _objectsToDisplayArray[_objCtrl].SetActive(false);
        _objCtrl--;
        if (_objCtrl < 0)
        {
            _objCtrl = _objectsToDisplayArray.Length - 1;
        }
        _objectsToDisplayArray[_objCtrl].SetActive(true);
        ValidateItem();
    }

    //Change the isCorrect boolean
    private void ValidateItem()
    {
        if (correctSpriteNumber - 1 == _objCtrl)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
        OnClicked();
    }

    private void ValidateItemFirstTime()
    {
        if (correctSpriteNumber - 1 == _objCtrl)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
    }
    #endregion

    #region INTERFACE_METHODS
    public int CompareTo(MKItem obj)
    {
        // If other is not a valid object reference, this instance is greater.
        if (obj == null) return 1;
        return _internalID.CompareTo(obj._internalID);
    }
    #endregion


    #region GETTERS_AND_SETTERS
    //Get the reference to compare if the number is the correct
    public int GetImgCtrl()
    {
        return _objCtrl;
    }
    #endregion

    #region COROUTINES
    #endregion
}
