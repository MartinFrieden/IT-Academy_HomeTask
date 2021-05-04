using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    public int amountToPool = 10;
    public GameObject prefabBullet;
    public GameObject prefabGranate;
    public GameObject prefabBall;
    List<GameObject> pooledBullets;
    List<GameObject> pooledGranates;
    List<GameObject> pooledBalls;

    public List<GameObject> Bullets { get { return pooledBullets; } private set { } }
    public List<GameObject> Granates { get { return pooledGranates; } private set { } }
    public List<GameObject> Balls { get { return pooledBalls; } private set { } }

    // Start is called before the first frame update
    void Awake()
    {
        pooledBullets = new List<GameObject>();
        pooledGranates = new List<GameObject>();
        pooledBalls = new List<GameObject>();

        CreateList(prefabBullet, pooledBullets);
        CreateList(prefabGranate, pooledGranates);
        CreateList(prefabBall, pooledBalls);
    }

    public GameObject GetPooledObject(List<GameObject> pooledList)
    {
        for (int i = 0; i < pooledList.Count; i++)
        {
            if (!pooledList[i].activeInHierarchy)
            {
                pooledList[i].GetComponent<ProjectileEvent>().OnRelease += Release;
                return pooledList[i];
            }
        }
        return null;
    }

    public void Release(GameObject go)
    {
        go.GetComponent<ProjectileEvent>().OnRelease -= Release;
        go.SetActive(false);
    }



    void CreateList(GameObject prefab, List<GameObject> pooledList)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            var go = Instantiate<GameObject>(prefab, transform.position, Quaternion.identity);
            go.transform.SetParent(transform);
            go.SetActive(false);
            pooledList.Add(go);
        }
    }

    private GameObject Reset(GameObject resetObject)
    {
        Debug.Log("Reset Object"+resetObject.name);
        return resetObject;
    }
}
