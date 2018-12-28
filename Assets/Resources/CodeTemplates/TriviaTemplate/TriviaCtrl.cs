using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class TriviaCtrl : MiniGame_Ctrl
{
    #region VARIABLES
    //Public Variables
    [HideInInspector]
    public bool isTriviaFinished;
    [Header("Trivia Variables")]
    public List<TriviaTxt> triviaTxtArray;
    //Private Variables
    [SerializeField]
    private int _answersQuantity = 4;
    private Transform[] _btnsPosArray;
    private TriviaBtn[] _btnsArray;
    private Vector3[] _btnStoredPosArray;
    private GameObject _clickBlocker;
    private float _waitAnswerTime;
    private Text _questionTxt, _contextTxt;
    private GameObject _triviaGame, _contextObj;
    private Button _contextBtn, _closeContext;
    private GameObject _panelFinal;

    private MomentServices _momentServices;
    #endregion


    #region SYSTEM_METHODS
    private void Awake()
    {
        PreInitializate();
    }
    private void Start()
    {
        Initializate();
    }
    #endregion


    #region CREATED_METHODS
    private void PreInitializate()
    {
        _clickBlocker = GameObject.Find("ClickBlocker");
        _clickBlocker.SetActive(false);
        _waitAnswerTime = 1.5f;
        isGameFinished = false;

        _questionTxt = GameObject.Find("QuestionText").GetComponent<Text>();
        _contextTxt = GameObject.Find("ContextText").GetComponent<Text>();
        _triviaGame = GameObject.Find("TriviaGame");
        _contextObj = GameObject.Find("ContextObj");
        _contextBtn = GameObject.Find("ContextBtn").GetComponent<Button>();
        _closeContext = GameObject.Find("CloseContext").GetComponent<Button>();
        _contextBtn.onClick.AddListener(CallContext);
        _closeContext.onClick.AddListener(CloseContext);

        _btnsPosArray = new Transform[_answersQuantity];
        _btnStoredPosArray = new Vector3[_answersQuantity];
        _btnsArray = new TriviaBtn[_answersQuantity];
    }

    private void Initializate()
    {
        _momentServices = new MomentServices();

        for (int i = 0; i < _answersQuantity; i++)
        {
            _btnsArray[i] = GameObject.Find("BtnOption_" + (i + 1)).GetComponent<TriviaBtn>();
            _btnsArray[i].GetComponent<Button>().onClick.AddListener(TriviaBtnClick);
            _btnsPosArray[i] = _btnsArray[i].transform;
            _btnStoredPosArray[i] = _btnsPosArray[i].localPosition;
        }

        ShuffleTransformArray(_btnStoredPosArray);
        ReassingBtnPos();

        UpdateDisplayedTxts();
        _triviaGame.SetActive(false);
        _panelFinal = GameObject.Find("Panel_Final");
        _panelFinal.SetActive(false);
    }

    private void ReassingBtnPos()
    {
        for (int i = 0; i < _answersQuantity; i++)
        {
            _btnsPosArray[i].localPosition = _btnStoredPosArray[i];
        }
    }

    private void ShuffleTransformArray(Vector3[] tArray)
    {
        Vector3 temp;
        for (int i = 0; i < tArray.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0, tArray.Length);
            temp = tArray[rnd];
            tArray[rnd] = tArray[i];
            tArray[i] = temp;
            tArray[i] = tArray[i];
        }
    }

    private void ChangeAnswer(bool wasCorrect)
    {
        if (wasCorrect)
        {
            triviaTxtArray.Remove(triviaTxtArray[0]);
        }
        else
        {
            TriviaTxt temp = triviaTxtArray[0];
            triviaTxtArray.Add(temp);
            triviaTxtArray.Remove(triviaTxtArray[0]);
        }
    }

    private void UpdateDisplayedTxts()
    {
        if (triviaTxtArray.Count > 0)
        {
            _questionTxt.text = triviaTxtArray[0].questionTxt;
            _contextTxt.text = triviaTxtArray[0].contextTxt;
            for (int i = 0; i < _btnsArray.Length; i++)
            {
                _btnsArray[i].internalText.text = triviaTxtArray[0].answersArray[i];
            }
        }
        else
        {
            _questionTxt.text = "";
            _contextTxt.text = "";
            for (int i = 0; i < _btnsArray.Length; i++)
            {
                _btnsArray[i].internalText.text = "";
            }
            _panelFinal.SetActive(true);
            isGameFinished = true;

            _momentServices.UpdateMoment(100);
        }
    }

    public void TriviaBtnClick()
    {
        TriviaBtn btnName = EventSystem.current.currentSelectedGameObject.GetComponent<TriviaBtn>();
        ShowFeedback();
        if (btnName.getIsCorrect())
        {
            ChangeAnswer(true);
            StartCoroutine(CorrectAnswerWaitTime());
        }
        else
        {
            ChangeAnswer(false);
            StartCoroutine(IncorrectAnswerWaitTime());
        }
    }

    private void ShowFeedback()
    {
        for (int i = 0; i < _btnsArray.Length; i++)
        {
            _btnsArray[i].ChangeToFeedbackColor();
        }
    }

    private void StopFeedback()
    {
        for (int i = 0; i < _btnsArray.Length; i++)
        {
            _btnsArray[i].ReturnToNormalColor();
        }
    }

    public void CallContext()
    {
        _triviaGame.SetActive(false);
        _contextObj.SetActive(true);
    }

    public void CloseContext()
    {
        _triviaGame.SetActive(true);
        _contextObj.SetActive(false);
    }
    #endregion


    #region COROUTINES
    private IEnumerator CorrectAnswerWaitTime()
    {
        _clickBlocker.SetActive(true);
        yield return new WaitForSeconds(_waitAnswerTime);
        ShuffleTransformArray(_btnStoredPosArray);
        ReassingBtnPos();
        _clickBlocker.SetActive(false);
        StopFeedback();
        UpdateDisplayedTxts();
        UpdateProgressBar();
        CallContext();
    }

    private IEnumerator IncorrectAnswerWaitTime()
    {
        _clickBlocker.SetActive(true);
        yield return new WaitForSeconds(_waitAnswerTime);
        ShuffleTransformArray(_btnStoredPosArray);
        ReassingBtnPos();
        _clickBlocker.SetActive(false);
        StopFeedback();
        UpdateDisplayedTxts();
        CallContext();
    }
    #endregion
}

[System.Serializable]
public class TriviaTxt : System.Object
{
    public string contextTxt;
    public string questionTxt;
    public string[] answersArray;
}