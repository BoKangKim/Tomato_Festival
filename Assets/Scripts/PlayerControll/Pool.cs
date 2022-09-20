using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Pool : MonoBehaviour, IPunPrefabPool 
{
    public readonly Dictionary<string, GameObject> ResourceCache = new Dictionary<string, GameObject>();
    List<GameObject> pool_list;
    string myPoolResourceList;

    void Awake()
    {
        myPoolResourceList = "Bullet";
        pool_list = new List<GameObject>();
    }

    void OnEnable()
    {
        PhotonNetwork.PrefabPool = this;
    }

    void OnDisable()
    {
        PhotonNetwork.PrefabPool = default;
    }

    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        GameObject inst = null;
        GameObject instance = null;

        if (prefabId.Equals(myPoolResourceList) == false)
        {
            PhotonNetwork.PrefabPool = default;
            instance = PhotonNetwork.Instantiate(prefabId, position, rotation);
            PhotonNetwork.PrefabPool = this;
            return instance;
        }

        bool cached = ResourceCache.TryGetValue(prefabId, out inst);
        if (!cached)
        {
            inst = Resources.Load<GameObject>(prefabId);
            if (inst == null)
            {
                Debug.LogError("Not Found " + prefabId + "Check BulletPool.cs");
            }
            else
            {
                this.ResourceCache.Add(prefabId, inst);
            }
        }

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
        string compareString = myPoolResourceList[0] + "(Clone)";
        if (gameObject.name.Equals(compareString) == false)
        {
            Debug.Log("No Pooling Destroy");
            PhotonNetwork.PrefabPool = default;
            PhotonNetwork.Destroy(gameObject);
            PhotonNetwork.PrefabPool = this;
            return;
        }

        gameObject.SetActive(false);
        pool_list.Add(gameObject);
    }
}
