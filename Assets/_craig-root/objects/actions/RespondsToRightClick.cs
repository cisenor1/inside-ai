using insideai;
using UnityEngine;

class RespondsToRightClick: MonoBehaviour{
    public RightClickAction destination;

    void Start(){
        destination = TravelTo;
    }
    public void TravelTo(Vector3 destination){
        Debug.Log("going to " + destination);
    }
}