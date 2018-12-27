using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ctrl_Moment5 : MonoBehaviour {

	public static Ctrl_Moment5 Ctrl;
	
	
	[Header ("array answer values")]
	public int[] _answersValue;

	[Header ("array question texts")]

	public string[] _arrayquestions;

	[Header ("evaluation Category")]

	public string _evaluationCategory;

	private EvaluationServices _evaluationServices;

	private QuestionServices _questionServices;

	private AnswerServices _answerServices;
    void Awake () {
        if (Ctrl == null) {
            Ctrl = this;
        } else if (Ctrl != null)
            Destroy (gameObject);
    }

	// Use this for initialization
	void Start () {

		_answersValue = new int[10]{0,0,0,0,0,0,0,0,0,0};

		_evaluationServices = new EvaluationServices();

		_questionServices = new QuestionServices();

		_answerServices = new AnswerServices();
		

	}

	public Evaluation createEvaluation(){

		var evaluation = _evaluationServices.CreateEvaluation(_evaluationCategory);

		return evaluation;
	}

	public int setAnswersValue(){
		
		int result = 0;

		int evaluationid = DataBaseParametersCtrl.Ctrl._evaluationLoaded.id;

		var questions = _questionServices.GetQuestions(evaluationid);

		int counter = 0;
		foreach (var q in questions)
		{
			var answers = _answerServices.GetAnswers(q.id);
			Answer[] arrayanswers = new Answer[5];
			int count = 0;
			foreach (var a in answers)
			{
				arrayanswers[count] = a;
				count++;
			}

			result += _answerServices.UpdateAnswer(arrayanswers[_answersValue[counter]-1]);
			counter++;
		}

		return result;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}