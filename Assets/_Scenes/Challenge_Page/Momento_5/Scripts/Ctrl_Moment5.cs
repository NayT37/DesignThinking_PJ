using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ctrl_Moment5 : MonoBehaviour {

	public static Ctrl_Moment5 Ctrl;
	
	
	[Header ("array answer values")]
	public int[] _answersValue;
    void Awake () {
        if (Ctrl == null) {
            Ctrl = this;
        } else if (Ctrl != null)
            Destroy (gameObject);
    }

	// Use this for initialization
	void Start () {

		_answersValue = new int[10]{0,0,0,0,0,0,0,0,0,0};
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}