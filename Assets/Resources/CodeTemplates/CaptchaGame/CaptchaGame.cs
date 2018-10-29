using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class CaptchaGame : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Size of Images to be displayed (show images array) //Enum
    public enum DisplayedImages { six = 6, eight = 8, nine = 9 };
    public DisplayedImages imagesToDisplay = DisplayedImages.six;
    //Size of True Images
    public int correctImageQuantity = 3;
    public float imagesSize = 100f;
    [Range(0.5f, 10.0f)]
    public float horizontalSeparation = 2f;
    [Range(0.5f, 1.9f)]
    public float verticalSeparation = 0.75f;
    //Path for correct images
    public string pathForImages;
    //Empty parent object for images ubication
    [SerializeField]
    private Transform _parentObject;
    //Private Variables
    //General False Images
    private Sprite[] _incorrectImgsArray;
    private Sprite[] _correctImgsArray;
    //General True Images
    //Show Images Array (True + false) classObj?
    #endregion
    #region SYSTEM_METHODS
    public virtual void Start() { Initializate(); }
    public virtual void Update() { }
    #endregion
    #region CREATED_METHODS
    private void Initializate()
    {
        try
        {
            _incorrectImgsArray = Resources.LoadAll<Sprite>(pathForImages);
            _correctImgsArray = Resources.LoadAll<Sprite>(pathForImages);
        }
        catch (Exception e)
        {
            Debug.Log("Path is incorrect" + e);
        }

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
                    NewObj.SetActive(true);
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
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)), dimension.rect.height / verticalSeparation, 0);
                    }
                    else if (i >= 4)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i - 3) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)), dimension.rect.height / -verticalSeparation, 0);
                    }
                    NewObj.SetActive(true);
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
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)), dimension.rect.height / verticalSeparation, 0);
                    }
                    else if (i >= 3)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i - 3) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)), dimension.rect.height / -verticalSeparation, 0);
                    }
                    else if (i >= 6)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width / horizontalSeparation)) * (i - 6) - (dimension.rect.width + (dimension.rect.width / horizontalSeparation)), dimension.rect.height / -verticalSeparation, 0);
                    }
                    NewObj.SetActive(true);
                }
                break;
        }

    }

    private void OrderImages(Image[] arr) { }
    //Random Images Array

    public void ValidateAnswer()
    {
        //Check Click  //If Ok  //if False
    }
    #endregion
}

[System.Serializable]
public class CaptchaImage : System.Object
{
    public Sprite imgSprite;
    public bool isCorrect;
}
