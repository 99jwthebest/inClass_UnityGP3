using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class ShopItemWidget : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image grayoutImage;
    [SerializeField] Color grayoutColor;
    [SerializeField] Color normalColor;

    ShopItem item;

    internal void Init(ShopItem item, int credits)
    {
        icon.sprite = item.Icon;
        itemName.text = item.Title;
        price.text = "$" + item.Price.ToString();
        description.text = item.Description;
        this.item = item;

        Refresh(credits);
    }

    public void Refresh(int credits)
    {
        if (credits < item.Price)
        {
            grayoutImage.enabled = true;
            price.color = grayoutColor;
        }
        else
        {
            grayoutImage.enabled = false;
            price.color = normalColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
