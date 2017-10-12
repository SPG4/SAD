using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{

    public Rigidbody2D rb2D;
    int layer_mask;
    Vector2 direction;
    public string buttonInput; //The button tells us which player is attempting to use an ability
    public string chosenAbility; //what ability is being used
    public float mana;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        layer_mask = LayerMask.GetMask("Interactable Objects"); // Used in raycast to only hit objects on a specific layer
        mana = 5;
    }

    void Update()
    {
        //Debug.Log(mana);

        if (chosenAbility == "Shield")
        {
            if (Input.GetButton(buttonInput) && mana > 0)    //if button is pressed down the shield is active and mana is used       
            {
                transform.GetChild(0).gameObject.SetActive(true);
                mana -= Time.deltaTime;
                if (mana < 0)
                    transform.GetChild(0).gameObject.SetActive(false); //Shield must always be the first child of the players!
            }

            else if (!Input.GetButton(buttonInput)) //when button is not pressed the shield is inactive and mana recharges
            {
                transform.GetChild(0).gameObject.SetActive(false);

                {
                    if (mana < 5)
                        mana += Time.deltaTime;
                    else
                        mana = 5.0f;
                }
            }
        }

        else if (Input.GetButtonDown(buttonInput)) //abilities that use raycast (Standard Ability, Sizegun)
        {

            direction = gameObject.transform.localScale; // choosing direction for the raycast
            direction.y = 0;
            Debug.DrawRay(transform.position, direction, Color.red, 10);

            // Debug.Log(direction);
            if (Physics2D.Raycast(transform.position, direction, 7, layer_mask)) //checking for objects that the raycast hits
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 7, layer_mask); // 7 = range for ability
                Debug.Log(hit.collider.gameObject.name);

                if (chosenAbility == "StandardAbility")
                {
                    if (buttonInput == "UseAbilityP2") // find direction object should move in
                        direction.x -= direction.x * 2;

                    if (buttonInput == "UseAbilityP1")
                    {
                        // .SendMessage(Player1);
                    }

                    else if (buttonInput == "UseAbilityP2")
                    {
                        // .SendMessage(Player2);
                    }

                    hit.collider.gameObject.SendMessage(chosenAbility, direction); //Sending message to object telling it what ability has been used on it, plus a direction (for standard ability)
                }

                else
                    hit.collider.gameObject.SendMessage(chosenAbility, buttonInput);
            }
        }
    }
}

