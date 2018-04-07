using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivesFood : MonoBehaviour {

	public Trigger trigger;
	// Use this for initialization
	void Start () {
		trigger.action = DispenseFood;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DispenseFood(GameObject actor){
		actor.SendMessage("AddFood", 100);
	}
}
