using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    public Rigidbody2D rb2D;
    int layer_mask;
    public Vector2 direction;
    public string buttonInput;
    public string chosenAbility;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        layer_mask = LayerMask.GetMask("Interactable Objects");
    }

    void Update()
    {
        if (Input.GetButtonDown(buttonInput))
        {
            //direction = Vector2.right; //fixa detta när vi har koll på vilket håll karaktären är riktad åt

            Debug.Log(direction);
            //if (Physics2D.Raycast(transform.position, Vector2.right, 7, layer_mask) != null)
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 7, layer_mask); // 7 = range
                if (hit != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    hit.collider.gameObject.SendMessage(chosenAbility, direction);

                    if (chosenAbility == "StandardAbility" && buttonInput == "UseAbilityP1")
                    {
                        // .SendMessage(Player1);
                    }

                    else if (chosenAbility == "StandardAbility" && buttonInput == "UseAbilityP2")
                    {
                        // .SendMessage(Player2);
                    }
                }
            }

            //Debug.DrawRay(transform.position, Vector2.right, Color.red, 10);


        }

    }
}
