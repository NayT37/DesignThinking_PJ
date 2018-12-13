using SQLite4Unity3d;
using System;

public class NodeHasNode  {

	[PrimaryKey]
	public int id { get; set; }
	public int nodeId { get; set; }

	public override string ToString ()
	{
		return string.Format ("[NodeHasNode: Id={0}, nodeId={1}]", id, nodeId);
	}

	
}
