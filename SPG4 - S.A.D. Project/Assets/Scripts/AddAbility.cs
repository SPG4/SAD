using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAbility : MonoBehaviour {

    public string abilityToAdd;
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Transform>().root.SendMessage("AddAbility", abilityToAdd);
        }
    }
}
