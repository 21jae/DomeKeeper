using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_WormAttackDead : M_State
{
    O_WormAttack wormAttack;
    public O_WormAttackDead(M_Base @base, M_StateMachine stateMachine, string aniboolname, O_WormAttack wormAttack) : base(@base, stateMachine, aniboolname)
    {
        this.wormAttack = wormAttack;
    }

    public override void Enter()
    {
        base.Enter();

        int x = Random.Range(1, 3);
        if (x == 1)
            wormAttack.audioSource.clip = wormAttack.clip2;
        else
            wormAttack.audioSource.clip = wormAttack.clip4;

        wormAttack.audioSource.PlayOneShot(wormAttack.audioSource.clip);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        wormAttack.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
