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

[System.Serializable]
public class ObjectToSend
{
    public CourseWeb[] courses;

    public GroupWeb[] groups;

    public TrainingWeb[] trainings;
    public CaseWeb[] cases;
    public MomentWeb[] moments;
    public ProjectWeb[] projects;
    public PublicWeb[] publics;
    public EmpathymapWeb[] empathymaps;
    public SectorWeb[] sectors;
    public ProblemWeb[] problems;
    public FieldWeb[] fields;
    public StoryTellingWeb[] storytellings;
    public NoteWeb[] notes;
    public MindmapWeb[] mindmaps;
    public SectionWeb[] sections;
    public NodeWeb[] nodes;
    public EvaluationWeb[] evaluations;
    public QuestionWeb[] questions;
    public AnswerWeb[] answers;
        
}
