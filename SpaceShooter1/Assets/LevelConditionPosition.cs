using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionPosition : MonoBehaviour
    {
        [SerializeField] private Transform m_Point;
        [Range(0,float.MaxValue)]
        [SerializeField] private float m_Radius;
        private bool InTrigger;
        private void Start()
        {
            InTrigger = false;
        }

        private void Update()
        {
            var ship = Player.Instance.ActiveShip;
            if(ship!=null)
            {
                float dist = Vector3.Distance(ship.transform.position, m_Point.position);
                if(dist<m_Radius && !InTrigger)
                {
                    InTrigger = true;
                    LevelSequenceController.Instance?.FinishCurrentLevel(true);
                }
                
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(m_Point.position, transform.forward, m_Radius);
        }
#endif
    }
}