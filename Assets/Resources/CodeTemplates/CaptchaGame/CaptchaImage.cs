using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

public class CaptchaImage : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES
    //Public Variables
    [HideInInspector]
    public Sprite imgSprite;
    [HideInInspector]

    public bool isCorrect;
    [HideInInspector]

    public Vector3 originalPosition;
    [HideInInspector]

    public Image internalImage;
    [HideInInspector]

    public int indexNumber;

    //Private Variables
    private CaptchaGame _captchaGame;
    private Sprite _defaultImage;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        ValidateAnswer();
    }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _captchaGame = GameObject.FindObjectOfType<CaptchaGame>();
        if (internalImage == null)
        {
            internalImage = GetComponent<Image>();
        }
        originalPosition = transform.localPosition;
        _defaultImage = Resources.Load<Sprite>("CodeTemplates/CaptchaGame/defaultCaptcha");
    }

    public void ValidateAnswer()
    {
        //Check Click
        if (isCorrect)
        {
            try
            {
                _captchaGame.CorrectAnswerBhvr(indexNumber);
            }
            catch (Exception e)
            {
                internalImage.sprite = _defaultImage;
            }
        }
        else
        {
            _captchaGame.IncorrectAnswerBhvr();
        }
    }

    //May be not used
    public void ReassignPosition(Vector3 newPosition)
    {
        transform.localPosition = newPosition;
    }
    #endregion


    #region COROUTINES
    #endregion
}
