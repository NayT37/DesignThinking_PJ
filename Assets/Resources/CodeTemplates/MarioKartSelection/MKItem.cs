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
    public Sprite[] imagesToDisplayArray;
    //Which is the right answer? (from 1 to array's length)
    public int correctSpriteNumber;
    //This bool is for autoevaluate the Item and know when is correct
    [HideInInspector]
    public bool isCorrect;

    //Event's variables
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    //Private Variables
    private Image _internalImage;
    //This int is very important because it's the main control of images an autoevaluate function
    private int _imgCtrl;
    //This int lets this items to be sorted in MKResultItem
    private int _internalID;
    #endregion


    #region SYSTEM_METHODS
    //Called in Awake to avoid some problems
    private void Awake() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        //Get Image
        _internalImage = GetComponent<Image>();
        //Create a random value to display a random image of the array
        _imgCtrl = UnityEngine.Random.Range(0, imagesToDisplayArray.Length);
        //Change the getted image's sprite
        _internalImage.sprite = imagesToDisplayArray[_imgCtrl];
        //This item is not correct yet
        isCorrect = false;
        //Validate to know if it is correct
        ValidateItem();
        //And get the ID with the number
        _internalID = int.Parse(name.Split('_')[1]);
    }

    //Go forward in the sprite's array
    public void ShowNextImage()
    {
        _imgCtrl++;
        if (_imgCtrl > imagesToDisplayArray.Length - 1)
        {
            _imgCtrl = 0;
        }
        _internalImage.sprite = imagesToDisplayArray[_imgCtrl];
        ValidateItem();
    }

    //Go backwards in the sprite's array
    public void ShowPreviousImage()
    {
        _imgCtrl--;
        if (_imgCtrl < 0)
        {
            _imgCtrl = imagesToDisplayArray.Length - 1;
        }
        _internalImage.sprite = imagesToDisplayArray[_imgCtrl];
        ValidateItem();
    }

    //Change the isCorrect boolean
    private void ValidateItem()
    {
        if (correctSpriteNumber - 1 == _imgCtrl)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
        OnClicked();
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
        return _imgCtrl;
    }
    #endregion

    #region COROUTINES
    #endregion
}
