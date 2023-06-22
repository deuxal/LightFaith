using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float rotationSpeed = 10f; // velocidad de rotaci�n del objeto

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition; // obtenemos la posici�n del mouse en la pantalla
        mousePosition.z = 10; // establecemos la distancia desde la c�mara al objeto
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition); // convertimos la posici�n del mouse a un punto en el mundo

        Vector3 direction = target - transform.position; // obtenemos la direcci�n hacia el punto del mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // calculamos el �ngulo de rotaci�n
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); // creamos una rotaci�n en el �ngulo calculado
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime); // rotamos el objeto suavemente hacia la direcci�n del mouse
    }
}
