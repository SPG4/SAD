using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingGlass : MonoBehaviour {

    public GameObject puddle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Instantiate(puddle);
            Destroy(gameObject);
        }
    }
}
