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

	private ProjectServices _projectServices;

	private PublicServices _publicServices;

	private ProblemServices _problemServices;

	private FieldServices _fieldServices;

	private EmpathymapServices _empathymapServices;

	private SectorServices _sectorServices;

	private StorytellingServices _storytellingServices;

	private NoteServices _noteServices;

	private MindmapServices _mindmapServices;

	private EvaluationServices _evaluationServices;

	private QuestionServices _questionServices;

	private AnswerServices _answerServices;

	private SectionServices _sectionServices;

	private NodeServices _nodeServices;

	

	// Use this for initialization
	void Start () {

		_courseServices = new CourseServices();
		_groupServices = new GroupServices();
		_trainingServices = new TrainingServices();
		_caseServices = new CaseServices();
		_momentServices = new MomentServices();
		_projectServices = new ProjectServices();
		_publicServices = new PublicServices();
		_problemServices = new ProblemServices();
		_fieldServices = new FieldServices();
		_empathymapServices = new EmpathymapServices();
		_sectorServices = new SectorServices();
		_storytellingServices = new StorytellingServices();
		_noteServices = new NoteServices();
		_mindmapServices = new MindmapServices();
		_evaluationServices = new EvaluationServices();
		_questionServices = new QuestionServices();
		_answerServices = new AnswerServices();
		_sectionServices = new SectionServices();
		_nodeServices = new NodeServices();

		
		_answerServices = new AnswerServices();

		// ds.CreateDB ();
		DebugText.text = "";

		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		var g = new Group{
				name = "Jojoa-Group-7-Updated",
				creationDate = "2018-12-6 15:00:00",
				studentsCounter = 5,
				courseId = 9,
				lastUpdate = date,
		};

		g.name = "Jojoa-Group-100-Created";

		DataBaseParametersCtrl.Ctrl._groupLoaded = g;

		var project = _projectServices.GetProjectNamed("Proyecto_Prueba_Final",15);
		Debug.Log(project);
		_projectServices.DeleteProject(project);
		// var course = _courseServices.CreateCourse("Curso-Prueba-Final");
		// Debug.Log(course);

		// var moments = _momentServices.GetMoments(7);

		// foreach (var m in moments)
		// {	
		// 	_momentServices.UpdateMoment(m, 100);
		// }

		// moments = _momentServices.GetMoments(7);
		// ToConsole(moments);
		
		// var cases = _caseServices.GetCases(7);
		// ToConsole(cases);

		// var group = _groupServices.CreateGroup(g.name, g.courseId);
		// Debug.Log(group);

		// var result = _groupServices.GetGroupNamed(g.name, g.courseId);
		// ToConsole(result);

		// _groupServices.DeleteGroup(result);

		// int result = _groupServices.UpdateGroup(g);
		// Debug.Log(result);
		
		// var groups = _groupServices.GetGroups(2);
		// ToConsole(groups);

		// var answers = _answerServices.GetAnswers();
		// ToConsole(answers);

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
