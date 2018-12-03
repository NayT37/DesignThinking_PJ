using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriviaBtn : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    [HideInInspector]
    public Text internalText;
    //Private Variables
    [SerializeField]
    private bool _isCorrect;
    private Color32 _normalClr, _correctClr, _incorrectClr;
    private Image _internalImg;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        internalText = GetComponentInChildren<Text>();
        _internalImg = GetComponent<Image>();
        _normalClr = _internalImg.color;
        _correctClr = new Color32(0, 255, 0, 255);
        _incorrectClr = new Color32(255, 0, 0, 255);
    }

    public void ChangeToFeedbackColor()
    {
        if (_isCorrect) { _internalImg.color = _correctClr; } else { _internalImg.color = _incorrectClr; }
    }
    public void ReturnToNormalColor() { _internalImg.color = _normalClr; }
    #endregion

    #region GETTERS_AND_SETTERS
    public bool getIsCorrect()
    {
        return _isCorrect;
    }
    #endregion
}
