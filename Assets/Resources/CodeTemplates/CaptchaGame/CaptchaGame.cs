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
    //Images are gonna be squares, how big are they gonna be?
    public float imagesSize = 100f;
    //Separation variables
    [Range(0.5f, 10.0f)]
    public float horizontalSeparation = 2f;
    [Range(0.5f, 1.9f)]
    public float verticalSeparation = 0.75f;
    //This lists can be filled in other ways. Suggestion: Resources.LoadAll<Sprites>
    public List<Sprite> _incorrectImgsList;
    public List<Sprite> _correctImgsList;
    [HideInInspector]
    public int ctrlAnswersToFinish;

    //Private Variables
    [SerializeField]
    //Empty parent object for images ubication. Suggestion: Use GameObject.Find(string)
    private Transform _parentObject;
    //Array to store positions to make'em random
    private Vector3[] _storedPositionArray;
    //Array to store created objects. It is a game object so it can access to transform property easily
    private GameObject[] _displayedImagesArray;
    #endregion


    #region SYSTEM_METHODS
    public virtual void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _storedPositionArray = new Vector3[(int)imagesToDisplay];
        _displayedImagesArray = new GameObject[(int)imagesToDisplay];
        ShuffleSprtList(_incorrectImgsList);
        ShuffleSprtList(_correctImgsList);
        ctrlAnswersToFinish = 0;
        switch ((int)imagesToDisplay)
        {
            case 6:
                for (int i = 0; i < (int)imagesToDisplay; i++)
                {
                    GameObject NewObj = new GameObject(); //Create the GameObject
                    Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
                    NewObj.AddComponent<CaptchaImage>(); //Add CaptchaImage Component script
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
                    //AssignImageBehaviour(i, NewObj);
                    _storedPositionArray[i] = NewObj.transform.localPosition;
                    _displayedImagesArray[i] = NewObj;
                }
                AssignImageBehaviour();
                break;
            case 8:
                for (int i = 0; i < (int)imagesToDisplay; i++)
                {
                    GameObject NewObj = new GameObject(); //Create the GameObject
                    Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
                    NewObj.AddComponent<CaptchaImage>(); //Add CaptchaImage Component script
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
                    //  AssignImageBehaviour(i, NewObj);
                    _storedPositionArray[i] = NewObj.transform.localPosition;
                    _displayedImagesArray[i] = NewObj;
                }
                AssignImageBehaviour();
                break;
            case 9:
                for (int i = 0; i < (int)imagesToDisplay; i++)
                {
                    GameObject NewObj = new GameObject(); //Create the GameObject
                    Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
                    NewObj.AddComponent<CaptchaImage>(); //Add CaptchaImage Component script
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
                    //   AssignImageBehaviour(i, NewObj);
                    _storedPositionArray[i] = NewObj.transform.localPosition;
                    _displayedImagesArray[i] = NewObj;

                }
                AssignImageBehaviour();
                break;
        }
        RandomImageArray();
    }
    //Random Images Array
    public virtual void RandomImageArray()
    {
        //  Vector3[] temp = _storedPositionArray;
        Vector3 tempObj;
        for (int i = 0; i < _storedPositionArray.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0, _storedPositionArray.Length);
            tempObj = _storedPositionArray[rnd];
            _storedPositionArray[rnd] = _storedPositionArray[i];
            _storedPositionArray[i] = tempObj;
            //  _storedPositionArray[i] = _storedPositionArray[i];
        }
        ReassignImagePositions();
    }

    public virtual void ReassignImagePositions()
    {
        for (int i = 0; i < _displayedImagesArray.Length; i++)
        {
            _displayedImagesArray[i].transform.localPosition = _storedPositionArray[i];
        }
    }

    public virtual void AssignImageBehaviour()
    {
        int incorrectIndex = 0;
        for (int i = 0; i < _displayedImagesArray.Length; i++)
        {
            CaptchaImage captchaImage = _displayedImagesArray[i].GetComponent<CaptchaImage>();
            captchaImage.internalImage = captchaImage.GetComponent<Image>();
            captchaImage.indexNumber = i;
            if (i < correctImageQuantity)
            {
                captchaImage.isCorrect = true;
                captchaImage.internalImage.sprite = _correctImgsList[0];
                _correctImgsList.RemoveAt(0);
            }
            else
            {
                captchaImage.isCorrect = false;
                captchaImage.internalImage.sprite = _incorrectImgsList[incorrectIndex];
                incorrectIndex++;
            }
        }
    }

    public virtual void ShuffleSprtList(List<Sprite> list)
    {
        Sprite temp;
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = UnityEngine.Random.Range(0, list.Count);
            temp = list[rnd];
            list[rnd] = list[i];
            list[i] = temp;
            list[i] = list[i];
        }
    }

    public void CorrectAnswerBhvr(int index)
    {
        CaptchaImage temp = _displayedImagesArray[index].GetComponent<CaptchaImage>();
        temp.internalImage.sprite = _correctImgsList[0];
        _correctImgsList.RemoveAt(0);
        RandomImageArray();
    }

    public void IncorrectAnswerBhvr()
    {
        ShuffleSprtList(_incorrectImgsList);
        for (int i = 0; i < _displayedImagesArray.Length; i++)
        {
            CaptchaImage captchaImage = _displayedImagesArray[i].GetComponent<CaptchaImage>();
            if (!captchaImage.isCorrect)
            {
                captchaImage.internalImage.sprite = _incorrectImgsList[i];
            }
        }
        RandomImageArray();

    }

    public void ValidateFinishGame() { if (ctrlAnswersToFinish == correctImageQuantity) { FinishGame(); } }

    public virtual void FinishGame()
    {
        print("Game was finished");
    }
    #endregion
}