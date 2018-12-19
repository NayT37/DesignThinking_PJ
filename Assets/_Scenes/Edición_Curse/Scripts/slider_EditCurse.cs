using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class slider_EditCurse : MonoBehaviour, IPointerClickHandler, IPointerDownHandler {


	public float slider_value;
	private Slider _sliderEditCurse;

	void Start () {
		_sliderEditCurse = GetComponent<Slider> ();
		slider_value = 0;
	}

	void Update () {
		
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		slider_value = _sliderEditCurse.value;
	}

	#endregion

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		slider_value = _sliderEditCurse.value;	
	}

	#endregion
}
