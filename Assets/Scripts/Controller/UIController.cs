using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyTxt;
    [SerializeField] private TextMeshProUGUI capacityTxt;
    void Awake()
    {
        GameObject moneyUIObject = GameObject.Find("MoneyTxt");
        GameObject stackUIObject = GameObject.Find("CapacityTxt");
        if (moneyUIObject != null)
        {
            moneyTxt = moneyUIObject.GetComponent<TextMeshProUGUI>();
        }
        if(stackUIObject != null)
        {
            capacityTxt = stackUIObject.GetComponent<TextMeshProUGUI>();
        }
    }

    public void OnUpdatedMoney(float money)
    {
        moneyTxt.text = money.ToString();
    }
    public void OnUpdatedStackCapacity(int capacity)
    {
        capacityTxt.text = capacity.ToString();
    }
}
