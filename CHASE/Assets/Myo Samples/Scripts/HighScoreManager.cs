using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class HighScoreManager : MonoBehaviour {

    private string connString;

    public List<HighScores> highScoresList = new List<HighScores>();

	// Use this for initialization
	void Start () {
        connString = "URI=file:" + Application.dataPath + "/HighScoresDb.sqlite";
        GetScores();
        
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    public void InsertScores(string level, int points, double time){

        using (IDbConnection conn = new SqliteConnection(connString))
        {
            conn.Open();

            using (IDbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = string.Format("INSERT INTO Scores(Level,Points,Time) VALUES(\"{0}\",\"{1}\",\"{2}\")",level,points,time);

                cmd.CommandText = sqlQuery;
                cmd.ExecuteScalar();
                conn.Close();
                
            }

        }
    }

    public void DeleteScores()
    {
        using (IDbConnection conn = new SqliteConnection(connString))
        {
            conn.Open();

            using (IDbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = string.Format("DELETE FROM Scores");

                cmd.CommandText = sqlQuery;
                cmd.ExecuteScalar();
                conn.Close();

            }
        }
    }

    private void GetScores()
    {
        highScoresList.Clear();

        using (IDbConnection conn = new SqliteConnection(connString))
        {
            conn.Open();

            using (IDbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Scores ORDER BY Time ASC LIMIT 10";

                cmd.CommandText = sqlQuery;

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        highScoresList.Add(new HighScores(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDouble(3)));
                    }
                    conn.Close();
                    reader.Close();
                }
            }

        }
    }
}
