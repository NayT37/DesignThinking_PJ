using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItem : DropBehavior {

	public GameObject canvas;
	private CtrlEmpatizar _ctrl;
	// Use this for initialization
	void Start () {
		_ctrl = canvas.GetComponent<CtrlEmpatizar>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnDrop(UnityEngine.EventSystems.PointerEventData eventData){
		_ctrl.ActivateNextPanel();
	}
}
