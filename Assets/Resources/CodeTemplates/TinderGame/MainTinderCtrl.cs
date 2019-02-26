using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR;
using Vuforia;
using Image = UnityEngine.UI.Image;

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
        XRSettings.enabled = false;
        VuforiaBehaviour.Instance.enabled = false;

        _momentServices = new MomentServices();

        _finishedCanvasQuantity = 0;
        _finalCanvasTxt = GameObject.Find("FinalCanvas_Txt").GetComponent<Text>();
        _finalCanvasTxt.gameObject.SetActive(false);
        _canvasByRound = new TinderGame[3];

        _canvasByRound[0] = GameObject.Find("Canvas_Tinder").GetComponent<TinderGame>();
        _canvasByRound[1] = GameObject.Find("Canvas_Tinder (1)").GetComponent<TinderGame>();
        _canvasByRound[2] = GameObject.Find("Canvas_Tinder (2)").GetComponent<TinderGame>();

        _roundsNumber = _canvasByRound.Length;
    }

    public void FinishGame()
    {
        _canvasByRound[_finishedCanvasQuantity].SetViewTo(false);
        _finishedCanvasQuantity++;
        UpdateProgressBar();
        if (_finishedCanvasQuantity < completeProgressValue)
        {
            _trueImg = _canvasByRound[_finishedCanvasQuantity].GetTrueImg();
            _falseImg = _canvasByRound[_finishedCanvasQuantity].GetFalseImg();
        }
        else
        {
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
