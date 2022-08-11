using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : Highlightable, IPickable
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;

    public override void Awake()
    {
        base.Awake();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void Drop(Interactor interactor)
    {
        gameObject.transform.SetParent(null);
        _rigidbody.isKinematic = false;
        _collider.enabled = true;
    }

    public void Grab(Interactor interactor)
    {
        transform.position = interactor.holdSpot.position;
        transform.rotation = interactor.holdSpot.rotation;
        transform.SetParent(interactor.transform);
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
    }

    private void Setup()
    {
        // Rigidbody is kinematic almost all the time, except when we drop it on the floor
        // re-enabling when picked up.
        _rigidbody.isKinematic = true;
        _collider.enabled = false;

    }

    //public void Interact(Interactor interactor)
    //{
    //    if (interactor.itemholding) { }
    //    Debug.Log("tring");
    //    this.transform.position = interactor.holdSpot.position;
    //    this.transform.parent = interactor.transform;
    //}


}
