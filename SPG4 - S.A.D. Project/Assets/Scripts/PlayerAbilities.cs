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

    public AudioClip standardSound;
    public AudioClip teleportSound;
    public AudioClip sizeSound;

    int layer_mask;
    int ball_layer;
    float mana;
    [SerializeField]
    float shots;
    bool buttonPressed;
    bool addedAbility = false;
    public bool usedP1, usedP2;

    Vector2 direction;
    List<string> abilityList;
    DistanceJoint2D distanceJoint;
    SpringJoint2D springJoint;
    PlayerController player;
    Save abilityTracker;
    public AnalyticsTracker analyticsTracker;

    void Start()
    {
        layer_mask = LayerMask.GetMask("Interactable Objects"); // Used in raycast to only hit objects on a specific layer
        ball_layer = LayerMask.GetMask("Interactable ball object");
        shots = 1;
        abilityTracker = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<Save>();
        abilityList = new List<string>();
        //abilityList.Add("StandardAbility"); //The standard ability should always be added since it is always available for the player
        abilityList.Add("");

        if (level >= 2)
        {
            abilityList.Remove("");
            abilityList.Add("StandardAbility");
        }
        if (level >= 3)
            abilityList.Add("SizeGun");
        if (level >= 4)
            abilityList.Add("ShootTeleportBall");
        if (level >= 5)
            abilityList.Add("Shield");

        if (level == 0)
        {
            abilityList.Add("StandardAbility");
            abilityList.Add("SizeGun");
            abilityList.Add("ShootTeleportBall");
            abilityList.Add("Shield");
        }

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
            //analyticsTracker.TriggerEvent();
            if (chosenAbility == "SizeGun" || chosenAbility == "StandardAbility" || chosenAbility == "RopeAbility")
                CastRayAbility();

            //Using shots variable to make sure you can only shoot one at a time, setting the value of shot back to 1 
            //in ShootBall when the TeleportBallEvent method is called, maybe not the best solution to 
            //change shot in another script but we can fix that later
            if (chosenAbility == "ShootTeleportBall" && shots > 0 && player.InsideAntiGravArea() == false)
            {
                SoundLevelManager.Instance.PlaySingle(teleportSound);
                shots--;
                gameObject.SendMessage(chosenAbility);

                if (gameObject.name == "Player 1")
                    usedP1 = true;
                if (gameObject.name == "Player 2")
                    usedP2 = true;
            }
        }
        if (transform.GetChild(0).gameObject.tag == "ShieldP1")
            if (transform.GetChild(0).gameObject.activeSelf)
                usedP1 = true;
        if (transform.GetChild(0).gameObject.tag == "ShieldP2")
            if (transform.GetChild(0).gameObject.activeSelf)
                usedP2 = true;
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
                if (buttonInput == "UseAbilityP2")// find direction object should move in
                {
                    direction = direction * -1;
                    abilityTracker.SendMessage("AddOneUseOfStandard", 2);  //Sends message to Save to save use of ability
                    Debug.Log("totally happened");
                    usedP2 = true;
                    print(usedP2);
                }
                else
                {
                    abilityTracker.SendMessage("AddOneUseOfStandard", 1);
                    usedP1 = true;
                }

                if (hit)
                {
                    SoundLevelManager.Instance.PlaySingle(standardSound);
                    hit.collider.gameObject.SendMessage(chosenAbility, direction); //Sending message to object telling it what ability has been used on it, plus a direction (for standard ability)

                }
                else if (ballHit)
                    ballHit.collider.gameObject.SendMessage(chosenAbility, direction);
            }

            else if (chosenAbility == "SizeGun")
            {
                SoundLevelManager.Instance.PlaySingle(sizeSound);
                hit.collider.gameObject.SendMessage(chosenAbility, buttonInput);
            }
        }
    }

    void UseShield()
    {
        if (Input.GetButton(buttonInput) && mana > 0 && chosenAbility == "Shield")    //if button is pressed down the shield is active and mana is used       
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