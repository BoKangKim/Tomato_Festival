using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    #region �̱���
    private static T instance = null; // �̱����� �Ҵ��� ���� ����
    static public T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<T>();
                    if (instance == null)
                        Debug.LogError("Not Found Singleton Class");
                }
            }

            return instance;
        }
    }
    #endregion
    
}
