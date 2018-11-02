using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TinderGame : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    public List<Sprite> correctImgs, incorrectImgs;
    public int imagesSize;

    //Private Variables
    public List<TinderImage> _tinderImagesList;

    private int _imagesQuantity;
    private Transform _parentObject;

    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ValidateAnswer(false);
        }
    }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        //  _tinderImagesList = new List<TinderImage>();
        _imagesQuantity = correctImgs.Count + incorrectImgs.Count;



        GameObject NewObj = new GameObject(); //Create the GameObject
        Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
        NewObj.AddComponent<TinderImage>(); //Add CaptchaImage Component script
        var dimension = NewObj.GetComponent<RectTransform>();
        dimension.SetParent(_parentObject);
        dimension.sizeDelta = new Vector2(imagesSize, imagesSize);
        NewObj.transform.localScale = new Vector3(1, 1, 1);
        NewObj.transform.localPosition = new Vector3(0, 0, 0);


        /*         for (int i = 0; i < _imagesQuantity; i++)
                {
                    TinderImage item = new TinderImage();
                    //item.internalImage.sprite = 
                    //item.isCorrect =
                    _tinderImagesList.Add(item);
                } */
    }

    public void UpdateDisplayedImage()
    {

    }

    private void ValidateAnswer(bool some)
    {
        if (some)
        { //Correct case
            _tinderImagesList.RemoveAt(0);
        }
        else
        {
            TinderImage temp = _tinderImagesList[0];
            _tinderImagesList.RemoveAt(0);
            _tinderImagesList.Insert(_tinderImagesList.Count, temp);
        }
    }
    #endregion


    #region COROUTINES
    #endregion
}
