using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class TinderGame : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    public List<Sprite> correctImgs, incorrectImgs;
    public int imagesSize;
    [HideInInspector]
    public bool selectedAnswer;

    //Private Variables
    private List<TinderImage> _tinderImagesList;
    private int _imagesQuantity;
    private Transform _parentObject;
    private TinderImage _displayedTinderImg;
    private Image _displayedImg;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update()
    {
        if (_displayedImg.sprite == null)
        {
            _displayedImg.sprite = _tinderImagesList[0].spriteToShow;
        }
    }

    //Suscribe to event to validate answer
    void OnEnable()
    {
        TinderImage.ValidateImage += ValidateAnswer;
    }


    //Unsuscribe to event to validate answer
    void OnDisable()
    {
        TinderImage.ValidateImage -= ValidateAnswer;
    }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        //Init the list
        _tinderImagesList = new List<TinderImage>();
        //Quantity is equal to the correct images count plus the incorrect images count
        _imagesQuantity = correctImgs.Count + incorrectImgs.Count;
        //This boolean changes while the image is dragged at letf or right
        selectedAnswer = false;
        try
        {
            //Try to 
            _parentObject = GameObject.Find("Img_Holder").GetComponent<Transform>();
        }
        catch (Exception e)
        {
            Debug.Log("Parent object was not found");
        }

        GameObject NewObj = new GameObject(); //Create the GameObject
        NewObj.AddComponent<TinderImage>(); //Add CaptchaImage Component script
        NewObj.AddComponent<Image>(); //Add Image component
        var dimension = NewObj.GetComponent<RectTransform>(); //This is to define the image size
        dimension.SetParent(_parentObject); //After find the parent, set the object as a child
        dimension.sizeDelta = new Vector2(imagesSize, imagesSize); //Give the size, the scale and the position
        NewObj.transform.localScale = new Vector3(1, 1, 1);
        NewObj.transform.localPosition = new Vector3(0, 0, 0);
        _displayedTinderImg = NewObj.GetComponent<TinderImage>(); //Store the TinderImage component into a local variable
        _displayedImg = NewObj.GetComponent<Image>(); //Store the Image component into a local variable

        //Add elements to the list
        for (int i = 0; i < _imagesQuantity; i++)
        {
            TinderImage newTndImg = new TinderImage();
            //If image is correct
            if (i < correctImgs.Count)
            {
                newTndImg.spriteToShow = correctImgs[i];
                newTndImg.internalAnswer = true;
            }
            //If image is not correct
            else
            {
                newTndImg.spriteToShow = incorrectImgs[i - correctImgs.Count];
                newTndImg.internalAnswer = false;
            }
            _tinderImagesList.Add(newTndImg);
        }
        //Shuffle list
        ShuffleSprtList(_tinderImagesList);
    }

    public void UpdateDisplayedImage()
    {
        if (_tinderImagesList.Count > 0)
        {
            _displayedImg.sprite = _tinderImagesList[0].spriteToShow;
        }
        else
        {
            _displayedImg.sprite = Resources.Load<Sprite>("CodeTemplates/CaptchaGame/defaultCaptcha");
            FinishGame();
        }
    }

    private void ValidateAnswer()
    {
        //If there are images in the list
        if (_tinderImagesList.Count > 0)
        {
            //Set the displayed image to the first tinder image list
            _displayedTinderImg = _tinderImagesList[0];
            //If the selected value is the same as the image list's answer expected
            if (selectedAnswer == _displayedTinderImg.internalAnswer)
            {
                //Correct case
                _tinderImagesList.RemoveAt(0);
                UpdateDisplayedImage();
            }
            else
            {
                //Incorrect case
                TinderImage temp = _tinderImagesList[0];
                _tinderImagesList.RemoveAt(0);
                _tinderImagesList.Insert(_tinderImagesList.Count, temp);
                UpdateDisplayedImage();
            }
        }
    }

    private void ShuffleSprtList(List<TinderImage> list)
    {
        TinderImage temp;
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = UnityEngine.Random.Range(0, list.Count);
            temp = list[rnd];
            list[rnd] = list[i];
            list[i] = temp;
            list[i] = list[i];
        }
    }

    public virtual void FinishGame()
    {
        //Override this method for different behavior
    }
    #endregion


    #region COROUTINES
    #endregion
}
