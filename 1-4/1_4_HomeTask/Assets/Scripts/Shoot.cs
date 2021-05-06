using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //позиция выстрела
    public Transform startPos;

    //способы стрельбы
    delegate void Shooting();
    Shooting shoot;

    void Start()
    {
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
                shoot = ShootGun;
                break;
            case "Granate":
                shoot = ShootGranate;
                break;
            case "Ball":
                shoot = ShootBall;
                break;
            default:
                break;
        }
    }

    //при отходе от стенда убрать снаряды
    public void OnTriggerExit(Collider other)
    {
        shoot = null;
    }

    //высрел пулей
    public void ShootGun()
    {
        GameObject projGO = BulletManager.instance.GetPooledObject(BulletManager.Projectiles.bullets);
        AudioManager.instance.PlaySound(AudioManager.Sounds.bulletStart, gameObject.transform.position);

        projGO.transform.position = startPos.position;
        projGO.transform.rotation = startPos.rotation;
        projGO.SetActive(true);
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.AddForce(projGO.transform.forward*2000, ForceMode.Force);
    }

    //бросок гранаты
    public void ShootGranate()
    {
        GameObject projGO = BulletManager.instance.GetPooledObject(BulletManager.Projectiles.granates);

        projGO.transform.position = startPos.position;
        projGO.transform.rotation = startPos.rotation;
        projGO.SetActive(true);
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.zero;
        rigidB.AddForce((projGO.transform.forward+projGO.transform.up) * 5, ForceMode.Impulse);
    }

    //бросок мячика
    public void ShootBall()
    {
        GameObject projGO = BulletManager.instance.GetPooledObject(BulletManager.Projectiles.balls);

        projGO.transform.position = startPos.position;
        projGO.transform.rotation = startPos.rotation;
        projGO.SetActive(true);
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

        rigidB.AddForce((projGO.transform.forward + projGO.transform.up) * 5, ForceMode.Impulse);
    }
}
