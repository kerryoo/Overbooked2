using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform playerPivot;
    private readonly HashSet<Interactable> _interactables = new HashSet<Interactable>();
    public Interactable CurrentInteractable { get; private set; }
    [SerializeField] private int _numColliders;

    private void Awake()
    {
        if (playerPivot == null) playerPivot = transform;
    }

    private void Update()
    {   
        Interactable closest = TryGetClosestInteractable();
        if (closest != null && Input.GetKeyDown(KeyCode.Space))
        {
            closest.Interact(this);
        }
        if (closest == CurrentInteractable) return;
        else
        {
            CurrentInteractable?.ToggleHighlight(false);
            CurrentInteractable = closest;
            closest?.ToggleHighlight(true);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (!interactable) return;

        _interactables.Add(interactable);
        _numColliders += 1;
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (!interactable) return;

        _interactables.Remove(interactable);
        _numColliders -= 1;

    }



    private Interactable TryGetClosestInteractable()
    {
        var minDistance = float.MaxValue;
        Interactable closest = null;
        foreach (var interactable in _interactables)
        {
            var distance = Vector3.Distance(playerPivot.position, interactable.gameObject.transform.position);
            if (distance > minDistance) continue;
            minDistance = distance;
            closest = interactable;
        }
        return closest;
    }

}
