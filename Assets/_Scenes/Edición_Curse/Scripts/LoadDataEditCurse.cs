using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoadDataEditCurse : MonoBehaviour, IPointerDownHandler {



	void Start () {
		
	}

	IEnumerator GotoScene(){
		SceneManager.LoadScene ("Edit_Group", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Edit_Curse");
	}

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
//		StartCoroutine (GotoScene ());
		Debug.Log("Click");
	}

	#endregion
}
