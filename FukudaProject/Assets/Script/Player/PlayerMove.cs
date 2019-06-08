using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MoveState
{
    IDLE,
    MOVE,
    ROLL,
}

public class PlayerMove : MonoBehaviour
{
    //入力方向
    [SerializeField, Header("DEBUG")] float m_InputX, m_InputY;
    [SerializeField, Header("移動速度(normal)")] float m_NormalSpeed;
    [SerializeField, Header("前転する力")] float m_RollPower;
    [SerializeField, Header("debug")] float debug;

    private float m_LerpSpeed = 10.0f;
    private float gravitySpeed = 0f;

    private bool m_RollChack = false;
    [Header("スライディングアニメーション")] bool m_SlideAnim;

    MoveState m_MoveState;

    //プレイヤーの向きを取得する
    private Vector3 m_PlayerRotationNow;
    //カメラの方向
    private Vector3 m_CameraForward;
    //プレイヤーの進む向き
    private Vector3 m_MoveForward;

    [SerializeField] CollisionChack m_CollisionChack;

    /// <summary>
    /// ジャンプアニメーション
    /// </summary>
    public bool SlideAnim { get { return m_SlideAnim; } }

    /// <summary>
    /// X方向が入力されているか
    /// </summary>
    public float InputX { get { return m_InputX; } }

    /// <summary>
    /// Y方向が入力されているか
    /// </summary>
    public float InputY { get { return m_InputY; } }

    Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerRotationNow = GetComponent<Transform>().position;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MoveState = MoveState.MOVE;
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
    }

    private void FixedUpdate()
    {
        MoveStateContoroller();
    }

    /// <summary>
    /// ステートを管理する
    /// </summary>
    void MoveStateContoroller()
    {
        switch (m_MoveState)
        {
            case MoveState.IDLE:
                break;
            case MoveState.MOVE:
                Move(m_LerpSpeed);
                break;
            case MoveState.ROLL:
                Roll();
                break;
        }
    }

    /// <summary>
    /// プレイヤーの移動
    /// プレイヤーの向きを移動に沿って変える
    /// ジャンプ処理  
    /// </summary>
    private void Move(float lerpspeed)
    {
        PlayerForward();
        //移動方向にスピードをかける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す
        m_Rigidbody.velocity = (m_MoveForward * m_NormalSpeed + new Vector3(0, m_Rigidbody.velocity.y, 0)) * Time.deltaTime;
        //キャラクターの向きを進行方向に
        if (m_MoveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_MoveForward), lerpspeed * Time.deltaTime);
        }
        //前転状態に移行
        if (Input.GetKeyDown(KeyCode.Space) && m_RollChack == false)
            m_MoveState = MoveState.ROLL;
    }

    /// <summary>
    /// 前転
    /// </summary>
    private void Roll()
    {
        PlayerForward();
        //if (Input.GetKeyDown(KeyCode.Space) && m_RollChack == false)
        m_RollChack = true;
        if (m_RollChack)
        {
            print("はいｔｔるよ");
            m_SlideAnim = true;
            m_Rigidbody.AddForce(m_MoveForward * m_RollPower,ForceMode.Impulse);
            StartCoroutine(WaitChack());
        }
    }

    /// <summary>
    /// プレイヤーが向いている方向に進むように
    /// </summary>
    void PlayerForward()
    {
        //カメラの方向から、X-Z平面の単位ベクトルを取得
        m_CameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));

        //方向キー入力値とカメラの向きから、移動方向を決定
        m_MoveForward = m_CameraForward * m_InputY + Camera.main.transform.right * m_InputX;
    }

    /// <summary>
    /// 入力キーを設定する
    /// </summary>
    void InputKey()
    {
        m_InputX = Input.GetAxis("Horizontal");
        m_InputY = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// ROLL状態からMOVE状態に移行するときに少し時間を空ける
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitChack()
    {
        yield return new WaitForSeconds(0.5f);
        print("愛してる");
        m_RollChack = false;
        m_SlideAnim = false;
        m_MoveState = MoveState.MOVE;
    }
}
