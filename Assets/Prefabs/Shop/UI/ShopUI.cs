using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Shop shop;
    
    [SerializeField] ShopItemWidget itemWidgetPrefab;
    
    [SerializeField] Transform shopList;

    [SerializeField] CreditComponent creditComponent;

    List<ShopItemWidget> shopItemWidgets = new List<ShopItemWidget>();

    private void Awake()
    {

        foreach(ShopItem item in shop.GetItems())
        {
            ShopItemWidget newWidget = Instantiate(itemWidgetPrefab, shopList);
            newWidget.Init(item, creditComponent.GetCredits());
            shopItemWidgets.Add(newWidget);
        }

        creditComponent.onCreditChanged += RefreshItems;

    }

    private void RefreshItems(int newCredit)
    {
        foreach(ShopItemWidget item in shopItemWidgets)
        {
            item.Refresh(newCredit);

        }
    }
}
