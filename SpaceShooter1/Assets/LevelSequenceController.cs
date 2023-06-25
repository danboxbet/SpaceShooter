using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {

        public static string MainMenuSceneNickname = "main_menu";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public static SpaceShip PlayerShip { get; set; }

        public float timeForDestroyEnemy;

        public float multiplier;
        public void StartEpisode(Episode e)
        {
            multiplier = 0;
            CurrentEpisode = e;
            CurrentLevel = 0;

            //сбрасываем статы перед началом эпизода
            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);
           

        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }
        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            CalculateLevelStatistic();
            
            ResultPanelController.Instance.ShowResilts(LevelStatistics, success);
                

            
          
        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();
            CurrentLevel++;
           
            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
               

                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
                

            }
            
        }
        private void CalculateLevelStatistic()
        {
            LevelStatistics.score = Player.Instance.Score;
            LevelStatistics.numKills = Player.Instance.NumKills;
            LevelStatistics.time = (int)LevelController.Instance.LevelTime;
            multiplier = (LevelController.Instance.numEnemy * timeForDestroyEnemy) / LevelStatistics.time;
            if ((int)multiplier >= 1)
                LevelStatistics.score =(int) (Player.Instance.Score * multiplier);
            GameStatistics.Instance.score += LevelStatistics.score;
            GameStatistics.Instance.kills += LevelStatistics.numKills;
            GameStatistics.Instance.time += LevelStatistics.time;
            GameStatistics.Instance.Save();

        }
        public void ResetStats()
        {
            LevelStatistics.Reset();
        }

    }
}