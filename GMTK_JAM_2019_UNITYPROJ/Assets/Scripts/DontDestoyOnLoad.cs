using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoyOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}