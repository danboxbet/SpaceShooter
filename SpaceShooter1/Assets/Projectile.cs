using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity;
        public float Velocity => m_Velocity;

        [SerializeField] private float m_LifeTime;
        public float LifeTime => m_LifeTime;

        [SerializeField] private int m_Damage;
        public int Damage => m_Damage;

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        private float m_Timer;

        public UnityEvent m_EventDeath;

        private void Update()
        {
            Player.Instance.IsDead();
            
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;

            bool isPlayerProjectile = m_Parent == Player.Instance.ActiveShip;
            if (isPlayerProjectile && Player.Instance.IsDeth)
            {
                OnProjectileLifeEnd(null, transform.position);
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);
            if(hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();
                if(dest!=null && dest!=m_Parent)
                {
                    
                    dest.ApplyDamage(m_Damage);
                    if(m_Parent==Player.Instance.ActiveShip)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);
                    }
                    
                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;
            if (m_Timer > m_LifeTime)
            {
                m_EventDeath.Invoke();
                Instantiate(m_ImpactEffectPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            m_EventDeath.Invoke();
            Instantiate(m_ImpactEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        private Destructible m_Parent;

        public void SetPatentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}