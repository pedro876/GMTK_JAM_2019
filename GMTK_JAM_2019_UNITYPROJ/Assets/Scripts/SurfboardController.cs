using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfboardController : MonoBehaviour
{
    Rigidbody rigidbody;

    [SerializeField][Tooltip("Fuerza con la que la tabla va hacia el cursor")] float force = 1.0f;
    [SerializeField][Tooltip("Porcentaje de velocidad que pierde la tabla cada frame")] float decelerationFactor = 0.1f;
    //[SerializeField][Tooltip("Velocidad tope de la tabla")] float topSpeed = 10;

    Vector3 mouseVector = Vector3.zero;

    Camera camera;
    BoxCollider boxCollider;

    void Start()
    {
        gameObject.tag = "Surfboard";

        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        camera = Camera.main;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        mouseVector = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);

        rigidbody.AddForce(mouseVector * force * Time.deltaTime, ForceMode.Acceleration);

        rigidbody.velocity -= rigidbody.velocity * decelerationFactor;

        /*if (rigidbody.velocity.magnitude > topSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * topSpeed;
        }
        */

    }
}