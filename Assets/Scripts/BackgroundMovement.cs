using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public Camera mainCamera; // —сылка на главную камеру
    public float parallaxFactor = 1.0f; // ‘актор парадокса (насколько быстро движетс€ фон)

    private Vector3 initialCameraPosition;
    private Vector3 initialPosition;

    private void Start()
    {
        initialCameraPosition = mainCamera.transform.position;
        initialPosition = transform.position;
    }

    private void Update()
    {
        // ¬ычисл€ем, насколько нужно переместить фон по горизонтали
        float deltaX = (mainCamera.transform.position.x - initialCameraPosition.x) * parallaxFactor;
        float deltaY = (mainCamera.transform.position.y - initialCameraPosition.y) * parallaxFactor;

        // ”станавливаем новую позицию дл€ фона
        Vector3 newPosition = initialPosition + new Vector3(deltaX, deltaY, 0f);
        transform.position = newPosition;
    }

}
