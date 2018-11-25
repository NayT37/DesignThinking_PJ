using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class CasesHUD_Ctrl : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private int _actualMoment;
    private int _actualCase;
    private string _actualScn;
    #endregion


    #region SYSTEM_METHODS
    private void Start() {
		Initializate();
		Btn_momento1 = GameObject.Find ("Btn_1").GetComponent<Image> ();
		Btn_momento2 = GameObject.Find ("Btn_2").GetComponent<Image> ();
		Btn_momento3 = GameObject.Find ("Btn_3").GetComponent<Image> ();

		Emp_Btn = GameObject.Find ("Btn_Emp").GetComponent<Image> ();
		Def_Btn = GameObject.Find ("Btn_Def").GetComponent<Image> ();
		Ide_Btn = GameObject.Find ("Btn_Ide").GetComponent<Image> ();
		Pro_Btn = GameObject.Find ("Btn_Pro").GetComponent<Image> ();
		Eva_Btn = GameObject.Find ("Btn_Eva").GetComponent<Image> ();


		Text_Changed = GameObject.Find ("NamePhase").GetComponent<Text> ();
		Text_TitleCase = GameObject.Find ("CaseTitle").GetComponent<Text> ();


	}
    private void Update() { }
    #endregion


	#region Botones cambios de sprite
	private Image Btn_momento1;
	private Image Btn_momento2; 
	private Image Btn_momento3; 
	#endregion

	#region Botones momentos, cambio de sprite
	private Image Emp_Btn;
	private Image Def_Btn; 
	private Image Ide_Btn; 
	private Image Pro_Btn; 
	private Image Eva_Btn; 
	#endregion

	#region Sprites Publicos para los botones de los momentos y de los casos 
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
	public Sprite EvaluarNull;
	public Sprite EvaluarSelected;
	[Header("MOMENTOS")]
	public Sprite Moment1_Selected;
	public Sprite Moment2_Selected;
	public Sprite Moment3_Selected;
	public Sprite Moment1_Null;
	public Sprite Moment2_Null;
	public Sprite Moment3_Null;
	#endregion

	#region Variables textos UpHUD
	private Text Text_Changed;
	private Text Text_TitleCase;
	#endregion



    #region CREATED_METHODS
    private void Initializate()
    {
        _actualMoment = 1;
        _actualCase = 1;
        _actualScn = "";
        StartCoroutine(ChangeScene("C" + _actualCase + "_M" + _actualMoment, _actualScn));
    }
    public void CaseBtnClick(int caseValue)
    {
        if (_actualCase != caseValue)
        {

            _actualMoment = 1;
            switch (caseValue)
            {
			case 1:
				print (caseValue + " was clicked. At " + _actualMoment + " moment.");

				//Sprites for cases
				Btn_momento1.sprite = Moment1_Selected;
				Btn_momento2.sprite = Moment2_Null;
				Btn_momento3.sprite = Moment3_Null;

				//Sprites for moments
				Emp_Btn.sprite = EmpatizarPointer;
				Def_Btn.sprite = DefinirNull;
				Ide_Btn.sprite = IdearNull;
				Pro_Btn.sprite = PrototiparPointer;
				Eva_Btn.sprite = EvaluarNull;


				//Texto Cambio de Nombre del caso
				Text_TitleCase.text = "CASO1";
				Text_Changed.text = "EMPATIZAR";

					break;
			case 2:
				print (caseValue + " was clicked. At " + _actualMoment + " moment.");
				//Sprites for cases
				Btn_momento2.sprite = Moment2_Selected;
				Btn_momento1.sprite = Moment1_Null;
				Btn_momento3.sprite = Moment3_Null;

				//Sprites for moments
				Emp_Btn.sprite = EmpatizarPointer;
				Def_Btn.sprite = DefinirNull;
				Ide_Btn.sprite = IdearNull;
				Pro_Btn.sprite = PrototiparPointer;
				Eva_Btn.sprite = EvaluarNull;


				//Texto Cambio de Nombre del caso
				Text_TitleCase.text = "CASO2";
				Text_Changed.text = "EMPATIZAR";

                    break;
                case 3:
                    print(caseValue + " was clicked. At " + _actualMoment + " moment.");
				//Sprite for cases
				Btn_momento3.sprite = Moment3_Selected;
				Btn_momento1.sprite = Moment1_Null;
				Btn_momento2.sprite = Moment2_Null;

				//Sprites for moments
				Emp_Btn.sprite = EmpatizarPointer;
				Def_Btn.sprite = DefinirNull;
				Ide_Btn.sprite = IdearNull;
				Pro_Btn.sprite = PrototiparPointer;
				Eva_Btn.sprite = EvaluarNull;

				//Texto Cambio de Nombre del caso
				Text_TitleCase.text = "CASO3";
				Text_Changed.text = "EMPATIZAR";

                    break;
            }
            _actualCase = caseValue;
            StartCoroutine(ChangeScene("C" + _actualCase + "_M" + _actualMoment, _actualScn));
        }
    }
    public void MomentBtnClick(int momentValue)
    {
        if (_actualMoment != momentValue)
        {
            switch (momentValue)
            {
			case 1:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
				StartCoroutine (ChangeScene ("C" + _actualCase + "_M" + momentValue, _actualScn));

				//Sprites for moments
				Emp_Btn.sprite = EmpatizarSelected;
				Def_Btn.sprite = DefinirNull;
				Ide_Btn.sprite = IdearNull;
				Pro_Btn.sprite = PrototiparPointer;
				Eva_Btn.sprite = EvaluarNull;

//				DOTween.Play ("3");
				//Texto que indica el nombre de la fase
				Text_Changed.text = "EMPATIZAR";
                    break;
                case 2:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));
				//Sprites for moments
				Emp_Btn.sprite = EmpatizarPointer;
				Def_Btn.sprite = DefinirSelected;
				Ide_Btn.sprite = IdearNull;
				Pro_Btn.sprite = PrototiparPointer;
				Eva_Btn.sprite = EvaluarNull;

//				DOTween.Play ("3");
				//Texto que indica el nombre de la fase
				Text_Changed.text = "DEFINIR";

                    break;
                case 3:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));
				//Sprites for moments
				Emp_Btn.sprite = EmpatizarPointer;
				Def_Btn.sprite = DefinirNull;
				Ide_Btn.sprite = IdearSelected;
				Pro_Btn.sprite = PrototiparPointer;
				Eva_Btn.sprite = EvaluarNull;

//				DOTween.Play ("3");
				//Texto que indica el nombre de la fase
				Text_Changed.text = "IDEAR";
                    break;
			case 4:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
				StartCoroutine (ChangeScene ("C" + _actualCase + "_M" + momentValue, _actualScn));

				//Sprites for moments
				Emp_Btn.sprite = EmpatizarPointer;
				Def_Btn.sprite = DefinirNull;
				Ide_Btn.sprite = IdearNull;
				Pro_Btn.sprite = PrototiparSelected;
				Eva_Btn.sprite = EvaluarNull;


//				DOTween.Play ("3");
				//Texto que indica el nombre de la fase
				Text_Changed.text = "PROTOTIPAR";
                    break;
                case 5:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));

				//Sprites for moments
				Emp_Btn.sprite = EmpatizarPointer;
				Def_Btn.sprite = DefinirNull;
				Ide_Btn.sprite = IdearNull;
				Pro_Btn.sprite = PrototiparPointer;
				Eva_Btn.sprite = EvaluarSelected;

//				DOTween.Play ("3");
				//Texto que indica el nombre de la fase
				Text_Changed.text = "EVALUAR";
                    break;
            }
            _actualMoment = momentValue;
        }
    }
    #endregion


    #region INTERFACE_METHODS
	public void GoHome(){
		StartCoroutine (Home());
	}
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    private IEnumerator ChangeScene(string sceneToLoad, string sceneToUnload)
    {
        if (sceneToUnload != "")
        {
            SceneManager.UnloadSceneAsync(sceneToUnload);
        }

        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        yield return null;
        _actualScn = sceneToLoad;

    }

	private IEnumerator Home(){
		
		SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Main_HUD");
	}
    #endregion
}
