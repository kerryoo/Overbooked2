using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform trickTrans;
    [SerializeField] Transform initialTrans;
    [SerializeField] Transform characterSelectTrans;
    [SerializeField] Transform mainTrans;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private void Start()
    {
        targetPosition = trickTrans.position;
        targetRotation = trickTrans.rotation;
    }

    public void goToMenuAngle()
    {
        targetPosition = initialTrans.position;
        targetRotation = initialTrans.rotation;
    }

    public void goToSelectAngle()
    {
        targetPosition = characterSelectTrans.position;
        targetRotation = characterSelectTrans.rotation;
    }

    public void goToOriginalAngle()
    {
        targetPosition = initialTrans.position;
        targetRotation = initialTrans.rotation;
    }

    public void goToMainAngle()
    {
        targetPosition = mainTrans.position;
        targetRotation = mainTrans.rotation;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 3f * Time.deltaTime);
    }
}
