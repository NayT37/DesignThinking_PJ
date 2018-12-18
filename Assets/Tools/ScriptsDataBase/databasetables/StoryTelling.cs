using SQLite4Unity3d;
using System;

public class StoryTelling  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public int projectId { get; set; }
	public string lastUpdate { get; set; }

	public int version { get; set; }

	public override string ToString ()
	{
		return string.Format ("[StoryTelling: Id={0},  percentage={1}, creationDate={2}, projectId={3}, lastUpdate={4}, version={5}]", 
								id, percentage, creationDate, projectId, lastUpdate, version);
	}

	
}
