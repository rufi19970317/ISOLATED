using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    // �� �θ� Ŭ����
    public Define.Scene SceneType { get; protected set; }

    void Start()
    {
        Init();
    }

    // ����
    protected virtual void Init()
    {
        // UI EventSystem�� ���� ���, ����
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    // �ʱ�ȭ
    public abstract void Clear();
}
