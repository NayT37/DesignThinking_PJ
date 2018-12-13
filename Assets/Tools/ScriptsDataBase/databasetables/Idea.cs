using SQLite4Unity3d;
using System;

public class Idea  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string creationDate { get; set; }

	public string description { get; set; }

	public int sectorId { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Idea: Id={0}, creationDate={1}, description{2}, sectorId={3}, lastUpdate={4}]", 
								id, creationDate, description, sectorId, lastUpdate);
	}

	
}
