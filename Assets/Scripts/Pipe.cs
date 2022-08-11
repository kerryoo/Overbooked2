using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour, IPipe
{
    [SerializeField] Transform[] serializedEntryPoints;
    public Transform[] entryPoints
    {
        get { return serializedEntryPoints; }
        set { serializedEntryPoints = value; }
    }

    [SerializeField] GameObject muzzleFlashFX;

    public void onPipeEnter(Transform entryPoint, GameObject objectToSpawn)
    {
        bool passedToPipe = false;
        if (entryPoint == entryPoints[0])
        {
            Collider[] colliders = Physics.OverlapBox(entryPoints[1].position, new Vector3(0.15f, 0.4f, 0.4f));
            for (int i = 0; i < colliders.Length; i++)
            {
                IPipe collidedPipe = colliders[i].GetComponent<IPipe>();
                if (collidedPipe != null && colliders[i].transform.root != transform)
                {
                    for (int j = 0; j < collidedPipe.entryPoints.Length; j++)
                    {
                        if (Vector3.Distance(entryPoints[1].position, collidedPipe.entryPoints[j].position) < BalanceSheet.pipeDistanceThreshold)
                        {
                            collidedPipe.onPipeEnter(collidedPipe.entryPoints[j], objectToSpawn);
                            passedToPipe = true;
                            break;
                        }
                    }

                }
            }
            if (!passedToPipe)
            {
                Instantiate(muzzleFlashFX, entryPoints[1].position, entryPoints[1].rotation);
                GameObject docs = Instantiate(objectToSpawn, entryPoints[1].position, entryPoints[1].rotation);
                docs.GetComponent<Rigidbody>().AddForce(docs.transform.forward * 10, ForceMode.Impulse);
            }

        }
        else if (entryPoint == entryPoints[1])
        {
            Collider[] colliders = Physics.OverlapBox(entryPoints[0].position, new Vector3(0.15f, 0.4f, 0.4f));
            for (int i = 0; i < colliders.Length; i++)
            {
                IPipe collidedPipe = colliders[i].GetComponent<IPipe>();
                if (collidedPipe != null && colliders[i].transform.root != transform)
                {
                    for (int j = 0; j < collidedPipe.entryPoints.Length; j++)
                    {
                        if (Vector3.Distance(entryPoints[0].position, collidedPipe.entryPoints[j].position) < BalanceSheet.pipeDistanceThreshold)
                        {
                            collidedPipe.onPipeEnter(collidedPipe.entryPoints[j], objectToSpawn);
                            passedToPipe = true;
                            break;
                        }
                    }

                }

            }
            if (!passedToPipe)
            {
                Instantiate(muzzleFlashFX, entryPoints[0].position, entryPoints[0].rotation);
                GameObject docs = Instantiate(objectToSpawn, entryPoints[0].position, entryPoints[0].rotation);
                docs.GetComponent<Rigidbody>().AddForce(docs.transform.forward * 10, ForceMode.Impulse);
            }

        }


    }

    private void Update()
    {
    }

}
