using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    [SerializeField] float m_moveTime = 0.5f;

    public void Move(Vector3 direction, float distance)
    {
        Vector3 moveVector = direction.normalized * distance;
        transform.DOMove(transform.position + moveVector, m_moveTime);
    }
}
