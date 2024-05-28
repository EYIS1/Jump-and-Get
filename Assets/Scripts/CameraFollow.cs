using UnityEngine;
using System.Collections.Generic;
using System.Net.NetworkInformation;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.25f;
    public float xOffset = 0f;
    public float towerHeightOffset = 12f;
    public int variable = 0;
    private PlayerController playerController;

    private int previousVariable = 0;
    private Dictionary<int, bool> towerCameraStates = new Dictionary<int, bool>(); // Используем словарь для хранения состояний камеры на башнях

    //
    public float maxApproachDistance = 1.0f; // Максимальное допустимое приближение
    //

    private void Start()
    {
        previousVariable = variable;
        playerController = Camera.main.GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        Vector3 nextTowerPosition = new Vector3(target.position.x, target.position.y + towerHeightOffset, 0);

        Vector3 desiredPosition = new Vector3(target.position.x + xOffset, transform.position.y, 0);

        // Если значение переменной изменилось и камера еще не поднималась на этой башне
        if (variable != previousVariable)
        {
            if (!towerCameraStates.ContainsKey(variable)) // Проверяем, поднималась ли камера на этой башне
            {
                desiredPosition.y = nextTowerPosition.y;
                towerCameraStates[variable] = true; // Устанавливаем флаг для башни
            }
            previousVariable = variable;
        }

        //transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //
        // Вычисляем направление к целевому объекту
        Vector3 directionToTarget = target.position - transform.position;

        // Вычисляем расстояние до целевого объекта
        float distanceToTarget = directionToTarget.magnitude;

        // Если расстояние меньше максимального приближения, прекращаем движение
        if (distanceToTarget <= maxApproachDistance)
        {
            return;
        }

        // Вычисляем новую позицию объекта с учетом скорости движения
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, 50f * Time.deltaTime);

        // Перемещаем объект к новой позиции
        transform.position = newPosition;
        //

        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, -10f), smoothSpeed);
    }
}
