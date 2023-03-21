using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // �÷��̾ �Ѵ� ī�޶� ����

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

                // �÷��̾ ���� ������ ��, �� ������ ī�޶� �̵�
                RaycastHit hit;
                if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
                {
                    // dist = ���� �÷��̾� ����
                    float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                    transform.position = _player.transform.position + dist * _delta.normalized;
                }

                // �÷��̾ �Ѵ� ī�޶�
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

    // ī�޶� ����
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
