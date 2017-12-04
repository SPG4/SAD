using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTAM : MonoBehaviour {

    public GameObject exit;
    public float time;
    public string nameOfProjectile;

    public GameObject ply1;
    public GameObject ply2;
    public GameObject projectile;

    private bool startTimer = false;
    float orgTime;
	// Use this for initialization
	void Start () {
        orgTime = time;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")){
            startTimer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")){
            startTimer = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Projectiles"))
        {
            if (time <= 0)
            {
                MovePlayers();
                time = orgTime;
            }
        }

        // Check for projectile and move it outside.
        if (time <= 0 && collision.gameObject.name == nameOfProjectile)
        {
            MoveObjects(collision.gameObject);
            projectile.gameObject.SendMessage("ExitTAM");
        }
    }

    void Update()
    {
        if (startTimer == true)
        {
            time -= Time.deltaTime;
        }

    }

    void MoveObjects(GameObject obj)
    {
        obj.gameObject.transform.position = exit.transform.position;
    }
    void MovePlayers()
    {
        ply1.gameObject.transform.position = exit.transform.position;
        ply2.gameObject.transform.position = exit.transform.position;
    }
    void NoTime()
    {
        time = 2;
    }
}
