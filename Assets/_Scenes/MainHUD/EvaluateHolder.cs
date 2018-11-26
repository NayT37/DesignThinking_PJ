using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateHolder : MonoBehaviour
{

    Animator evaluateHolder;
    // Use this for initialization
    void Start()
    {
        evaluateHolder = GetComponent<Animator>();
        /*         evaluateHolder.SetBool("isAirplane", true);
                evaluateHolder.SetBool("isCorrect", true); */
        print(evaluateHolder.GetBool("isAirplane"));
    }

    public void playAnimation(int entero)
    {
        if (evaluateHolder == null)
        {
            evaluateHolder = GameObject.Find("EvaluateHolder").GetComponent<Animator>();
        }

        switch (entero)
        {
            case 1:
                evaluateHolder.SetBool("isAirplane", true);
                evaluateHolder.SetBool("isPc", false);
                evaluateHolder.SetBool("isPhone", false);
                break;
            case 2:
                evaluateHolder.SetBool("isAirplane", false);
                evaluateHolder.SetBool("isPc", false);
                evaluateHolder.SetBool("isPhone", true);
                break;
            case 3:
                evaluateHolder.SetBool("isAirplane", false);
                evaluateHolder.SetBool("isPc", true);
                evaluateHolder.SetBool("isPhone", false);
                break;
        }
    }
}
