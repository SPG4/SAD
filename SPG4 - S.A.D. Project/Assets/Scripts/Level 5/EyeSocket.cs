using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSocket : MonoBehaviour {
    public string objectNameToTrigger = "";
    public Sprite changeToTexture = null;
    private bool locked = false;
    Level5 lvl5 = new Level5();

    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (changeToTexture != null)
        {
            if (collision.gameObject.name == "Eyeball" && locked == false)
            {
                locked = true;
                this.gameObject.GetComponentInParent<SpriteRenderer>().sprite = changeToTexture;
                collision.gameObject.SetActive(false);
                lvl5.IncrementFromSocket();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
