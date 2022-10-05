using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

public class ImgUIManager : MonoBehaviourPun
{
	//Canvas canvas = null;

	[SerializeField] Sprite[] items = new Sprite[7];
	Dictionary<string, Sprite> itemslist;

	private void Awake()
	{
		//canvas = FindObjectOfType<Canvas>();
	}
    private void Start()
    {
		itemslist = new Dictionary<string, Sprite>();

		itemslist.Add("Bullet", items[0]);
		itemslist.Add("Handgun", items[1]);
		itemslist.Add("Repeater", items[2]);
		itemslist.Add("Shotgun", items[3]);
		itemslist.Add("SniperRifle", items[4]);
		itemslist.Add("Grenade", items[5]);
		itemslist.Add("Shield", items[6]);
	}


	public void RenderItem(string GetblockItemName, Vector3 blockpos)
    {
		Vector3 screenPos = Camera.main.WorldToScreenPoint(blockpos);
		

		Image instimg = PhotonNetwork.Instantiate("ItemUI", screenPos, Quaternion.identity).GetComponent<Image>();
		instimg.sprite = itemslist[GetblockItemName];
		instimg.transform.parent = this.transform;
		//instimg.transform.parent = canvas.transform;
	}


}
