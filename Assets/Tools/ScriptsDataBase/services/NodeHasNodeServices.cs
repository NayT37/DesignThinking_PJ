using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class NodeHasNodeServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private NodeHasNode _nullNodeHasNode = new NodeHasNode{
				id = 0,
				nodeId = 0		
		};
	


	/// <summary>
	/// Description to method to create a nodehasnode
	/// </summary>
	/// <param name="nodehasnode">
	/// Attribute that contains an object of type nodehasnode with all the data of the nodehasnode that will be created.
	/// </param>
	/// <returns>
	/// An object of type nodehasnode with all the data of the nodehasnode that was created.
	/// </returns>

	public NodeHasNode CreateNodeHasNode(NodeHasNode nodehasnode){

		// var publicValidation = GetProblemNamed(nodehasnode.name, nodehasnode.nodeId);

		// if ((publicValidation.name).Equals("null"))
		// {
			_connection.Insert (nodehasnode);
			return nodehasnode;
		// } else {
		// 	return _nullPublic;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get NodeHasNode with the specified nodeId
	/// </summary>
	/// <param name="nodeId">
	/// node identifier to find the correct nodehasnode that will be searched
	/// </param>
	/// <returns>
	/// An object of type nodehasnode with all the data of the nodehasnode that was searched and if doesnt exist so return an empty nodehasnode.
	/// </returns>
	public NodeHasNode GetNodeHasNodeNamed( int nodeId){
		
		var n = _connection.Table<NodeHasNode>().Where(x => x.nodeId == nodeId).FirstOrDefault();

		if (n == null)
			return _nullNodeHasNode;	
		else 
			return n;
	}

	/// <summary>
	/// Description of the method to obtain all the nodeHasNodes of a specific node
	/// </summary>
	/// <param name="nodeId">
	/// integer to define the identifier of the node from which all the related Nodes will be brought.
	/// <returns>
	/// A IEnumerable list of all the Nodes found from the identifier of the node that was passed as a parameter
	/// </returns>
	public IEnumerable<NodeHasNode> GetNodeHasNodes(int nodeId){
		return _connection.Table<NodeHasNode>().Where(x => x.nodeId == nodeId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Nodes
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the nodeHasNodes found
	/// </returns>
	public IEnumerable<NodeHasNode> GetNodeHasNodes(){
		return _connection.Table<NodeHasNode>();
	}

	/// <summary>
	/// Description of the method to delete a nodehasnode
	/// </summary>
	/// <param name="nodeHasNodeToDelete">
	/// An object of type nodehasnode that contain the nodehasnode that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteEmpathymap(NodeHasNode nodeHasNodeToDelete){

		return _connection.Delete(nodeHasNodeToDelete);
	}

	/// <summary>
	/// Description of the method to update a nodehasnode
	/// </summary>
	/// <param name="nodeHasNodeToUpdate">
	/// An object of type nodehasnode that contain the nodehasnode that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(NodeHasNode nodeHasNodeToUpdate){
		return _connection.Update(nodeHasNodeToUpdate, nodeHasNodeToUpdate.GetType());
	}
}

