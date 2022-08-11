using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    GameObject gameObject { get; }
    public void Grab(Interactor interactor);
    public void Drop(Interactor interactor);
}
