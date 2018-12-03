using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateHolder : MonoBehaviour
{

    private Animator _animator;
    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void playAnimation(int caseNumber)
    {
        if (_animator == null)
        {
            _animator = GameObject.Find("EvaluateHolder").GetComponent<Animator>();
        }
        _animator.SetBool("isDefault", false);
        switch (caseNumber)
        {
            case 1:
                _animator.SetBool("isAirplane", true);
                _animator.SetBool("isPc", false);
                _animator.SetBool("isPhone", false);
                break;
            case 2:
                _animator.SetBool("isAirplane", false);
                _animator.SetBool("isPc", false);
                _animator.SetBool("isPhone", true);
                break;
            case 3:
                _animator.SetBool("isAirplane", false);
                _animator.SetBool("isPc", true);
                _animator.SetBool("isPhone", false);
                break;
        }
    }

    public void SetCorrect(bool value)
    {
        _animator.SetBool("isCorrect", value);
    }

    public void ReturnToDefault()
    {
        _animator.SetBool("isDefault", true);
    }
}
