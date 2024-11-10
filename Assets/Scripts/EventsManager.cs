using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    UIController uiController;
    PlayerMoneyManager playerMoneyManager;
    StackManager stackManager;
    private void Start()
    {
        playerMoneyManager = FindAnyObjectByType<PlayerMoneyManager>();
        uiController = FindAnyObjectByType<UIController>();
        stackManager = FindAnyObjectByType<StackManager>();

        playerMoneyManager.UpdatedMoney += uiController.OnUpdatedMoney;
        stackManager.UpdatedStackCapacity += uiController.OnUpdatedStackCapacity;
    }
}
