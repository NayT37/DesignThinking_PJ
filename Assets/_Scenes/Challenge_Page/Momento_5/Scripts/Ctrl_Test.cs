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

    private int _evaluationPosition;

    private bool isContinue;

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

        isContinue = true;

        _answersValue = new int[10]{1,1,1,1,1,1,1,1,1,1};

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
        Debug.Log(name);
        _answersValue[_evaluationPosition-1] = Convert.ToInt32(name);
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
    }

    private void eventClick(string name)
    {

        if (name.Equals("nextBtn"))
        { 
            _evaluationPosition++;

            if (_evaluationPosition<11)
            {
                if (_evaluationPosition==2)
                {
                    isContinue = false;
                    //Hacer lógica para mostrar el botón de back
                } else if (_evaluationPosition<10)
                {
                    isContinue = false;   
                }else {

                    for (int i = 0; i < _answersValue.Length; i++)
                    {
                        Debug.Log(_answersValue[i]);
                    }
                    isContinue = true;
                    //Lógica para desaparecer botón next
                } 
            } else {
                _evaluationPosition=10;
            }

            
        } else{

             _evaluationPosition--;
            
            if (_evaluationPosition>0)
            {
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
		
        yield return new WaitForSeconds(2.0f);	
		SceneManager.LoadScene("M_5B", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("M_5A");
    }
    #endregion
}
