using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Pool : MonoBehaviour, IPunPrefabPool 
{
    public readonly Dictionary<string, GameObject> ResourceCache = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, List<GameObject>> ListCache = new Dictionary<string, List<GameObject>>();

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
        GameObject res = null;
        GameObject instance = null;

        #region Resource Caching
        bool cached = ResourceCache.TryGetValue(prefabId, out res);
        if (!cached)
        {
            res = Resources.Load<GameObject>(prefabId);
            if (res == null)
            {
                Debug.LogError("Not Found " + prefabId + "Check Pool.cs");
                return null;
            }
            else
            {
                this.ResourceCache.Add(prefabId, res);
            }
        }
        #endregion

        #region ListCaching
        List<GameObject> list = null;
        bool listCached = ListCache.TryGetValue(prefabId,out list);
        if (!listCached)
        {
            list = new List<GameObject>();
            ListCache.Add(prefabId,list);
        }
        #endregion

        if (list.Count == 0)
        {
            instance = GameObject.Instantiate(res, position, rotation) as GameObject;
        }
        else if(list.Count > 0)
        {
            instance = list[0];
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            list.RemoveAt(0);
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
        string prefabId = gameObject.name.Replace("(Clone)","");
        
        List<GameObject> list = null;
        bool listCached = ListCache.TryGetValue(prefabId, out list);
        if (!listCached)
        {
            Debug.LogError("Not Found "+ gameObject.name + "in ListCache");
            return;
        }

        gameObject.SetActive(false);
        list.Add(gameObject);
    }

}
