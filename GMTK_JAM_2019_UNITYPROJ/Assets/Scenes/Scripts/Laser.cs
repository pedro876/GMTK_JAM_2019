using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject laserBase;
    [SerializeField] float timePerGrowTick = 0.05f;
    [SerializeField] Vector2 growPerTick = new Vector2(0.05f, 0f);

    bool canGrow = true;

    void Start()
    {
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    { 
        if(canGrow)
        {
            laserBase.transform.localScale = new Vector3(laserBase.transform.localScale.x + growPerTick.x, laserBase.transform.localScale.y + growPerTick.y, laserBase.transform.localScale.z);
        }
        yield return new WaitForSecondsRealtime(timePerGrowTick);
        StartCoroutine(Grow());
    }

    void OnCollisionEnter(Collision collision)
    {
        canGrow = false;
    }
}