using SQLite4Unity3d;
using System;

public class Answer  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }

	public int counter { get; set; }
	public int value { get; set; }
	public string creationDate { get; set; }
	public string lastUpdate { get; set; }

	public int questionId { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Answer: Id={0}, counter={1}, value={2}, creationDate={4}, description{5}, questionId={6}]", 
								id, counter, value, creationDate, lastUpdate, questionId);
	}

	
}
