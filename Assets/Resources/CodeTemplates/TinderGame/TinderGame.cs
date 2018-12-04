using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class TinderGame : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    public List<String> correctTxts, incorrectTxts;
    [HideInInspector]
    public bool selectedAnswer;

    //Private Variables
    [SerializeField]
    private Sprite _draggableImg;
    [SerializeField]
    private List<TinderImage> _tinderImagesList;
    private int _imagesQuantity;
    private Transform _parentObject;
    private TinderImage _displayedTinderImg;
    private Image _displayedImg;
    private Text _displayedTxt;
    private MainTinderCtrl tinderCtrl;
    private Image _trueImg, _falseImg;
    private Transform _trueImgSize, _falseImgSize;
    private Color32 _normalClr, _correctClr, _incorrectClr;

    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    private void Start()
    {
        tinderCtrl = GameObject.FindObjectOfType<MainTinderCtrl>();
        _trueImg = transform.Find("TrueFalse_Holder/TrueImage").GetComponent<Image>();
        _falseImg = transform.Find("TrueFalse_Holder/FalseImage").GetComponent<Image>();
        _trueImgSize = _trueImg.transform;
        _falseImgSize = _falseImg.transform;
        _normalClr = new Color32(255, 255, 255, 255);
        _correctClr = new Color32(0, 255, 0, 255);
        _incorrectClr = new Color32(255, 0, 0, 255);
    }
    private void Update()
    {
        if (_displayedTxt.text == "")
        {
            _displayedTxt.text = _tinderImagesList[0].internalText;
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
        _imagesQuantity = correctTxts.Count + incorrectTxts.Count;
        //This boolean changes while the image is dragged at letf or right
        selectedAnswer = false;
        try
        {
            //Try to 
            // _parentObject = transform.Find("Gun").gameObject;("Img_Holder").GetComponent<Game>();
            _parentObject = transform.Find("Img_Holder");
        }
        catch (Exception e)
        {
            Debug.Log("Parent object was not found");
        }

        GameObject NewObj = new GameObject(); //Create the GameObject
        NewObj.AddComponent<TinderImage>(); //Add CaptchaImage Component script
        Image intImg = NewObj.AddComponent<Image>(); //Add Image component
        intImg.sprite = _draggableImg;
        var dimension = NewObj.GetComponent<RectTransform>(); //This is to define the image size
        dimension.SetParent(_parentObject); //After find the parent, set the object as a child
        dimension.sizeDelta = new Vector2(500, 200); //Give the size, the scale and the position
        NewObj.transform.localScale = new Vector3(1, 1, 1);
        NewObj.transform.localPosition = new Vector3(0, 0, 0);
        _displayedTinderImg = NewObj.GetComponent<TinderImage>(); //Store the TinderImage component into a local variable
        _displayedImg = NewObj.GetComponent<Image>(); //Store the Image component into a local variable

        //Add elements to the list
        for (int i = 0; i < _imagesQuantity; i++)
        {
            TinderImage newTndImg = new TinderImage();
            //If image is correct
            if (i < correctTxts.Count)
            {
                // newTndImg.spriteToShow = correctTxts[i];
                newTndImg.internalText = correctTxts[i];
                newTndImg.internalAnswer = true;
            }
            //If image is not correct
            else
            {
                // newTndImg.spriteToShow = incorrectTxts[i - correctTxts.Count];
                newTndImg.internalText = incorrectTxts[i - correctTxts.Count];
                newTndImg.internalAnswer = false;
            }
            _tinderImagesList.Add(newTndImg);
        }
        //Shuffle list
        ShuffleSprtList(_tinderImagesList);

        _displayedTxt = transform.Find("TextToDisplay").GetComponent<Text>();
        // GameObject.Find("TextToDisplay").GetComponent<Text>();
        _displayedTxt.transform.SetParent(NewObj.transform);
    }

    public void UpdateDisplayedImage()
    {
        if (_tinderImagesList.Count > 0)
        {
            // _displayedImg.sprite = _tinderImagesList[0].spriteToShow;
            _displayedTxt.text = _tinderImagesList[0].internalText;
        }
        else
        {
            // _displayedImg.sprite = null//Resources.Load<Sprite>("CodeTemplates/CaptchaGame/defaultCaptcha");
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
                /*                 
                Use this to add the image at the bottom
                                TinderImage temp = _tinderImagesList[0];
                                _tinderImagesList.RemoveAt(0);
                                _tinderImagesList.Insert(_tinderImagesList.Count, temp);
                                UpdateDisplayedImage();
                 */
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

    public void TrueFeedback()
    {
        _trueImgSize.localScale = new Vector3(2.5f, 2.5f, 1);
        _trueImg.color = _correctClr;
    }
    public void FalseFeedback()
    {
        _falseImgSize.localScale = new Vector3(2.5f, 2.5f, 1);
        _falseImg.color = _incorrectClr;
    }
    public void ReturnToNormal()
    {
        _trueImgSize.localScale = new Vector3(1, 1, 1);
        _falseImgSize.localScale = new Vector3(1, 1, 1);
        _trueImg.color = _normalClr;
        _falseImg.color = _normalClr;
    }

    public virtual void FinishGame()
    {
        //Override this method for different behavior
        print("Game was finished");
        tinderCtrl.FinishGame();
    }
    #endregion


    #region GETTERS_AND_SETTERS
    public Image GetTrueImg()
    {
        return _trueImg;
    }
    public Image GetFalseImg()
    {
        return _falseImg;
    }
    #endregion
}
