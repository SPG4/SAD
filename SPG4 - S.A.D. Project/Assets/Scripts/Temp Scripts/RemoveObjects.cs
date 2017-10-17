using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RemoveObjects : MonoBehaviour
{
    public bool IsPlayerNoob = false;
    public float playerY = 0.0f;
    // Use this for initialization
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find(collision.gameObject.name).SetActive(false);
    }

    //Update is called once per frame
    void Update()
    {
        if (IsPlayerNoob == true)
        {
            if (GameObject.FindGameObjectWithTag("Player").transform.position.y <= playerY || GameObject.FindGameObjectWithTag("Player2").transform.position.y <= playerY)
            {
                GameObject.FindGameObjectWithTag("Player").SetActive(false);
                SceneManager.LoadScene("Level 1 - Prototype");
            }
        }
    }
}
