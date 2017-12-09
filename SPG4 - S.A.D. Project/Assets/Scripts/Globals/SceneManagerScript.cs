using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour {
    public Text ValueTxt;
    public Text ValueTxt2;
	// Use this for initialization
	void Start () {
        ValueTxt.text = PersistentManager.Instance.Player1Power.ToString();
        ValueTxt2.text = PersistentManager.Instance.Player2Power.ToString();
        //Example of how to increase this continuing thing
        //PersistentManager.Instance.Player1Power++;
    }

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void LoadNextScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene +1, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
