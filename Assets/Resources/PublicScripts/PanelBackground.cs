using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class PanelBackground : MonoBehaviour, IPointerClickHandler
{
	#region VARIABLES
	//Public Variables
	//Private Variables
	private bool _canClose;
	private GameObject _nextBtn;
	private GameObject _backBtn;
	#endregion


	#region SYSTEM_METHODS
	private void Start() { Initializate(); }
	private void Update() { }
	#endregion


	#region CREATED_METHODS
	private void Initializate()
	{
		_canClose = false;
		_nextBtn = transform.Find("NextBtn").gameObject;
		_backBtn = transform.Find("BackBtn").gameObject;
		_backBtn.SetActive(false);
	}

	private void ClosePanel()
	{
		if (_canClose)
			this.gameObject.SetActive(false);
	}
	#endregion


	#region INTERFACE_METHODS
	public void OnPointerClick(PointerEventData eventData)
	{
		ClosePanel();
	}
	#endregion


	#region GETTERS_AND_SETTERS
	#endregion


//	#region COROUTINES
//	private IEnumerator WaitUntilLoad()
//	{
//		yield return new WaitForSeconds(2);
//		_canClose = true;
//		_closeBtn.SetActive(true);
//	}
//
//	#endregion

	#region METHOD NEXT BUTTON
	public void OnNextBtn(){
		_backBtn.SetActive (true);
	}
	#endregion
}

