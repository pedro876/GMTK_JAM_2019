using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] [Tooltip("Evento que se lanza cuando pulsas el botón")] UnityEvent OnPress;
    [SerializeField] [Tooltip("Evento que se lanza cuando mantienes el botón")] UnityEvent OnHold;
    [SerializeField] [Tooltip("Evento que se lanza cuando levantas el botón")] UnityEvent OnUp;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Button")
        {
            Debug.Log("trigger");
            OnPress.Invoke();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Button")
        {
            Debug.Log("Hold");
            OnHold.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Button")
        {
            Debug.Log("Exit");
            OnUp.Invoke();
        }
    }
}
