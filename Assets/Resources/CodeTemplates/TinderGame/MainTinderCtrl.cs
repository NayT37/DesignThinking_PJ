using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainTinderCtrl : MiniGame_Ctrl
{

    #region VARIABLES
    //Public Variables
    //Private Variables
    [SerializeField]
    private Canvas[] _canvasByRound;
    private int _roundsNumber;
    private int _finishedCanvasQuantity;
    private Text _finalCanvasTxt;
    private Image _trueImg, _falseImg;
    private Transform _trueImgSize, _falseImgSize;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _roundsNumber = _canvasByRound.Length;
        _finishedCanvasQuantity = 0;
        _finalCanvasTxt = GameObject.Find("FinalCanvas_Txt").GetComponent<Text>();
        _finalCanvasTxt.gameObject.SetActive(false);
        foreach (Canvas canvas in _canvasByRound)
        {
            canvas.gameObject.SetActive(false);
        }
        _canvasByRound[0].gameObject.SetActive(true);
    }

    public void FinishGame()
    {
        _canvasByRound[_finishedCanvasQuantity].gameObject.SetActive(false);
        _finishedCanvasQuantity++;
        UpdateProgressBar();
        if (_finishedCanvasQuantity < completeProgressValue)
        {
            _canvasByRound[_finishedCanvasQuantity].gameObject.SetActive(true);
            TinderGame temp;
            temp = _canvasByRound[_finishedCanvasQuantity].GetComponent<TinderGame>();
            _trueImg = temp.GetTrueImg();
            _falseImg = temp.GetFalseImg();
        }
        else
        {
            isGameFinished = true;
            _finalCanvasTxt.gameObject.SetActive(true);
        }
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
