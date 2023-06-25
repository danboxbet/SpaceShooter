using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayersExplosion : MonoBehaviour
    {
        [SerializeField] private float lifeTime;
        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}