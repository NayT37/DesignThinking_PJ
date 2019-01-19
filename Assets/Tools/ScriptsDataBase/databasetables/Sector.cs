using SQLite4Unity3d;
using System;

[Serializable]
public class Sector  {

	// [PrimaryKey, AutoIncrement]
	// public int id { get; set; }
	// public string name { get; set; }

	// public string description {get; set; }
	// public string creationDate { get; set; }

	
	// public int empathymapId { get; set; }

	// public string lastUpdate { get; set; }
	public int id;
	public string name;

	public string description {get; set; }
	public string creationDate;

	
	public int empathymapId;

	public string lastUpdate;

	public override string ToString ()
	{
		return string.Format ("[Sector: Id={0}, Name={1}, Description={2}, creationDate={3}, empathyMap={4}, lastUpdate={5}]", 
								id, name, description, creationDate, empathymapId, lastUpdate);
	}

	
}
