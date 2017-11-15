using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioListener.volume = 0.3f;
    }
}
