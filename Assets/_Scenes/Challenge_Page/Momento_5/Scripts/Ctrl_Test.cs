using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_Test : MonoBehaviour {

	public Button _btnnextquestion;
	public Button _btnbackquestion;

    public Button _btnclose;

    public Button _btnYes;

    public Button _btnNo;

    public Button[] _arrayAnswers;
    
    public Text _questionText;

    public Text _questionNumber;

    private int[] _answersValue;

    private int[] _answersposition;

    private int _evaluationPosition;

    private bool isContinue;

    public Image[] _arrayShadowsAnswer;

    private int _lastShadowActivated;

    private string[] _arrayDescriptions = new string[]{"Factor innovador de producto/servicio en el mercado actual. (Diferenciador respecto de la competencia).",
													  "Nivel de respuesta a las necesidades, costumbres y hábitos de los potenciales clientes y/o beneficiarios.", 
													  "En que medida el producto/servicio soluciona un problema.", 
													  "Nivel de probabilidad de que existan o pueden aparecer productos /servicios que sustituyan a mi idea.", 
													  "Nivel de dificultad para poner en marcha el desarrollo de la idea (Producto/servicio).", 
													  "Nivel de competencia existente hace que sea complicado el desarrollo del producto/servicio.", 
													  "Necesidad de Financiación externa.", 
													  "Estimación de que suba la demanda y el interés por el producto/servicio prototipado en el tiempo.", 
													  "Nivel de disponibilidad de recursos (humano, técnicos, Financieros) para el desarrollo de producto/servicio.", 
													  "Probabilidad de desarrollar el producto/servicio, en corto tiempo."};

	// Use this for initialization
	void Start () {

        _lastShadowActivated = 1;

        //_arrayShadowsAnswer[_lastShadowActivated-1].color = new Color32(255,255,255,255);
        
        isContinue = true;

        _answersValue = new int[10]{1,1,1,1,1,1,1,1,1,1};

        _answersposition = new int[10]{1,1,1,1,1,1,1,1,1,1};

        _evaluationPosition = 1;

        _questionText.text = _arrayDescriptions[0];
        _questionNumber.text = "Pregunta " + _evaluationPosition;
		
		InitializeButtons();


	}

    void InitializeButtons(){

        _btnnextquestion.onClick.AddListener(delegate{ eventClick(_btnnextquestion.name);});
		_btnbackquestion.onClick.AddListener(delegate{ eventClick(_btnbackquestion.name);});
        _btnclose.onClick.AddListener(delegate{ closeClick(_btnclose.name);});
        _btnYes.onClick.AddListener(delegate{ closeClick(_btnYes.name);});
        _btnNo.onClick.AddListener(delegate{ closeClick(_btnNo.name);});
        _arrayAnswers[0].onClick.AddListener(delegate{ answerClick(_arrayAnswers[0].name);});
        _arrayAnswers[1].onClick.AddListener(delegate{ answerClick(_arrayAnswers[1].name);});
        _arrayAnswers[2].onClick.AddListener(delegate{ answerClick(_arrayAnswers[2].name);});
        _arrayAnswers[3].onClick.AddListener(delegate{ answerClick(_arrayAnswers[3].name);});
        _arrayAnswers[4].onClick.AddListener(delegate{ answerClick(_arrayAnswers[4].name);});

        _btnYes.interactable = false;
        _btnNo.interactable = false;
        
    }

    private void answerClick(string name)
    {

        if (name.Equals(_lastShadowActivated.ToString()))
        {
            
        } else {
            _arrayShadowsAnswer[_lastShadowActivated-1].color = new Color32(255,255,255,0);
            _lastShadowActivated = Convert.ToInt32(name);
            _arrayShadowsAnswer[_lastShadowActivated-1].color = new Color32(255,255,255,255);
            _answersValue[_evaluationPosition-1] = Convert.ToInt32(name);
            _answersposition[_evaluationPosition-1] = _lastShadowActivated;
        }
        
        
    }

    private void closeClick(string name)
    {
        if (name.Equals("yesBtn"))
        {
          
        } else if (name.Equals("noBtn"))
        {
            
        } else{
            Debug.Log("Button close pressed");
        }
        
        DOTween.Play("bg_transition_err");
        StartCoroutine(ChangeScene());
    }

    private void eventClick(string name)
    {

        if (name.Equals("nextBtn"))
        { 
           
            if (_evaluationPosition<10)
            {
                
                if (_evaluationPosition==1)
                {
                    isContinue = false;
                    //Hacer lógica para mostrar el botón de back
                } else if (_evaluationPosition<10)
                {
                    isContinue = false;   
                }else {

                   
                } 
                
                _evaluationPosition++;

                _arrayShadowsAnswer[_lastShadowActivated-1].color = new Color32(255,255,255,0);
                _arrayShadowsAnswer[_answersposition[_evaluationPosition-1]-1].color = new Color32(255,255,255,255);
                _lastShadowActivated = _answersposition[_evaluationPosition-1];

            } else {
                _evaluationPosition=10;
                Ctrl_Moment5.Ctrl._answersValue = _answersValue;
                int result = Ctrl_Moment5.Ctrl.setAnswersValue();

                if (result==10){
                    DOTween.Play("bg_transition_suc");
                    StartCoroutine(ChangeScene());
                }else{
                    Debug.Log("Error al setear los datos");
                }
            }

            
        } else{

             _evaluationPosition--;
            
            if (_evaluationPosition>0)
            {

                _arrayShadowsAnswer[_lastShadowActivated-1].color = new Color32(255,255,255,0);
                _arrayShadowsAnswer[_answersposition[_evaluationPosition-1]-1].color = new Color32(255,255,255,255);
                _lastShadowActivated = _answersposition[_evaluationPosition-1];

                
                if (_evaluationPosition==1)
                {
                    isContinue = true;
                    //Hacer lógica para desaparecer el botón de back
                } else if (_evaluationPosition > 1 && _evaluationPosition < 9)
                {
                    isContinue = false;
                } else {
                    isContinue = false;
                    //Lógica para aparecer el botón next
                }  
            } else {
                _evaluationPosition = 1;
            }

           

        }

            _questionNumber.text = "Pregunta " + _evaluationPosition;
            _questionText.text = _arrayDescriptions[_evaluationPosition-1]; 
        
     
        // DOTween.Play(name);
		// DOTween.Play("bg_transition");
		// StartCoroutine(ChangeScene());

    }

    // Update is called once per frame
    void Update () {
		
	}

	#region COROUTINES
    private IEnumerator ChangeScene()
    {
		DOTween.Play("bg_transition");
        yield return new WaitForSeconds(3.0f);	
		SceneManager.LoadScene("M_5A", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("M_5B");
    }
    #endregion
}
