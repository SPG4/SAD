using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAMMovement : MonoBehaviour
{
    bool hasPaused;
    float timer;

    public Transform startPoint;
    public Transform endPoint;

    float speed = 0.15f;
    private float direction = 1.0f;
    private float currentPosition = 0.0f;

    void Update()
    {
        currentPosition = Mathf.Clamp01(currentPosition + speed * Time.deltaTime * direction);

        if (direction == 1.0 && currentPosition > 0.99)
            direction = -1.0f;
        if (direction == -1.0 && currentPosition < 0.01)
            direction = 1.0f;

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, currentPosition);

        //If TAM have either reached the startpoint or endpoint, the pause-couroutine will start
        if (currentPosition >= 0.99 && !hasPaused || currentPosition <= 0.01 && !hasPaused)
        {
            StartCoroutine(PauseMovement());
            hasPaused = true;
        }
    }

    /// <summary>
    /// Pauses TAM's movement for two seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator PauseMovement()
    {
        speed = 0;
        yield return new WaitForSeconds(2f);
        speed = 0.15f;
        hasPaused = false;
    }
}
