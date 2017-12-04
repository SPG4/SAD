using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTAM : MonoBehaviour
{
    public GameObject projectile;
    public Vector2 vector;
    public float force = 40;

    public float delay;
    bool canShoot = true;
    float timer, orgTime;

    // Use this for initialization
    void Start()
    {
        orgTime = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot == true)
        {   
            
        }
        if(delay >= 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            delay = orgTime;
            Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(vector * force);
        }
    }
}
