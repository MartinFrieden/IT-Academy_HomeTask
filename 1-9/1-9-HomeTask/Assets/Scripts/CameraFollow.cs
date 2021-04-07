using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    // Целевой объект, с позицией Y которого будет
    // синхронизироваться положение камеры.
    public Transform target;
    // Скорость следования за целевым объектом.
    public float followSpeed = 0.5f;
    // Определяет положение камеры после установки
    // позиций всех объектов
    // Наивысшая точка, где может находиться камера.
    public float topLimit = 10.0f;
    // Низшая точка, где может находиться камера.
    public float bottomLimit = -10.0f;

    public float leftLimit = -10f;
    public float rightLimit = 10f;


    private void LateUpdate()
    {
        // Если целевой объект определен...
        if (target != null)
        {
            // Получить его позицию
            Vector3 newPosition = this.transform.position;

            // Определить, где камера должна находиться
            newPosition.y = Mathf.Lerp(newPosition.y, target.position.y, followSpeed);
            newPosition.x = Mathf.Lerp(newPosition.x, target.position.x, followSpeed);

            // Предотвратить выход позиции за граничные точки
            newPosition.y = Mathf.Min(newPosition.y, topLimit);
            newPosition.y = Mathf.Max(newPosition.y, bottomLimit);
            newPosition.x = Mathf.Max(newPosition.x, leftLimit);
            newPosition.x = Mathf.Min(newPosition.x, rightLimit);

            // Обновить местоположение
            transform.position = newPosition;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 topPoint =
        new Vector3(this.transform.position.x,
        topLimit, this.transform.position.z);
        Vector3 bottomPoint =
        new Vector3(this.transform.position.x,
        bottomLimit, this.transform.position.z);

        Vector3 leftPoint =
        new Vector3(leftLimit,
        this.transform.position.y, this.transform.position.z);

        Vector3 rightPoint =
        new Vector3(rightLimit,
        this.transform.position.y, this.transform.position.z);


        Gizmos.DrawLine(topPoint, bottomPoint);

        Gizmos.DrawLine(leftPoint, rightPoint);
    }
}
