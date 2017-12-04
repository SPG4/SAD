using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTAM : MonoBehaviour
{
    public GameObject projectile;
    public float delay;
    public float speedFactor;
    // Use this for initialization
    void Start()
    {

    }
    // GetComponent<Rigidbody2D>().AddForce(direction * force);
    // Update is called once per frame
    void Update()
    {

    }

    //IEnumerator Shoots()
    //{
    //    while (true)
    //    {
    //        GameObject clone = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
    //        clone.GetComponent<Rigidbody2D>().velocity = transform.right * speedFactor;
    //        yield return new WaitForSeconds(delay);
    //    }
    //}
}
