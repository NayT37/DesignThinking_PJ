using SQLite4Unity3d;
using System;

public class Node  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string creationDate { get; set; }

	public string description { get; set; }

	public int sectionId { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Node: Id={0}, creationDate={1}, description={2}, sectionId={3}, lastUpdate={4}]", 
								id, creationDate, description, sectionId, lastUpdate);
	}

	
}
