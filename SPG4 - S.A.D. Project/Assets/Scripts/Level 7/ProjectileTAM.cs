using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileTAM : MonoBehaviour
{

    public float time;
    float timer, timer2;
    public Sprite changeToTexture = null;
    float deadlyTime = 0;
    bool outsideTAM = true;
    bool isDeadly = false;
    Rigidbody2D rb;
    Color32 color;
    byte cRed, cGreen, cBlue;

    // Use this for initialization
    void Start()
    {
        timer = time / 255;
        rb = gameObject.GetComponent<Rigidbody2D>();
        cRed = 255;
        cGreen = 255;
        cBlue = 255;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(isDeadly == true)
        {
            Deadly();
            if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                int scene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        // Updates the color
        if (timer2 > timer)
        {
            cGreen -= 1;
            cBlue -= 1;
            timer2 = 0;
        }
        else
        {
            timer2 += Time.deltaTime;
        }
        if (outsideTAM == true)
        {
            UpdateColor();
        }


        // Sets projectile to deadly
        if (time <= 0 && outsideTAM == true)
        {
            isDeadly = true;
            if(deadlyTime > 0.8f)
            {
                Destroy(gameObject);
            }
            else
            {
                deadlyTime += Time.deltaTime;
            }
        }
    }

    void UpdateColor()
    {
        color = new Color32(cRed, cGreen, cBlue, 255);
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    void Deadly()
    {
        rb.bodyType = RigidbodyType2D.Static;
        gameObject.GetComponentInParent<SpriteRenderer>().sprite = changeToTexture;
    }

    void EnterTAM()
    {
        outsideTAM = false;
    }

    void ExitTAM()
    {
        outsideTAM = true;
    }
}
