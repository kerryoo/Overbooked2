using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ApplicationPageDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject classifierScreen;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<Button>().interactable = false;
            classifierScreen.GetComponent<Classifier2DManager>().SelectApplication(eventData.pointerDrag);
        }
    }
}
