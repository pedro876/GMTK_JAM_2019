using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfboardController : MonoBehaviour
{
    Rigidbody rigidbody;

    [SerializeField][Tooltip("Fuerza con la que la tabla va hacia el cursor")] float force = 1.0f;
    [SerializeField][Tooltip("Distancia minima que debe haber entre el cursor y el centro de la tabla para que empieze a pararse")] float stopThreshold = 1.0f;
    [SerializeField][Tooltip("Porcentaje de velocidad que pierde la tabla cada frame")] float decelerationFactor = 0.1f;

    Vector3 mousePosition;
    Camera camera;
    BoxCollider boxCollider;

    void Start()
    {
        gameObject.tag = "Surfboard";

        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        camera = Camera.main;
    }

    void Update()
    {
        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = gameObject.transform.position.z;
    }

    private void FixedUpdate()
    {
        if ((transform.position - mousePosition).magnitude > stopThreshold)
            rigidbody.AddForce((mousePosition - transform.position).normalized * force * Time.deltaTime, ForceMode.Acceleration);

        rigidbody.velocity -= rigidbody.velocity * decelerationFactor;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, stopThreshold);
    }
}