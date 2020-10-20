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
    public CourseWeb[] Course;

    public List<GroupWeb> Group;

    public List<TrainingWeb> Training;
    public List<CaseWeb> Case;
    public List<MomentWeb> Moment;
    public ProjectWeb[] Project;
    public List<PublicWeb> Public;
    public List<EmpathymapWeb> Empathymap;
    public List<SectorWeb> Sector;
    public ProblemWeb[] Problem;
    public FieldWeb[] Field;
    public StoryTellingWeb[] Storytelling;
    public NoteWeb[] Note;
    public MindmapWeb[] Mindmap;
    public SectionWeb[] Section;
    public NodeWeb[] Node;
    public EvaluationWeb[] Evaluation;
    public QuestionWeb[] Question;
    public AnswerWeb[] Answer;
        
}
