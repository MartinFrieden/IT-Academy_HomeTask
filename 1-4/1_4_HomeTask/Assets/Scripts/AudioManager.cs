using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public int amountToPool = 10;
    public AudioClip bulletStartSound;
    public AudioClip bulletEndSound;
    public AudioClip explosionSound;
    public AudioClip ballSound;



    public GameObject prefabBulletSound;

    List<GameObject> pooledBulletsSounds;

    public List<GameObject> BulletsSound { get { return pooledBulletsSounds; } private set { } }

    void Awake()
    {
        pooledBulletsSounds = new List<GameObject>();

        CreateList(prefabBulletSound, pooledBulletsSounds);
    }

    public GameObject GetPooledObject(List<GameObject> pooledList)
    {
        for (int i = 0; i < pooledList.Count; i++)
        {
            if (!pooledList[i].activeInHierarchy)
            {
                return pooledList[i];
            }
        }
        return null;
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
}
