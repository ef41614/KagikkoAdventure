using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InputManager で定義された Vertical, Horizontal でそれぞれ前後移動, 回転をする。
/// </summary>
[RequireComponent(typeof(CharacterController), typeof(Collider))]
public class CubeController : MonoBehaviour
{
    /// <summary>GameObjectに追加されたキャラクターコントローラー</summary>
    CharacterController m_charCtrl;

    /// <summary>前後移動の速度</summary>
    [SerializeField] private float m_MoveSpeed = 1f;

    /// <summary>回転速度</summary>
    [SerializeField] private float m_RotateSpeed = 1f;

    [SerializeField] float m_moveRangeUnit = 3.0f;

    void Start()
    {
        m_charCtrl = GetComponent<CharacterController>();
    }

    void Update()
    {
        /* 入力に応じて移動・回転・発射する */
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (vertical != 0) m_charCtrl.SimpleMove(vertical * transform.forward * m_MoveSpeed);
        if (horizontal != 0)
        {
            if (vertical >= 0) transform.Rotate(0, horizontal * m_RotateSpeed, 0);
            else transform.Rotate(0, -1 * horizontal * m_RotateSpeed, 0);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        BallController bc = hit.gameObject.GetComponent<BallController>();

        if (bc)
            bc.Move(transform.forward, Random.Range(1, 4) * m_moveRangeUnit);
    }
}