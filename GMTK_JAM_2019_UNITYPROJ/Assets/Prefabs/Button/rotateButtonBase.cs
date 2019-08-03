using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateButtonBase : MonoBehaviour
{
    [SerializeField] Vector3 localAxis;
    [SerializeField] float speed = 10f;
    Vector3 axis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        axis = transform.localToWorldMatrix * localAxis;

        transform.RotateAround(transform.position, axis, speed * Time.deltaTime);
    }
}
