using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private readonly Vector3 offset = new Vector3(0f, 0.5f, -10f);
    private readonly float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        // Mantén las posiciones X y Z actuales de la cámara
        Vector3 currentPos = transform.position;
        
        // Calcula la nueva posición Y basada en la posición Y del objetivo
        float targetY = target.position.y + offset.y;

        // Crea una nueva posición con X y Z actuales y la nueva Y
        Vector3 targetPosition = new Vector3(currentPos.x, targetY, currentPos.z);

        // Suaviza el movimiento solo en el eje Y
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
