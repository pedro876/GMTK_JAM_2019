using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class SnapToGrid : MonoBehaviour
{
    [SerializeField] int gridSize = 10; 

    void Update()
    {
        SnapPositionToGRid();
    }

    private void SnapPositionToGRid()
    {
        transform.position = new Vector3(

                Mathf.RoundToInt(transform.position.x / gridSize) * gridSize,
                Mathf.RoundToInt(transform.position.y / gridSize) * gridSize,
                0f
            );
    }
}