using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJ_PlayerDownState : WJ_PlayerState
{
    public WJ_PlayerDownState(WJ_Player _player, WJ_PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.gameObject.layer = 3;
        player.layerChangeTime = player.layerChangeDelay;

    }

    public override void Update()
    {
        base.Update();

        //player.SetVelocity(rbody.velocity.x, yInput * player.Speed);

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (xInput != 0)
            player.SetVelocity(player.Speed * 0.8f * xInput, rbody.velocity.y);
        if(Input.GetKey(KeyCode.Space))
        {
            stateMachine.ChangeState(player.upState);
        }



    }

    public override void Exit()
    {
        base.Exit();


    }
}
