using SQLite4Unity3d;
using System;

public class Section  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public string creationDate { get; set; }

	
	public int mindMapId { get; set; }

	public bool isOptional {get; set; }

	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Section: Id={0}, Name={1},  creationDate={2}, mindMapId={3}, isOptional={4}, lastUpdate={5}]", 
								id, name, creationDate, mindMapId, isOptional, lastUpdate);
	}

	
}
