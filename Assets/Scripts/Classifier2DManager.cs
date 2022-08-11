using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Classifier2DManager : MonoBehaviour
{
    [SerializeField] ApplicationObject.ApplicationId appId;
    [SerializeField] GameObject blankImage, spongeCCApp, spongeDL, spongeStub, krabCCApp, krabbyDL, krabStub, krabFlyer, krabPatty, krabMenu;
    [SerializeField] GameObject applicationViewer, currPage;
    [SerializeField] Button prevPageButton, nextPageButton, closeButton, submitButton, deleteButton;
    [SerializeField] GameObject applicationTypeStep, creditCardStep, mortgageStep;
    [SerializeField] GameObject ccDriversLicenseDraggable, ccPaystubDraggable, mDriversLicenseDraggable, mTaxReturnDraggable;
    [SerializeField] TextMeshProUGUI pageCounter;

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
                        newPage = spongeCCApp;
                        break;
                    case 1:
                        newPage = spongeStub;
                        break;
                    case 2:
                        newPage = spongeDL;
                        break;
                }

                pageCounter.text = $"{pageIndex+1}/3";

                break;
            case ApplicationObject.ApplicationId.Sandy:
                break;
            case ApplicationObject.ApplicationId.Patrick:
                break;
            case ApplicationObject.ApplicationId.MrKrabs:
                switch (pageIndex)
                {
                    case 0:
                        newPage = krabCCApp;
                        break;
                    case 1:
                    case 8:
                        newPage = krabFlyer;
                        break;
                    case 3:
                        newPage = krabStub;
                        break;
                    case 5:
                    case 7:
                        newPage = krabMenu;
                        break;
                    case 6:
                        newPage = spongeDL;
                        break;
                    case 9:
                        newPage = krabbyDL;
                        break;
                    default:
                        newPage = blankImage;
                        break;
                }

                pageCounter.text = $"{pageIndex+1}/???";
                break;
        }

        currPage = Instantiate(newPage, applicationViewer.transform.position, Quaternion.identity);
        currPage.transform.SetParent(applicationViewer.transform, true);
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
