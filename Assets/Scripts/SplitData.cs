using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet(�Ѿ�) Grenade(����ź) Shield(��)
// Handgun(����) - Repeater(������) - Shotgun - SniperRifle(������)
public class SplitData : MonoBehaviour
{
    List<string> gunData;
    List<string> itemData;
    List<string> testList;
    private void Awake()
    {
        gunData = new List<string>();
        itemData = new List<string>();
        testList = new List<string>();
        testList.Add("Bullet");
        testList.Add("Grenade");
        testList.Add("Shield");
        testList.Add("Grenade");
        testList.Add("Shield");
        testList.Add("Grenade");
        testList.Add("Shield");
        testList.Add("Grenade");
        testList.Add("Shield");
    }

    private void Start()
    {
        GetAndSplitData(testList);
    }

    // ������ �޾ƿ��� �и� �ϴ� �Լ�
    public void GetAndSplitData(List<string> data) 
    {
        for(int i = 0; i < data.Count; i++)
        {
            if(data[i].Equals("Bullet") 
                || data[i].Equals("Grenade")
                || data[i].Equals("Shield"))
            {
                itemData.Add(data[i]);
            }
            else if(data[i].Equals("Handgun")
                || data[i].Equals("Repeater")
                || data[i].Equals("Shotgun")
                || data[i].Equals("SniperRifle"))
            {
                gunData.Add(data[i]);
            }
        }

        Items[] myItemsData = FindObjectsOfType<Items>();

        for(int i = 0;i < myItemsData.Length; i++)
        {
            myItemsData[i].SetList(itemData);
            myItemsData[i].ItemListSetting();
        }
    }

    
}
