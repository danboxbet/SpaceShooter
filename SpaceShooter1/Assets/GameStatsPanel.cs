using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsPanel : MonoBehaviour
{
    [SerializeField] private Text m_scoreText;
    [SerializeField] private Text m_killsText;
    [SerializeField] private Text m_timeText;

    public void ShowStats()
    {
        GameStatistics.Instance.GetGameStats();
        m_scoreText.text = "Score : " + GameStatistics.Instance.score.ToString();
        m_killsText.text = "Kills : " + GameStatistics.Instance.kills.ToString();
        m_timeText.text = "Time in game : " + Math.Round(GameStatistics.Instance.time, 2).ToString();
    }

}
