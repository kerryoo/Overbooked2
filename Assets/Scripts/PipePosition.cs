using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePosition : MonoBehaviour
{
    public Light spotlight;
    void Start()
    {
        spotlight = GetComponent<Light>();
        spotlight.enabled = false;
    }


}
