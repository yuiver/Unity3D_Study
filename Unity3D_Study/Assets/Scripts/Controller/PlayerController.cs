using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;
    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        //Managers.Resource.Instantiate("UI/UI_Button");

        //TEMP
        Managers.UI.ShowPopupUI<UI_Button>();

        //UI_Button ui =
        //Managers.UI.ClosePopupUI(ui);
    }

    public enum PlayerState
    { 
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    {
    
    }
    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        // �ִϸ��̼�
        Animator anim = GetComponent<Animator>();
        // ���� ���� ���¿� ���� ������ �Ѱ��ش�
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        //�ִϸ��̼�
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);

    }

    void Update()
    {
        {
            switch (_state)
            {
                case PlayerState.Die:
                    UpdateDie();
                    break;
                case PlayerState.Moving:
                    UpdateMoving();
                    break;
                case PlayerState.Idle:
                    UpdateIdle();
                    break;

            }
        }


    }

    //void OnKeyboard()
    //{
    //    if (Input.GetKey(KeyCode.UpArrow))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //        transform.position += Vector3.forward * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.DownArrow))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
    //        transform.position += Vector3.back * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
    //        transform.position += Vector3.left * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
    //        transform.position += Vector3.right * Time.deltaTime * _speed;
    //    }
    //}

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;
        //if (evt != Define.MouseEvent.Click)
            //return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //���콺�� ��ġ�� ����ī�޶��� ��ġ���� ���콺�� �ִ� ��ũ���� ��ġ�� ������ ��ٴ� ��

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);
        //����ĳ��Ʈ�� �׷��� ����׷� ���� ([����ī�޶� ��ġ����],[������ �� * �Ÿ�],[������ ��],[�ð�]


        //����ĳ��Ʈ�� ���� ���𰡷� ���� ������ ������ ����
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        //�����ɼ�.����ĳ��Ʈ([���������],[������ ���� �繰����],[�ִ�Ÿ�],[� ���̾��� ������ ȣ������])
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            //����׷α׸� ȣ���Ѵ� [Raycast Camera @ [�浹������ ����ĳ��Ʈ�� �浹�� ������Ʈ�� �̸��� �����´�.]]

            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag}");
            //����׷α׸� ȣ���Ѵ� [Raycast Camera @ [����.�浹.������Ʈ.�±�(�浹������ ����ĳ��Ʈ�� �浹�� ������Ʈ�� �±׸� �����´�.)
        }
    }
}
