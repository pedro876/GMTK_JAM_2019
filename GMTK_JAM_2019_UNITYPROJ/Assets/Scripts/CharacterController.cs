using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField][Tooltip("Velocidad de movimiento horizontal")] float horizontalSpeed = 2.0f;

    [SerializeField][Tooltip("Velocidad del JetPack")] float jetPackSpeed = 4.0f;
    [SerializeField][Tooltip("Segundos que dura el combustible del JetPack")] float jetPackFuelTime = 6.0f;

    float jetPackCurrentFuelTime;

    float jetPackStartTime = 0f;
    float jetPackElapsedTime = 0f;

    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameObject.tag = "Player";

        jetPackCurrentFuelTime = jetPackFuelTime;
    }

    void FixedUpdate()
    {
        HorizontalMovement();
        JetPackMovement();
    }

    void HorizontalMovement()
    {
        rigidbody.velocity += (Input.GetAxisRaw("Horizontal") * transform.right * horizontalSpeed) * Time.deltaTime;
    }

    void JetPackMovement()
    {
        if (Input.GetButtonDown("Jump")) jetPackStartTime = Time.time;

        if (Input.GetButton("Jump"))
        {
            jetPackElapsedTime = Time.time - jetPackStartTime;
            Debug.Log(jetPackElapsedTime);
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
        if (collision.gameObject.tag == "Surfboard") jetPackCurrentFuelTime = jetPackFuelTime;
    }
}
