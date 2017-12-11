using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScenesForShow : MonoBehaviour {

    public static LoadScenesForShow Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene + 1, LoadSceneMode.Single);
    }

    public void LoadScene(int sceneNr)
    {
        SceneManager.LoadScene(sceneNr, LoadSceneMode.Single);
    }
}
