using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cashAmount;
    [SerializeField] TextMeshProUGUI timeLeft;
    [SerializeField] ProgressBar clock;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject marketPlaceUI;

    private bool isMarketPlaceOpen = false;

    public void setCashAmount(float cash)
    {
        cashAmount.SetText(cash.ToString("f2"));
    }

    public void setTimeLeft(float time)
    {
        float timeElapsed = BalanceSheet.timePerLevel - time;
        float multiplier = 480f / BalanceSheet.timePerLevel;
        float adjustedTimeElapsed = multiplier * timeElapsed + 540f;
        float timeElapsedPercentage = timeElapsed / BalanceSheet.timePerLevel;
        clock.currentPercent = timeElapsedPercentage * 100;

        if (adjustedTimeElapsed > 720)
        {
            adjustedTimeElapsed -= 720;
            int minutes = Mathf.FloorToInt(adjustedTimeElapsed % 60);
            int hours = Mathf.FloorToInt(adjustedTimeElapsed / 60);
            if (hours == 0)
            {
                hours = 12;
            }
            timeLeft.SetText(string.Format("{0:00}:{1:00}", hours, minutes) + "PM");
        } else
        {
            int minutes = Mathf.FloorToInt(adjustedTimeElapsed % 60);
            int hours = Mathf.FloorToInt(adjustedTimeElapsed / 60);
            if (hours == 0)
            {
                hours = 12;
            }
            timeLeft.SetText(string.Format("{0:00}:{1:00}", hours, minutes) + "AM");
        }
    }

    public void turnOnMainUI()
    {
        mainUI.SetActive(true);
    }

    public void toggleMarketPlace()
    {
        isMarketPlaceOpen = !isMarketPlaceOpen;
        marketPlaceUI.SetActive(isMarketPlaceOpen);
    }
}
