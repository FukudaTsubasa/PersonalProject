using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChack : MonoBehaviour
{
    [SerializeField]private bool m_IsHit_Jump;

    /// <summary>
    /// 無限ジャンプ防止
    /// </summary>
    public bool IsHit_Jump
    {
        get { return m_IsHit_Jump; }
    }

    /// <summary>
    /// プレイヤーの当たり判定が何かに当たっていたら
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        m_IsHit_Jump = true;
    }

    /// <summary>
    /// プレイヤーの当たり判定がなにも触れていなかったら
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        m_IsHit_Jump = false;
    }
}
