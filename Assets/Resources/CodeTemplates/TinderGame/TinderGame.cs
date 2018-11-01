using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinderGame : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    public int imagesQuantity;
    //Private Variables
    public List<TinderImage> _tinderImagesList;
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
        _tinderImagesList = new List<TinderImage>();
        for (int i = 0; i < imagesQuantity; i++)
        {
            TinderImage item = new TinderImage();
            //item.internalImage.sprite = 
            //item.isCorrect =
            _tinderImagesList.Add(item);
        }
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
            _tinderImagesList.Insert(0, temp);
        }
    }
    #endregion
    #region COROUTINES
    #endregion
}
