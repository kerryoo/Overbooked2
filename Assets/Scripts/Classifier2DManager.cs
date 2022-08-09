using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Classifier2DManager : MonoBehaviour
{
    [SerializeField] GameObject applicationPageText;
    [SerializeField] Button prevPageButton, nextPageButton, closeButton, submitButton;
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
        app.pages.Add(new Page("blah blah blah"));
        app.pages.Add(new Page("here is page 2"));
        app.pages.Add(new Page("here is page 3"));

        pageTMP = applicationPageText.GetComponent<TextMeshProUGUI>();
        pageTMP.text = app.pages[pageIndex].text;

        prevPageButton.onClick.AddListener(TaskOnPrevPageButtonClicked);
        nextPageButton.onClick.AddListener(TaskOnNextPageButtonClicked);
        closeButton.onClick.AddListener(TaskOnCloseButtonClicked);
        submitButton.onClick.AddListener(TaskOnSubmitButtonClicked);

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

    }

    void TaskOnCreditCardToggleChanged(bool value)
    {
        if (value)
        {
            untoggleMortgage();
        }
    }

    void TaskOnCreditCardSubToggleChanged(bool value, int index)
    {
        if (value)
        {
            creditCardToggle.isOn = true;
            untoggleMortgage();
        }
    }

    void TaskOnMortgageToggleChanged(bool value)
    {
        if (value)
        {
            untoggleCreditCard();
        }
    }

    void TaskOnMortgageSubToggleChanged(bool value, int index)
    {
        if (value)
        {
            mortgageToggle.isOn = true;
            untoggleCreditCard();
        }
    }

    void untoggleCreditCard()
    {
        creditCardToggle.isOn = false;
        foreach (Toggle toggle in creditCardSubToggles)
        {
            toggle.isOn = false;
        }
    }

    void untoggleMortgage()
    {
        mortgageToggle.isOn = false;
        foreach (Toggle toggle in mortgageSubToggles)
        {
            toggle.isOn = false;
        }
    }
}
