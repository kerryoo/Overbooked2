using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Timer gameTimer;
    [SerializeField] UIManager uiManager;

    [SerializeField] GameObject[] buyableItems;
    [SerializeField] GameObject deliveryVan;
    [SerializeField] Transform deliverySpawnLocation;
    [SerializeField] Transform dropOffLocation;
    [SerializeField] Transform leaveLocation;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject explosions;
    [SerializeField] GameObject terribleUI;
    [SerializeField] CameraControl cameraControl;

    private float cash;
    private bool started = false;

    // Update is called once per frame
    void Update()
    {
        uiManager.setTimeLeft(gameTimer.timeLeft);
        uiManager.setCashAmount(cash);

        if (Input.GetKeyDown(KeyCode.P)) {
            cash += 50;
        }

        if (Input.GetKeyDown(KeyCode.O) && !started)
        {
            explosions.SetActive(true);
            mainMenu.SetActive(true);
            Destroy(terribleUI);
            cameraControl.goToMenuAngle();
            started = true;
        }
    }

    public void onGameStart()
    {
        uiManager.turnOnMainUI();
        gameTimer.setTimer(BalanceSheet.timePerLevel);
        
    }

    public void onBuy(int id)
    {
        GameObject van = Instantiate(deliveryVan, deliverySpawnLocation.position, deliverySpawnLocation.rotation);
        DeliveryVan vanScript = van.GetComponent<DeliveryVan>();
        vanScript.setFields(dropOffLocation, leaveLocation, buyableItems[id]);
    }
    public void addCash(float amount)
    {
        cash += amount;
    }
}
