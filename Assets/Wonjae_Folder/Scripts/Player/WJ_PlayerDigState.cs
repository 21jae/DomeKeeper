public class WJ_PlayerDigState : WJ_PlayerGroundState
{
    public WJ_PlayerDigState(WJ_Player _player, WJ_PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();



        player.SetVelocity(xInput * player.Speed, rbody.velocity.y);


        /*     if (mousePosition.x > player.transform.position.x)
             {
                 player.Flip();
             }
             else
             {
                 player.Flip();
             }*/



    }

    public override void Exit()
    {
        base.Exit();

    }

}
