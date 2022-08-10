using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Classifier2DManager : MonoBehaviour
{
    [SerializeField] GameObject applicationPrefab, licensePrefab;
    [SerializeField] GameObject applicationViewer, currPage;
    [SerializeField] Button prevPageButton, nextPageButton, closeButton, submitButton, deleteButton;
    [SerializeField] GameObject applicationTypeStep, creditCardStep, mortgageStep;
    [SerializeField] GameObject ccDriversLicenseDraggable, ccCreditReportDraggable, mDriversLicenseDraggable, mTaxReturnDraggable;

    ApplicationObject.ApplicationId appId;
    int pageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: pass in applicationObject
        appId = ApplicationObject.ApplicationId.Spongebob;

        renderPage(appId, pageIndex);

        prevPageButton.onClick.AddListener(TaskOnPrevPageButtonClicked);
        nextPageButton.onClick.AddListener(TaskOnNextPageButtonClicked);
        closeButton.onClick.AddListener(TaskOnCloseButtonClicked);
        submitButton.onClick.AddListener(TaskOnSubmitButtonClicked);
        deleteButton.onClick.AddListener(TaskOnDeleteButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnPrevPageButtonClicked()
    {
        if (pageIndex > 0)
        {
            pageIndex--;
            renderPage(appId, pageIndex);
        }
    }

    void TaskOnNextPageButtonClicked()
    {
        if (pageIndex < ApplicationObject.Length(appId) - 1)
        {
            pageIndex++;
            renderPage(appId, pageIndex);
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

    public void SelectApplication(GameObject applicationDraggable)
    {
        if (applicationDraggable.name == "CreditCardDraggable")
        {
            submitButton.gameObject.SetActive(true);
            applicationTypeStep.SetActive(false);
            creditCardStep.SetActive(true);
        } else if (applicationDraggable.name == "MortgageDraggable")
        {
            submitButton.gameObject.SetActive(true);
            applicationTypeStep.SetActive(false);
            mortgageStep.SetActive(true);
        }

        if (IsValidApp())
        {
            submitButton.interactable = true;
        }
    }

    // HELPERS

    GameObject SetApplicationPage(string name, string address, string ssn, string income)
    {
        GameObject appPage = Instantiate(applicationPrefab, applicationViewer.transform.position, Quaternion.identity);

        appPage.transform.Find("Field/Textbox/FieldText").GetComponent<TextMeshProUGUI>().text = name;
        appPage.transform.Find("Field (1)/Textbox/FieldText").GetComponent<TextMeshProUGUI>().text = address;
        appPage.transform.Find("Field (2)/Textbox/FieldText").GetComponent<TextMeshProUGUI>().text = ssn;
        appPage.transform.Find("Field (3)/Textbox/FieldText").GetComponent<TextMeshProUGUI>().text = income;

        return appPage;
    }

    void renderPage(ApplicationObject.ApplicationId appId, int pageIndex)
    {
        Destroy(currPage);

        switch(appId)
        {
            case ApplicationObject.ApplicationId.Spongebob:
                switch(pageIndex)
                {
                    case 0:
                        currPage = SetApplicationPage("Sponge", "a", "b", "c");
                        currPage.transform.SetParent(applicationViewer.transform, true);
                        break;
                    case 1:
                        currPage = Instantiate(licensePrefab, applicationViewer.transform.position, Quaternion.identity);
                        currPage.transform.SetParent(applicationViewer.transform, true);
                        break;
                    case 2:
                        currPage = SetApplicationPage("zzz", "zzz", "zzz", "zzz");
                        currPage.transform.SetParent(applicationViewer.transform, true);
                        break;
                }
                break;
            case ApplicationObject.ApplicationId.Sandy:
                break;
            case ApplicationObject.ApplicationId.Patrick:
                break;
            case ApplicationObject.ApplicationId.MrKrabs:
                break;
        }

        
    }


    bool IsValidCreditCardApp()
    {
        return creditCardStep.activeSelf && !ccDriversLicenseDraggable.GetComponent<Button>().interactable && !ccCreditReportDraggable.GetComponent<Button>().interactable;
    }

    bool IsValidMortgageApp()
    {
        return mortgageStep.activeInHierarchy && !mDriversLicenseDraggable.GetComponent<Button>().interactable && !mTaxReturnDraggable.GetComponent<Button>().interactable;
    }

    bool IsValidApp()
    {
        return IsValidCreditCardApp() || IsValidMortgageApp();
    }
}
