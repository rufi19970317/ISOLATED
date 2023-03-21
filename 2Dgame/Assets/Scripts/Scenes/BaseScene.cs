using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    // 씬 부모 클래스
    public Define.Scene SceneType { get; protected set; }

    void Start()
    {
        Init();
    }

    // 세팅
    protected virtual void Init()
    {
        // UI EventSystem이 없을 경우, 생성
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    // 초기화
    public abstract void Clear();
}
