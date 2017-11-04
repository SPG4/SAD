using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

    public GameObject spawnObject;
    bool hasHappened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasHappened)
        {
            GameObject spawnedObject = Instantiate(spawnObject);
            hasHappened = true;
        }
        
    }
}
