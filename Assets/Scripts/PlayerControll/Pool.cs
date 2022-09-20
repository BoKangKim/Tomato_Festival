using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Pool : MonoBehaviour, IPunPrefabPool 
{
    //#region Singleton
    //private static BulletPool instance = null;

    //public static BulletPool Inst
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindObjectOfType<BulletPool>();
    //            if (instance == null)
    //            {
    //                Debug.LogError("instance is null");
    //            }
    //        }

    //        return instance;
    //    }
    //}

    //#endregion

    public readonly Dictionary<string, GameObject> ResourceCache = new Dictionary<string, GameObject>();
    List<GameObject> pool_list;

    void Awake()
    {
        PhotonNetwork.PrefabPool = this;
        pool_list = new List<GameObject>();
    }

    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        GameObject inst = null;
        bool cached = this.ResourceCache.TryGetValue(prefabId, out inst);
        if (!cached)
        {
            inst = Resources.Load<GameObject>(prefabId);
            if(inst == null)
            {
                Debug.LogError("Not Found " + prefabId + "Check BulletPool.cs");
            }
            else
            {
                this.ResourceCache.Add(prefabId,inst);
            }
        }

        GameObject instance = null;

        if (pool_list.Count == 0)
        {
            instance = GameObject.Instantiate(inst, position, rotation) as GameObject;
        }
        else if(pool_list.Count > 0)
        {
            instance = pool_list[0];
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            pool_list.RemoveAt(0);
        }

        if(instance != null)
        {
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            Debug.LogError("Instance is Null Check Pool.cs");
            return null;
        }
    }

    public void Destroy(GameObject gameObject)
    {
        gameObject.SetActive(false);
        pool_list.Add(gameObject);
    }
}
