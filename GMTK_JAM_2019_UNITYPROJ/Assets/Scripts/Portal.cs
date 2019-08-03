using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal destinationPortal;
    bool canTeleport = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canTeleport)
        {
            other.transform.position = destinationPortal.transform.position;
            destinationPortal.canTeleport = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canTeleport = true;
    }

}
