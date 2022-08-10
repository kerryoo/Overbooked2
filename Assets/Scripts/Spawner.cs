using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform exitPoint;
    [SerializeField] GameObject muzzleFlashFX;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Collider[] colliders = Physics.OverlapBox(exitPoint.position, new Vector3(0.15f, 0.4f, 0.4f));
        bool passedToPipe = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            IPipe collidedPipe = colliders[i].GetComponent<IPipe>();
            if (collidedPipe != null)
            {
                for (int j = 0; j < collidedPipe.entryPoints.Length; j++)
                {
                    if (Vector3.Distance(exitPoint.position, collidedPipe.entryPoints[j].position) < BalanceSheet.pipeDistanceThreshold)
                    {
                        collidedPipe.onPipeEnter(collidedPipe.entryPoints[j]);
                        passedToPipe = true;
                        break;
                    }
                }

            }

        }
        if (!passedToPipe)
        {
            Instantiate(muzzleFlashFX, exitPoint.position, exitPoint.rotation);
        }
    }
}
