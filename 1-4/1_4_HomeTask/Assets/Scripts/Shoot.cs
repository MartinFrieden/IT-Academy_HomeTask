using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{


    //cписок снарядов
    public List<GameObject> projectiles;
    //текущий снаряд
    GameObject currentProjectile;
    //позиция выстрела
    public Transform startPos;

    //способы стрельбы
    delegate void Shooting();
    Shooting shoot;

    void Start()
    {
        currentProjectile = null;
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            shoot?.Invoke();
        }
    }

    //смена снаряда при приближении к разным стендам
    public void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Gun":
                currentProjectile = projectiles[0];
                shoot += ShootGun;
                Debug.Log(currentProjectile.name);
                break;
            case "Granate":
                currentProjectile = projectiles[1];
                shoot += ShootGranate;
                Debug.Log(currentProjectile.name);
                break;
            case "Ball":
                currentProjectile = projectiles[2];
                shoot += ShootBall;
                Debug.Log(currentProjectile.name);
                break;
            default:
                break;
        }
    }

    //при отходе от стенда убрать снаряды
    public void OnTriggerExit(Collider other)
    {
        currentProjectile = null;
        shoot = null;
    }

    //высрел пулей
    public void ShootGun()
    {
        GameObject projGO = BulletManager.instance.GetPooledObject(BulletManager.instance.Bullets);
        GameObject soundGO = AudioManager.instance.GetPooledObject(AudioManager.instance.BulletsSound);

        projGO.transform.position = startPos.position;
        soundGO.transform.position = startPos.position;
        projGO.transform.rotation = startPos.rotation;
        soundGO.GetComponent<SoundControl>().clipToPlayStart = AudioManager.instance.bulletStartSound;
        projGO.SetActive(true);
        soundGO.SetActive(true);

        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.AddForce(projGO.transform.forward*2000, ForceMode.Force);
        Debug.Log("Shoot Gun");
    }

    //бросок гранаты
    public void ShootGranate()
    {
        GameObject projGO = BulletManager.instance.GetPooledObject(BulletManager.instance.Granates);

        projGO.transform.position = startPos.position;
        projGO.transform.rotation = startPos.rotation;
        projGO.SetActive(true);

        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.zero;
        rigidB.AddForce((projGO.transform.forward+projGO.transform.up) * 5, ForceMode.Impulse);

        Debug.Log("Shoot Granate");
    }

    //бросок мячика
    public void ShootBall()
    {
        GameObject projGO = BulletManager.instance.GetPooledObject(BulletManager.instance.Balls);

        projGO.transform.position = startPos.position;
        projGO.transform.rotation = startPos.rotation;
        projGO.SetActive(true);

        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

        rigidB.AddForce((projGO.transform.forward + projGO.transform.up) * 5, ForceMode.Impulse);
        Debug.Log("Shoot Ball");
    }

}
