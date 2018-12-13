using SQLite4Unity3d;
using System;

public class Sector  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public string creationDate { get; set; }

	
	public int empathyMapId { get; set; }

	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Section: Id={0}, Name={1},  creationDate={2}, mindMapId={3}, lastUpdate={4}]", 
								id, name, creationDate, empathyMapId, lastUpdate);
	}

	
}
