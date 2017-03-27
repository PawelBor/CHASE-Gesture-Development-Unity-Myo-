using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores
{
    public int ID { get; set; }
    public string Level { get; set; }
    public int Points { get; set; }
    public double Time { get; set; }

    public HighScores(int id, string level, int points, double time) {
        this.ID = id;
        this.Level = level;
        this.Points = points;
        this.Time = time;
    }
}