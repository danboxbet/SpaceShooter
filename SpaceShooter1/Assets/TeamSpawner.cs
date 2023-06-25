using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class TeamSpawner : MonoBehaviour
    {
       

        [SerializeField] private Entity m_EntityPrefabs;
        [SerializeField] private Transform[] m_SpawnPoint;
        [SerializeField] private int numEnemy;
       
        [SerializeField] private int m_NumSpawns;
        [SerializeField] private float m_RespawnTime;
        private float m_Timer;

        private void Start()
        {
           
            m_Timer = m_RespawnTime;
            LevelController.Instance.numEnemy = numEnemy;
        }


        private void Update()
        {
            if (m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;
            }
            if ( m_Timer < 0)
            {
  
                    SpawnEntities();
                   
                
                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                
                GameObject e = Instantiate(m_EntityPrefabs.gameObject);
                int randomposition = Random.Range(0, m_SpawnPoint.Length);
                e.transform.position = m_SpawnPoint[randomposition].position;
            }
        }


    }
}