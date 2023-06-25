using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatistics : SingletonBase<GameStatistics>
{

    public int score;
    public float time;
    public int kills;
    // Start is called before the first frame update
    private void Start()
    {
        score = PlayerPrefs.GetInt("score");
        kills = PlayerPrefs.GetInt("kills");
        time = PlayerPrefs.GetFloat("time");
    }
    public void Save()
    {
        PlayerPrefs.SetInt("kills", kills);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetFloat("time", time);
    }

    public void GetGameStats()
    {
        score = PlayerPrefs.GetInt("score");
        kills = PlayerPrefs.GetInt("kills");
        time = PlayerPrefs.GetFloat("time");
    }
}
