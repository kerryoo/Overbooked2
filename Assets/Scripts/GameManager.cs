using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Timer gameTimer;
    [SerializeField] UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        gameTimer.setTimer(BalanceSheet.timePerLevel);
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.setTimeLeft(gameTimer.timeLeft);
    }
}
