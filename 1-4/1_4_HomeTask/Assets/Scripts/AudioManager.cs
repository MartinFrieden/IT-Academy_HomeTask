using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public enum Sounds
    {
        bulletStart,
        bulletEnd,
        explosion,
        ballTouch,
        barrel
    }

    [SerializeField] int amountToPool = 10;
    [SerializeField] AudioClip bulletStartSound;
    [SerializeField] AudioClip bulletEndSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip ballSound;
    [SerializeField] AudioClip barrelSound;

    Dictionary<Sounds, AudioClip> soundsDict;

    [SerializeField] GameObject prefabBulletSound;

    List<GameObject> pooledBulletsSounds;

    void Awake()
    {
        soundsDict = new Dictionary<Sounds, AudioClip>
        {
            { Sounds.bulletStart, bulletStartSound },
            { Sounds.bulletEnd, bulletEndSound },
            { Sounds.explosion, explosionSound },
            { Sounds.ballTouch, ballSound },
            { Sounds.barrel, barrelSound}
        };

        pooledBulletsSounds = new List<GameObject>();

        CreateList(prefabBulletSound, pooledBulletsSounds);
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledBulletsSounds.Count; i++)
        {
            if (!pooledBulletsSounds[i].activeInHierarchy)
            {
                pooledBulletsSounds[i].GetComponent<SoundEvent>().OnRelease += Release;
                pooledBulletsSounds[i].transform.SetParent(null);
                return pooledBulletsSounds[i];
            }
        }
        return null;
    }

    void Release(GameObject go)
    {
        go.GetComponent<SoundEvent>().OnRelease -= Release;
        go.SetActive(false);
        go.transform.SetParent(transform);
        
        
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

    public void PlaySound(Sounds type, Vector3 pos)
    {
        if (GetPooledObject() == null)
        {
            AddInstance();
        }
        GameObject go = GetPooledObject();
        go.transform.position = pos;
        go.SetActive(true);
        go.GetComponent<AudioSource>().PlayOneShot(soundsDict[type]);

    }

    void AddInstance()
    {
        var go = Instantiate<GameObject>(prefabBulletSound, transform.position, Quaternion.identity);
        go.transform.SetParent(transform);
        go.SetActive(false);
        pooledBulletsSounds.Add(go);
    }
}
