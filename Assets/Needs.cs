using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needs : MonoBehaviour {

	public float hunger = 100f;
	public float hungerSpeed = 1f;
	private string state = "patrolling";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		hunger -= hungerSpeed * Time.deltaTime;
		if (hunger < 0) hunger = 0;
		if (hunger < 25 && state == "patrolling"){
			// look for food.
			Debug.Log("I'm Hungry!");
			SendMessage("FindFood");
			state = "findingFood";
		}
		if (hunger > 75){
			state = "patrolling";
		}
	}

	public void AddFood(float amount){
		hunger += amount;
		hunger = Mathf.Clamp(hunger, 0, 100);
	}
}
