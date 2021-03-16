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
            shoot();
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
        GameObject projGO = Instantiate<GameObject>(currentProjectile);

        projGO.transform.position = startPos.position;
        projGO.transform.rotation = startPos.rotation;

        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

        rigidB.AddForce(projGO.transform.forward*5000, ForceMode.Force);

        Destroy(projGO, 5);
        Debug.Log("Shoot Gun");
    }

    //бросок гранаты
    public void ShootGranate()
    {
        GameObject projGO = Instantiate<GameObject>(currentProjectile);

        projGO.transform.position = startPos.position;
        projGO.transform.rotation = startPos.rotation;

        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

        rigidB.AddForce((projGO.transform.forward+projGO.transform.up) * 5, ForceMode.Impulse);

        Debug.Log("Shoot Granate");
    }

    //бросок мячика
    public void ShootBall()
    {
        GameObject projGO = Instantiate<GameObject>(currentProjectile);

        projGO.transform.position = startPos.position;
        projGO.transform.rotation = startPos.rotation;

        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

        rigidB.AddForce((projGO.transform.forward + projGO.transform.up) * 5, ForceMode.Impulse);
        Debug.Log("Shoot Ball");
    }

}
