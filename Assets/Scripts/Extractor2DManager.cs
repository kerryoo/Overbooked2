using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Extractor2DManager : MonoBehaviour
{
    [SerializeField] ApplicationObject.ApplicationId appId;
    [SerializeField] GameObject blankImage, annotatedCC, spongeDL, spongeStub;
    [SerializeField] GameObject applicationViewer, currPage;
    [SerializeField] Button prevPageButton, nextPageButton, closeButton, submitButton, deleteButton;
    [SerializeField] List<GameObject> selectors;
    [SerializeField] TextMeshProUGUI pageCounter;

    List<GameObject> fields = new();

    GameObject selectedLabel, selectedField;

    int pageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        RenderPage(appId, pageIndex);

        prevPageButton.onClick.AddListener(TaskOnPrevPageButtonClicked);
        nextPageButton.onClick.AddListener(TaskOnNextPageButtonClicked);
        closeButton.onClick.AddListener(TaskOnCloseButtonClicked);
        submitButton.onClick.AddListener(TaskOnSubmitButtonClicked);
        deleteButton.onClick.AddListener(TaskOnDeleteButtonClicked);

        for (int i = 0; i < selectors.Count; i++)
        {
            int temp = i;
            selectors[i].GetComponent<Button>().onClick.AddListener(() => TaskOnSelected(temp));
        }
    }

    void TaskOnPrevPageButtonClicked()
    {
        if (pageIndex > 0)
        {
            pageIndex--;
            RenderPage(appId, pageIndex);
        }
    }

    void TaskOnNextPageButtonClicked()
    {
        if (pageIndex < ApplicationObject.Length(appId) - 1)
        {
            pageIndex++;
            RenderPage(appId, pageIndex);
        }
    }

    void TaskOnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }

    void TaskOnSubmitButtonClicked()
    {
        gameObject.SetActive(false);
    }

    void TaskOnDeleteButtonClicked()
    {
        gameObject.SetActive(false);
    }

    void TaskOnSelected(int index)
    {
        if (selectedField != null)
        {
            selectedField.GetComponent<Button>().interactable = false;
            selectedField.GetComponent<Outline>().enabled = false;
            selectors[index].GetComponent<Button>().interactable = false;
            selectedField = null;

            if (IsValidApp())
            {
                submitButton.interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < selectors.Count; i++)
            {
                if (i != index)
                {
                    selectors[i].GetComponent<Outline>().enabled = false;
                }
                else
                {
                    selectors[i].GetComponent<Outline>().enabled = true;
                    selectedLabel = selectors[i];
                }

            }
        }
    }

    void TaskOnSelectedField(int index)
    {
        if (selectedLabel != null)
        {
            selectedLabel.GetComponent<Button>().interactable = false;
            selectedLabel.GetComponent<Outline>().enabled = false;
            fields[index].GetComponent<Button>().interactable = false;
            selectedLabel = null;

            if (IsValidApp())
            {
                submitButton.interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < fields.Count; i++)
            {
                if (i != index)
                {
                    fields[i].GetComponent<Outline>().enabled = false;
                }
                else
                {
                    fields[i].GetComponent<Outline>().enabled = true;
                    selectedField = fields[i];
                }

            }
        }
    }

    // HELPERS

    void RenderPage(ApplicationObject.ApplicationId appId, int pageIndex)
    {
        Destroy(currPage);

        GameObject newPage = blankImage;

        switch(appId)
        {
            case ApplicationObject.ApplicationId.Spongebob:
                switch(pageIndex)
                {
                    case 0:
                        newPage = annotatedCC;

                        currPage = Instantiate(newPage, applicationViewer.transform.position, Quaternion.identity);

                        fields.Add(currPage.transform.Find("Image/AddressBox").gameObject);
                        fields.Add(currPage.transform.Find("Image/NameBox").gameObject);
                        fields.Add(currPage.transform.Find("Image/IncomeBox").gameObject);
                        fields.Add(currPage.transform.Find("Image/SignatureBox").gameObject);
                        fields.Add(currPage.transform.Find("Image/SSNBox").gameObject);

                        for (int i = 0; i < fields.Count; i++)
                        {
                            int temp = i;
                            fields[i].GetComponent<Button>().onClick.AddListener(() => TaskOnSelectedField(temp));
                        }

                        currPage.transform.SetParent(applicationViewer.transform, true);
                        return;
                    case 1:
                        newPage = spongeStub;
                        break;
                    case 2:
                        newPage = spongeDL;
                        break;
                }

                pageCounter.text = $"{pageIndex+1}/3";

                break;
            default:
                break;
        }

        currPage = Instantiate(newPage, applicationViewer.transform.position, Quaternion.identity);
        currPage.transform.SetParent(applicationViewer.transform, true);
    }

    bool IsValidApp()
    {
        return selectors.TrueForAll(selector => !selector.GetComponent<Button>().interactable);
    }
}
