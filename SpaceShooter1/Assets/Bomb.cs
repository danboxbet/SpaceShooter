using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Projectile))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Bomb : MonoBehaviour
    {
       
        [SerializeField] private float m_Radius;
        private CircleCollider2D this_Collider;
        private Projectile m_Projectile;
        
        
        
        
        private void Start()
        {
 
            this_Collider = GetComponent<CircleCollider2D>();
             m_Projectile = GetComponent<Projectile>();
            
            m_Projectile.m_EventDeath.AddListener(ApplyDamageBombs);

        }
        private void Update()
        {
            this_Collider.radius = m_Radius;  
        }
       
      

        public void ApplyDamageBombs()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_Radius);
            for (int i = 0; i < colliders.Length; i++)
            {
                Collider2D collider = colliders[i];
                Destructible destructible = collider.GetComponentInParent<Destructible>();

                if (destructible != null)
                {

                    destructible.ApplyDamage(m_Projectile.Damage);
                }

            }
        }
        private void OnDestroy()
        {
            m_Projectile.m_EventDeath.RemoveListener(ApplyDamageBombs);
        }

    }
}