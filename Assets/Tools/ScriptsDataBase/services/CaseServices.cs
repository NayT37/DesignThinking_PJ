using SQLite4Unity3d;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class CaseServices : MonoBehaviour
{

    private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

    private MomentServices _momentS = new MomentServices();

    private string[] arraymomentsname = new string[] { "moment_1", "moment_2", "moment_3", "moment_4", "moment_5" };
    private Case _nullCase =
        new Case
        {
            id = 0,
            name = "null",
            percentage = 0,
            creationDate = "null",
            trainingId = 0,
            lastUpdate = "null"
        };

    private IEnumerable<Case> _casesLoaded = new Case[]{
        new Case{
                id = 0,
                name = "null",
                percentage = 0,
                creationDate = "null",
                trainingId = 0,
                lastUpdate = "null"
        },
        new Case{
                id = 0,
                name = "null",
                percentage = 0,
                creationDate = "null",
                trainingId = 0,
                lastUpdate = "null"
        },
        new Case{
                id = 0,
                name = "null",
                percentage = 0,
                creationDate = "null",
                trainingId = 0,
                lastUpdate = "null"
        },
        new Case{
                id = 0,
                name = "null",
                percentage = 0,
                creationDate = "null",
                trainingId = 0,
                lastUpdate = "null"
        },
        new Case{
                id = 0,
                name = "null",
                percentage = 0,
                creationDate = "null",
                trainingId = 0,
                lastUpdate = "null"
        }
    };
    private int counterCases = 0;





    /// <summary>
    /// Description to method to create a case
    /// </summary>
    /// <param name="casename">
    /// Attribute that contains an string with the name of the case that will be created.
    /// </param>
    /// <param name="trainingid">
    /// Attribute that contains an string with the name of the case that will be created.
    /// </param>
    /// <returns>
    /// An integer response of the query (0 = the object was not created correctly. !0 = the object was created correctly)
    /// </returns>
    private Int64 checkId;
    public int Createcase(string casename, Int64 trainingid)
    {

        //valueToResponse = 1

        //Get the current date to create the new group
        string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

        int valueToReturn = 0;

        var new_c = new Case
        {
			id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId(),
            name = casename,
            percentage = 0,
            creationDate = date,
            trainingId = trainingid,
            lastUpdate = date
        };
        checkId = new_c.id;
        while (GetCaseId(checkId).id == new_c.id)
        {
            new_c.id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId();
            Debug.Log(GetCaseId(checkId).id);
        }
        
        int result = _connection.Insert(new_c);

        //If the creation of the case is correct, then the moments corresponding to that case are created.
        if (result != 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //Creation of the moments
                valueToReturn += _momentS.CreateMoment(arraymomentsname[i], new_c.id);
            }
        }
        else
        {
            valueToReturn = 100;
        }
    
        return valueToReturn;
    }

    /// <summary>
    /// Description to method Get Case that contain in the DataBaseParametersCtrl.!-- _trainingLoaded
    /// </summary>
    /// <param name="caseid">
    /// integer to define the identifier of the case that will be searched.
    /// <returns>
    /// <returns>
    /// An object of type case with all the data of the case that was searched and if doesnt exist so return an empty case.
    /// </returns>
    public Case GetCaseId(Int64 caseid)
    {

        //valueToResponse = 2

        var c = _connection.Table<Case>().Where(x => x.id == caseid).FirstOrDefault();

        if (c == null)
            return _nullCase;
        else
            return c;
    }

    /// <summary>
    /// Description of the method to obtain all the cases of a specific training
    /// </summary>
    /// <param name="trainingId">
    /// integer to define the identifier of the training from which all the related cases will be brought.
    /// <returns>
    /// A IEnumerable list of all the Cases found from the identifier of the training that was passed as a parameter
    /// </returns>
    public IEnumerable<Case> GetCases(Int64 trainingId)
    {

        //valueToResponse = 3
        return (IEnumerable<Case>)_connection.Query<Case> ("select * from 'Case' where trainingId = " + trainingId +" ORDER BY creationDate ASC");
        //return _connection.Table<Cases>().Where(x => x.trainingId == trainingId).OrderBy(m => m.creationDate);
    }

    public IEnumerable<Case> GetAllCases(){

		//valueToResponse = 2 

        var cases = _connection.Table<Case>();

		List<Case> finalCases = new List<Case>();

        foreach (var item in cases)
        {
            if (item.id.ToString().StartsWith(DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard))
            {
                finalCases.Add(item);
                counterCases++;
            }
        }

        return finalCases;
        
        }

        public int GetAllCasesCount(){

        return counterCases;
        
    }

    /// <summary>
    /// Description of the method to delete a case
    /// </summary>
    /// <param name="caseToDelete">
    /// An object of type case that contain the case that will be deleted.
    /// <returns>
    /// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
    /// </returns>
    public int DeleteCase(Case caseToDelete)
    {

        //valueToResponse = 4

        // All the moments belonging to the case that will be deleted are obtained.
        var moments = _momentS.GetMoments(caseToDelete.id);

        int result = _connection.Delete(caseToDelete);
        int valueToReturn = 0;

        //If the elimination of the case is correct, then the moments corresponding to that case are eliminated.
        if (result != 0)
        {
            foreach (var moment in moments)
            {
                valueToReturn += _momentS.DeleteMoment(moment);
            }

            Debug.Log("CASO BORRADO --- " + result);
        }
        else
        {
            valueToReturn = 0;
        }

        return valueToReturn;
    }

    /// <summary>
    /// Description of the method to update a case
    /// </summary>
    /// <param name="caseid">
    /// Identifier of the case that will be updated.
    /// <returns>
    /// An integer response of the query (0 = the object was not updated correctly. !0 = the object was updated correctly)
    /// </returns>
    public int UpdateCase(Int64 caseid)
    {

        var _trainingServices = new TrainingServices();

        var caseToUpdate = GetCaseId(caseid);
        var moments = _momentS.GetMoments(caseid);
        int valueToReturn = 0;
        int average = 0;

        if (caseToUpdate.id != 0)
        {
            foreach (var moment in moments)
            {
                average += moment.percentage;
            }

            average = average / 5;

            caseToUpdate.percentage = average;
            caseToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

            valueToReturn = _connection.Update(caseToUpdate, caseToUpdate.GetType());

            if (valueToReturn != 0)
            {
                _trainingServices.UpdateTraining(caseToUpdate.trainingId);
            }
        }
        return valueToReturn;
    }
}

