﻿using SQLite4Unity3d;
using System;

[Serializable]
public class MindmapWeb
 {

//     [PrimaryKey, AutoIncrement]
//     public int id { get; set; }

//     public int percentage { get; set; }

//     public string creationDate { get; set; }

//     public int storytellingId { get; set; }

//     public string image { get; set; }

//     public string lastUpdate { get; set; }

//     public int version { get; set; }

     	public Int64 id;

        public int percentage;

        public string creationDate;

        public Int64 storytellingId;

        public string image;

        public string lastUpdate;

        public int version;

        public string ideaDescription;


    public override string ToString()
    {
        return string.Format("[Mindmap: Id={0}, percentage={1},  creationDate={2}, storyTellingId={3} image={4}, lastUpdate={5}, version={6}, ideaDescription={7}]",
                                id, percentage, creationDate, storytellingId, image, lastUpdate, version, ideaDescription);
    }
    

}
