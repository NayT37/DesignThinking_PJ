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
    public Vector2 imagesSize = new Vector2(100, 100);
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

        /*         //Create and add objects
                GameObject NewObj = new GameObject(); //Create the GameObject
                Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
                                                               //  NewImage.sprite = currentSprite; //Set the Sprite of the Image Component on the new GameObject
                NewObj.GetComponent<RectTransform>().SetParent(_parentObject); //Assign the newly created Image GameObject as a Child of the Parent Panel.
                NewObj.SetActive(true); //Activate the GameObject */

        switch (imagesToDisplay)
        {
            case DisplayedImages.six:
                for (int i = 0; i < (int)imagesToDisplay; i++)
                {
                    GameObject NewObj = new GameObject(); //Create the GameObject
                    Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
                    var dimension = NewObj.GetComponent<RectTransform>();
                    dimension.SetParent(_parentObject);
                    dimension.sizeDelta = imagesSize;
                    NewObj.transform.localScale = new Vector3(1, 1, 1);
                    if (i < 3)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width * 0.5f)) * (i) - dimension.rect.width * 1.5f, dimension.rect.height * 0.75f, 0);
                    }
                    else if (i >= 3)
                    {
                        NewObj.transform.localPosition = new Vector3((dimension.rect.width + (dimension.rect.width * 0.5f)) * (i - 3) - dimension.rect.width * 1.5f, dimension.rect.height * -0.75f, 0);
                    }

                    NewObj.SetActive(true);
                }
                break;
            case DisplayedImages.eight:
                break;
            case DisplayedImages.nine:
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
