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

    private bool isQueryOk = false;

    private Case _caseGetToDB = new Case();

    private int resultToDB = 0;

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

    public int Createcase(string casename, int trainingid)
    {

        //valueToResponse = 1

        //Get the current date to create the new group
        string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

        int valueToReturn = 0;

        var new_c = new Case
        {
            name = casename,
            percentage = 0,
            creationDate = date,
            trainingId = trainingid,
            lastUpdate = date
        };

        int result = _connection.Insert(new_c);

        //If the creation of the case is correct, then the moments corresponding to that case are created.
        if (result != 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //Creation of the moments
                valueToReturn += _momentS.CreateMoment(arraymomentsname[i], new_c.id);
            }
            Debug.Log(new_c);
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
    public Case GetCaseId(int caseid)
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
    public IEnumerable<Case> GetCases(int trainingId)
    {

        //valueToResponse = 3

        return _connection.Table<Case>().Where(x => x.trainingId == trainingId);
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
            Debug.Log("Se borró el caso correctamente");
        }
        else
        {
            valueToReturn = 0;
            Debug.Log("No se borró el caso");
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
    public int UpdateCase(int caseid)
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



    #region METHODS to get data to DB

    public void setDBToWeb(string methodToCall, int valueToResponse, Case _case)
    {

        //UserData tempUser = new UserData (player.id, player.cycle, game);
        string json = JsonUtility.ToJson(_case, true);
        UnityWebRequest postRequest = SetJsonForm(json, methodToCall);
        if (postRequest != null)
        {
            switch (valueToResponse)
            {
                case 1:

                    StartCoroutine(waitDB_ToCreateCase(postRequest));

                    break;

                case 4:

                    StartCoroutine(waitDB_ToDeleteCase(postRequest));

                    break;

            }
        }


    }

    private UnityWebRequest SetJsonForm(string json, string method)
    {
        try
        {
            UnityWebRequest web = UnityWebRequest.Put(DataBaseParametersCtrl.Ctrl._ipServer + method + "/put", json);
            web.SetRequestHeader("Content-Type", "application/json");
            return web;
        }
        catch
        {
            return null;
        }
    }

    IEnumerator waitDB_ToCreateCase(UnityWebRequest www)
    {
        using (www)
        {
            while (!www.isDone)
            {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
            ResponseCreateCase resp = null;

            try
            {
                resp = JsonUtility.FromJson<ResponseCreateCase>(www.downloadHandler.text);
            }
            catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty(www.error) && resp == null)
            { //Error al descargar data
                Debug.Log(www.error);
                try
                {

                }
                catch (System.Exception e) { Debug.Log(e); }
                yield return null;
            }
            else

            if (resp != null)
            { // Informacion obtenida exitosamente
                if (!resp.error)
                { // sin error en el servidor
                    _caseGetToDB = resp.caseCreated;
                    isQueryOk = true;
                }
                else
                { // no existen usuarios
                }

            }
            else
            { //Error en el servidor de base de datos
              // Debug.Log ("user error: " + resp.error);
                try
                {

                }
                catch { }
                // HUDController.HUDCtrl.MessagePanel (resp.msg);
            }
        }

        yield return null;
    }

    IEnumerator waitDB_ToDeleteCase(UnityWebRequest www)
    {
        using (www)
        {
            while (!www.isDone)
            {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
            ResponseDeleteCase resp = null;

            try
            {
                resp = JsonUtility.FromJson<ResponseDeleteCase>(www.downloadHandler.text);
            }
            catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty(www.error) && resp == null)
            { //Error al descargar data
                Debug.Log(www.error);
                try
                {

                }
                catch (System.Exception e) { Debug.Log(e); }
                yield return null;
            }
            else

            if (resp != null)
            { // Informacion obtenida exitosamente
                if (!resp.error)
                { // sin error en el servidor
                    resultToDB = resp.result;
                    isQueryOk = true;
                }
                else
                { // no existen usuarios
                }

            }
            else
            { //Error en el servidor de base de datos
              // Debug.Log ("user error: " + resp.error);
                try
                {

                }
                catch { }
                // HUDController.HUDCtrl.MessagePanel (resp.msg);
            }
        }

        yield return null;
    }

    #endregion

    #region METHODS to get data to DB
    public IEnumerator GetToDB(string methodToCall, string parameterToGet, int valueToResponse)
    {

        WWW postRequest = new WWW(DataBaseParametersCtrl.Ctrl._ipServer + methodToCall + parameterToGet); // buscar en el servidor al usuario
        switch (valueToResponse)
        {
            case 2:

                yield return (waitDB_ToGetCaseId(postRequest));

                break;

            case 3:

                yield return (waitDB_ToGetCases(postRequest));

                break;
        }
    }

    IEnumerator waitDB_ToGetCaseId(WWW www)
    {
        using (www)
        {
            while (!www.isDone)
            {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
            ResponseGetCaseId resp = null;

            try
            {
                resp = JsonUtility.FromJson<ResponseGetCaseId>(www.text);
            }
            catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty(www.error) && resp == null)
            { //Error al descargar data
                Debug.Log(www.error);
                try
                {

                }
                catch (System.Exception e) { Debug.Log(e); }
                yield return null;
            }
            else

            if (resp != null)
            { // Informacion obtenida exitosamente
                if (!resp.error)
                { // sin error en el servidor
                    _caseGetToDB = resp._case;
                    isQueryOk = true;
                }
                else
                { // no existen usuarios
                }

            }
            else
            { //Error en el servidor de base de datos
              // Debug.Log ("user error: " + resp.error);
                try
                {

                }
                catch { }
                // HUDController.HUDCtrl.MessagePanel (resp.msg);
            }
        }

        yield return null;
    }

    IEnumerator waitDB_ToGetCases(WWW www)
    {
        using (www)
        {
            while (!www.isDone)
            {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
            ResponseGetCases resp = null;

            try
            {
                resp = JsonUtility.FromJson<ResponseGetCases>(www.text);
            }
            catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty(www.error) && resp == null)
            { //Error al descargar data
                Debug.Log(www.error);
                try
                {

                }
                catch (System.Exception e) { Debug.Log(e); }
                yield return null;
            }
            else

            if (resp != null)
            { // Informacion obtenida exitosamente
                if (!resp.error)
                { // sin error en el servidor
                    _casesLoaded = resp.cases;
                    isQueryOk = true;
                }
                else
                { // no existen usuarios
                }

            }
            else
            { //Error en el servidor de base de datos
              // Debug.Log ("user error: " + resp.error);
                try
                {

                }
                catch { }
                // HUDController.HUDCtrl.MessagePanel (resp.msg);
            }
        }

        yield return null;
    }

    #endregion
}

