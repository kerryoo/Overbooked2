using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryVan : MonoBehaviour
{
    public Transform dropOffTrans;
    public Transform leaveTrans;
    public Transform packageSpawnLocation;
    public GameObject objectToSpawn;

    private Vector3 targetPosition;
    private bool droppedOff;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 3 * Time.deltaTime);
        if (Vector3.Distance(dropOffTrans.position, transform.position) < 1f && !droppedOff)
        {
            Instantiate(objectToSpawn, packageSpawnLocation.position, packageSpawnLocation.rotation);
            droppedOff = true;
            targetPosition = leaveTrans.position;
        }
        if (Vector3.Distance(leaveTrans.position, transform.position) < 1f)
        {
            Destroy(gameObject);
        }

    }

    public void setFields(Transform dropOffTrans, Transform leaveTrans, GameObject objectToSpawn)
    {
        this.dropOffTrans = dropOffTrans;
        this.objectToSpawn = objectToSpawn;
        this.leaveTrans = leaveTrans;
    }

    private void Start()
    {
        targetPosition = dropOffTrans.position;
    }
}
