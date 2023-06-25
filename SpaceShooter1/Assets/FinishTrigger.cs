using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class FinishTrigger : MonoBehaviour
    {
       // [SerializeField] private SpaceShip spaceShip;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip spaceShip = collision.GetComponentInParent<SpaceShip>();
            if(spaceShip!=null)
            spaceShip.gameObject.SetActive(false);
        }
    }
}