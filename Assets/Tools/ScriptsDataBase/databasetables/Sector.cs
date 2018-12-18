using SQLite4Unity3d;
using System;

public class Sector  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }

	public string description {get; set; }
	public string creationDate { get; set; }

	
	public int empathyMapId { get; set; }

	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Section: Id={0}, Name={1}, Description={2}, creationDate={3}, mindMapId={4}, lastUpdate={5}]", 
								id, name, description, creationDate, empathyMapId, lastUpdate);
	}

	
}
