using SQLite4Unity3d;
using System;

// [Serializable]
public class Note  {

	
	[PrimaryKey]
	public int id { get; set; }
	public int position { get; set; }
	public string creationDate { get; set; }

	public string description { get; set; }

	public int storytellingId { get; set; }
	public string lastUpdate { get; set; }
	// public int id;
	// public int position;
	// public string creationDate;

	// public string description;

	// public int storytellingId;
	// public string lastUpdate;

	public override string ToString ()
	{
		return string.Format ("[Note: Id={0}, position={1}, creationDate={2}, description={3}, storyTellingId={4}, lastUpdate={5}]", 
								id, position, creationDate, description, storytellingId, lastUpdate);
	}

	
}
