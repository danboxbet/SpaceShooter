using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace SpaceShooter
{
    public interface IlevelCondition
    {
        bool IsCompleted { get; }
    }
    public class LevelController : SingletonBase<LevelController>
    {

       

        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;

        [SerializeField] private UnityEvent m_EventLevelCompleted;

        private IlevelCondition[] m_Conditions;

        private bool m_IsLevelCompleted;

        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        public int numEnemy;
        // Start is called before the first frame update
        void Start()
        {
            m_Conditions = GetComponentsInChildren<IlevelCondition>();

        }

        // Update is called once per frame
        void Update()
        {
            if(!m_IsLevelCompleted)
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelConditions();
            }
        }

        private void CheckLevelConditions()
        {
            
           
            if (m_Conditions == null || m_Conditions.Length == 0) return;

            int numCompleted = 0;
            foreach(var v in m_Conditions)
            {
                if (v.IsCompleted)
                    numCompleted++;

            }
            if(numCompleted==m_Conditions.Length)
            {
                if (Player.Instance.NumKills >= numEnemy)
                {
                   
                    m_IsLevelCompleted = true;
                    m_EventLevelCompleted?.Invoke();
                    LevelSequenceController.Instance?.FinishCurrentLevel(true);
                   
                }
            }

           
        }

       
      
    }
}