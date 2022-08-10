using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Classifier2DManager : MonoBehaviour
{
    [SerializeField] GameObject spongeCCApp, spongeDL, spongeStub;
    [SerializeField] GameObject applicationViewer, currPage;
    [SerializeField] Button prevPageButton, nextPageButton, closeButton, submitButton, deleteButton;
    [SerializeField] GameObject applicationTypeStep, creditCardStep, mortgageStep;
    [SerializeField] GameObject ccDriversLicenseDraggable, ccPaystubDraggable, mDriversLicenseDraggable, mTaxReturnDraggable;

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

    void renderPage(ApplicationObject.ApplicationId appId, int pageIndex)
    {
        Destroy(currPage);

        switch(appId)
        {
            case ApplicationObject.ApplicationId.Spongebob:
                switch(pageIndex)
                {
                    case 0:
                        currPage = Instantiate(spongeCCApp, applicationViewer.transform.position, Quaternion.identity);
                        currPage.transform.SetParent(applicationViewer.transform, true);
                        break;
                    case 1:
                        currPage = Instantiate(spongeDL, applicationViewer.transform.position, Quaternion.identity);
                        currPage.transform.SetParent(applicationViewer.transform, true);
                        break;
                    case 2:
                        currPage = Instantiate(spongeStub, applicationViewer.transform.position, Quaternion.identity);
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
        return creditCardStep.activeSelf && !ccDriversLicenseDraggable.GetComponent<Button>().interactable && !ccPaystubDraggable.GetComponent<Button>().interactable;
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
