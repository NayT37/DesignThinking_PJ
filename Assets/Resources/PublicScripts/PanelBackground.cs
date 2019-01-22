using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public class PanelBackground : MonoBehaviour
{
	#region VARIABLES
	//Public Variables
	//Private Variables
	public GameObject canvasInstruction;
	private bool _canClose;
	private GameObject _nextBtn;
	private GameObject _backBtn;
	private GameObject _exitBtn;
	private Text _titleText;
	private Text _internalText;
	private int Round = 0;

	public GameObject Emp;
	public GameObject Def;
	public GameObject Ide;
	public GameObject Pro;
	public GameObject Eva;


	private Color32 _originalColor = new Color32 (255, 255, 255, 255);
	private Color32 _newColor = new Color32 (255, 255, 255, 37);
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
		_exitBtn = transform.Find ("ExitCanvas").gameObject;
		_exitBtn.SetActive (false);
		_backBtn.SetActive(false);
	}
	private void CalculateRound(){
		int contador = 0;
		switch(Round){
		case 1:
			_titleText.GetComponent<Text> ().text = "EMPATIZAR";
			_internalText.text = "Recuerda debemos ser capaces de ponernos en los zapatos de nuestro " +
			"público objetivo para ser capaces de generar soluciones consecuentes con sus realidades.";
			_backBtn.SetActive (false);
			Emp.GetComponent<Image> ().color = _originalColor;
			break;
		case 2: 
			_titleText.GetComponent<Text> ().text = "DEFINIR";
			_internalText.text = "A partir de la información recopilada durante la fase Empatizar, debemos " +
				"quedarnos con lo que realmente aporta valor y nos lleva al alcance de nuevas perspectivas interesantes.";
			_backBtn.SetActive (true);
			Def.GetComponent<Image> ().color = _originalColor;

			break;
		case 3:
			_titleText.GetComponent<Text> ().text = "IDEAR";
			_internalText.text = "A partir de la delimitación de nuestro interés, en esta fase  generaremos un sinfín " +
				"de opciones. No debemos quedarnos con la primera idea que se nos ocurra.";
			Ide.GetComponent<Image> ().color = _originalColor;
			break;
		case 4:
			_titleText.GetComponent<Text> ().text = "PROTOTIPAR";
			_internalText.text = "A partir de la planteado en  la fase de idear, construimos prototipos que nos permitan " +
			"hacer las ideas palpables y  visualizar las posibles soluciones, poniendo de manifiesto elementos que debemos " +
			"mejorar o refinar antes de llegar al resultado final.";
			_nextBtn.SetActive (true);
			Pro.GetComponent<Image> ().color = _originalColor;
			break;
		case 5: 
			_titleText.GetComponent<Text> ().text = "EVALUAR";
			_internalText.text = "En esta fase analizamos el prototipo construido con los usuarios implicados en la solución " +
			"que estemos desarrollando, para identificar mejoras significativas, fallos a resolver, posibles carencias. " +
			"Durante esta fase evolucionaremos nuestra idea hasta convertirla en la solución que estábamos buscando.";
			_nextBtn.SetActive (false);
			Eva.GetComponent<Image> ().color = _originalColor;
			_exitBtn.SetActive (true);
			break;
		}
		if (contador == 1) {
			
		}
	}

	#endregion
	#region METHOD FOR BUTTONS
	public void OnNextBtn(){
		Round += 1;
	}
	public void OnBackBtn(){
		Round -= 1;
	}
	public void closePanel(){
		canvasInstruction.SetActive (false);
	}
	#endregion


}

