using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel7 : MonoBehaviour {

    GameObject tam;
    public int player;
    GameObject camera;
    GameObject dialogue;
    GameObject gameHandler;
    int balanceOffset = 10;

    void Start ()
    {
        dialogue = GameObject.FindGameObjectWithTag("DialogueManager");
    }
	
    void TamDead()
    {
        print("moving");
        tam = GameObject.FindGameObjectWithTag("Boss");

        gameObject.GetComponent<Transform>().position = tam.GetComponent<Transform>().position - new Vector3(10, 0, 0);
        gameObject.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        if (player == 1)
        {
            gameObject.GetComponent<Transform>().position -= new Vector3(3, 0, 0);
            gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<CameraController>().minSizeY = 7;
            camera.GetComponent<CameraController>().camYOffset = 6;
            gameHandler = GameObject.FindGameObjectWithTag("GameHandler");

            if (gameHandler.GetComponent<Save>().GetTotalPlayerPoint(1) >= gameHandler.GetComponent<Save>().GetTotalPlayerPoint(2) + balanceOffset) // blue bigger
            {
                dialogue.SendMessage("AddText", "Green: Hey, we did it!");
                dialogue.SendMessage("AddText", "Blue: You mean I did it? This whole time you’ve just been along for the ride while I’ve been doing the real work. I think it’s time I get rid of the weak link.");
                dialogue.SendMessage("AddText", "Green: Hey, what do you mean? What are you doing?");
            }

            else if (gameHandler.GetComponent<Save>().GetTotalPlayerPoint(2) >= gameHandler.GetComponent<Save>().GetTotalPlayerPoint(1) + balanceOffset) //green bigger
            {
                dialogue.SendMessage("AddText", "Blue: Hey, we did it!");
                dialogue.SendMessage("AddText", "Green: You mean I did it. This whole time you’ve just been undermining me, telling me I’m not good enough, but in reality I’m the only one doing the actual work! I’m sick of it, I’m sick of you!");
                dialogue.SendMessage("AddText", "Green: Hey, no need overreact man! Wait, what are you doing?");
            }

            else // balance
            {
                dialogue.SendMessage("AddText", "Green: Hey, we actually did it! Together!");
                dialogue.SendMessage("AddText", "Blue: Yeah, would you look at that, we’re a pretty good team! Maybe if we try to work together more from now on, things will get easier in the future!");
            }
        }
    }
}
