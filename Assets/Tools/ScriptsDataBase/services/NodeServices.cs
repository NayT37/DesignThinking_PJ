using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class NodeServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Node _nullNode = new Node{
				id = 0,
				creationDate = "null",
				description = "null",
				sectionId = 0,
				lastUpdate = "null"			
		};
	


	/// <summary>
	/// Description to method to create a node
	/// </summary>
	/// <param name="node">
	/// Attribute that contains an object of type node with all the data of the node that will be created.
	/// </param>
	/// <returns>
	/// An object of type node with all the data of the node that was created.
	/// </returns>

	public Node CreateNode(Node node){

		// var publicValidation = GetProblemNamed(node.name, node.sectionId);

		// if ((publicValidation.name).Equals("null"))
		// {
			_connection.Insert (node);
			return node;
		// } else {
		// 	return _nullPublic;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get Node with the specified sectionId
	/// </summary>
	/// <param name="sectionId">
	/// sectionId identifier to find the correct node that will be searched
	/// </param>
	/// <returns>
	/// An object of type node with all the data of the node that was searched and if doesnt exist so return an empty node.
	/// </returns>
	public Node GetNodeNamed( int sectionId){
		
		var n = _connection.Table<Node>().Where(x => x.sectionId == sectionId).FirstOrDefault();

		if (n == null)
			return _nullNode;	
		else 
			return n;
	}

	/// <summary>
	/// Description of the method to obtain all the notes of a specific section
	/// </summary>
	/// <param name="sectionId">
	/// integer to define the identifier of the section from which all the related Nodes will be brought.
	/// <returns>
	/// A IEnumerable list of all the Nodes found from the identifier of the section that was passed as a parameter
	/// </returns>
	public IEnumerable<Node> GetNodes(int sectionId){
		return _connection.Table<Node>().Where(x => x.sectionId == sectionId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Nodes
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the notes found
	/// </returns>
	public IEnumerable<Node> GetNodes(){
		return _connection.Table<Node>();
	}

	/// <summary>
	/// Description of the method to delete a node
	/// </summary>
	/// <param name="nodeToDelete">
	/// An object of type node that contain the node that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteEmpathymap(Node nodeToDelete){

		return _connection.Delete(nodeToDelete);
	}

	/// <summary>
	/// Description of the method to update a node
	/// </summary>
	/// <param name="nodeToDelete">
	/// An object of type node that contain the node that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(Node nodeToDelete){
		return _connection.Update(nodeToDelete, nodeToDelete.GetType());
	}
}

