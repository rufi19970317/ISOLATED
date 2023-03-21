using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapChanger : MonoBehaviour
{
    GameObject nowMap;

    Tilemap _nowTileMap;
    Color _tileMapColor;
    GameObject _baseMap;
    
    public GameObject baseMap { get { return _baseMap; }}

    int _mapCount = 1;
    bool isEndMap = false;
    bool isStartMap = false;
    bool isInvisible = false;
    float _timer = 0f;
    float _colorTimer = 0f;

    void Update()
    {
        // ¸Ê ½ÃÀÛ ½Ã
        if(isStartMap && _mapCount > 1)
        {
            nowMap.transform.position = Vector2.MoveTowards(nowMap.transform.position, new Vector2(0f, 0f), Time.deltaTime * 2f);
            if(nowMap.transform.position.y <= 0f)
            {
                nowMap.transform.position = new Vector2(0f, 0f);
                isStartMap = false;
            }
        }

        // End Map ½Ã, ¸Ê ±ô¹ÚÀÌ°Ô ²û
        if (isEndMap)
        {
            if (_timer <= 5f)
            {
                if (_colorTimer >= 1f)
                {
                    _tileMapColor.a = (isInvisible) ? 1f : 0.5f;
                    _nowTileMap.color = _tileMapColor;
                    _colorTimer = 0f;
                    isInvisible = !isInvisible;
                }
            }
            else if(_timer > 5f)
            {
                if (_colorTimer >= 0.5f)
                {
                    _tileMapColor.a = (isInvisible) ? 1f : 0.5f;
                    _nowTileMap.color = _tileMapColor;
                    _colorTimer = 0f;
                    isInvisible = !isInvisible;
                }
            }

            if (_timer > 10f)
                StartMap();

            _timer += Time.deltaTime;
            _colorTimer += Time.deltaTime;
        }
    }

    #region º£ÀÌ½º ¸Ê »ý¼º
    public void CreateMap()
    {
        _baseMap = Managers.Resource.Instantiate("Map/Map_Base");
    }
    #endregion

    #region ½ºÅ×ÀÌÁö Map ºÒ·¯¿À±â
    // ¸Ê ÇÁ¸®ÆÕ ºÒ·¯¿À±â
    public void StartMap()
    {
        Clear();

        isStartMap = true;
        if(_mapCount < 10)
            nowMap = Managers.Resource.Instantiate("Map/Map_00" + _mapCount, _baseMap.transform);
        else if(_mapCount >= 10)
            nowMap = Managers.Resource.Instantiate("Map/Map_0" + _mapCount, _baseMap.transform);

        if (_mapCount == 1)
        {
            nowMap.transform.position = new Vector2(0f, 0f);
            isStartMap = false;
        }
        else
            nowMap.transform.position = new Vector2(0f, 15f);

        _mapCount++;
    }

    // ¸Ê ÇÁ¸®ÆÕ Á¾·á ÁØºñ
    public void EndMap()
    {
        _nowTileMap = nowMap.GetComponent<Tilemap>();
        _tileMapColor = _nowTileMap.color;
        isEndMap = true;
    }

    void Clear()
    {
        if(nowMap != null)
            Managers.Resource.Destroy(nowMap);

        isEndMap = false;
        isInvisible = true;
        _timer = 0f;
        _colorTimer = 0f;
    }
    #endregion
}
