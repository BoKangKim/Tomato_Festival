using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    List<string> items;
    float shieldTime = 2f;

    private void Awake()
    {
        items = new List<string>();
    }

    void Start()
    {
        Player player = GetComponent<Player>();
        items.Add("bullet");
        items.Add("grenade");
        items.Add("shield");

        for(int i = 0; i < items.Count; i++)
        {
            if(items[i] == "bullet")
            {
                player.bulletCount += 5;
                items.RemoveAt(i);
            }
        }
    }

    IEnumerator Shield()
    {
        yield return null;
    }

    IEnumerator Grenade()
    {
        yield return null;
    }
    
}
