using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using insideai;
public class Selectable : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    public static HashSet<Selectable> Selected = new HashSet<Selectable>();
    public static HashSet<Selectable> AllSelectables = new HashSet<Selectable>();
    public Color selectedTint;
    public RightClickAction rightClickAction;
    private Renderer _renderer;
    private Color _initialColor;
    void Awake()
    {
        AllSelectables.Add(this);
        _renderer = GetComponentInChildren<Renderer>();
        _initialColor = _renderer.material.color;
    }

    public void OnRightClick(Vector3 screenPoint){
        if (rightClickAction != null) rightClickAction(screenPoint);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        _renderer.material.color = _initialColor;
    }

    public void OnSelect(BaseEventData eventData)
    {
        _renderer.material.color = selectedTint;
        Selected.Add(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                DeselectAll(eventData);
                OnSelect(eventData);
                break;
            case PointerEventData.InputButton.Middle:
                break;
            case PointerEventData.InputButton.Right:
                break;
            default:
                break;
        }
    }

    public static void DeselectAll(BaseEventData eventData)
    {
        foreach (Selectable s in Selected)
        {
            s.OnDeselect(eventData);
        }
        Selected.Clear();
    }
}