using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class MouseInterfaceHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    public Image selectionBox;

    [HideInInspector]
    public Vector2 startPosition;

    [HideInInspector]    
    public Rect selectionRect;

    public void OnBeginDrag(PointerEventData eventData)
    {
		Selectable.DeselectAll(eventData);
        selectionBox.gameObject.SetActive(true);
        startPosition = eventData.position;
        selectionRect = new Rect();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position.x < startPosition.x)
        {
            selectionRect.xMin = eventData.position.x;
            selectionRect.xMax = startPosition.x;
        }
        else
        {
            selectionRect.xMin = startPosition.x;
            selectionRect.xMax = eventData.position.x;
        }
        if (eventData.position.y < startPosition.y)
        {
            selectionRect.yMin = eventData.position.y;
            selectionRect.yMax = startPosition.y;
        }
        else
        {
            selectionRect.yMax = eventData.position.y;
            selectionRect.yMin = startPosition.y;
        }

        selectionBox.rectTransform.offsetMin = selectionRect.min;
        selectionBox.rectTransform.offsetMax = selectionRect.max;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        selectionBox.gameObject.SetActive(false);
		foreach (Selectable s in Selectable.AllSelectables)
		{
			if (selectionRect.Contains(Camera.main.WorldToScreenPoint(s.transform.position))){
				s.OnSelect(eventData);
			}
		}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                	Selectable.DeselectAll(eventData);
					Click(eventData);
                break;
            case PointerEventData.InputButton.Middle:
                break;
            case PointerEventData.InputButton.Right:
                    RightClick(eventData);
                break;
            default:
                break;
        }
    }

	private void Click(PointerEventData eventData){
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);
		float myDistance = 0f;
		foreach (RaycastResult result in results)
		{
			if (result.gameObject == gameObject){
				// It's me
				myDistance = result.distance;
				break;
			}
		}

		float maxDistance = Mathf.Infinity;
		GameObject nextObject = null;
		foreach (RaycastResult result in results)
		{
			if (result.distance > myDistance && result.distance < maxDistance ){
				nextObject = result.gameObject;
				maxDistance = result.distance;
			}
		}

		if (nextObject){
			ExecuteEvents.Execute<IPointerClickHandler>(nextObject, eventData, (obj,eventD)=>{ obj.OnPointerClick((PointerEventData)eventD); });
		}
	}

    private void RightClick(PointerEventData eventData){
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            RespondsToRightClick r = result.gameObject.GetComponent<RespondsToRightClick>();
            if (r){
                foreach (Selectable s in Selectable.Selected)
                {
                    s.OnRightClick(result.worldPosition);
                }
            }
        }
    }
}
