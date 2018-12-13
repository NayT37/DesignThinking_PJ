using SQLite4Unity3d;
using System;

public class Note  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public int position { get; set; }
	public string creationDate { get; set; }

	public string description { get; set; }

	public int storyTellingId { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Note: Id={0}, position={1}, creationDate={2}, description={3}, storyTellingId={4}, lastUpdate={5}]", 
								id, position, creationDate, description, storyTellingId, lastUpdate);
	}

	
}
