using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Renderer>().material.color = Color.red;

        //turn down music
    }
}
