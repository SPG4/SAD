using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl5Puzzel4Turret : MonoBehaviour {

    public GameObject projectile;
    public float speedFactor;
    public float delay;
    public bool isDeadly = false;
    public bool destroyOnContact = false;
    static public bool isDeadlySet = false;
    static public bool destroyOnTriggerContant = false;
    static public bool canFire;
    // Use this for initialization

    void Start()
    {
        canFire = false;
        if (isDeadly == false) { isDeadlySet = false; }
        else if (isDeadly == true) { isDeadlySet = true; }
        if (destroyOnContact == false) { destroyOnTriggerContant = false; }
        else if (destroyOnContact == true) { destroyOnTriggerContant = true; }

        StartCoroutine(Shoots());
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Makes a copy of the projectile and move it forward.
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoots()
    {
        while (true)
        {
            if(canFire == true)
            {
                GameObject clone = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
                clone.GetComponent<Rigidbody2D>().velocity = transform.right * speedFactor;
            }
            canFire = false;
            yield return new WaitForSeconds(delay);
        }
    }
}
