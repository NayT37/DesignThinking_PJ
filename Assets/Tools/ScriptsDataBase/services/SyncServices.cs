using SQLite4Unity3d;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif


public class SyncServices : MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;
	
	private CourseServices _courseServices = new CourseServices();

	private GroupServices _groupServices = new GroupServices();

	private TrainingServices _trainingServices = new TrainingServices();

	private CaseServices _caseServices = new CaseServices();

	private MomentServices _momentServices = new MomentServices();

	private ProjectServices _projectServices = new ProjectServices();

	private PublicServices _publicServices = new PublicServices();

	private StorytellingServices _storytellingServices = new StorytellingServices();
	
	private NoteServices _noteServices = new NoteServices();

	private MindmapServices _mindmapServices = new MindmapServices();

	private SectionServices _sectionServices = new SectionServices();

	private NodeServices _nodeServices = new NodeServices();

	private EvaluationServices _evaluationServices = new EvaluationServices();

	private QuestionServices _questionServices = new QuestionServices();

	private AnswerServices _answerServices = new AnswerServices();
	
	private EmpathymapServices _empathyMapServices = new EmpathymapServices();
	
	private SectorServices _sectorServices = new SectorServices();
	
	private ProblemServices _problemServices = new ProblemServices();

	private FieldServices _fieldServices = new FieldServices();

	private int counterCourses, counterGroups , counterProjects , counterStorytellings , counterMindmaps , counterNotes , counterEvaluations = 0;


	#region METHODS to get data to DB

	public void sendDataToSync(){

		var courses = _courseServices.GetAllCourses();

		counterCourses = _courseServices.GetAllCoursesCount();

		CourseWeb[] coursesweb = new CourseWeb[counterCourses];

		int countCourse = 0;

		foreach (var item in courses)
		{
			var cw = new CourseWeb();
			cw.id = item.id;
			cw.name = item.name;
			cw.percentage = item.percentage;
			cw.creationDate = item.creationDate;
			cw.teacherIdentityCard = item.teacherIdentityCard;
			cw.lastUpdate = item.lastUpdate;
			coursesweb[countCourse] = cw;
			counterCourses++;
		}
		var groups = _groupServices.GetAllGroups();

		counterGroups = _groupServices.GetAllGroupsCount();

		GroupWeb[] groupsweb = new GroupWeb[counterGroups];
		TrainingWeb[] trainingsweb;
		CaseWeb[] casesweb;
		MomentWeb[] momentsweb;
		if (counterGroups == 0)
		{
			trainingsweb = new TrainingWeb[0];
			casesweb = new CaseWeb[0];
			momentsweb = new MomentWeb[0];
		} else {
			trainingsweb = new TrainingWeb[counterGroups];
			casesweb = new CaseWeb[counterGroups*3];
			momentsweb = new MomentWeb[counterCourses*15];

			var trainings = _trainingServices.GetAllTrainings();
			var cases = _caseServices.GetAllCases();
			var moments = _momentServices.GetAllMoments();

			int countGroup = 0;

			foreach (var item in groups)
			{
				var cw = new GroupWeb();
				cw.id = item.id;
				cw.name = item.name;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.courseId = item.courseId;
				cw.lastUpdate = item.lastUpdate;
				groupsweb[countGroup] = cw;
				countGroup++;
			}

			int countTraining = 0;

			foreach (var item in trainings)
			{
				var cw = new TrainingWeb();
				cw.id = item.id;
				cw.name = item.name;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.groupId = item.groupId;
				cw.lastUpdate = item.lastUpdate;
				trainingsweb[countTraining] = cw;
				countTraining++;
			}

			int countCases = 0;

			foreach (var item in cases)
			{
				var cw = new CaseWeb();
				cw.id = item.id;
				cw.name = item.name;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.trainingId = item.trainingId;
				cw.lastUpdate = item.lastUpdate;
				casesweb[countCases] = cw;
				countCases++;
			}

			int countMoments = 0;

			foreach (var item in moments)
			{
				var cw = new MomentWeb();
				cw.id = item.id;
				cw.name = item.name;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.caseId = item.caseId;
				cw.lastUpdate = item.lastUpdate;
				momentsweb[countMoments] = cw;
				countMoments++;
			}
		} 
		
		counterProjects = _projectServices.GetAllProjectsCount();

		ProjectWeb[] projectsweb;
		PublicWeb[] publicsweb;
		EmpathymapWeb[] empathymapsweb;
		SectorWeb[] sectorsweb;
		ProblemWeb[] problemsweb;
		FieldWeb[] fieldsweb;
		StoryTellingWeb[] storytellingsweb;
		NoteWeb[] notesweb;
		MindmapWeb[] mindmapsweb;
		SectionWeb[] sectionsweb;
		NodeWeb[] nodesweb;
		EvaluationWeb[] evaluationsweb;
		QuestionWeb[] questionsweb;
		AnswerWeb[] answersweb;

		if (counterProjects == 0)
		{
			projectsweb = new ProjectWeb[0];
			publicsweb = new PublicWeb[0];
			empathymapsweb = new EmpathymapWeb[0];
			sectorsweb = new SectorWeb[0];
			problemsweb = new ProblemWeb[0];
			fieldsweb = new FieldWeb[0];
			storytellingsweb = new StoryTellingWeb[0];
			notesweb = new NoteWeb[0];
			mindmapsweb = new MindmapWeb[0];
			sectionsweb = new SectionWeb[0];
			nodesweb = new NodeWeb[0];
			evaluationsweb = new EvaluationWeb[0];
			questionsweb = new QuestionWeb[0];
			answersweb = new AnswerWeb[0];

		} else {


			var projects = _projectServices.GetAllProjects();
			var publics = _publicServices.GetAllPublics();
			var empathymaps = _empathyMapServices.GetAllEmpathymaps();
			var sectors = _sectorServices.GetAllSectors();
			var problems = _problemServices.GetAllProblems();
			var fields = _fieldServices.GetAllFields();
			var storytellings = _storytellingServices.GetAllStoryTellings();
			var notes = _noteServices.GetAllNotes();
			var mindmaps = _mindmapServices.GetAllMindmaps();
			var sections = _sectionServices.GetAllSections();
			var nodes = _nodeServices.GetAllNodes();
			var evaluations = _evaluationServices.GetAllEvaluations();
			var questions = _questionServices.GetAllQuestions();
			var answers = _answerServices.GetAllAnswers();

			projectsweb = new ProjectWeb[counterProjects];
			publicsweb = new PublicWeb[counterProjects];
			empathymapsweb = new EmpathymapWeb[counterProjects];
			sectorsweb = new SectorWeb[counterProjects*6];
			problemsweb = new ProblemWeb[counterProjects];
			fieldsweb = new FieldWeb[counterProjects*3];

			int countstorys = _storytellingServices.GetAllStoryTellingsCount();

			storytellingsweb = new StoryTellingWeb[countstorys];

			int countnots = _noteServices.GetAllNotesCount();

			notesweb = new NoteWeb[countnots];

			int countmmaps = _mindmapServices.GetAllMindmapsCount();

			mindmapsweb = new MindmapWeb[countmmaps];
			sectionsweb = new SectionWeb[countmmaps*6];
			nodesweb = new NodeWeb[countmmaps*18];

			int countevals = _evaluationServices.GetAllEvaluationsCount();

			evaluationsweb = new EvaluationWeb[countevals];
			questionsweb = new QuestionWeb[countevals*10];
			answersweb = new AnswerWeb[countevals*50];

			int countProjects = 0;

			foreach (var item in projects)
			{
				var cw = new ProjectWeb();
				cw.id = item.id;
				cw.name = item.name;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.groupId = item.groupId;
				cw.lastUpdate = item.lastUpdate;
				projectsweb[countProjects] = cw;
				countProjects++;
			}

			int countPublics = 0;

			foreach (var item in publics)
			{
				var cw = new PublicWeb();
				cw.id = item.id;
				cw.ageRange = item.ageRange;
				cw.gender = item.gender;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.projectId = item.projectId;
				cw.lastUpdate = item.lastUpdate;
				publicsweb[countPublics] = cw;
				countPublics++;
			}

			int countProblems = 0;

			foreach (var item in problems)
			{
				var cw = new ProblemWeb();
				cw.id = item.id;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.projectId = item.projectId;
				cw.lastUpdate = item.lastUpdate;
				problemsweb[countProblems] = cw;
				countProblems++;
			}

			int counteFields = 0;

			foreach (var item in fields)
			{
				var cw = new FieldWeb();
				cw.id = item.id;
				cw.name = item.name;
				cw.percentage = item.percentage;
				cw.description = item.description;
				cw.creationDate = item.creationDate;
				cw.problemId = item.problemId;
				cw.lastUpdate = item.lastUpdate;
				fieldsweb[counteFields] = cw;
				counteFields++;
			}

			int countEmpathymaps = 0;

			foreach (var item in empathymaps)
			{
				var cw = new EmpathymapWeb();
				cw.id = item.id;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.projectId = item.projectId;
				cw.lastUpdate = item.lastUpdate;
				empathymapsweb[countEmpathymaps] = cw;
				countEmpathymaps++;
			}

			int countSectors = 0;

			foreach (var item in sectors)
			{
				var cw = new SectorWeb();
				cw.id = item.id;
				cw.name = item.name;
				cw.description = item.description;
				cw.creationDate = item.creationDate;
				cw.empathymapId = item.empathymapId;
				cw.lastUpdate = item.lastUpdate;
				sectorsweb[countSectors] = cw;
				countSectors++;
			}


			int countstoryt = 0;

			foreach (var item in storytellings)
			{
				var cw = new StoryTellingWeb();
				cw.id = item.id;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.projectId = item.projectId;
				cw.lastUpdate = item.lastUpdate;
				cw.version = item.version;
				storytellingsweb[countstoryt] = cw;
				countstoryt++;
			}

			int countnotes = 0;

			foreach (var item in notes)
			{
				var cw = new NoteWeb();
				cw.id = item.id;
				cw.position = item.position;
				cw.creationDate = item.creationDate;
				cw.description = item.description;
				cw.storytellingId = item.storytellingId;
				cw.lastUpdate = item.lastUpdate;
				notesweb[countnotes] = cw;
				countnotes++;
			}

			int countmidnmaps = 0;

			foreach (var item in mindmaps)
			{
				var cw = new MindmapWeb();
				cw.id = item.id;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.storytellingId = item.storytellingId;
				cw.image = item.image;
				cw.lastUpdate = item.lastUpdate;
				cw.version = item.version;
				cw.ideaDescription = item.ideaDescription;
				mindmapsweb[countmidnmaps] = cw;
				countmidnmaps++;
			}

			int countsections = 0;

			foreach (var item in sections)
			{
				var cw = new SectionWeb();
				cw.id = item.id;
				cw.name = item.name;
				cw.creationDate = item.creationDate;
				cw.mindmapId = item.mindmapId;
				cw.isOptional = item.isOptional;
				cw.lastUpdate = item.lastUpdate;
				sectionsweb[countsections] = cw;
				countsections++;
			}

			int countnodes = 0;

			foreach (var item in nodes)
			{
				var cw = new NodeWeb();
				cw.id = item.id;
				cw.creationDate = item.creationDate;
				cw.description = item.description;
				cw.sectionId = item.sectionId;
				cw.lastUpdate = item.lastUpdate;
				nodesweb[countnodes] = cw;
				countnodes++;
			}

			int countevaluations = 0;

			foreach (var item in evaluations)
			{
				var cw = new EvaluationWeb();
				cw.id = item.id;
				cw.category = item.category;
				cw.percentage = item.percentage;
				cw.creationDate = item.creationDate;
				cw.mindMapId = item.mindMapId;
				cw.lastUpdate = item.lastUpdate;
				evaluationsweb[countevaluations] = cw;
				countevaluations++;
			}

			int countquestions = 0;

			foreach (var item in questions)
			{
				var cw = new QuestionWeb();
				cw.id = item.id;
				cw.creationDate = item.creationDate;
				cw.description = item.description;
				cw.evaluationId = item.evaluationId;
				cw.lastUpdate = item.lastUpdate;
				cw.category = item.category;
				questionsweb[countquestions] = cw;
				countquestions++;
			}

			int countaswers = 0;

			foreach (var item in answers)
			{
				var cw = new AnswerWeb();
				cw.id = item.id;
				cw.counter = item.counter;
				cw.value = item.value;
				cw.creationDate = item.creationDate;
				cw.lastUpdate = item.lastUpdate;
				cw.questionId = item.questionId;
				answersweb[countaswers] = cw;
				countaswers++;
			}

		}
		
		
		
		

		// foreach (var item in courses)
		// {
		// 	Debug.Log(item);
		// }

		ObjectToSend objToSend = new ObjectToSend();

		objToSend.Course = coursesweb;
		objToSend.Group = groupsweb;
		objToSend.Training = trainingsweb;
		objToSend.Case = casesweb;
		objToSend.Moment = momentsweb;
		objToSend.Project = projectsweb;
		objToSend.Public = publicsweb;
		objToSend.Problem = problemsweb;
		objToSend.Field = fieldsweb;
		objToSend.Empathymap = empathymapsweb;
		objToSend.Sector = sectorsweb;
		objToSend.Storytelling = storytellingsweb;
		objToSend.Note = notesweb;
		objToSend.Mindmap = mindmapsweb;
		objToSend.Section = sectionsweb;
		objToSend.Node = nodesweb;
		objToSend.Evaluation = evaluationsweb;
		objToSend.Question = questionsweb;
		objToSend.Answer = answersweb;

		setDBToWeb("sync", 0, objToSend);
	}

	public void setDBToWeb(string methodToCall, int valueToResponse, ObjectToSend obj){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (obj, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			Debug.Log(postRequest.method);
			StartCoroutine (waitDB_ToSendData (postRequest));
			
		}
			
	
	}

	private UnityWebRequest SetJsonForm (string json, string method) {
		try {
			UnityWebRequest web = UnityWebRequest.Put ("https://evening-scrubland-94987.herokuapp.com/" + method, json);
			web.SetRequestHeader ("Content-Type", "application/json");
			Debug.Log(json);
			return web;
		} catch {
			return null;
		}
	}

	IEnumerator waitDB_ToSendData (UnityWebRequest www) {

		Debug.Log("entranod coroutine");
        using (www) {
            yield return www.SendWebRequest ();
			while (!www.isDone) {
				yield return null;
			}
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseSync resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseSync> (www.downloadHandler.text);
            } catch { }

				Debug.Log(www.downloadHandler.text);
            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					DataBaseParametersCtrl.Ctrl.isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }
	
	#endregion
	

    

	





}

