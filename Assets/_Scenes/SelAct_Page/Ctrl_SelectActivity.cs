using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ctrl_SelectActivity : MonoBehaviour {


	#region Variables
	public InputField NameCourse;
	private Text tmp;
	#endregion

	void Start () {
	}

	void Update(){
//		NameCourse = NameCourse.GetComponent<InputField> ().text;
	}

	public void SaveData(){
		string tmp;
		if (NameCourse.Equals("")) {
			tmp = NameCourse.ToString();
		}
	}
		

}
