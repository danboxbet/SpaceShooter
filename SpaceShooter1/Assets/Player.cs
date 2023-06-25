using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {

        [SerializeField] private int m_NumLives;
        [SerializeField] private SpaceShip m_ship;
        [SerializeField] private GameObject m_PlayerShipPrefab;
        [SerializeField] private PlayersExplosion playersExplosion;
        public SpaceShip ActiveShip=>m_ship;

        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private MovementController1 m_MovementController;

        public bool IsDeth;
        protected override void Awake()
        {
            base.Awake();
            if (m_ship != null) Destroy(m_ship.gameObject);
        }

       
        private void Start()
        {
            Respawn();
           // m_ship.EventOnDeath.AddListener(OnShopDeath);
        }

        private void OnShopDeath()
        {
            m_NumLives-=1;
            
            if (m_NumLives > 0)
            {
                 m_ship.EventOnDeath.RemoveListener(OnShopDeath);
                 Instantiate(playersExplosion, new Vector3(m_ship.transform.position.x,m_ship.transform.position.y,m_ship.transform.position.z), Quaternion.identity);
                Respawn();
            }
            else LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        private void Respawn()
        {
           if(LevelSequenceController.PlayerShip!=null)
            {
               
                var newPlayerShip = Instantiate(/*m_PlayerShipPrefab.gameObject*/LevelSequenceController.PlayerShip);

                m_ship = newPlayerShip.GetComponent<SpaceShip>();

                m_CameraController.SetTarget(m_ship.transform);
                m_MovementController.SetTargetShip(m_ship);
                m_ship.EventOnDeath.AddListener(OnShopDeath);
            }
            
        }
        #region Score

        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }

        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion


        public void SetNumLives(int num)
        {
            m_NumLives = num;
        }
        public void IsDead()
        {
            
            if (m_ship == null) IsDeth= true;
           
        }
        
    }
}