﻿using SQLite4Unity3d;
using System;

// [Serializable]
public class Section  {

	[PrimaryKey]
	public Int64 id { get; set; }
	public string name { get; set; }
	public string creationDate { get; set; }

	
	public Int64 mindmapId { get; set; }

	public bool isOptional {get; set; }

	public string lastUpdate { get; set; }
	// public int id;
	// public string name;
	// public string creationDate;

	
	// public int mindmapId;

	// public bool isOptional {get; set; }

	// public string lastUpdate;

	public override string ToString ()
	{
		return string.Format ("[Section: Id={0}, Name={1},  creationDate={2}, mindMapId={3}, isOptional={4}, lastUpdate={5}]", 
								id, name, creationDate, mindmapId, isOptional, lastUpdate);
	}

	
}
