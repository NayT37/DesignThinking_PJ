using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoadDataGoScene : MonoBehaviour, IPointerDownHandler {

	// Use this for initialization
	void Start () {
		
	}

	IEnumerator GoScene(){
//				Main_Ctrl.instance.NameCourse = NameCourse.text.ToString();
		SceneManager.LoadScene ("Edit_Curse", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("LoadGame");
	}

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		StartCoroutine (GoScene ());
		Debug.Log("enter");
	}

	#endregion
}
