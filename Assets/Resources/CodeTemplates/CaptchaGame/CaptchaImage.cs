using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

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
    //Private Variables
    private CaptchaGame _captchaGame;
    private Image _internalImage;
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
        _internalImage = GetComponent<Image>();
        if (isCorrect) { _internalImage.color = Color.green; } else { _internalImage.color = Color.red; }
        originalPosition = transform.localPosition;
    }

    public void ValidateAnswer()
    {
        //Check Click  //If Ok  //if False
        if (isCorrect)
        {
            print("It's ok, dissapear me");
        }
        else
        {
            print("It's not ok, random all");
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
