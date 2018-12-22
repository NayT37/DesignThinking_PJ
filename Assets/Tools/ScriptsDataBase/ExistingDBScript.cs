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

	private string[] arrayfieldsname = new string[]{"field_1","field_2","field_3"};


	

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
				courseId = 1,
				lastUpdate = date,
		};

		g.name = "Grupo_Prueba_Final";

	
		
		DataBaseParametersCtrl.Ctrl._courseLoaded = _courseServices.GetCourseId(1);

		DataBaseParametersCtrl.Ctrl._groupLoaded = _groupServices.GetGroupId(1);

		DataBaseParametersCtrl.Ctrl._trainingloaded = _trainingServices.GetTrainingId(1);

		DataBaseParametersCtrl.Ctrl._projectLoaded = _projectServices.GetProjectNamed("Proyecto_Prueba_Final", 1);

		DataBaseParametersCtrl.Ctrl._mindMapLoaded = _mindmapServices.GetMindmapId(1);

		DataBaseParametersCtrl.Ctrl._empathyMapLoaded = _empathymapServices.GetEmpathyMap(1);

		DataBaseParametersCtrl.Ctrl._storyTellingLoaded = _storytellingServices.GetStoryTellingNamed(1);

		DataBaseParametersCtrl.Ctrl._publicLoaded = _publicServices.GetPublicNamed(1);

		DataBaseParametersCtrl.Ctrl._problemLoaded = _problemServices.GetProblemNamed(1, 1);

		DataBaseParametersCtrl.Ctrl._evaluationLoaded = _evaluationServices.GetEvaluationId(1);

		//DataBaseParametersCtrl.Ctrl._sectorLoaded = _sectorServices.GetSectorNamed();

		//var sector = _sectorServices.UpdateSector("nuevo dato por diligenciar");

		// var notesa = _noteServices.GetNotes();

		// foreach (var item in notesa)
		// {
		// 	item.position = 0;
		// 	_noteServices.UpdateNote(item, "nota_actualizada");
		// }
		
		Debug.Log(DataBaseParametersCtrl.Ctrl._projectLoaded);
		Debug.Log(DataBaseParametersCtrl.Ctrl._problemLoaded);
		Debug.Log(DataBaseParametersCtrl.Ctrl._publicLoaded);
		Debug.Log(DataBaseParametersCtrl.Ctrl._empathyMapLoaded);
		Debug.Log(DataBaseParametersCtrl.Ctrl._storyTellingLoaded);
		Debug.Log(DataBaseParametersCtrl.Ctrl._mindMapLoaded);

		var nodes = _nodeServices.GetNodes();

		foreach (var item in nodes)
		{
			Debug.Log(item.ToString());
		}

		var notes = _noteServices.GetNotes();

		foreach (var item in notes)
		{
			Debug.Log(item.ToString());
		}

		var sectors = _sectorServices.GetSectors(1);
		
		foreach (var item in sectors)
		{
				// DataBaseParametersCtrl.Ctrl._sectorLoaded = item;
				// _sectorServices.UpdateSector("nuevo_dato");
		
			Debug.Log(item.ToString());
		}
		
		// var course = _courseServices.CreateCourse("Curso-Prueba-Final");
		// Debug.Log(course);

		// var group = _groupServices.CreateGroup(g.name, g.courseId);
		// Debug.Log(group);

		// var project = _projectServices.CreateProject("Proyecto_Prueba_Final","Agricola");
		// Debug.Log(project);

		
		// var problem = _problemServices.CreateProblem(arrayfieldsname);

		// var _public = _publicServices.CreatePublic("Infancia", "Mixto");

		// var evaluation = _evaluationServices.CreateEvaluation("producto_tangible");


		// var mindmap = _mindmapServices.GetMindmaps();

		// foreach (var item in mindmap)
		// {
		// 	Debug.Log(item.ToString());
		// }

		// var sections = _sectionServices.GetSections();
		// foreach (var item in sections)
		//  {
		//  	Debug.Log(item.ToString());
		// }

		// var mindmap = _mindmapServices.GetMindmapId(1);
		// Debug.Log(mindmap);

		// var node7 = _nodeServices.GetNodeId(7);
		// _nodeServices.UpdateNode(node7, "");
		// var node8 = _nodeServices.GetNodeId(8);
		// _nodeServices.UpdateNode(node8, "nodo_actualizado_3.......");
		// var node9 = _nodeServices.GetNodeId(9);
		// _nodeServices.UpdateNode(node9, "nodo_actualizado_3.......");
		// var node10 = _nodeServices.GetNodeId(10);
		// _nodeServices.UpdateNode(node10, "nodo_actualizado_3.......");
		// var node11 = _nodeServices.GetNodeId(11);
		// _nodeServices.UpdateNode(node11, "");
		// var node12 = _nodeServices.GetNodeId(12);
		// _nodeServices.UpdateNode(node12, "");

		// var mindmap = _mindmapServices.GetMindmapId(1);
		// Debug.Log(mindmap);

		// _noteServices.CreateNote("note_1");
		// _noteServices.CreateNote("note_2");
		// _noteServices.CreateNote("note_3");
		// _noteServices.CreateNote("note_4");
		// _noteServices.CreateNote("note_5");
		// _noteServices.CreateNote("note_6");
		// _noteServices.CreateNote("note_7");
		// _noteServices.CreateNote("note_8");
		
		
		
		

		// var st = _storytellingServices.GetStoryTellings();

		// foreach (var item in st)
		// {
		// 	Debug.Log(item.ToString());
		// }

		// var project1 = _projectServices.GetProjectNamed("Proyecto_Prueba_Final", 1);
		// _projectServices.DeleteProject(project1);


		// var course = _courseServices.GetCourseId(1);
		// Debug.Log(course.ToString());

		// var projects = _projectServices.GetProjects();

		// foreach (var item in projects)
		// {
		// 	Debug.Log(item.ToString());
		// }

		// var moments = _momentServices.GetMoments(7);

		// foreach (var m in moments)
		// {	
		// 	_momentServices.UpdateMoment(m, 100);
		// }

		// moments = _momentServices.GetMoments(7);
		// ToConsole(moments);
		
		// var cases = _caseServices.GetCases(7);
		// ToConsole(cases);

		
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
