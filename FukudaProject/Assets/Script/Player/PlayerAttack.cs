using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("プレイヤーが攻撃を始めたらAttack状態に移行する")] private bool m_AttackChack;
    [Header("プレイヤーが攻撃を始めたらAttack状態に移行する")] private bool m_AttackStateChack;
    [SerializeField] PlayerMove m_PlayerMove;

    /// <summary>
    /// Trueになったら攻撃アニメーションを開始する
    /// </summary>
    public bool AttackChack { get { return m_AttackChack; } }

    /// <summary>
    /// Trueになったら攻撃状態に移行
    /// </summary>
    public bool AttackStateChack { get { return m_AttackStateChack; } set { m_AttackStateChack = value; } }

    private void Update()
    {
        Debug.Log(m_AttackStateChack);
    }

    public void Attack()
    {
        print("aa");
        if(m_AttackStateChack)
        StartCoroutine(WaitAttack());
    }

    IEnumerator WaitAttack()
    {
        m_AttackChack = true;
        yield return new WaitForSeconds(1.0f);
        m_AttackChack = false;
        m_PlayerMove.m_MoveState = MoveState.MOVE;
        m_AttackStateChack = false;
    }
}
