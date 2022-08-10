using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPipe
{
    Transform[] entryPoints { get; set; }
    void onPipeEnter(Transform entryPoint);
}
