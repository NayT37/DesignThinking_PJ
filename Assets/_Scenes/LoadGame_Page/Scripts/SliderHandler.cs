using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler {


	public float sliderValue;
	private Slider _slider;

	void Start () {
		_slider = GetComponent<Slider> ();
		sliderValue = 0;
	}


	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		sliderValue = _slider.value;
		Debug.Log ("slider press");
	}

	#endregion

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		sliderValue = _slider.value;
		Debug.Log ("slider down");
	}

	#endregion
}
