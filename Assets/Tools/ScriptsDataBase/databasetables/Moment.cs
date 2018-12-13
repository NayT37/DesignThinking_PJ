﻿using SQLite4Unity3d;
using System;

public class Moment  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public int caseId { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Moment: Id={0}, Name={1},  percentage={2}, creationDate={3}, caseId={4}, lastUpdate={5}]", 
								id, name, percentage, creationDate, caseId, lastUpdate);
	}

	
}
