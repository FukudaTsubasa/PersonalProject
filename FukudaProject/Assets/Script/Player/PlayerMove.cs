using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //入力方向
    [SerializeField,Header("DEBUG")] float m_InputX,m_InputY;
    [SerializeField, Header("移動速度")] float m_Speed;

    //プレイヤーの向きを取得する
    private Vector3 m_PlayerRotationNow;

    /// <summary>
    /// X方向が入力されているか
    /// </summary>
    public float InputX
    {
        get { return m_InputX; }
    }

    /// <summary>
    /// Y方向が入力されているか
    /// </summary>
    public float InputY
    {
        get { return m_InputY; }
    }

    Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerRotationNow = GetComponent<Transform>().position;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// プレイヤーの移動
    /// プレイヤーの向きを移動に沿って変える
    /// </summary>
    private void Move()
    {
        Vector3 rotate = transform.position - m_PlayerRotationNow;
        m_Rigidbody.velocity = new Vector3(m_InputX, 0, m_InputY);
        if (rotate.magnitude > 0.01f)
            m_Rigidbody.rotation = Quaternion.LookRotation(rotate);
        m_PlayerRotationNow = transform.position;
    }

    /// <summary>
    /// 入力キーを設定する
    /// </summary>
    void InputKey()
    {
        m_InputX = Input.GetAxis("Horizontal") * m_Speed * Time.deltaTime;
        m_InputY = Input.GetAxis("Vertical") * m_Speed * Time.deltaTime;
    }
}
