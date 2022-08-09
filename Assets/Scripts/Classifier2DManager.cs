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
    [SerializeField] Toggle creditCardToggle;
    [SerializeField] List<Toggle> creditCardSubToggles;
    [SerializeField] Toggle mortgageToggle;
    [SerializeField] List<Toggle> mortgageSubToggles;

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

        creditCardToggle.onValueChanged.AddListener(TaskOnCreditCardToggleChanged);
        for (int i=0; i<creditCardSubToggles.Count; i++)
        {
            creditCardSubToggles[i].onValueChanged.AddListener((value) =>
            {
                TaskOnCreditCardSubToggleChanged(value, i);
            });
        }

        mortgageToggle.onValueChanged.AddListener(TaskOnMortgageToggleChanged);
        for (int i = 0; i < mortgageSubToggles.Count; i++)
        {
            mortgageSubToggles[i].onValueChanged.AddListener((value) =>
            {
                TaskOnMortgageSubToggleChanged(value, i);
            });
        }
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

    void TaskOnCreditCardToggleChanged(bool value)
    {
        if (value)
        {
            UntoggleMortgage();
        } else
        {
            UntoggleCreditCard();
        }
        UpdateSubmitButton();
    }

    void TaskOnCreditCardSubToggleChanged(bool value, int index)
    {
        if (value)
        {
            creditCardToggle.isOn = true;
            UntoggleMortgage();
        }
        UpdateSubmitButton();
    }

    void TaskOnMortgageToggleChanged(bool value)
    {
        if (value)
        {
            UntoggleCreditCard();
        } else
        {
            UntoggleMortgage();
        }
        UpdateSubmitButton();
    }

    void TaskOnMortgageSubToggleChanged(bool value, int index)
    {
        if (value)
        {
            mortgageToggle.isOn = true;
            UntoggleCreditCard();
        }
        UpdateSubmitButton();
    }

    // HELPERS

    void renderPage(Page page)
    {
        foreach (Transform child in applicationPage.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (string line in page.lines)
        {
            GameObject pageLine = Instantiate(linePrefab);
            pageLine.transform.SetParent(applicationPage.transform, false);
            pageLine.transform.Find("Canvas/Text").gameObject.GetComponent<TextMeshProUGUI>().text = line;
        }
    }

    void UntoggleCreditCard()
    {
        creditCardToggle.isOn = false;
        foreach (Toggle toggle in creditCardSubToggles)
        {
            toggle.isOn = false;
        }
    }

    void UntoggleMortgage()
    {
        mortgageToggle.isOn = false;
        foreach (Toggle toggle in mortgageSubToggles)
        {
            toggle.isOn = false;
        }
    }

    bool IsValidCreditCardApp()
    {
        return creditCardToggle.isOn && creditCardSubToggles.TrueForAll((toggle) => toggle.isOn);
    }

    bool IsValidMortgageApp()
    {
        return mortgageToggle.isOn && mortgageSubToggles.TrueForAll((toggle) => toggle.isOn);
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
