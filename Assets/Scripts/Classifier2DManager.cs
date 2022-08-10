using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Classifier2DManager : MonoBehaviour
{
    [SerializeField] GameObject linePrefab;

    [SerializeField] GameObject applicationPage;
    [SerializeField] Button prevPageButton, nextPageButton, closeButton, submitButton, deleteButton;
    [SerializeField] GameObject applicationTypeStep;

    ApplicationObject app;
    int pageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: pass in applicationObject
        app = gameObject.AddComponent<ApplicationObject>();
        app.pages.Add(new Page(new List<string>{"credit card app"}));
        app.pages.Add(new Page(new List<string> {"blank page"}));
        app.pages.Add(new Page(new List<string> {"zzz", "", "", "", "", "", "", "credit score: 500"}));
        app.pages.Add(new Page(new List<string> {"driver's license"}));

        renderPage(app.pages[0]);

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
            renderPage(app.pages[pageIndex]);
        }
    }

    void TaskOnNextPageButtonClicked()
    {
        if (pageIndex < app.pages.Count - 1)
        {
            pageIndex++;
            renderPage(app.pages[pageIndex]);
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
        if (applicationDraggable.transform.parent.gameObject.name == "ApplicationTypeStep")
        {
            submitButton.gameObject.SetActive(true);
            applicationTypeStep.SetActive(false);

        }
    }

    // HELPERS

    void renderPage(Page page)
    {

    }


    bool IsValidCreditCardApp()
    {
        return true;// creditCardToggle.isOn && creditCardSubToggles.TrueForAll((toggle) => toggle.isOn);
    }

    bool IsValidMortgageApp()
    {
        return true;// mortgageToggle.isOn && mortgageSubToggles.TrueForAll((toggle) => toggle.isOn);
    }

    bool IsValidApp()
    {
        return IsValidCreditCardApp() || IsValidMortgageApp();
    }

    void UpdateSubmitButton()
    {
        submitButton.interactable = IsValidApp();
    }
}
