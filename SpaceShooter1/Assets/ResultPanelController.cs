using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;
        [SerializeField] private Text m_Multiplier;

        [SerializeField] private Text m_Result;

        [SerializeField] private Text m_ButtonNextText;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);

        }

        public void ShowResilts(PlayerStatistics levelResults,bool succes)
        {
            gameObject.SetActive(true);
            m_Success = succes;
            m_Result.text = succes ? "Win" : "Lose";
            m_ButtonNextText.text = succes ? "Next" : "Restart";
            m_Kills.text = "Kills : " + levelResults.numKills.ToString();
            m_Score.text = "Score : " + levelResults.score.ToString();
            m_Time.text = "Time : " + levelResults.time.ToString();
            m_Multiplier.text = "Mupltiplier : " + Math.Round(LevelSequenceController.Instance.multiplier, 2).ToString(); 
            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            if(m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
               // LevelSequenceController.Instance.ResetStats();
                LevelSequenceController.Instance.RestartLevel();

            }
        }

    }
}