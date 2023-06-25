using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_SpaceShip;
        [SerializeField] private AsteroidMovement m_AsteroidMovement;
        [SerializeField] private GameObject m_AsteroidPref;
        [SerializeField] private Transform[] m_SpawnPoints;
        [SerializeField] private float timeSpawn;

        private Vector3 m_Target;
        public Vector3 IsTarget => m_Target;
        private float timer;

        private void Start()
        {
            timer = 0;
        }

        private void Update()
        {
            timer += Time.deltaTime;
           
            if(timer>=timeSpawn)
            {
                Spawn();
                timer = 0;
            }
        }

        private void Spawn()
        {

            int randomIndex = Random.Range(0, m_SpawnPoints.Length);
            Transform spawPoint = m_SpawnPoints[randomIndex];
            GameObject asteroid = Instantiate(m_AsteroidPref, spawPoint.position, Quaternion.identity);
            AsteroidMovement asteroidMovement = asteroid.gameObject.AddComponent<AsteroidMovement>();
            asteroid.gameObject.AddComponent<DestroyAsteroid>();
            asteroidMovement.SetAsteroidSpawner(this);
            asteroidMovement.SetLinearSpeed(m_AsteroidMovement.IsSpeed);
           asteroidMovement.SetTarget((m_SpaceShip.transform.position - spawPoint.position).normalized);
           asteroidMovement.SetSpawnedObject(asteroid);
            asteroidMovement.SetSpaceShip(m_SpaceShip);

        }
        
    }
}