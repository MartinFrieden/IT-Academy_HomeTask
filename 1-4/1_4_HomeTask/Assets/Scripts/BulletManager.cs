using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
   public enum Projectiles
   {
       bullets,
       granates,
       balls
   }

    
    public int amountToPool = 10;

    public GameObject prefabBullet;
 
    public GameObject prefabGranate;

    public GameObject prefabBall;

    Dictionary<Projectiles, List<GameObject>> Pools;

    // Start is called before the first frame update
    private void Awake()
    {
        Pools = new Dictionary<Projectiles, List<GameObject>>
        {
            { Projectiles.bullets, new List<GameObject>() },
            { Projectiles.granates, new List<GameObject>() },
            { Projectiles.balls, new List<GameObject>() }
        };

        CreateList(prefabBullet, Pools[Projectiles.bullets]);
        CreateList(prefabGranate, Pools[Projectiles.granates]);
        CreateList(prefabBall, Pools[Projectiles.balls]);      
    }

    public GameObject GetPooledObject(Projectiles type)
    {
        if (GetPooledObject(Pools[type]) == null)
        {
            Debug.Log("Больше нет");
            AddInstance(GetPrefab(type), Pools[type]);
        }
        return GetPooledObject(Pools[type]);
    }

    GameObject GetPrefab(Projectiles type)
    {
        switch (type)
        {
            case Projectiles.bullets:
                return prefabBullet;
             
            case Projectiles.granates:
                return prefabGranate;
     
            case Projectiles.balls:
                return prefabBall;
      
            default:
                Debug.LogError("ERRROR");
                return prefabBall;
        }
    }

    void AddInstance(GameObject prefab, List<GameObject> pooledList)
    {
        var go = Instantiate<GameObject>(prefab, transform.position, Quaternion.identity);
        go.transform.SetParent(transform);
        go.SetActive(false);
        pooledList.Add(go);
    }

    GameObject GetPooledObject(List<GameObject> pooledList)
    {
        for (int i = 0; i < pooledList.Count; i++)
        {
            if (!pooledList[i].activeInHierarchy)
            {
                pooledList[i].GetComponent<ProjectileEvent>().OnRelease += Release;
                pooledList[i].transform.SetParent(null);
                return pooledList[i];
            }
        }
        return null;
    }

    public void Release(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform);
        go.GetComponent<ProjectileEvent>().OnRelease -= Release;
    }

    void CreateList(GameObject prefab, List<GameObject> pooledList)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            var go = Instantiate(prefab, transform.position, Quaternion.identity);
            go.transform.SetParent(transform);          
            pooledList.Add(go);
            go.SetActive(false);
        }
    }
}
