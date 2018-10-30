using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class CaptchaGame : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Size of Images to be displayed (show images array)
    public enum DisplayedImages { six = 6, eight = 8, nine = 9 };
    public DisplayedImages imagesToDisplay = DisplayedImages.six;
    //How many displayed images are in the screen?
    public int correctImageQuantity = 3;
    public float imagesSize = 100f;
    [Range(0.5f, 10.0f)]
    public float horizontalSeparation = 2f;
    [Range(0.5f, 1.9f)]
    public float verticalSeparation = 0.75f;
    public Sprite[] _incorrectImgsArray;
    public Sprite[] _correctImgsArray;

    //Private Variables
    [SerializeField]
    //Empty parent object for images ubication
    private Transform _parentObject;

    private Sprite[] _showedImgsArray;
    private Vector3[] _storedPositionArray;
    private GameObject[] _displayedImagesArray;
    #endregion


    #region SYSTEM_METHODS
    public virtual void Start() { Initializate(); }
    public virtual void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _storedPositionArray = new Vector3[(int)imagesToDisplay];
        //_showedImgsArray = new Sprite[(int)imagesToDisplay];
        _displayedImagesArray = new GameObject[(int)imagesToDisplay];

        switch ((int)imagesToDisplay)
        {
            case 6:
                for (int i = 0; i < (int)imagesToDisplay; i++)
                {
                    GameObject NewObj = new GameObject(); //Create the GameObject
                    Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
                    var dimension = NewObj.GetComponent<RectTransform>();
                    dimension.SetParent(_parentObject);
                    dimension.sizeDelta = new Vector2(imagesSize, imagesSize);
                    NewObj.transform.localScale = new Vector3(1, 1, 1);
                    if (i < 3)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)), dimension.rect.height / verticalSeparation, 0);
                    }
                    else if (i >= 3)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i - 3) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)), dimension.rect.height / -verticalSeparation, 0);
                    }
                    AssignImageBehaviour(i, NewObj);
                    _storedPositionArray[i] = NewObj.transform.localPosition;
                    _displayedImagesArray[i] = NewObj;
                }
                break;
            case 8:
                for (int i = 0; i < (int)imagesToDisplay; i++)
                {
                    GameObject NewObj = new GameObject(); //Create the GameObject
                    Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
                    var dimension = NewObj.GetComponent<RectTransform>();
                    dimension.SetParent(_parentObject);
                    dimension.sizeDelta = new Vector2(imagesSize, imagesSize);
                    NewObj.transform.localScale = new Vector3(1, 1, 1);
                    if (i < 4)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)) - dimension.rect.width / 2, dimension.rect.height / verticalSeparation, 0);
                    }
                    else if (i >= 4)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i - 4) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)) - dimension.rect.width / 2, dimension.rect.height / -verticalSeparation, 0);
                    }
                    AssignImageBehaviour(i, NewObj);
                    _storedPositionArray[i] = NewObj.transform.localPosition;
                    _displayedImagesArray[i] = NewObj;
                }
                break;
            case 9:
                for (int i = 0; i < (int)imagesToDisplay; i++)
                {
                    GameObject NewObj = new GameObject(); //Create the GameObject
                    Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
                    var dimension = NewObj.GetComponent<RectTransform>();
                    dimension.SetParent(_parentObject);
                    dimension.sizeDelta = new Vector2(imagesSize, imagesSize);
                    NewObj.transform.localScale = new Vector3(1, 1, 1);
                    if (i < 3)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)), dimension.rect.height * 2 / verticalSeparation, 0);
                    }
                    else if (i >= 3 && i < 6)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i - 3) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)), 0, 0);
                    }
                    else if (i >= 6)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i - 6) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)), dimension.rect.height * 2 / -verticalSeparation, 0);
                    }
                    AssignImageBehaviour(i, NewObj);
                    _storedPositionArray[i] = NewObj.transform.localPosition;
                    _displayedImagesArray[i] = NewObj;
                }
                break;
        }
        RandomImageArray();
    }
    //Random Images Array
    private void RandomImageArray()
    {
        //  Vector3[] temp = _storedPositionArray;
        Vector3 tempObj;
        for (int i = 0; i < _storedPositionArray.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0, _storedPositionArray.Length);
            tempObj = _storedPositionArray[rnd];
            _storedPositionArray[rnd] = _storedPositionArray[i];
            _storedPositionArray[i] = tempObj;
            _storedPositionArray[i] = _storedPositionArray[i];
        }
        ReassignImagePositions();
    }

    private void ReassignImagePositions()
    {
        for (int i = 0; i < _displayedImagesArray.Length; i++)
        {
            _displayedImagesArray[i].transform.localPosition = _storedPositionArray[i];
        }
    }

    private void AssignImageBehaviour(int index, GameObject obj)
    {
        CaptchaImage captchaImage = obj.AddComponent<CaptchaImage>();
        if (index < correctImageQuantity)
        {
            captchaImage.isCorrect = true;
        }
        else
        {
            captchaImage.isCorrect = false;
        }
    }

    #endregion
}

[System.Serializable]
public class CaptchaImages : System.Object
{
    public Sprite imgSprite;
    public bool isCorrect;
}
