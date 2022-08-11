using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : Highlightable, IInteractable
{
    [SerializeField] GameObject classifierUI;
    [SerializeField] GameObject classifiedDocument;
    [SerializeField] Transform spawnLocation;
    [SerializeField] Transform dumpLocation;
    public void Interact(Interactor interactor)
    {
        Instantiate(classifierUI);
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(2f, 1f, 1f));
        for (int i = 0; i < colliders.Length; i++)
        {

            if (colliders[i].GetComponent<Document>() != null)
            {
                colliders[i].transform.position = dumpLocation.position;
                Instantiate(classifiedDocument, spawnLocation.position, spawnLocation.rotation);
            }
        }
    }

}
