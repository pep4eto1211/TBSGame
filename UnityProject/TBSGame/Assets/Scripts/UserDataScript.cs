using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserDataScript : MonoBehaviour {
    public static UserDataScript currentUserdata;

    public List<Creature> userArmy = new List<Creature>();
    public int gold;
    public bool isFirstGame = true;
    public int spendings = 0;

    public List<Creature> getUserArmy()
    {
        return userArmy;
    }

	void Awake () {
        currentUserdata = this;
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
