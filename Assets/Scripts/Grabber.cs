using System.Linq;
using UnityEngine;

public class Grabber : MonoBehaviour
{

    [SerializeField] Transform[] pipePositions;
    [SerializeField] float minDistanceTolerance = 0.5f;

    private GameObject selectedObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("drag"))
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = true;
                }
            }
            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                worldPosition.y = 4.5f;
                float minDistance = 10000;
                int minIndex = -1;

                for (int i = 0; i < pipePositions.Length; i++)
                {
                    float currDistance = Vector3.Distance(pipePositions[i].position, worldPosition);
                    if (currDistance < minDistance)
                    {
                        minDistance = currDistance;
                        minIndex = i;
                    }

                }

                if (minDistance < minDistanceTolerance)
                {
                    selectedObject.transform.position = pipePositions[minIndex].position;
                }
                else
                {
                    selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
                }


                selectedObject = null;
                Cursor.visible = true;
            }
        }

        if (selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            worldPosition.y = 4.5f;
            float minDistance = 10000;
            int minIndex = -1;
            for (int i = 0; i < pipePositions.Length; i++)
            {
                float currDistance = Vector3.Distance(pipePositions[i].position, worldPosition);
                if (currDistance < minDistance)
                {
                    minDistance = currDistance;
                    minIndex = i;
                }

            }

            if (minDistance < minDistanceTolerance)
            {
                selectedObject.transform.position = pipePositions[minIndex].position;
            } else
            {
                selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
            }

            

            if (Input.GetKeyDown(KeyCode.E))
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x,
                    selectedObject.transform.rotation.eulerAngles.y + 90f,
                    selectedObject.transform.rotation.eulerAngles.z));
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x,
                    selectedObject.transform.rotation.eulerAngles.y - 90f,
                    selectedObject.transform.rotation.eulerAngles.z));
            }

        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
}
