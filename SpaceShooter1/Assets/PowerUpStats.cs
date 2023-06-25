using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpStats : PowerUp
    {
        public enum EffectType
        {
            AddAmmo,
            AddWeakness,
            AddAcceleration,
            AddEnergy
        }
        [SerializeField] private EffectType m_EffectType;
        [SerializeField] private float m_Value;
        protected override void OnPickedUp(SpaceShip ship)
        {
            if(m_EffectType==EffectType.AddWeakness)
            {
                Player.Instance.SetNumLives((int)m_Value);
            }
            if(m_EffectType==EffectType.AddEnergy)
            {
                ship.AddEnergy((int)m_Value);
            }
            if(m_EffectType==EffectType.AddAmmo)
            {
                ship.AddAmmo((int)m_Value);
            }
            if(m_EffectType==EffectType.AddAcceleration)
            {
                ship.AddAcceleration(m_Value, 10);
            }
        }
    }
}