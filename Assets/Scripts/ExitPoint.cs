using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    [SerializeField] Transform exitPoint;
    [SerializeField] GameObject fx;
    [SerializeField] Transform dumpLocation;
    [SerializeField] GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Document>() != null)
        {
            Instantiate(fx, exitPoint.transform.position, exitPoint.transform.rotation);
            collision.transform.position = dumpLocation.position;
            gameManager.addCash(112.5f);
        }
    }
}
