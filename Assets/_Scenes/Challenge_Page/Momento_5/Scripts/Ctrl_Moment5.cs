using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ctrl_Moment5 : MonoBehaviour
{

    public static Ctrl_Moment5 Ctrl;


    [Header("array answer values")]
    public int[] _answersValue;

    [Header("array question texts")]

    public string[] _arrayquestions;

    [Header("evaluation Category")]

    public string _evaluationCategory;

    private EvaluationServices _evaluationServices;

    private QuestionServices _questionServices;

    private AnswerServices _answerServices;

    private int evaluationid;

    IEnumerable<Question> questions;

    IEnumerable<Answer> answers;

    private int[] answersarray;
    void Awake()
    {
        if (Ctrl == null)
        {
            Ctrl = this;
        }
        else if (Ctrl != null)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {

        _answersValue = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        _evaluationServices = new EvaluationServices();

        _questionServices = new QuestionServices();

        _answerServices = new AnswerServices();

        answersarray = new int[50];

    }

    public Evaluation createEvaluation()
    {

        var evaluation = _evaluationServices.CreateEvaluation(_evaluationCategory);

        return evaluation;
    }

    public int setAnswersValue(bool isUpdate)
    {

        int result = 0;

        int counter = 0;

        int counterArray = 0;

        questions = _questionServices.GetQuestions(DataBaseParametersCtrl.Ctrl._evaluationLoaded.id);

        foreach (var q in questions)
        {
            answers = _answerServices.GetAnswers(q.id);
            Answer[] arrayanswers = new Answer[5];
            int count = 0;
            foreach (var a in answers)
            {
                answersarray[counterArray] = a.counter;
                result += a.counter;
                arrayanswers[count] = a;
                count++;
                counterArray++;
            }

            if (isUpdate)
            {
                result += _answerServices.UpdateAnswer(arrayanswers[_answersValue[counter] - 1]);
                print(arrayanswers[_answersValue[counter] - 1]);
                counter++;
            }
        }

        return result;
    }

    public int getAnswersValue()
    {

        _answersValue = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        int result = setAnswersValue(false);

        Debug.Log(result);

        int questionsQuantity = 0;

        for (int i = 0; i < 5; i++)
        {
            questionsQuantity += answersarray[i];
        }

        if (result != 0)
        {

            int counterArrayIn = 0;
            int counterArrayOut = 0;
            for (int i = 0; i < answersarray.Length; i++)
            {
                _answersValue[counterArrayIn] += ((answersarray[i] * (counterArrayOut + 1)));

                counterArrayOut++;
                if ((i + 1) % 5 == 0)
                {
                    counterArrayIn++;
                    counterArrayOut = 0;
                }
            }
        }

        return questionsQuantity;

    }

    // Update is called once per frame
    void Update()
    {

    }
}