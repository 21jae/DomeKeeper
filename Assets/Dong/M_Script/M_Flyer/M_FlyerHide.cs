using UnityEngine;

public class M_FlyerHide : M_State
{
    int x;
    M_Flyer flyer;
    int toggle;
    float multi;
    public M_FlyerHide(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Flyer flyer) : base(@base, stateMachine, aniboolname)
    {
        this.flyer = flyer;
    }

    public override void Enter()
    {
        base.Enter();
        flyer.inactive.enabled = true;
        flyer.active.enabled = false;

        if (flyer.transform.position.x > 0) { x = -1; }
        else if (flyer.transform.position.x < 0) { x = 1; }

        flyer.moveLocation = new Vector2(Random.Range(2f, 20f) * x, Random.Range(-2f, 4f));

        flyer.normalVec = new Vector2(flyer.transform.position.y - flyer.moveLocation.y, flyer.moveLocation.x - flyer.transform.position.x);
        flyer.normalVec = flyer.normalVec / 2;

        flyer.centerVec = new Vector2(flyer.moveLocation.x + flyer.transform.position.x, flyer.moveLocation.y + flyer.transform.position.y);
        flyer.centerVec = (flyer.centerVec / 2);

        toggle = (int)((Random.Range(0, 2) - 0.5f) * 2); 
        multi = (float)(Random.Range(1, 3)) / 3; 


        flyer.path = new Vector2(flyer.centerVec.x + flyer.normalVec.x * toggle * multi, flyer.centerVec.y + flyer.normalVec.y * toggle * multi);
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
