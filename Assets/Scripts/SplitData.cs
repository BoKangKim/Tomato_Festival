using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

// Bullet(총알) Grenade(수류탄) Shield(방어막)
// Handgun(권총) - Repeater(연발총) - Shotgun - SniperRifle(저격총)
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
        //GetAndSplitData(testList);


    }

    // 데이터 받아오고 분리 하는 함수
    // TEST 매개 변수로 List 받아와야 함
    public void GetAndSplitData() 
    {
        List<string> data = testList;
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
        Debug.Log(myItemsData.Length);
        for(int i = 0;i < myItemsData.Length; i++)
        {
            if (myItemsData[i].photonView.IsMine)
            {
                Debug.Log("Split Data");
                myItemsData[i].SetList(itemData);
                myItemsData[i].ItemListSetting();
            }
        }
    }

    
}
