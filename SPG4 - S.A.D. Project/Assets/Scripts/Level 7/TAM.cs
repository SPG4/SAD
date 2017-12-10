using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAM : MonoBehaviour
{
    bool hasPaused = true;
    float timer;

    public Transform startPoint;
    public Transform endPoint;

    float speed = 0.1f;
    private float direction = 1.0f;
    private float currentPosition = 0.0f;

    public float health = 3;

    public new SpriteRenderer renderer;
    bool isDead;

    GameObject dialogue;
    GameObject player;

    public float speedTime;
    public float maxRotation;

    void Start()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
        dialogue = GameObject.FindGameObjectWithTag("DialogueManager");

        Dialogue();
    }
    void Update()
    {
        TAMMovement();

        if (transform.GetChild(0).gameObject.tag == "Tentacle")
            transform.GetChild(0).localEulerAngles = new Vector3(0, 0, -Mathf.PingPong(Time.time * speedTime, maxRotation));
    }

    /// <summary>
    /// Controls the movement of TAM
    /// </summary>
    void TAMMovement()
    {
        currentPosition = Mathf.Clamp01(currentPosition + speed * Time.deltaTime * direction);

        if (currentPosition >= 0.99f)
            hasPaused = false;

        //Changes directions of TAM if currenPosition has either reached 0 or 1
        if (direction == 1.0f && currentPosition > 0.99f)
        {
            direction = -1.0f;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
        }
        if (direction == -1.0f && currentPosition < 0.01f)
        {
            direction = 1.0f;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
        }

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, currentPosition);
        transform.rotation = Quaternion.identity;


        //If TAM has either reached the startpoint or endpoint, the pause-couroutine will start
        if (currentPosition >= 0.99f && !hasPaused || currentPosition <= 0.01f && !hasPaused)
        {
            StartCoroutine(PauseMovement());
            hasPaused = true;
        }
    }

    public void ApplyDamage()
    {
        health -= 1;
        StartCoroutine(Blink(0.2f, 0.2f));

        if (health <= 0)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.SendMessage("TamDead");
            player = GameObject.FindGameObjectWithTag("Player2");
            player.SendMessage("TamDead");

            speed = 0;
            isDead = true;
           
        }
    }

    void Dialogue()
    {
        dialogue.SendMessage("AddText", "Green: What is that thing?");
        dialogue.SendMessage("AddText", "Blue: I think that’s what we’re supposed to fight… but it’s like 10 times our size!");
        dialogue.SendMessage("AddText", "Green: We’ll have to outsmart it, it’s our only chance! Maybe we can find a way to use the environment and our new skills against it?");
        dialogue.SendMessage("AddText", "Blue: Good thinking!");
    }

    /// <summary>
    /// Changes TAM's speed for two seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator PauseMovement()
    {
        speed = 0;
        yield return new WaitForSeconds(2f);
        speed = 0.1f;
        hasPaused = false;
    }

    IEnumerator Blink(float duration, float blinkTime)
    {
        while (duration > 0f)
        {
            duration -= Time.deltaTime;

            //toggles the renderer
            renderer.enabled = !renderer.enabled;
            
            yield return new WaitForSeconds(blinkTime);
        }

        renderer.enabled = true;
        if (isDead)
            Destroy(gameObject);
    }
}
