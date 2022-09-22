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
        PinballDataList.Add("Repeater");
        PinballDataList.Add("Shotgun");
        PinballDataList.Add("SniperRifle");
    }

    // 데이터 받아오고 분리 하는 함수
    // TEST 매개 변수로 List 받아와야 함
    public List<string> GetAndSplitData(string name) 
    {
        
        if(PinballDataList.Count != 0 && itemData.Count == 0 && gunData.Count == 0)
        {
            for (int i = 0; i < PinballDataList.Count; i++)
            {
                if (PinballDataList[i].Equals("Bullet")
                    || PinballDataList[i].Equals("Grenade")
                    || PinballDataList[i].Equals("Shield"))
                {
                    itemData.Add(PinballDataList[i]);
                }
                else if (PinballDataList[i].Equals("Handgun")
                    || PinballDataList[i].Equals("Repeater")
                    || PinballDataList[i].Equals("Shotgun")
                    || PinballDataList[i].Equals("SniperRifle"))
                {
                    gunData.Add(PinballDataList[i]);
                }
            }
        }

        if (name.Equals("Item"))
        {
            return itemData;
        }
        else if (name.Equals("Gun"))
        {
            return gunData;
        }

        return null;
        
    }

    public void ResetLists()
    {
        PinballDataList.Clear();
        itemData.Clear();
        gunData.Clear();
    }
    
}
