using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField][Tooltip("Velocidad de movimiento horizontal")] float horizontalSpeed = 2.0f;
    [Space]
    [SerializeField][Tooltip("Velocidad del JetPack")] float jetPackSpeed = 4.0f;
    [SerializeField][Tooltip("Segundos que dura el combustible del JetPack")] float jetPackFuelTime = 6.0f;
    [Space]
    [SerializeField] float rotationMaxAngle = 20f;
    [SerializeField] float rotationSpeed = 1f;
    
    float jetPackCurrentFuelTime;

    float jetPackStartTime = 0f;
    float jetPackElapsedTime = 0f;

    float defaultMass = 0f;

    Rigidbody rigidbody;

    SurfboardController surfboard;

    void Start()
    {
        gameObject.tag = "Player";

        surfboard = FindObjectOfType<SurfboardController>();

        rigidbody = GetComponent<Rigidbody>();

        defaultMass = rigidbody.mass;

        jetPackCurrentFuelTime = jetPackFuelTime;
    }

    void Update()
    {
        HorizontalMovement();
        JetPackMovement();
    }

    void HorizontalMovement()
    {
        rigidbody.velocity += (Input.GetAxisRaw("Horizontal") * transform.right * horizontalSpeed) * Time.deltaTime;

        if(Input.GetAxisRaw("Horizontal") > 0)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, -rotationMaxAngle, 0f), rotationSpeed);

        else if (Input.GetAxisRaw("Horizontal") < 0)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, rotationMaxAngle, 0f), rotationSpeed);

        else transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 0f), rotationSpeed);

    }

    void JetPackMovement()
    {
        if (Input.GetButtonDown("Jump")) jetPackStartTime = Time.time;

        if (Input.GetButton("Jump"))
        {
            jetPackElapsedTime = Time.time - jetPackStartTime;
            
            if (jetPackElapsedTime < jetPackCurrentFuelTime)
                rigidbody.velocity += transform.up * jetPackSpeed * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jetPackCurrentFuelTime -= jetPackElapsedTime;
            jetPackElapsedTime = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Surfboard")
        {
            jetPackCurrentFuelTime = jetPackFuelTime;
            rigidbody.mass = 0f;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Surfboard") rigidbody.mass = defaultMass;
    }
}