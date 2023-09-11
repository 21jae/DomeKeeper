using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJ_PlayerGroundState : WJ_PlayerState
{
    public WJ_PlayerGroundState(WJ_Player _player, WJ_PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.layerChangeTime < 0)       
            player.gameObject.layer = 9;
        
        if(!player.IsGroundDetected() || yInput < 0)
            stateMachine.ChangeState(player.downState);

        if (Input.GetKey(KeyCode.Space))        
            stateMachine.ChangeState(player.upState);

    }

}
