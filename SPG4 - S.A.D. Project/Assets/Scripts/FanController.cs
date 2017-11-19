using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour {

    public bool fanOn; 

    float fanPushForce = 5000f;
    GameObject gameObjectPlayer;
    Transform particles;
    Transform wall;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //gameObjectPlayer = collision.gameObject;
        //gameObjectPlayer.SendMessage("AffectedByFan", fanPushForce);  
        //gameObjects.Add(collision.gameObject);

        //foreach (GameObject g in gameObjects)
        //{
        //    //Debug.Log(g);
        //    //g.GetComponent<Rigidbody2D>().gravityScale = 0;
        //    //g.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fanPushForce));
        //    g.SendMessage("AffectedByFan", fanPushForce);
        //}
    }

    // Use this for initialization
    void Start () {
        particles = this.gameObject.transform.GetChild(0);
        wall = this.gameObject.transform.GetChild(1);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(fanOn);
        if (fanOn)
        {
            particles.gameObject.SetActive(true);
            wall.gameObject.SetActive(false);
        }
        else
        {
            particles.gameObject.SetActive(false);
            wall.gameObject.SetActive(true);
        }
	}

    private void TurnFanOn(bool turnFanOn)
    {
        if (turnFanOn)
            fanOn = true;
        else
            fanOn = false;
    }

    public bool IsFanOn()
    {
        return fanOn;
    }
}
