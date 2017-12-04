using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour {

    public Transform target;
    private AudioSource TeleportSound;

	// Use this for initialization
	void Start () {
        target = this.gameObject.transform.GetChild(0);
        TeleportSound = gameObject.GetComponent<AudioSource>();
	}

    private void PlayTeleportSound()
    {
        TeleportSound.Play();
    }

    // Update is called once per frame
    void Update () {

        //Debug.Log(target.transform.position);
		
	}
}
