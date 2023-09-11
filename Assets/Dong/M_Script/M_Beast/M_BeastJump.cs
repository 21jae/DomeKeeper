using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class M_BeastJump : M_State
{
    M_Beast beast;
    public Collider2D[] collision;
    public M_BeastJump(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Beast beast) : base(@base, stateMachine, aniboolname)
    {
        this.beast = beast;
    }

    public override void Enter()
    {
        base.Enter();
        beast.rb.gravityScale = 1f;
        beast.onGround = false;
        beast.Jump(beast.jumpPower);

        beast.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        beast.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

    }
}
