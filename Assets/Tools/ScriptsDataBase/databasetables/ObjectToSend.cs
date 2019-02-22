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
    public IEnumerable<Course> courses;

    public Group[] groups;

    public Training[] trainings;
    public Case[] cases;
    public Moment[] moments;
    public Project[] projects;
    public Public[] publics;
    public Empathymap[] empathymaps;
    public Sector[] sectors;
    public Problem[] problems;
    public Field[] fields;
    public StoryTelling[] storytellings;
    public Note[] notes;
    public Mindmap[] mindmaps;
    public Section[] sections;
    public Node[] nodes;
    public Evaluation[] evaluations;
    public Question[] questions;
    public Answer[] answers;
        
}
