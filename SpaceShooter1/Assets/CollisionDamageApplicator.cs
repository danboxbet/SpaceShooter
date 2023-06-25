using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";
        [SerializeField] private float m_VelocityDamage;
        [SerializeField] private float m_DamageConstant;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;

            var destructable = transform.root.GetComponent<Destructible>();

            if(destructable!=null)
            {
                destructable.ApplyDamage((int) m_DamageConstant+(int)(m_VelocityDamage*collision.relativeVelocity.magnitude));
                
            }
        }
    }
}