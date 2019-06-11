using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("プレイヤーが攻撃を始めたらAttack状態に移行する")] private bool m_AttackChack;

    /// <summary>
    /// Trueになったら攻撃アニメーションを開始する
    /// </summary>
    public bool AttackChack { get { return m_AttackChack; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && m_AttackChack == false)
            m_AttackChack = true;
    }
}
