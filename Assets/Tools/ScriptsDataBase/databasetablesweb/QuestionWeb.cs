﻿using SQLite4Unity3d;
using System;

[Serializable]
public class QuestionWeb  {

	// [PrimaryKey, AutoIncrement]
	// public int id { get; set; }
	// public string creationDate { get; set; }

	// public string description { get; set; }

	// public int evaluationId { get; set; }
	// public string lastUpdate { get; set; }

	// public string category {get; set; }
	public Int64 id;
	public string creationDate;

	public string description;

	public Int64 evaluationId;
	public string lastUpdate;

	public string category ;

	public override string ToString ()
	{
		return string.Format ("[Question: Id={0}, creationDate={1}, description{2}, evaluationId={3}, lastUpdate={4}, category={5}]", 
								id,  creationDate, description, evaluationId, lastUpdate, category);
	}

	
}
