using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCreditsMusic : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<GameMusic>().GetComponent<AudioSource>().Stop();
    }
}
