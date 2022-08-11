using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classifier : MonoBehaviour, IPipe
{
    [SerializeField] Transform[] serializedEntryPoints;
    [SerializeField] GameObject muzzleFlashFX;
    [SerializeField] GameObject mortgageApp;
    [SerializeField] GameObject creditApp;
    [SerializeField] GameObject loanApp;
    [SerializeField] GameObject dunnoApp;
    [SerializeField] GameObject doesntApp;
    [SerializeField] GameObject invalidApp;

    [SerializeField] Transform mortgagePoint;
    [SerializeField] Transform creditPoint;
    [SerializeField] Transform otherPoint;
    public Transform[] entryPoints
    {
        get { return serializedEntryPoints; }
        set { serializedEntryPoints = value; }
    }


    public void onPipeEnter(Transform entryPoint, GameObject objectToSpawn)
    {
        
        Document document = objectToSpawn.GetComponent<Document>();
        if (document.mortgageCount == 0 && document.creditCount == 0)
        {
            for (int i = 0; i < document.loanCount; i++)
            {
                shootDocuments(creditPoint, loanApp);
            }
            for (int i = 0; i < document.doesntCount; i++)
            {
                shootDocuments(mortgagePoint, doesntApp);
            }
            for (int i = 0; i < document.dunnoCount; i++)
            {
                shootDocuments(otherPoint, dunnoApp);
            }
            for (int i = 0; i < document.invalidCount; i++)
            {
                shootDocuments(otherPoint, invalidApp);
            }
        }
        else
        {
            for (int i = 0; i < document.mortgageCount; i++)
            {
                shootDocuments(mortgagePoint, mortgageApp);
            }

            for (int i = 0; i < document.creditCount; i++)
            {
                shootDocuments(creditPoint, creditApp);
            }

            for (int i = 0; i < document.loanCount; i++)
            {
                shootDocuments(otherPoint, loanApp);
            }
            for (int i = 0; i < document.doesntCount; i++)
            {
                shootDocuments(otherPoint, doesntApp);
            }
            for (int i = 0; i < document.dunnoCount; i++)
            {
                shootDocuments(otherPoint, dunnoApp);
            }
            for (int i = 0; i < document.invalidCount; i++)
            {
                shootDocuments(otherPoint, invalidApp);
            }

        }

    }

    private void shootDocuments(Transform toCheck, GameObject toSpawn)
    {
        bool passedToPipe = false;
        Collider[] colliders = Physics.OverlapBox(toCheck.position, new Vector3(0.15f, 0.4f, 0.4f));
        for (int i = 0; i < colliders.Length; i++)
        {
            IPipe collidedPipe = colliders[i].GetComponent<IPipe>();
            if (collidedPipe != null && colliders[i].transform.root != transform)
            {
                for (int j = 0; j < collidedPipe.entryPoints.Length; j++)
                {
                    if (Vector3.Distance(toCheck.position, collidedPipe.entryPoints[j].position) < BalanceSheet.pipeDistanceThreshold)
                    {
                        collidedPipe.onPipeEnter(collidedPipe.entryPoints[j], toSpawn);
                        passedToPipe = true;
                        break;
                    }
                }

            }
        }
        if (!passedToPipe)
        {
            Instantiate(muzzleFlashFX, toCheck.position, toCheck.rotation);
            GameObject docs = Instantiate(toSpawn, toCheck.position, toCheck.rotation);
            docs.GetComponent<Rigidbody>().AddForce(docs.transform.forward * 10, ForceMode.Impulse);
        }
    }
}
