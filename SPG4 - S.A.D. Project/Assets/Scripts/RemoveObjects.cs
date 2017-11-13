using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RemoveObjects : MonoBehaviour
{
    [Tooltip("Restart level if players trigger on enter.")]
    public bool RestartLevel = false;

    /// <summary>
    /// Checks if object is player. If object is player, restart level, otherwise destroy object.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && RestartLevel == true )
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        if (collision.gameObject.name.Contains("Eyeball") && RestartLevel == true)
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        else
        {
            GameObject.Destroy(collision.gameObject);
        }

    }
}
