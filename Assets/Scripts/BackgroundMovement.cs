using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public Camera mainCamera; // ������ �� ������� ������
    public float parallaxFactor = 1.0f; // ������ ��������� (��������� ������ �������� ���)

    private Vector3 initialCameraPosition;
    private Vector3 initialPosition;

    private void Start()
    {
        initialCameraPosition = mainCamera.transform.position;
        initialPosition = transform.position;
    }

    private void Update()
    {
        // ���������, ��������� ����� ����������� ��� �� �����������
        float deltaX = (mainCamera.transform.position.x - initialCameraPosition.x) * parallaxFactor;
        float deltaY = (mainCamera.transform.position.y - initialCameraPosition.y) * parallaxFactor;

        // ������������� ����� ������� ��� ����
        Vector3 newPosition = initialPosition + new Vector3(deltaX, deltaY, 0f);
        transform.position = newPosition;
    }

}
