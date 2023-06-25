using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : SingletonBase<BonusSpawner>
{
    [SerializeField] private GameObject[] m_Prefabs;
    public void ShipWasDestroyed(Transform transform)
    {
        int IsAddBonus = Random.Range(0, 8);
        if(IsAddBonus==0)
        SpawnBonus(transform);
    }
    private void SpawnBonus(Transform transform)
    {
        int index = Random.Range(0, m_Prefabs.Length);
        GameObject bonus = Instantiate(m_Prefabs[index], transform.position, Quaternion.identity);
    }
    

}
