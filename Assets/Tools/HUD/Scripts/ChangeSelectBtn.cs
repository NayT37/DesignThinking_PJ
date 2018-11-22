using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class ChangeSelectBtn : MonoBehaviour {

	//Variables dentro del HUD
	private Image Empatizar_btn;
	private Image Definir_btn;
	private Image Idear_btn;
	private Image Prototipar_btn;
	private Image Probar_btn;


	//Variables correspondientes al cambio de escenas
	[Header("EMPATIZAR")]
	public Sprite EmpatizarPointer;
	public Sprite EmpatizarSelected;
	[Header("DEFINIR")]
	public Sprite DefinirNull;
	public Sprite DefinirPointer;
	public Sprite DefinirSelected;
	[Header("IDEAR")]
	public Sprite IdearNull;
	public Sprite IdearPointer;
	public Sprite IdearSelected;
	[Header("PROTOTIPAR")]
	public Sprite PrototiparPointer;
	public Sprite PrototiparSelected;
	[Header("PROBAR")]
	public Sprite ProbarNull;
	public Sprite ProbarSelected;



	void Start () {
		//start animation
		DOTween.Play ("5");
	}

	public void SelectEmpatizar(){
		Empatizar_btn = GameObject.Find ("Empatizar").GetComponent<Image>();
		Empatizar_btn.sprite = EmpatizarSelected;
		Definir_btn = GameObject.Find ("Definir").GetComponent<Image> ();
		Definir_btn.sprite = DefinirNull;
	}


	public void SelectDefinir(){
		Definir_btn = GameObject.Find ("Definir").GetComponent<Image> ();
		Definir_btn.sprite = DefinirSelected;
		Empatizar_btn = GameObject.Find ("Empatizar").GetComponent<Image>();
		Empatizar_btn.sprite = EmpatizarPointer;
	}


	public void SelectIdear(){
		Idear_btn = GameObject.Find ("Idear").GetComponent<Image> ();
		Idear_btn.sprite = IdearSelected;
		Definir_btn = GameObject.Find ("Definir").GetComponent<Image> ();
		Definir_btn.sprite = DefinirPointer;
		Empatizar_btn = GameObject.Find ("Empatizar").GetComponent<Image>();
		Empatizar_btn.sprite = EmpatizarPointer;
	}


	public void SelectPrototipar(){
		Prototipar_btn = GameObject.Find ("Prototipar").GetComponent<Image> ();
		Prototipar_btn.sprite = PrototiparSelected;
		Idear_btn = GameObject.Find ("Idear").GetComponent<Image> ();
		Idear_btn.sprite = IdearPointer;
		Definir_btn = GameObject.Find ("Definir").GetComponent<Image> ();
		Definir_btn.sprite = DefinirPointer;
		Empatizar_btn = GameObject.Find ("Empatizar").GetComponent<Image>();
		Empatizar_btn.sprite = EmpatizarPointer;
	}


	public void SelectProbar(){
		Probar_btn = GameObject.Find ("Probar").GetComponent<Image> ();
		Probar_btn.sprite = ProbarSelected;
		Prototipar_btn = GameObject.Find ("Prototipar").GetComponent<Image> ();
		Prototipar_btn.sprite = PrototiparPointer;
		Idear_btn = GameObject.Find ("Idear").GetComponent<Image> ();
		Idear_btn.sprite = IdearPointer;
		Definir_btn = GameObject.Find ("Definir").GetComponent<Image> ();
		Definir_btn.sprite = DefinirPointer;
		Empatizar_btn = GameObject.Find ("Empatizar").GetComponent<Image>();
		Empatizar_btn.sprite = EmpatizarPointer;
	}
}
