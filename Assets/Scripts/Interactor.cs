using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform playerPivot;
    private readonly HashSet<Highlightable> _highlightables = new HashSet<Highlightable>();
    public Highlightable CurrentHighlightable { get; private set; }
    [SerializeField] private int _numColliders;
    [SerializeField] public Transform holdSpot;
    public IPickable itemholding;

    private void Awake()
    {
        if (playerPivot == null) playerPivot = transform;
    }

    private void Update()
    {
        Highlightable closest = TryGetClosestInteractable();
        if (closest != CurrentHighlightable)
        {
            CurrentHighlightable?.ToggleHighlight(false);
            CurrentHighlightable = closest;
            closest?.ToggleHighlight(true);
        }
        if ((itemholding != null) && Input.GetKeyDown(KeyCode.Space))
        {
            itemholding.Drop(this);
            itemholding = null;
            return;
        }
        if (closest != null)
        {
            if (closest is IInteractable && Input.GetKeyDown(KeyCode.LeftControl))
            {
                IInteractable tmp = (IInteractable)closest;
                tmp.Interact(this);
            }

            if (closest is IPickable && Input.GetKeyDown(KeyCode.Space))
            {
                IPickable tmp = (IPickable)closest;
                tmp.Grab(this);
                itemholding = tmp;
                CurrentHighlightable = null;
                _highlightables.Remove(closest);
                _numColliders -= 1;
                closest?.ToggleHighlight(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Highlightable highlightable = other.GetComponent<Highlightable>();
        if (!highlightable) return;

        _highlightables.Add(highlightable);
        _numColliders += 1;
    }

    private void OnTriggerExit(Collider other)
    {
        Highlightable highlightable = other.GetComponent<Highlightable>();
        if (!highlightable) return;

        _highlightables.Remove(highlightable);
        _numColliders -= 1;

    }



    private Highlightable TryGetClosestInteractable()
    {
        var minDistance = float.MaxValue;
        Highlightable closest = null;
        foreach (var highlightable in _highlightables)
        {
            var distance = Vector3.Distance(playerPivot.position, highlightable.gameObject.transform.position);
            if (distance > minDistance) continue;
            minDistance = distance;
            closest = highlightable;
        }
        return closest;
    }

}
