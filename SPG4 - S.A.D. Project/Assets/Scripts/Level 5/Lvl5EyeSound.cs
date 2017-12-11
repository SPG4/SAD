using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl5EyeSound : MonoBehaviour {
    public AudioClip playSound;
    public float volume;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Eye"))
        {
            SoundLevelManager.Instance.PlaySingle(playSound);
        }
    }
}
