using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, DisallowMultipleComponent]
public class MainCamera : MonoBehaviour
{
    [SerializeField] GameObject m_Target; // an object to follow
    [SerializeField, Header("位置を調整する")] Vector3 m_Offset; // offset form the target object

    [SerializeField, Header("プレイヤーとの距離")] private float m_Distance = 4.0f; // distance from following object
    [SerializeField, Header("極地")] private float m_PolarAngle = 45.0f; // angle with y-axis
    [SerializeField, Header("方位角")] private float m_AzimuthalAngle = 45.0f; // angle with x-axis

    [SerializeField, Header("拡大率（Max）")] private float m_MinDistance = 1.0f;
    [SerializeField, Header("拡大率（Min）")] private float m_MaxDistance = 7.0f;
    [SerializeField, Header("極地制御（Max）")] private float m_MinPolarAngle = 5.0f;
    [SerializeField, Header("極地制御（Min）")] private float m_MaxPolarAngle = 75.0f;
    [SerializeField, Header("ｘ軸の感度")] private float m_MouseXSensitivity = 5.0f;
    [SerializeField, Header("ｙ軸の感度")] private float m_MouseYSensitivity = 5.0f;
    [SerializeField, Header("スクロールの感度")] private float m_ScrollSensitivity = 5.0f;

    void LateUpdate()
    {
        CameraLookToPlayer();
    }

    /// <summary>
    /// プレイヤーに追従・プレイヤーを軸に回転・倍率を拡大縮小する
    /// </summary>
    void CameraLookToPlayer()
    {
        if (Input.GetMouseButton(1))
        {
            PlayerUpdateAngle(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        PlayerUpdateDistance(Input.GetAxis("Mouse ScrollWheel"));

        var lookAtPos = m_Target.transform.position + m_Offset;
        PlayerUpdatePosition(lookAtPos);
        transform.LookAt(lookAtPos);
    }

    /// <summary>
    /// プレイヤーの周りを回転させる
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void PlayerUpdateAngle(float x, float y)
    {
        x = m_AzimuthalAngle - x * m_MouseXSensitivity;
        m_AzimuthalAngle = Mathf.Repeat(x, 360);

        y = m_PolarAngle + y * m_MouseYSensitivity;
        m_PolarAngle = Mathf.Clamp(y, m_MinPolarAngle, m_MaxPolarAngle);
    }

    /// <summary>
    /// 拡大率
    /// </summary>
    /// <param name="scroll"></param>
    void PlayerUpdateDistance(float scroll)
    {
        scroll = m_Distance - scroll * m_ScrollSensitivity;
        m_Distance = Mathf.Clamp(scroll, m_MinDistance, m_MaxDistance);
    }

    /// <summary>
    /// カメラの位置
    /// </summary>
    /// <param name="lookAtPos"></param>
    void PlayerUpdatePosition(Vector3 lookAtPos)
    {
        var da = m_AzimuthalAngle * Mathf.Deg2Rad;
        var dp = m_PolarAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + m_Distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + m_Distance * Mathf.Cos(dp),
            lookAtPos.z + m_Distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }
}
