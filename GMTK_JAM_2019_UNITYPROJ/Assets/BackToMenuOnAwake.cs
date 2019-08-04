using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenuOnAwake : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<SceneTransitionManager>().LoadScene("MainMenu");
    }
}
