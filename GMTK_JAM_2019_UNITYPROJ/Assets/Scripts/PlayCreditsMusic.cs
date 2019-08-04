using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCreditsMusic : MonoBehaviour
{
    private void Awake()
    {
        GameMusic gameMusic = FindObjectOfType<GameMusic>();

        if(gameMusic != null)
            gameMusic.GetComponent<AudioSource>().Stop();
    }

    private void Update()
    {
        Cursor.visible = true;
    }
}
