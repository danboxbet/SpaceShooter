using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AIPointPatrol : MonoBehaviour
    {
        [SerializeField] private Transform[] TargetsPoints;
        [SerializeField] private float m_Radius;

        public Transform[] TargetPoint => TargetsPoints;
        public float Radius => m_Radius;

#if UNITY_EDITOR
        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(transform.position, m_Radius);
        }
#endif
    }
}