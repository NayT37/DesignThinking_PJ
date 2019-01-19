using System;
using System.Collections.Generic;
using UnityEngine;

#region RESPONSE TEACHER
[Serializable]
public class ResponseGetTeacher {
	public bool error;
	public string msg;
	public Teacher teacher;
}
#endregion

#region RESPONSES COURSE
[Serializable]
public class ResponseGetCourses {
	public bool error;
	public string msg;
	public Course[] courses;
}

[Serializable]
public class ResponseCreateCourse {
	public bool error;
	public string msg;
	public Course courseCreated;
}

[Serializable]
public class ResponseGetNamedCourse {
	public bool error;
	public string msg;
	public Course courseNamed;
}

[Serializable]
public class ResponseGetCoursesCounter {
	public bool error;
	public string msg;
	public int counter;
}

[Serializable]
public class ResponseUpdateCourse {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseDeleteCourse {
	public bool error;
	public string msg;
	public int result;
}

#endregion

#region RESPONSES GROUP
[Serializable]
public class ResponseGetGroups {
	public bool error;
	public string msg;
	public Group[] groups;
}

[Serializable]
public class ResponseCreateGroup {
	public bool error;
	public string msg;
	public Group groupCreated;
}

[Serializable]
public class ResponseGetNamedGroup {
	public bool error;
	public string msg;
	public Group groupNamed;
}

[Serializable]
public class ResponseGetGroupsCounter {
	public bool error;
	public string msg;
	public int counter;
}

[Serializable]
public class ResponseUpdateGroup {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseDeleteGroup {
	public bool error;
	public string msg;
	public int result;
}

#endregion

#region RESPONSES TRAINING
[Serializable]
public class ResponseGetTrainingId {
	public bool error;
	public string msg;
	public Training training;
}


[Serializable]

public class ResponseGetTrainingGroupId {
	public bool error;
	public string msg;
	public Training training;
}

[Serializable]
public class ResponseCreateTraining {
	public bool error;
	public string msg;
	public Training trainingCreated;
}
#endregion

#region RESPONSES CASE

[Serializable]
public class ResponseCreateCase {
	public bool error;
	public string msg;
	public Case caseCreated;
}

[Serializable]
public class ResponseGetCaseId {
	public bool error;
	public string msg;
	public Case _case;
}


[Serializable]

public class ResponseGetCases {
	public bool error;
	public string msg;
	public Case[] cases;
}

[Serializable]
public class ResponseDeleteCase {
	public bool error;
	public string msg;
	public int result;
}
#endregion

#region RESPONSES MOMENT

[Serializable]
public class ResponseCreateMoment {
	public bool error;
	public string msg;
	public Moment momentCreated;
}


[Serializable]

public class ResponseGetMoments {
	public bool error;
	public string msg;
	public Moment[] moments;
}

[Serializable]
public class ResponseDeleteMoment {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseUpdateMoment {
	public bool error;
	public string msg;
	public int result;
}
#endregion

#region RESPONSES PROJECT
[Serializable]
public class ResponseGetProjects {
	public bool error;
	public string msg;
	public Project[] projects;
}

[Serializable]
public class ResponseCreateProject {
	public bool error;
	public string msg;
	public Project projectCreated;
}

[Serializable]
public class ResponseGetProjectId {
	public bool error;
	public string msg;
	public Project project;
}

[Serializable]
public class ResponseGetProjectsCounter {
	public bool error;
	public string msg;
	public int counter;
}

[Serializable]
public class ResponseUpdateProject {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseDeleteProject {
	public bool error;
	public string msg;
	public int result;
}

#endregion

#region RESPONSES PROBLEM

[Serializable]
public class ResponseCreateProblem {
	public bool error;
	public string msg;
	public Problem problemCreated;
}

[Serializable]
public class ResponseGetProblem {
	public bool error;
	public string msg;
	public Problem problem;
}

[Serializable]
public class ResponseGetProblemCounter {
	public bool error;
	public string msg;
	public int counter;
}

[Serializable]
public class ResponseDeleteProblem {
	public bool error;
	public string msg;
	public int result;
}

#endregion

#region RESPONSES FIELD

[Serializable]
public class ResponseCreateField {
	public bool error;
	public string msg;
	public Field fieldCreated;
}

[Serializable]
public class ResponseGetFields {
	public bool error;
	public string msg;
	public IEnumerable<Field> fields;
}

[Serializable]
public class ResponseDeleteField {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseUpdateField {
	public bool error;
	public string msg;
	public int result;
}

#endregion

#region RESPONSES EMPATHYMAP

[Serializable]
public class ResponseCreateEmpathymap {
	public bool error;
	public string msg;
	public Empathymap empathymapCreated;
}

[Serializable]
public class ResponseGetEmpathymap {
	public bool error;
	public string msg;
	public Empathymap empathymap;
}

[Serializable]
public class ResponseDeleteEmpathymap {
	public bool error;
	public string msg;
	public int result;
}
#endregion

#region RESPONSES SECTOR

[Serializable]
public class ResponseCreateSector {
	public bool error;
	public string msg;
	public Sector sectorCreated;
}

[Serializable]
public class ResponseGetSectors {
	public bool error;
	public string msg;
	public Sector[] sectors;
}

[Serializable]
public class ResponseDeleteSector {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseUpdateSector {
	public bool error;
	public string msg;
	public int result;
}
#endregion

#region RESPONSES STORYTELLING
[Serializable]
public class ResponseGetStoryTellings {
	public bool error;
	public string msg;
	public StoryTelling[] storytellings;
}

[Serializable]
public class ResponseCreateStoryTelling {
	public bool error;
	public string msg;
	public StoryTelling storytellingCreated;
}

[Serializable]
public class ResponseGetStoryTellingsCounter {
	public bool error;
	public string msg;
	public int counter;
}

[Serializable]
public class ResponseUpdateStorytelling {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseDeleteStorytelling {
	public bool error;
	public string msg;
	public int result;
}
#endregion

#region RESPONSES MINDMAP

[Serializable]
public class ResponseCreateMindmap {
	public bool error;
	public string msg;
	public Mindmap mindmapCreated;
}

[Serializable]
public class ResponseGetMindmaps {
	public bool error;
	public string msg;
	public Mindmap[] mindmaps;
}

[Serializable]
public class ResponseDeleteMindmap {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseUpdateMindmap {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseGetMindmapsCounter {
	public bool error;
	public string msg;
	public int counter;
}
#endregion

#region RESPONSES EVALUATION

[Serializable]
public class ResponseCreateEvaluation {
	public bool error;
	public string msg;
	public Evaluation evaluationCreated;
}

[Serializable]
public class ResponseGetEvaluation {
	public bool error;
	public string msg;
	public Evaluation evaluation;
}

[Serializable]
public class ResponseDeleteEvaluation {
	public bool error;
	public string msg;
	public int result;
}
#endregion

#region RESPONSES QUESTION

[Serializable]
public class ResponseCreateQuestion {
	public bool error;
	public string msg;
	public Question questionCreated;
}

[Serializable]
public class ResponseGetQuestion {
	public bool error;
	public string msg;
	public Question question;
}

[Serializable]
public class ResponseGetQuestions {
	public bool error;
	public string msg;
	public Question[] questions;
}

[Serializable]
public class ResponseDeleteQuestion {
	public bool error;
	public string msg;
	public int result;
}
#endregion

#region RESPONSES ANSWER

[Serializable]
public class ResponseCreateAnswer {
	public bool error;
	public string msg;
	public Answer answerCreated;
}

[Serializable]
public class ResponseGetAnswers {
	public bool error;
	public string msg;
	public Answer[] answers;
}

[Serializable]
public class ResponseDeleteAnswer {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseUpdateAnswer {
	public bool error;
	public string msg;
	public int result;
}
#endregion

#region RESPONSES SECTION

[Serializable]
public class ResponseCreateSection {
	public bool error;
	public string msg;
	public Section sectionCreated;
}

[Serializable]
public class ResponseGetSections {
	public bool error;
	public string msg;
	public Section[] sections;
}

[Serializable]
public class ResponseDeleteSection {
	public bool error;
	public string msg;
	public int result;
}

#endregion

#region RESPONSES NODE

[Serializable]
public class ResponseCreateNode {
	public bool error;
	public string msg;
	public Node nodeCreated;
}

[Serializable]
public class ResponseGetNodes {
	public bool error;
	public string msg;
	public Node[] nodes;
}

[Serializable]
public class ResponseDeleteNode {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseUpdateNode {
	public bool error;
	public string msg;
	public int result;
}

#endregion

#region RESPONSES NOTE

[Serializable]
public class ResponseCreateNote {
	public bool error;
	public string msg;
	public Note noteCreated;
}

[Serializable]
public class ResponseGetNotes {
	public bool error;
	public string msg;
	public Note[] notes;
}

[Serializable]
public class ResponseGetNotesCounter {
	public bool error;
	public string msg;
	public int counter;
}

[Serializable]
public class ResponseDeleteNote {
	public bool error;
	public string msg;
	public int result;
}

[Serializable]
public class ResponseUpdateNote {
	public bool error;
	public string msg;
	public int result;
}

#endregion

#region RESPONSES PUBLIC

[Serializable]
public class ResponseCreatePublic {
	public bool error;
	public string msg;
	public Public publicCreated;
}

[Serializable]
public class ResponseGetPublic {
	public bool error;
	public string msg;
	public Public _public;
}
#endregion

/**
Class to help convert the array of objects to an array of JSONs and the other way around */
public static class JsonHelper {
	public static T[] FromJson<T> (string json) {
		WrapperAssetBundles<T> wrapper = JsonUtility.FromJson<WrapperAssetBundles<T>> (json);
		return wrapper.AssetsBundles;
	}

	public static string ToJson<T> (T[] array) {
		WrapperAssetBundles<T> wrapper = new WrapperAssetBundles<T> ();
		wrapper.AssetsBundles = array;
		return JsonUtility.ToJson (wrapper);
	}

	public static string ToJson<T> (T[] array, bool prettyPrint) {
		WrapperAssetBundles<T> wrapper = new WrapperAssetBundles<T> ();
		wrapper.AssetsBundles = array;
		return JsonUtility.ToJson (wrapper, prettyPrint);
	}
	public static T[] FromJsonInPlayer<T> (string json) {
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
		return wrapper.Player;
	}

	public static string ToJsonInPlayer<T> (T[] array) {
		Wrapper<T> wrapper = new Wrapper<T> ();
		wrapper.Player = array;
		return JsonUtility.ToJson (wrapper);
	}

	public static string ToJsonInPlayer<T> (T[] array, bool prettyPrint) {
		Wrapper<T> wrapper = new Wrapper<T> ();
		wrapper.Player = array;
		return JsonUtility.ToJson (wrapper, prettyPrint);
	}

	[Serializable]
	private class WrapperAssetBundles<T> {
		public T[] AssetsBundles;
	}

	[Serializable]
	private class Wrapper<T> {
		public T[] Player;
	}
}