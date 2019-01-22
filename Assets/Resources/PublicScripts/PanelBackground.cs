using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public class PanelBackground : MonoBehaviour, IPointerClickHandler
{
	#region VARIABLES
	//Public Variables
	//Private Variables
	private bool _canClose;
	private GameObject _nextBtn;
	private GameObject _backBtn;
	private Text _titleText;
	private Text _internalText;
	private int Round = 0;
	#endregion


	#region SYSTEM_METHODS
	private void Start() { Initializate(); }
	private void Update() { CalculateRound (); }
	#endregion


	#region CREATED_METHODS
	private void Initializate()
	{
		Round += 1;
		_titleText = GameObject.Find ("TitleInternal").GetComponent<Text>();
		_internalText = GameObject.Find ("InternalTxtB").GetComponent<Text> ();
		_canClose = false;
		_nextBtn = transform.Find("NextBtn").gameObject;
		_backBtn = transform.Find("BackBtn").gameObject;
		_backBtn.SetActive(false);
	}
	private void CalculateRound(){
		switch(Round){
		case 1:
			_titleText.GetComponent<Text> ().text = "EMPATIZAR";
			_internalText.text = "Recuerda debemos ser capaces de ponernos en los zapatos de nuestro " +
			"público objetivo para ser capaces de generar soluciones consecuentes con sus realidades.";
			_backBtn.SetActive (false);
			break;
		case 2: 
			_titleText.GetComponent<Text> ().text = "DEFINIR";
			_internalText.text = "A partir de la información recopilada durante la fase Empatizar, debemos " +
				"quedarnos con lo que realmente aporta valor y nos lleva al alcance de nuevas perspectivas interesantes.";
			_backBtn.SetActive (true);
			break;
		case 3:
			_titleText.GetComponent<Text> ().text = "IDEAR";
			_internalText.text = "A partir de la delimitación de nuestro interés, en esta fase  generaremos un sinfín " +
				"de opciones. No debemos quedarnos con la primera idea que se nos ocurra.";
			break;
		case 4:
			_titleText.GetComponent<Text> ().text = "PROTOTIPAR";
			_internalText.text = "A partir de la planteado en  la fase de idear, construimos prototipos que nos permitan " +
			"hacer las ideas palpables y  visualizar las posibles soluciones, poniendo de manifiesto elementos que debemos " +
			"mejorar o refinar antes de llegar al resultado final.";
			_nextBtn.SetActive (true);
			break;
		case 5: 
			_titleText.GetComponent<Text> ().text = "EVALUAR";
			_internalText.text = "En esta fase analizamos el prototipo construido con los usuarios implicados en la solución " +
			"que estemos desarrollando, para identificar mejoras significativas, fallos a resolver, posibles carencias. " +
			"Durante esta fase evolucionaremos nuestra idea hasta convertirla en la solución que estábamos buscando.";
			_nextBtn.SetActive (false);
			break;
		}
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



	#region METHOD FOR BUTTONS
	public void OnNextBtn(){
		Round += 1;
	}
	public void OnBackBtn(){
		Round -= 1;
	}
	#endregion
}

