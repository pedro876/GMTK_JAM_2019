using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField] GameObject objectToIgnore;

    void Awake()
    {
        Collider[] colliders = objectToIgnore.GetComponents<Collider>();

        foreach (Collider collider in colliders)
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collider);
    }
}