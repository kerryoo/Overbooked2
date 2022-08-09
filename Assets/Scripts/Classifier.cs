using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classifier : MonoBehaviour
{
    [SerializeField] Transform exitPoint1;
    [SerializeField] Transform exitPoint2;
    [SerializeField] Transform exitPoint3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onApplicationCollide(ApplicationObject collidedApp)
    {
        ApplicationObject.Type applicationType = collidedApp.applicationType;
        string applierName = collidedApp.applierName;

        if (applicationType == ApplicationObject.Type.CreditCard)
        {
            collidedApp.transform.position = exitPoint1.transform.position;
        }
        else if (applicationType == ApplicationObject.Type.Mortgage)
        {
            collidedApp.transform.position = exitPoint2.transform.position;
        }
    }
}
