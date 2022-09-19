using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    List<string> items;
    Player player = null;
    float shieldTime = 2f;
    public GameObject GrenadePrefab = null;
    SpriteRenderer spriteRender = null;

    private void Awake()
    {
        items = new List<string>();
    }

    void Start()
    {
        player = GetComponent<Player>();
        spriteRender = player.GetComponent<SpriteRenderer>();
        items.Add("bullet");
        items.Add("Grenade");
        items.Add("Shield");

        for(int i = 0; i < items.Count; i++)
        {
            if(items[i] == "bullet")
            {
                player.bulletCount += 5;
                items.RemoveAt(i);
            }
        }
    }

    public void StartItemCoroutine()
    {
        StartCoroutine(items[player.MyItemIndex]);
    }

    IEnumerator Shield()
    {
        spriteRender.color = Color.blue;
        player.IsShieldTime = true;
        yield return new WaitForSeconds(shieldTime);
        spriteRender.color = Color.yellow;
        player.IsShieldTime = false;
    }

    IEnumerator Grenade()
    {
        GameObject grenade = Instantiate(GrenadePrefab);
        grenade.transform.position = player.transform.position + new Vector3(0.5f,0.5f,0f);
        Rigidbody2D myRigidbody = grenade.GetComponent<Rigidbody2D>();
        myRigidbody.AddForce(player.GetFireDir() * 1000f);
        yield return new WaitForSeconds(3f);
        GrenadeExplosion(grenade);
        Destroy(grenade.gameObject);
    }

    private void GrenadeExplosion(GameObject grenade)
    {
        Player target = player.GetTarget();

        if(Vector3.Distance(grenade.transform.position,target.transform.position) <= 5f)
        {
            target.SendMessage("StartKnockBackCoroutine",grenade.transform.position,SendMessageOptions.DontRequireReceiver);
        }
        
    }
    
}
