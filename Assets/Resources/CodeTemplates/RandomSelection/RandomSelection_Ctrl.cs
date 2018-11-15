using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class RandomSelection_Ctrl : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    public int maxStudentsQuantity = 10;
    public int minStudentsQuantity = 2;
    //Private Variables
    [SerializeField]
    private float _circleRadius = 2.0f;
    private GameObject _inputPopUp;
    private InputField _inputText;
    private int _studentsQuantity;
    private Transform _arrowObj;
    private Button _validateBtn;
    private List<Vector3> _circlePositionList;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _inputPopUp = GameObject.Find("PopUp_Panel");
        _inputText = GameObject.Find("PopUp_Input").GetComponent<InputField>();
        _inputText.Select();
        _validateBtn = GameObject.Find("Confirm_Btn").GetComponent<Button>();
        _validateBtn.onClick.AddListener(ValidateQuantity);
        _circlePositionList = new List<Vector3>();
    }

    public void ValidateQuantity()
    {
        try
        {
            //Try to parse the text
            _studentsQuantity = int.Parse(_inputText.text);
            if (_studentsQuantity <= maxStudentsQuantity && _studentsQuantity >= minStudentsQuantity)
            {
                OrderReferencePoints();
                _inputPopUp.SetActive(false);
            }
            else if (_studentsQuantity >= maxStudentsQuantity)
            {
                print("Please use a smaller number");
                _inputText.text = "";
                WrongFeedback();
                _inputText.Select();
            }
            else if (_studentsQuantity < minStudentsQuantity)
            {
                print("Please use a higher number");
                _inputText.text = "";
                WrongFeedback();
                _inputText.Select();
            }
        }
        catch (FormatException e)
        {
            //If is not a number...
            print("Please use a number");
            WrongFeedback();
            _inputText.Select();
        }
    }

    public void SpinArrow() { }

    private void OrderReferencePoints()
    {
        //   float temp = (float)360 / _studentsQuantity;
        // float floatToAdd;

        float floatToAdd;
        Vector3 centrePos = new Vector3(0, 0, 0);
        for (int i = 0; i < _studentsQuantity; i++)
        {
            GameObject obj = new GameObject();
            Image img = obj.AddComponent<Image>();
            Transform trnsfm = obj.transform;
            Transform parent = GameObject.Find("Counter_Holder").GetComponent<Transform>();
            trnsfm.SetParent(parent);
            //img.sprite = 
            float temp = i * 1.0f / _studentsQuantity;
            //   floatToAdd = temp * (i + 1);


            // "i" now represents the progress around the circle from 0-1
            // we multiply by 1.0 to ensure we get a fraction as a result.
            //var i = (pointNum * 1.0) / numPoints;
            // get the angle for this step (in radians, not degrees)
            var angle = temp * Mathf.PI * 2;
            // print(angle);
            // the X &amp; Y position for this angle are calculated using Sin &amp; Cos
            var x = Mathf.Sin(angle) * _circleRadius;
            var y = Mathf.Cos(angle) * _circleRadius;
            var pos = new Vector3(x, y, 0) + centrePos;
            // no need to assign the instance to a variable unless you're using it afterwards:
            //Instantiate(beadPrefab, pos, Quaternion.identity);
            //print(pos);
            _circlePositionList.Add(pos);
            trnsfm.localPosition = _circlePositionList[i];
            print(_circlePositionList[i]);
        }

        /* 
        for (var pointNum = 0; pointNum < numPoints; pointNum++)
{


 // "i" now represents the progress around the circle from 0-1
 // we multiply by 1.0 to ensure we get a fraction as a result.
 var i = (pointNum * 1.0) / numPoints;
 // get the angle for this step (in radians, not degrees)
 var angle = i * Mathf.PI * 2;
 // the X &amp; Y position for this angle are calculated using Sin &amp; Cos
 var x = Mathf.Sin(angle) * radiusX;
 var y = Mathf.Cos(angle) * radiusY;
 var pos = Vector3(x, y, 0) + centrePos;
 // no need to assign the instance to a variable unless you're using it afterwards:
 Instantiate (beadPrefab, pos, Quaternion.identity);

}   
 */
    }

    private void WrongFeedback() { print("Show a feedback"); }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
