using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //プレイヤーオブジェクトを参照
    private GameObject m_Player;
    //カメラの位置を調整
    [SerializeField, Header("カメラの位置を調整")] Vector3 m_Offset;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = m_Player ?? GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        PlayerLook(m_Offset);
    }


    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// プレイヤーを追いかける
    /// </summary>
    void PlayerLook(Vector3 offset)
    {
        //プレイヤーの位置を取得
        transform.position = m_Player.transform.position + offset;
    }

}