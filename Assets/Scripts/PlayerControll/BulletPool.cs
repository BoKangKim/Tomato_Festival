using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<Bullet>
{
    #region Singleton
    private static BulletPool instance = null;

    public static BulletPool Inst 
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BulletPool>();
                if (instance == null)
                    instance = new GameObject("BulletPool").AddComponent<BulletPool>();
            }

            return instance;
        }
    }

    #endregion

    //������Ʈ Ǯ
    [SerializeField] Bullet bulletPrefab;

    public override Bullet CreatePool()
    {
        return bulletPrefab;
    }


}
