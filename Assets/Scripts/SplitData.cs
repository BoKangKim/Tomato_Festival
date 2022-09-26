using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

// Bullet(�Ѿ�) Grenade(����ź) Shield(��)
// Handgun(����) - Repeater(������) - Shotgun - SniperRifle(������)
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
        PinballDataList.Add("SniperRifle");
        PinballDataList.Add("Shotgun");
    }

    // ������ �޾ƿ��� �и� �ϴ� �Լ�
    // TEST �Ű� ������ List �޾ƿ;� ��
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
