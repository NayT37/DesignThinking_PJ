using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Globalization;

public class ExistingDBScript : MonoBehaviour {

	public Text DebugText;

	public DataService ds;
	private CourseServices _courseServices;
	private GroupServices _groupServices;

	private CaseServices _caseServices;

	private TrainingServices _trainingServices;

	private MomentServices _momentServices;

	private AnswerServices _answerServices;

	

	// Use this for initialization
	void Start () {

		_courseServices = new CourseServices();
		_groupServices = new GroupServices();
		_trainingServices = new TrainingServices();
		_caseServices = new CaseServices();
		_momentServices = new MomentServices();
		_answerServices = new AnswerServices();

		// ds.CreateDB ();
		DebugText.text = "";

		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		var g = new Group{
				name = "Jojoa-Group-7-Updated",
				creationDate = "2018-12-6 15:00:00",
				studentsCounter = 5,
				courseId = 2,
				lastUpdate = date,
		};

		g.name = "Jojoa-Group-100-Created";

		DataBaseParametersCtrl.Ctrl._groupLoaded = g;

		// var moments = _momentServices.GetMoments(7);

		// foreach (var m in moments)
		// {	
		// 	_momentServices.UpdateMoment(m, 100);
		// }

		// moments = _momentServices.GetMoments(7);
		// ToConsole(moments);
		
		// var cases = _caseServices.GetCases(7);
		// ToConsole(cases);

		//var group = _groupServices.CreateGroup(g.name, g.courseId);
		//ToConsole(group);

		// var result = _groupServices.GetGroupNamed(g.name, g.courseId);
		// ToConsole(result);

		// _groupServices.DeleteGroup(result);

		// int result = _groupServices.UpdateGroup(g);
		// Debug.Log(result);
		
		// var groups = _groupServices.GetGroups(2);
		// ToConsole(groups);

		var answers = _answerServices.GetAnswers();
		ToConsole(answers);

	}

	
	
	private void ToConsole(IEnumerable<Headquarters> headquarters){
		foreach (var headquarter in headquarters) {
			ToConsole(headquarter.ToString());
		}
	}

	private void ToConsole(IEnumerable<Answer> answers){
		foreach (var answer in answers) {
			ToConsole(answer.ToString());
		}
	}

	private void ToConsole(IEnumerable<Course> courses){
		foreach (var course in courses) {
			ToConsole(course.ToString());
		}
	}

	private void ToConsole(IEnumerable<Group> groups){
		foreach (var group in groups) {
			ToConsole(group.ToString());
		}
	}

	private void ToConsole(IEnumerable<Moment> moments){
		foreach (var moment in moments) {
			ToConsole(moment.ToString());
		}
	}

	private void ToConsole(IEnumerable<Case> cases){
		foreach (var _case in cases) {
			ToConsole(_case.ToString());
		}
	}

	private void ToConsole(Group group){
		ToConsole(group.ToString());
	}

	private void ToConsole(string msg){
		DebugText.text += msg + "\n";
		Debug.Log (msg);
	}

}
