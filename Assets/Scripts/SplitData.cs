using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

// Bullet(총알) Grenade(수류탄) Shield(방어막)
// Handgun(권총) - Repeater(연발총) - Shotgun - SniperRifle(저격총)
public class SplitData : MonoBehaviourPun
{
    List<string> gunData;
    List<string> itemData;
    List<string> PinballDataList;
    private void Awake()
    {
        gunData = new List<string>();
        itemData = new List<string>();
        PinballDataList = new List<string>();
        PinballDataList.Add("Bullet");
        PinballDataList.Add("Grenade");
        PinballDataList.Add("Shield");
        PinballDataList.Add("Grenade");
        PinballDataList.Add("Shield");
        PinballDataList.Add("Grenade");
        PinballDataList.Add("Shield");
        PinballDataList.Add("Grenade");
        PinballDataList.Add("Shield");
    }

    // 데이터 받아오고 분리 하는 함수
    // TEST 매개 변수로 List 받아와야 함
    public void GetAndSplitData() 
    {
        List<string> data = PinballDataList;
        for(int i = 0; i < data.Count; i++)
        {
            if(PinballDataList[i].Equals("Bullet") 
                || PinballDataList[i].Equals("Grenade")
                || PinballDataList[i].Equals("Shield"))
            {
                itemData.Add(data[i]);
            }
            else if(PinballDataList[i].Equals("Handgun")
                || PinballDataList[i].Equals("Repeater")
                || PinballDataList[i].Equals("Shotgun")
                || PinballDataList[i].Equals("SniperRifle"))
            {
                gunData.Add(PinballDataList[i]);
            }
        }

        Items[] myItemsData = FindObjectsOfType<Items>();
        
        for(int i = 0;i < myItemsData.Length; i++)
        {
            if(myItemsData[i].ItemIsMine == true)
            {
                myItemsData[i].SetList(itemData);
                myItemsData[i].ItemListSetting();
            }
        }
    }
    
}
