using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //入力方向
    [SerializeField, Header("DEBUG")] float m_InputX, m_InputY;
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
        //カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));

        //方向キー入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * m_InputY + Camera.main.transform.right * m_InputX;

        //移動方向にスピードをかける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す
        m_Rigidbody.velocity = moveForward * (m_Speed * Time.deltaTime) + new Vector3(0, m_Rigidbody.velocity.y, 0);

        //キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
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
