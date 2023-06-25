using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MovementController1 : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField] private SpaceShip m_TargetShip;
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

        [SerializeField] private VirtualJoystick m_MobileJoystick;

        [SerializeField] private ControlMode m_ControlMode;

        [SerializeField] private PointerClickHold m_MobileFirePrimary;

        [SerializeField] private PointerClickHold m_MobileFireSecondary;

        private void Start()
        {
            if (m_ControlMode == ControlMode.Keyboard)
            {

                m_MobileJoystick.gameObject.SetActive(false);
                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {

                m_MobileJoystick.gameObject.SetActive(true);
                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (m_TargetShip == null) return;
            if (m_ControlMode == ControlMode.Keyboard)
                ControlKeyboard();
            if (m_ControlMode == ControlMode.Mobile)
                ControlMobile();

        }
        private void ControlMobile()
        {
            /*Vector3 dir = m_MobileJoystick.Value;

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up);
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);

            m_TargetShip.thrustControl = Mathf.Max(0, dot);
            m_TargetShip.TorgueControl = -dot2;*/

            var dir = m_MobileJoystick.Value;
            m_TargetShip.thrustControl = dir.y;
            m_TargetShip.TorgueControl = -dir.x;

            if (m_MobileFirePrimary.IsHold)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (m_MobileFireSecondary.IsHold)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }
        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torgue = 0;
            if (Input.GetKey(KeyCode.W))
                thrust = 1.0f;
            if (Input.GetKey(KeyCode.S))
                thrust = -1.0f;
            if (Input.GetKey(KeyCode.A))
                torgue = 1.0f;
            if (Input.GetKey(KeyCode.D))
                torgue = -1.0f;
            m_TargetShip.thrustControl = thrust;
            m_TargetShip.TorgueControl = torgue;

            if(Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }
        }
    }
}