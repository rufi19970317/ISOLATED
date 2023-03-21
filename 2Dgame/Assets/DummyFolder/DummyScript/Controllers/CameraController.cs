using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 플레이어를 쫓는 카메라 세팅

    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.Platformer;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 12.0f, -10.0f);

    [SerializeField]
    GameObject _player = null;

    public void SetPlayer(GameObject player) { _player = player; }

    void LateUpdate()
    {
        switch(_mode)
        {
            case Define.CameraMode.QuaterView:
                if (_player.isValid() == false)
                {
                    return;
                }

                // 플레이어가 벽에 가렸을 시, 벽 앞으로 카메라 이동
                RaycastHit hit;
                if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
                {
                    // dist = 벽과 플레이어 사이
                    float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                    transform.position = _player.transform.position + dist * _delta.normalized;
                }

                // 플레이어를 쫓는 카메라
                else
                {
                    transform.position = _player.transform.position + _delta;
                    transform.LookAt(_player.transform);
                }
                break;
            case Define.CameraMode.Platformer:
                if (_player == null) _player = Managers.Game.GetPlayer();
                /*
                if(_player != null)
                    transform.position = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
                */
                break;
        }
    }

    // 카메라 세팅
    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }

    public void SetPlatformer(Vector3 delta)
    {
        _mode = Define.CameraMode.Platformer;
    }
}
