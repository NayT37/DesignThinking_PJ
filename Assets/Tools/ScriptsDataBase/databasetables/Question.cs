using SQLite4Unity3d;
using System;

public class Question  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public int grade { get; set; }
	public string creationDate { get; set; }

	public string description { get; set; }

	public int evaluationId { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Question: Id={0},  grade={1}, creationDate={2}, description{3}, evaluationId={4}, lastUpdate={5}]", 
								id, grade, creationDate, description, evaluationId, lastUpdate);
	}

	
}
