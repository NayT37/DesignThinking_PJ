using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    #region Variables
    //Custom inspector variables

    //Create an enum to create a dropdown menu
    public enum Orientation { Left, Right };
    //Create an referente to the enum to display the dropdown menu
    public Orientation orientation;
    [Range(0.5f, 6.0f)]
    public float RotateSpeed = 3f;
    [Range(5, 50)]
    public int RotationDistance = 25;


    //Private variables
    //Get the Parent
    private Transform _parentObj;
    //Get the parent radius
    private float _radius;
    //Get an offset to move the object at certain distance
    private float _radiusOffset;
    //Add both radius to get the final radius
    private float _desiredRadius;
    //Get parents center
    private Vector2 _parentCenter;
    private float _angle;
    #endregion

    #region System Methods
    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        Rotate();
    }
    #endregion

    #region Created Methods
    private void InitVariables()
    {
        _parentObj = transform.parent;
        _parentCenter = _parentObj.position;
        _radius = _parentObj.GetComponent<RectTransform>().rect.width / 2;
        _radiusOffset = transform.GetComponent<RectTransform>().rect.width / 2;
        _desiredRadius = _radius + _radiusOffset + RotationDistance;
    }
    private void Rotate()
    {
        if (orientation == Orientation.Right)
        {
            //Adds to go right
            _angle += RotateSpeed * Time.deltaTime;
        }
        else
        {
            //Substracts to go left
            _angle -= RotateSpeed * Time.deltaTime;
        }
        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * _desiredRadius;
        transform.position = _parentCenter + offset;
    }
    #endregion
}
