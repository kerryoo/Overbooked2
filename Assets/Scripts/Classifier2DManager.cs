using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Classifier2DManager : MonoBehaviour
{
    [SerializeField] GameObject applicationPageText;
    [SerializeField] Button prevPageButton, nextPageButton, closeButton, submitButton, deleteButton;
    [SerializeField] Toggle creditCardToggle;
    [SerializeField] List<Toggle> creditCardSubToggles;
    [SerializeField] Toggle mortgageToggle;
    [SerializeField] List<Toggle> mortgageSubToggles;

    ApplicationObject app;
    TextMeshProUGUI pageTMP;
    int pageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        app = gameObject.AddComponent<ApplicationObject>();
        app.pages.Add(new Page("credit card app"));
        app.pages.Add(new Page("blank page"));
        app.pages.Add(new Page("zzz\n\n\n\n\n\n\n\ncredit score: 1000"));
        app.pages.Add(new Page("driver's license"));

        pageTMP = applicationPageText.GetComponent<TextMeshProUGUI>();
        pageTMP.text = app.pages[pageIndex].text;

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
            pageTMP.text = app.pages[pageIndex].text;
        }
    }

    void TaskOnNextPageButtonClicked()
    {
        if (pageIndex < app.pages.Count - 1)
        {
            pageIndex++;
            pageTMP.text = app.pages[pageIndex].text;
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
