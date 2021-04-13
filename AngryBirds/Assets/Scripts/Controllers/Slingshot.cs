using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    public GameObject prefabBird;       //префаб птички
    public GameObject launchPoint;      //точка запуска птички
    public float velocityBird = 10f;    //скорость птички

    public Vector2 launchPos;           //координаты точки запуска
    public GameObject projectile;       //переменная текущей птички
    public bool isReadyToShoot;
    private Rigidbody2D projectileRigid;
    float maxMagnitude;

    LineRenderer _lineRenderer;
    CommonBird _commonBird;

    public Transform leftBranch;
    public Transform rightBranch;

    Vector3[] linePos;

    private void Awake()
    {
        launchPoint.SetActive(false);
        launchPos = launchPoint.transform.position;
        maxMagnitude = GetComponent<CircleCollider2D>().radius;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReadyToShoot)
        {
            //позиция пальца
            Vector2 touchPos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //расстояние между пальцем и точкой запуска
            Vector2 deltaTouch = touchPos2D - launchPos;
            //ограничить "натяжение" рогатки
            if (deltaTouch.magnitude > maxMagnitude)
            {
                deltaTouch.Normalize();
                deltaTouch *= maxMagnitude;
            }
            //переместить птичку в заданную позицию
            Vector2 newPosition = launchPos + deltaTouch;
            projectile.transform.position = newPosition;
            //отпускаем птичку
            if (Input.touchCount == 0)
            {
                linePos = new Vector3[] { leftBranch.position, launchPoint.transform.position, rightBranch.position };
                _lineRenderer.SetPositions(linePos);
                isReadyToShoot = false;
                projectileRigid.isKinematic = false;
                projectileRigid.velocity = -deltaTouch * velocityBird;
                _commonBird.enabled = true;
                projectile = null;
                GameManager.instance.birds.RemoveAt(0);
            }
            linePos = new Vector3[] { leftBranch.position, newPosition, rightBranch.position };
            _lineRenderer.SetPositions(linePos);
        }
        else
        {
            linePos = new Vector3[] { leftBranch.position, launchPoint.transform.position, rightBranch.position };
            _lineRenderer.SetPositions(linePos);
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.birds.Count != 0)
        {
            //активировать птичку
            launchPoint.SetActive(true);
            isReadyToShoot = true;
            projectile = GameManager.instance.birds[0];
            //указываем камере за каким объектом следить
            CameraFollow.target = projectile;
            //получить скрипт поведения птички после ее запуска
            _commonBird = projectile.GetComponent<CommonBird>();
            projectile.transform.position = launchPos;
            projectileRigid = projectile.GetComponent<Rigidbody2D>();
            projectileRigid.isKinematic = true;
        }
        else
        {
            GameManager.instance.endLevel?.Invoke();
        }
    }

    private void OnMouseUp()
    {
         launchPoint.SetActive(false);  
    }
}
