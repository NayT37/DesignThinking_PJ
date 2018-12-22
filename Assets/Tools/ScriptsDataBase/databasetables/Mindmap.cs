using SQLite4Unity3d;
using System;

public class Mindmap  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }

	public int percentage { get; set; }

	public string creationDate { get; set; }

	public int storytellingId { get; set; }

	public string image { get; set; }

	public string lastUpdate { get; set; }

	public int version { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Mindmap: Id={0}, percentage={1},  creationDate={2}, storyTellingId={3} image={4}, lastUpdate={5}, version={6}]", 
								id, percentage, creationDate,storytellingId, image, lastUpdate, version);
	}

	
}
