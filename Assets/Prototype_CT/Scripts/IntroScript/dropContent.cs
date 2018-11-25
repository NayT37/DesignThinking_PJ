using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropContent : DropBehavior {

	public GameObject panel;

	// Use this for initialization
	void Start () {
		panel.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}

	public override void OnDrop (UnityEngine.EventSystems.PointerEventData eventData) {
		if (!internalItem) {
			panel.SetActive (true);
		}
	}
}