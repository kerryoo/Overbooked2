using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Timer gameTimer;
    [SerializeField] UIManager uiManager;

    private float cash;

    // Update is called once per frame
    void Update()
    {
        uiManager.setTimeLeft(gameTimer.timeLeft);
        uiManager.setCashAmount(cash);

        if (Input.GetKeyDown(KeyCode.P)) {
            cash += 50;
        }
    }

    public void onGameStart()
    {
        uiManager.turnOnMainUI();
        gameTimer.setTimer(BalanceSheet.timePerLevel);
        
    }
}
