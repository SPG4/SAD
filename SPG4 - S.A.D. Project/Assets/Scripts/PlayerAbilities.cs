using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{

    public Rigidbody2D rb2D;
    int layer_mask;
    Vector2 direction;
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
            
            direction = gameObject.transform.localScale;
            direction.y = 0;
            Debug.DrawRay(transform.position, direction, Color.red, 10);

           

            Debug.Log(direction);
            if (Physics2D.Raycast(transform.position, direction, 7, layer_mask))
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 7, layer_mask); // 7 = range
                Debug.Log(hit.collider.gameObject.name);

                if (buttonInput == "UseAbilityP2")
                    direction.x -= direction.x * 2;
                hit.collider.gameObject.SendMessage(chosenAbility, direction);


                //Nedanför = kontrollera antal gånger ability används
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

        


    }

}

