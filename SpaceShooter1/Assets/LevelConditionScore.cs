using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour,IlevelCondition
    {
        [SerializeField] private int score;

        private bool m_Reached;

        bool IlevelCondition.IsCompleted
        {
            get
            {
                if(Player.Instance!=null && Player.Instance.ActiveShip!=null)
                {
                    if(Player.Instance.Score>=score || Player.Instance.NumKills!=0)
                    {
                        m_Reached = true;
                        
                    }
                }
                return m_Reached;
            }
        }
    }
}