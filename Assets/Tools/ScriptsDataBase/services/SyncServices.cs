using SQLite4Unity3d;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

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


	#region METHODS to get data to DB

	public void sendDataToSync(){

		var courses = _courseServices.GetAllCourses();
		var groups = _groupServices.GetAllGroups();
		var trainings = _trainingServices.GetAllTrainings();
		var cases = _caseServices.GetAllCases();
		var moments = _momentServices.GetAllMoments();
		var projects = _projectServices.GetAllProjects();
		var publics = _publicServices.GetAllPublics();
		var storytellings = _storytellingServices.GetAllStoryTellings();
		var notes = _noteServices.GetAllNotes();
		var empathymaps = _empathyMapServices.GetAllEmpathymaps();
		var sectors = _sectorServices.GetAllSectors();
		var problems = _problemServices.GetAllProblems();
		var fields = _fieldServices.GetAllFields();
		var mindmaps = _mindmapServices.GetAllMindmaps();
		var sections = _sectionServices.GetAllSections();
		var nodes = _nodeServices.GetAllNodes();
		var evaluations = _evaluationServices.GetAllEvaluations();
		var questions = _questionServices.GetAllQuestions();
		var answers = _answerServices.GetAllAnswers();

		foreach (var item in courses)
		{
			Debug.Log(item);
		}

		ObjectToSend objToSend = new ObjectToSend();

		objToSend.courses = courses;

		Debug.Log(JsonUtility.ToJson(objToSend));
	}

	public void setDBToWeb(string methodToCall, int valueToResponse, ObjectToSend group){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (group, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			
			StartCoroutine (waitDB_ToSendData (postRequest));
			
		}
			
	
	}

	private UnityWebRequest SetJsonForm (string json, string method) {
		try {
			UnityWebRequest web = UnityWebRequest.Put (DataBaseParametersCtrl.Ctrl._ipServer + method, json);
			web.SetRequestHeader ("Content-Type", "application/json");
			Debug.Log(json);
			return web;
		} catch {
			return null;
		}
	}

	IEnumerator waitDB_ToSendData (UnityWebRequest www) {
        using (www) {
            yield return www.SendWebRequest ();
			while (!www.isDone) {
				yield return null;
			}
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateGroup resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateGroup> (www.downloadHandler.text);
            } catch { }

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

