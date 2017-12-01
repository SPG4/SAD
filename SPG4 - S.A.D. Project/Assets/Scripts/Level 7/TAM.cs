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

    void Update()
    {
        TAMMovement();
    }

    /// <summary>
    /// Controls the movement of TAM
    /// </summary>
    void TAMMovement()
    {
        currentPosition = Mathf.Clamp01(currentPosition + speed * Time.deltaTime * direction);

        //Changes directions of TAM if currenPosition has either reached 0 or 1
        if (direction == 1.0f && currentPosition > 0.99f)
            direction = -1.0f;
        if (direction == -1.0f && currentPosition < 0.01f)
            direction = 1.0f;

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, currentPosition);
        transform.rotation = Quaternion.identity;

        if (currentPosition >= 0.99f)
            hasPaused = false;

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
}
