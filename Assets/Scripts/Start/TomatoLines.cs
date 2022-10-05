using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoLines : MonoBehaviour
{
    [SerializeField] GameObject[] tomatos;
    int index = 0;

    private void Awake()
    {
        AddList();
    }

    private void AddList()
    {
        for(int i = 0; i < 20; i ++)
        {
            index++;

            if (index == 2)
            {
                index = 0;
            }

            GameObject addTomato = Instantiate(tomatos[index]);

            addTomato.transform.parent = transform;
            addTomato.transform.localPosition = new Vector3(3.5f * i, 0, 0);
        }
    }

}
