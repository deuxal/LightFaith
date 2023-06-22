using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float rotationSpeed = 10f; // velocidad de rotación del objeto

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition; // obtenemos la posición del mouse en la pantalla
        mousePosition.z = 10; // establecemos la distancia desde la cámara al objeto
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition); // convertimos la posición del mouse a un punto en el mundo

        Vector3 direction = target - transform.position; // obtenemos la dirección hacia el punto del mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // calculamos el ángulo de rotación
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); // creamos una rotación en el ángulo calculado
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime); // rotamos el objeto suavemente hacia la dirección del mouse
    }
}
