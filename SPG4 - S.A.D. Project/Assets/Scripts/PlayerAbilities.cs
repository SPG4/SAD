using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{

    public Rigidbody2D rb2D;
    int layer_mask;
    Vector2 direction;
    public string nextAbilityInput;
    public string buttonInput; //The button tells us which player is attempting to use an ability
    public string chosenAbility; //what ability is being used
    public float mana;
    public float shots;
    List<string> abilityList;
    DistanceJoint2D distanceJoint;
    SpringJoint2D springJoint;
    bool buttonPressed;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        layer_mask = LayerMask.GetMask("Interactable Objects"); // Used in raycast to only hit objects on a specific layer
        mana = 5;
        shots = 1;

        abilityList = new List<string>();
        abilityList.Add("StandardAbility"); //The standard ability should always be added since it is always available for the player

        //the code below should be deleted when the system for choosing abilities has been added
        abilityList.Add("SizeGun");
        abilityList.Add("Shield");
        abilityList.Add("ShootTeleportBall");
        abilityList.Add("RopeAbility");

        chosenAbility = abilityList[0];
    }

    void Update()
    {
        //Change Ability
        if (Input.GetButtonDown(nextAbilityInput)) 
        {
            int temp = 0;

            for (int i = 0; i < abilityList.Count; i++) // Find the position of the current ability in the list
            {
                if (abilityList[i] == chosenAbility)
                {
                    temp = (i + 1);
                    break;
                }
            }

            if (temp >= abilityList.Count)
                temp = 0;
            chosenAbility = abilityList[temp]; //sets current ability to next ability in list
        }

        if (chosenAbility == "Shield")
            UseShield();

        else if (Input.GetButtonDown(buttonInput))
        {
            if (chosenAbility == "SizeGun" || chosenAbility == "StandardAbility")
                CastRayAbility();

            if (chosenAbility == "RopeAbility")
                CastAlternativeRayAbility();

            //Using shots variable to make sure you can only shoot one at a time, setting the value of shot back to 1 
            //in ShootBall when the TeleportBallEvent method is called, maybe not the best solution to 
            //change shot in another script but we can fix that later
            if (chosenAbility == "ShootTeleportBall" && shots > 0)
            {
                shots--;
                gameObject.SendMessage(chosenAbility);
            }
        }

        //Debug.Log(chosenAbility);
    }
    /// <summary>
    /// Casts a Ray and then uses ability
    /// </summary>
    void CastRayAbility()
    {
        direction = gameObject.transform.localScale; // choosing direction for the raycast
        direction.y = 0;
        //Debug.DrawRay(transform.position, direction, Color.red, 10);

        if (Physics2D.Raycast(transform.position, direction, 7, layer_mask)) //checking for objects that the raycast hits
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 7, layer_mask); // 7 = range for ability
                                                                                                //Debug.Log(hit.collider.gameObject.name);
            if (chosenAbility == "StandardAbility")
            {
                if (buttonInput == "UseAbilityP2") // find direction object should move in
                    direction.x -= direction.x * 2;

                hit.collider.gameObject.SendMessage(chosenAbility, direction); //Sending message to object telling it what ability has been used on it, plus a direction (for standard ability)
            }

            else if (chosenAbility == "SizeGun")
                hit.collider.gameObject.SendMessage(chosenAbility, buttonInput);
        }
    }

    /// <summary>
    /// Casts a Ray and then uses ability
    /// </summary>
    void CastAlternativeRayAbility()
    {
        if (chosenAbility == "RopeAbility" && !buttonPressed) //If the input button has not been pressed, the ability can be used
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;

            if (Physics2D.Raycast(transform.position, target - transform.position, 7, layer_mask))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, target - transform.position, 7, layer_mask); //A raycast from the player to the mouse cursor's position
                if (buttonInput == "UseAbilityP1")
                {
                    //Creates a dinstance joint connected between the player and the interactable object
                    distanceJoint = gameObject.AddComponent<DistanceJoint2D>();
                    distanceJoint.enabled = true;
                    distanceJoint.maxDistanceOnly = true;
                    distanceJoint.enableCollision = true;
                    distanceJoint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    distanceJoint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                    distanceJoint.distance = Vector3.Distance(transform.position, hit.point);
                }

                if (buttonInput == "UseAbilityP2")
                {
                    //Creates a spring joint connected between the player and the interactable object
                    springJoint = gameObject.AddComponent<SpringJoint2D>();
                    springJoint.enabled = true;
                    springJoint.enableCollision = true;
                    springJoint.autoConfigureDistance = false;
                    springJoint.distance = 0.005f;
                    springJoint.dampingRatio = 1f;
                    springJoint.frequency = 2f;
                    springJoint.connectedAnchor = target;
                }
            }

            buttonPressed = true;
        }

        //If the input button has already been pressed, and a joint is connected to an object, then the joint is destroyed
        else if (chosenAbility == "RopeAbility" && buttonPressed)
        {
            if (buttonInput == "UseAbilityP1")
                Destroy(distanceJoint);
            if (buttonInput == "UseAbilityP2")
                Destroy(springJoint);

            buttonPressed = false;
        }
    }

    void UseShield()
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

            if (mana < 5)
                mana += Time.deltaTime;
            else
                mana = 5.0f;
        }
    }

    void AddAbilityToList(string abilityName) //call with SendMessage
    {
        abilityList.Add(abilityName);
    }

    public string GetCurrentAbility()
    {
        return chosenAbility;
    }
}



