using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal destinationPortal;
    [SerializeField] ParticleSystem teleportVFX;
    bool canTeleport = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canTeleport)
        {
            if (destinationPortal != null)
            {
                other.transform.position = destinationPortal.transform.position;
                destinationPortal.canTeleport = false;
                if (!destinationPortal.teleportVFX.isPlaying) destinationPortal.teleportVFX.Play();
                if (!destinationPortal.GetComponent<AudioSource>().isPlaying) destinationPortal.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canTeleport = true;
    }

}