using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Mod4LJT.Regulation;

abstract class GoogleSpreadsheetDeserialization<T> where T : CommonRegulation
{
    void Start()
    {
        // ID of the spreadsheet and range to retrieve data
        string spreadsheetId = "your-spreadsheet-id";
        string range = "Sheet1!A1:B5";

        // Retrieve data from the spreadsheet
        GetSpreadsheetData(spreadsheetId, range);
    }

    void GetSpreadsheetData(string spreadsheetId, string range)
    {
        // Google Sheets API endpoint
        string apiUrl = $"https://sheets.googleapis.com/v4/spreadsheets/{spreadsheetId}/values/{range}";

        WWW www = new WWW(apiUrl);

        // Check if there was an error in the request
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError($"Error: {www.error}");
        }

        // Parse the JSON response using JsonUtility
        SpreadsheetResponse spreadsheetResponse = JsonUtility.FromJson<SpreadsheetResponse>(www.text);

        // Retrieve the values from the response
        List<List<object>> spreadsheetData = spreadsheetResponse.Values;

        // Use the deserialized objects
        List<YourObject> objects = DeserializeData(spreadsheetData);

        foreach (YourObject obj in objects)
        {
            Debug.Log($"ID: {obj.Id}, Name: {obj.Name}");
        }
    }

    List<YourObject> DeserializeData(List<List<object>> spreadsheetData)
    {
        List<YourObject> objects = new List<YourObject>();

        foreach (var row in spreadsheetData)
        {
            // Assuming the first column contains the ID and the second column contains the Name
            int id = Convert.ToInt32(row[0]);
            string name = Convert.ToString(row[1]);

            // Create your object using the retrieved data
            YourObject obj = new YourObject
            {
                Id = id,
                Name = name
            };

            objects.Add(obj);
        }

        return objects;
    }
}

// Your custom object class
[System.Serializable]
public class YourObject
{
    public int Id;
    public string Name;
}

// Model class for deserializing the Spreadsheet API response
[System.Serializable]
public class SpreadsheetResponse
{
    public List<List<object>> Values;
}
