using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainTinderCtrl : MiniGame_Ctrl
{

    #region VARIABLES
    //Public Variables
    //Private Variables
    private TinderGame[] _canvasByRound;
    private int _roundsNumber;
    private int _finishedCanvasQuantity;
    private Text _finalCanvasTxt;
    private Image _trueImg, _falseImg;
    private Transform _trueImgSize, _falseImgSize;

    private MomentServices _momentServices;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _momentServices = new MomentServices();

        _finishedCanvasQuantity = 0;
        _finalCanvasTxt = GameObject.Find("FinalCanvas_Txt").GetComponent<Text>();
        _finalCanvasTxt.gameObject.SetActive(false);
        _canvasByRound = new TinderGame[3];

        _canvasByRound[0] = GameObject.Find("Canvas_Tinder").GetComponent<TinderGame>();
        _canvasByRound[1] = GameObject.Find("Canvas_Tinder (1)").GetComponent<TinderGame>();
        _canvasByRound[2] = GameObject.Find("Canvas_Tinder (2)").GetComponent<TinderGame>();
        for (int i = 0; i < _canvasByRound.Length; i++)
        {
            print("Canvas is " + _canvasByRound[i]);
            _canvasByRound[i].SetViewTo(false);
        }
        _roundsNumber = _canvasByRound.Length;
        _canvasByRound[0].SetViewTo(false);
    }

    public void FinishGame()
    {
        _canvasByRound[_finishedCanvasQuantity].SetViewTo(false);
        _finishedCanvasQuantity++;
        UpdateProgressBar();
        if (_finishedCanvasQuantity < completeProgressValue)
        {
            _canvasByRound[_finishedCanvasQuantity].SetViewTo(true);
            TinderGame temp;
            temp = _canvasByRound[_finishedCanvasQuantity];
            _trueImg = temp.GetTrueImg();
            _falseImg = temp.GetFalseImg();
            print("Tinder: Working in " + _finishedCanvasQuantity);
        }
        else
        {
            print("Tinder: Its all over");
            isGameFinished = true;
            _finalCanvasTxt.gameObject.SetActive(true);
            _momentServices.UpdateMoment(100);
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
