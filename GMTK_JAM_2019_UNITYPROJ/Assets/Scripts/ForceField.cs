using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField] GameObject objectToIgnore;

    void Awake()
    {
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), objectToIgnore.GetComponent<Collider>());
    }
}