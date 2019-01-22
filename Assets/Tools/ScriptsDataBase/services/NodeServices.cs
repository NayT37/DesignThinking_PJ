using SQLite4Unity3d;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class NodeServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Node _nullNode = 
		new Node{
				id = 0,
				creationDate = "null",
				description = "null",
				sectionId = 0,
				lastUpdate = "null"			
		};

	private IEnumerable<Node> _nodesLoaded = new Node[]{
		new Node{
				id = 0,
				creationDate = "null",
				description = "null",
				sectionId = 0,
				lastUpdate = "null"			
		},
		new Node{
				id = 0,
				creationDate = "null",
				description = "null",
				sectionId = 0,
				lastUpdate = "null"			
		},
		new Node{
				id = 0,
				creationDate = "null",
				description = "null",
				sectionId = 0,
				lastUpdate = "null"			
		}
	};

	/// <summary>
	/// Description to method to create a node
	/// </summary>
	/// <returns>
	/// An object of type node with all the data of the node that was created.
	/// </returns>

	public Node CreateNode(){

		//valueToResponse = 1

		//The identifier of the storytelling is obtained to be able to pass 
		//it as an attribute in the new mindmap that will be created
		int sectionid = DataBaseParametersCtrl.Ctrl._sectionLoaded.id;

		//Get the current date to create the new ndoe
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();


		 var new_n = new Node{
				creationDate = date,
				description = "",
				sectionId = sectionid,
				lastUpdate = date			
		};

		int result = _connection.Insert (new_n);	
		
		return new_n;
		
		
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
		//valueToResponse = 2
		return _connection.Table<Node>().Where(x => x.sectionId == sectionId);
	}

	/// <summary>
	/// Description of the method to delete a node
	/// </summary>
	/// <param name="nodeToDelete">
	/// An object of type node that contain the node that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteNode(Node nodeToDelete){

		//valueToResponse = 3

		int result = _connection.Delete(nodeToDelete);
		
		return result;
	}

	/// <summary>
	/// Description of the method to update a moment
	/// </summary>
	/// <param name="nodeToUpdate">
	/// An object of type moment that contain the moment that will be updated.
	/// <returns>
	/// <param name="newdescription">
	/// An string that contain the new description that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	
	public int UpdateNode(Node nodeToUpdate, string newdescription){

		//valueToResponse = 4

		var _sectionServices = new 	SectionServices();

		nodeToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();
		nodeToUpdate.description = newdescription;

		int result = _connection.Update(nodeToUpdate, nodeToUpdate.GetType());

		if (result!=0)
		{
			_sectionServices.UpdateSection(nodeToUpdate.sectionId);
		}

		return result;
	}
}

