using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldForMainMenu : MonoBehaviour {

    float holdingTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Cancel"))
        {
            Debug.Log("Cancelling" + holdingTime);
            holdingTime += Time.deltaTime;
            if(holdingTime > 3)
            {
                LoadScenesForShow.Instance.LoadScene(0);
            }
        }
	}
}
