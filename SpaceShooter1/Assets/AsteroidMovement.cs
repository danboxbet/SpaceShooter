using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AsteroidMovement : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_SpaceShip;
        [SerializeField] private AsteroidSpawner m_AsteroidSpawner;
        [SerializeField] private float m_LinearSpeed;
        public float IsSpeed => m_LinearSpeed;
        private GameObject spawnedObject;
        private Vector3 m_Target;
        private float timer;
       
       


        private void FixedUpdate()
        {
            if (spawnedObject != null && m_AsteroidSpawner != null)
            {
               
               // spawnedObject.transform.position = Vector3.MoveTowards(spawnedObject.transform.position, m_Target, m_LinearSpeed * Time.fixedDeltaTime);
                spawnedObject.transform.position += m_Target * m_LinearSpeed * Time.fixedDeltaTime;
            }
                // spawnedObject.transform.position=m_AsteroidSpawner.IsTarget*m_LinearSpeed*Time.fixedDeltaTime;
                //spawnedObject.transform.Translate(m_AsteroidSpawner.IsTarget * m_LinearSpeed * Time.fixedDeltaTime);
        }

        

        public void SetSpawnedObject(GameObject asteroid)
        {
            spawnedObject = asteroid;
        }
       

        public void SetTarget(Vector3 target)
        {
            m_Target = target;
        }
        public void SetSpaceShip(SpaceShip spaceShip)
        {
            m_SpaceShip = spaceShip;
        }

        public void SetLinearSpeed(float speed)
        {
            m_LinearSpeed = speed;
        }
        public void SetAsteroidSpawner(AsteroidSpawner asteroidSpawner)
        {
            m_AsteroidSpawner = asteroidSpawner;
        }
       
    }

}