using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour {

    public string roomName;

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter(Collider collidingObject)
    {
        SceneManager.LoadScene(roomName, LoadSceneMode.Single); //NOTE: decided to remove main scene as well to keep memory use low. Also simplifies logic by not considering state of MainChamber - may want to change this if some event happens everytime we return to scene
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
