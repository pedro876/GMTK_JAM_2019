using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] ParticleSystem [] jetPackVFXs;
    [Space]

    [HideInInspector] public static bool canWin = false;
    [HideInInspector] public static bool canMove = true;

    [SerializeField][Tooltip("Velocidad de movimiento horizontal")] float horizontalSpeed = 2.0f;
    [SerializeField] [Tooltip("Velocidad mínima que debe llevar para empezar a girar")] float minRotationInput = 0.5f;
    [Space]

    [SerializeField][Tooltip("Velocidad del JetPack")] float jetPackSpeed = 4.0f;
    [SerializeField][Tooltip("Segundos que dura el combustible del JetPack")] float jetPackFuelTime = 6.0f;
    [SerializeField] Image jetPackFuelBarUI;
    [Space]

    [SerializeField] float rotationMaxAngle = 20f;
    [SerializeField] float rotationSpeed = 1f;

    float jetPackCurrentFuelTime;
    
    float jetPackStartTime = 0f;
    float jetPackElapsedTime = 0f;

    float fuelBarUIDefaultHeight;
    float fuelBarUICurrentHeight;

    float defaultMass = 0f;

    Rigidbody rigidbody;
    SceneTransitionManager sceneTransition;
    SurfboardController surfboard;


    void Start()
    {
        gameObject.tag = "Player";
        
        surfboard = FindObjectOfType<SurfboardController>();
        rigidbody = GetComponent<Rigidbody>();
        sceneTransition = FindObjectOfType<SceneTransitionManager>();

        fuelBarUIDefaultHeight = jetPackFuelBarUI.rectTransform.sizeDelta.y;

        defaultMass = rigidbody.mass;

        jetPackCurrentFuelTime = jetPackFuelTime;
        fuelBarUICurrentHeight = fuelBarUIDefaultHeight;

        canWin = false;
    }

    void Update()
    {
        if (canMove)
        {
            HorizontalMovement();
            JetPackMovement();
        }
    }

    void HorizontalMovement()
    {
        rigidbody.velocity += (Input.GetAxisRaw("Horizontal") * transform.right * horizontalSpeed) * Time.deltaTime;

        if(Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0f)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, -rotationMaxAngle, 0f), rotationSpeed);

            else if (Input.GetAxis("Horizontal") < 0f)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, rotationMaxAngle, 0f), rotationSpeed);

            else transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 0f), rotationSpeed);
        }

        else
        {
            if (rigidbody.velocity.x > minRotationInput)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, -rotationMaxAngle, 0f), rotationSpeed);

            else if (rigidbody.velocity.x < -minRotationInput)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, rotationMaxAngle, 0f), rotationSpeed);

            else transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 0f), rotationSpeed);
        }
    }

    void JetPackMovement()
    {
        if (Input.GetButtonDown("Jump")) jetPackStartTime = Time.time;

        if (Input.GetButton("Jump"))
        {
            jetPackElapsedTime = Time.time - jetPackStartTime;

            if (jetPackElapsedTime < jetPackCurrentFuelTime)
            {
                foreach (ParticleSystem jetPackVFX in jetPackVFXs)
                    if (!jetPackVFX.isPlaying) jetPackVFX.Play();

                rigidbody.velocity += transform.up * jetPackSpeed * Time.deltaTime;
                UpdateFuelUI();
            }

            else foreach (ParticleSystem jetPackVFX in jetPackVFXs) jetPackVFX.Stop();
        }

        if (Input.GetButtonUp("Jump"))
        {
            foreach (ParticleSystem jetPackVFX in jetPackVFXs)
                jetPackVFX.Stop();

            jetPackCurrentFuelTime -= jetPackElapsedTime;
            fuelBarUICurrentHeight = jetPackFuelBarUI.rectTransform.sizeDelta.y;
        }
    }

    void UpdateFuelUI()
    {
        jetPackFuelBarUI.rectTransform.sizeDelta = new Vector2(jetPackFuelBarUI.rectTransform.sizeDelta.x, Mathf.Lerp(fuelBarUICurrentHeight, 0f, jetPackElapsedTime/jetPackCurrentFuelTime));
    }

    void RechargeJetPack()
    {
        jetPackCurrentFuelTime = jetPackFuelTime;

        jetPackFuelBarUI.rectTransform.sizeDelta = new Vector2(jetPackFuelBarUI.rectTransform.sizeDelta.x, fuelBarUIDefaultHeight);
        fuelBarUICurrentHeight = jetPackFuelBarUI.rectTransform.sizeDelta.y;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Surfboard")
        {
            RechargeJetPack();

            rigidbody.mass = 0f;

            if (canWin)
            {
                sceneTransition.LoadNextScene();
            }
        }

        else if (collision.gameObject.tag == "KillsPlayer") DeathSequence();
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Surfboard") rigidbody.mass = defaultMass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Portal") RechargeJetPack();

        else if(other.gameObject.tag == "Goal")
        {
            canWin = true;
            Destroy(other.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "KillsPlayer") DeathSequence();
    }

    void DeathSequence()
    {
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        Instantiate(deathVFX, transform.position, Quaternion.identity);

        sceneTransition.ReloadScene();

        FindObjectOfType<AudioController>().playDeath();

        Destroy(surfboard.gameObject);
        Destroy(gameObject);
        
    }

}