using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    [SerializeField] private float money;
    public delegate void UpdateMoney(float money);

    public event UpdateMoney UpdatedMoney;
    public void RemoveMoney(float value)
    {
        money -= value;
        if (UpdatedMoney != null)
        {
            UpdatedMoney(money);
        }
    }
    public void AddMoney(float value)
    {
        money += value;
        if (UpdatedMoney != null)
        {
            UpdatedMoney(money);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sell"))
        {
            if (StackManager.Instance.npcList.Count > 0)
            {
                AudioManager.Instance.PlaySellSound();
                AddMoney(StackManager.Instance.SellNpcs());
            }
        }
        else if (other.CompareTag("Buy"))
        {
            if (money >= StackManager.Instance.stackPrice)
            {
                AudioManager.Instance.PlayBuySound();
                StartCoroutine(BuyItemsCoroutine());
            }
        }
    }

    private IEnumerator BuyItemsCoroutine()
    {
        PlayerMeshManager._instance.UpdateColor();
        while (money >= StackManager.Instance.stackPrice)
        {
            RemoveMoney(StackManager.Instance.stackPrice);
            StackManager.Instance.AddNpcCapacity(1);

            yield return null;
        }
    }



}
