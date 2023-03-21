using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    Queue<GameObject> childs = new Queue<GameObject>();
    float time = 1f;

    void Awake()
    {
        for(int i = 0; i <4; i++)
            childs.Enqueue(transform.GetChild(i).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0f)
        {
            GameObject child = childs.Dequeue();
            child.transform.parent = null;
            child.transform.parent = transform;
            childs.Enqueue(child);
            time = 1f;
        }
    }
}
