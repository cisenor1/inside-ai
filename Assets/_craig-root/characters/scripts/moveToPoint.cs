using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System.Linq;

public class moveToPoint : MonoBehaviour {

	public Transform targetPoint;
	public float moveSpeed = 1f;
	public GameObject destinationMarker;
	private NavMeshAgent _agent;
	private Stack<Vector3> _waypoints = new Stack<Vector3>();
	// Use this for initialization
	void Start () {
		_agent = GetComponent<NavMeshAgent>();
		FindWaypoints();
		_agent.destination = _waypoints.Pop();
		GetComponent<Selectable>().rightClickAction = PriorityGoTo;
	}

	private void PriorityGoTo(Vector3 target){
		Vector3 point = target;
		Instantiate(destinationMarker, point, Quaternion.identity);
		Debug.Log(name + " Prioritymove to " + point);
		_waypoints.Push(_agent.destination);
		_agent.SetDestination(point);
	}

	private void FindWaypoints(){
		var wp = GameObject.FindGameObjectsWithTag("waypoint");
		foreach (Vector3 t in wp.Select((w => w.transform.position)))
		{
			_waypoints.Push(t);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_agent.remainingDistance < 1f){
			if (!_waypoints.Any()){
				FindWaypoints();
			}
			_agent.SetDestination(_waypoints.Pop());
		}
	}

	public void FindFood(){
		// Get all food dispensers
		var dispensers = GameObject.FindGameObjectsWithTag("food-dispenser").Select(dispenser => dispenser.transform.Find("ActionInterface"));
		if (!dispensers.Any()){
			return;
		}
		// Get the closest dispenser
		Vector3 closest = FindClosest(dispensers.Select(d => d.transform.position));
		
		// Push the current destination onto the stack
		_waypoints.Push(_agent.destination);

		// Set the dispenser as the new destination
		_agent.SetDestination(closest);
	}

	private Vector3 FindClosest(IEnumerable<Vector3> all){
		if (!all.Any()){
			throw new UnityException("Find Closest: No values provided.");
		}
		if (all.Count() == 1){
			return all.ElementAt(0);
		}
		Vector3 closest = all.ElementAt(0);
		float closestDistance = Vector3.Distance(transform.position, closest);
		foreach (Vector3 item in all)
		{
			float newDistance = Vector3.Distance(transform.position, item);
			if (newDistance < closestDistance){
				closest = item;
			}
		}
		return closest;
	}
}
