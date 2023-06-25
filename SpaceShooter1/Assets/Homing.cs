using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Homing : MonoBehaviour
    {
        [SerializeField] private Projectile m_Projectile;
        [SerializeField] private float Distance;
        [SerializeField] private float RotateSpeed;
        private Collider2D m_Collider2D;
        private Transform m_Target;
        private SpaceShip m_space;
        private void Start()
        {
            m_Collider2D = GetComponent<Collider2D>();
            StartCoroutine(StartCollider());
        }
        private IEnumerator StartCollider()
        {
            yield return new WaitForSeconds(0.3f);
            m_Collider2D.enabled = true;
        }
        private void Update()
        {
            float distance = m_Projectile.Velocity * Time.deltaTime;
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance*Distance);
            
            
            if (hit.collider != null )
            {
                m_Target = hit.transform;

            }
            if (m_Target!=null)
            {
                Vector3 direction = m_Target.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                float correctedAngle = angle - 90f;
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, correctedAngle);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);

                transform.position = Vector2.MoveTowards(transform.position, m_Target.position, m_Projectile.Velocity * Time.deltaTime);
                
            }
        }

    }
}