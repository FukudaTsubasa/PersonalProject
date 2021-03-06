﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator m_Animator;
    [SerializeField] PlayerMove m_PlayerMove;
    [SerializeField] PlayerAttack m_PlayerAttack;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RunAnimator();
        JumpAnimator();
    }

    /// <summary>
    /// キーが入力されていたら走るアニメーションを再生
    /// </summary>
    void RunAnimator()
    {
        if (m_PlayerMove.InputX != 0 || m_PlayerMove.InputY != 0)
            m_Animator.SetBool("Run", true);
        else if(m_PlayerMove.InputX == 0 && m_PlayerMove.InputY == 0)
            m_Animator.SetBool("Run", false);
    }

    /// <summary>
    /// スペースキーを押したらスライディングアニメ―ションを再生
    /// </summary>
    void JumpAnimator()
    {
        if(m_PlayerMove.SlideAnim)
            m_Animator.SetBool("Slide", true);
        else
            m_Animator.SetBool("Slide", false);
    }

    /// <summary>
    /// 左クリックしたら攻撃アニメーションを開始する
    /// </summary>
    void AttackAnimation()
    {
        
    }
}
