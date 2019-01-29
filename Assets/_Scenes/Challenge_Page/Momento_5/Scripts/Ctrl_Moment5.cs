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

	private int evaluationid;

	IEnumerable<Question> questions;

	IEnumerable<Answer> answers;
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

	public int setAnswersValue(bool isUpdate){
		
		int result = 0;

		int counter = 0;

		questions = _questionServices.GetQuestions(DataBaseParametersCtrl.Ctrl._evaluationLoaded.id);

		foreach (var q in questions)
		{
			answers = _answerServices.GetAnswers(q.id);
			Answer[] arrayanswers = new Answer[5];
			int count = 0;
			foreach (var a in answers)
			{
				arrayanswers[count] = a;
				count++;
			}

			if (isUpdate)
			{
				result += _answerServices.UpdateAnswer(arrayanswers[_answersValue[counter]-1]);	
				print(arrayanswers[_answersValue[counter]-1]);
				counter++;
			}
		}

		return result;
	}

	public void getAnswersValue(){

		setAnswersValue(false);

		int[] answersarray = new int[50];

		int result = 0;

		foreach (var a in answers)
		{
			result+= a.counter;
		}

		int counterQuestions = (result%10);

		int counterArrayIn = 0;
		int counterArrayOut = 0;
		for (int i = 0; i < answersarray.Length; i++)
		{
			_answersValue[counterArrayIn] += ((answersarray[i]*(counterArrayOut+1))*counterQuestions);
			counterArrayOut++;
			if ((i+1)%5==0)
			{
				print(_answersValue[counterArrayIn]);
				counterArrayIn++;
				counterArrayOut = 0;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}