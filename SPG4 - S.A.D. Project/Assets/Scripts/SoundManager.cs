using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip jumpSound;
    static AudioSource audiosrc;
	// Use this for initialization
	void Start ()
    {
        jumpSound = Resources.Load<AudioClip>("spin_jump");

        audiosrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlaySound(string clip)
    {
        
        switch (clip)
        {
            case "spin_jump":
                audiosrc.PlayOneShot(jumpSound);
                    break;
        }
    }
}
//SoundManager.PlaySound("jumpSound");
