using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfboardController : MonoBehaviour
{
    Rigidbody rigidbody;

    [SerializeField][Tooltip("Fuerza con la que la tabla va hacia el cursor")] float force = 1.0f;
    [SerializeField][Tooltip("Distancia minima que debe haber entre el cursor y el centro de la tabla para que empieze a pararse")] float stopThreshold = 1.0f;
    [SerializeField][Tooltip("Porcentaje de velocidad que pierde la tabla cada frame")] float decelerationFactor = 0.1f;
    [SerializeField] [Tooltip("Procentaje de velocidad que pierde la tabla cuando llega al centro")] float brakeFactor = 0.1f;

    //Vector3 lastMousePosition = Vector3.zero;
    //Vector3 mousePosition;
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
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = gameObject.transform.position.z;
    }

    private void FixedUpdate()
    {
        Debug.Log(Input.GetAxis("Mouse X") + " " + Input.GetAxis("Mouse Y"));

        mouseVector = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);

        /*if ((transform.position - mousePosition).magnitude > stopThreshold)
            rigidbody.AddForce((mousePosition - transform.position).normalized * force * Time.deltaTime, ForceMode.Acceleration);

        else rigidbody.velocity -= rigidbody.velocity * brakeFactor;

        rigidbody.velocity -= rigidbody.velocity * decelerationFactor;*/

        rigidbody.AddForce(mouseVector * force * Time.deltaTime, ForceMode.Acceleration);

        rigidbody.velocity -= rigidbody.velocity * decelerationFactor;

        //lastMousePosition = mousePosition;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, stopThreshold);
    }
}