using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class PlayerAbilities : MonoBehaviour
{
    public string nextAbilityInput;
    public string buttonInput; //The button tells us which player is attempting to use an ability
    public string chosenAbility; //what ability is being used
    public int level; //The level that is currentry played

    int layer_mask;
    int ball_layer;
    float mana;
    float shots;
    bool buttonPressed;
    bool addedAbility = false;

    Vector2 direction;
    List<string> abilityList;
    DistanceJoint2D distanceJoint;
    SpringJoint2D springJoint;
    PlayerController player;
    public AnalyticsTracker analyticsTracker;

    void Start()
    {
        layer_mask = LayerMask.GetMask("Interactable Objects"); // Used in raycast to only hit objects on a specific layer
        ball_layer = LayerMask.GetMask("Interactable ball object");
        shots = 1;

        abilityList = new List<string>();
        abilityList.Add("StandardAbility"); //The standard ability should always be added since it is always available for the player

        if (level >= 3)
            abilityList.Add("SizeGun");
        if (level >= 4)
            abilityList.Add("ShootTeleportBall");
        if (level >= 5)
            abilityList.Add("Shield");
        
        if (level == 0)
        {
            abilityList.Add("SizeGun");
            abilityList.Add("ShootTeleportBall");
            abilityList.Add("Shield");
        }
        //abilityList.Add("RopeAbility");

        chosenAbility = abilityList[0];

        player = gameObject.GetComponent<PlayerController>();

        player.SendMessage("SetActiveAbility", chosenAbility);
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
            player.SendMessage("SetActiveAbility", chosenAbility);
        }

        if (chosenAbility == "Shield")
            UseShield();

        else if (Input.GetButtonDown(buttonInput))
        {
            analyticsTracker.TriggerEvent();
            if (chosenAbility == "SizeGun" || chosenAbility == "StandardAbility" || chosenAbility == "RopeAbility")
                CastRayAbility();

            //Using shots variable to make sure you can only shoot one at a time, setting the value of shot back to 1 
            //in ShootBall when the TeleportBallEvent method is called, maybe not the best solution to 
            //change shot in another script but we can fix that later
            if (chosenAbility == "ShootTeleportBall" && shots > 0 && player.InsideAntiGravArea() == false)
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
        direction = player.crosshair.transform.position - gameObject.transform.position;
        Debug.DrawRay(transform.position, direction, Color.red, 10);

        if (Physics2D.Raycast(transform.position, direction, 7, layer_mask) || Physics2D.Raycast(transform.position, direction, 7, ball_layer)) //checking for objects that the raycast hits
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 7, layer_mask); // 7 = range for ability                                                                           
            RaycastHit2D ballHit = Physics2D.Raycast(transform.position, direction, 7, ball_layer);

            if (chosenAbility == "StandardAbility")
            {
                Debug.DrawRay(transform.position, direction, Color.red, 10);
                if (buttonInput == "UseAbilityP2") // find direction object should move in
                    direction = direction * -1;

                    if(hit)
                        hit.collider.gameObject.SendMessage(chosenAbility, direction); //Sending message to object telling it what ability has been used on it, plus a direction (for standard ability)
                    else if (ballHit)
                        ballHit.collider.gameObject.SendMessage(chosenAbility, direction);
            }

            else if (chosenAbility == "SizeGun")
                hit.collider.gameObject.SendMessage(chosenAbility, buttonInput);

            else if (chosenAbility == "RopeAbility" && !buttonPressed) //If the input button has not been pressed, the ability can be used
            {
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
                    springJoint.connectedAnchor = hit.point;
                }
            }
            buttonPressed = true;
        }
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

    public void ResetShot(int newShotsValue)
    {
        shots = newShotsValue;
    }

    public void AddAbility(string ability)
    {
        if (!addedAbility)
        {
            abilityList.Add(ability);
            addedAbility = true;
            chosenAbility = ability;
            Debug.Log("Added" + ability + buttonInput);
        }
    }
}