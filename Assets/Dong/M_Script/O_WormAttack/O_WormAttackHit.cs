using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class O_WormAttackHit : M_State
{
    O_WormAttack wormAttack;
    public O_WormAttackHit(M_Base @base, M_StateMachine stateMachine, string aniboolname, O_WormAttack wormAttack) : base(@base, stateMachine, aniboolname)
    {
        this.wormAttack = wormAttack;
    }

    public override void Enter()
    {
        base.Enter();
        wormAttack.rb.bodyType = RigidbodyType2D.Static;

         int x = Random.Range(1, 3);
        if(x ==1)
        wormAttack.audioSource.clip = wormAttack.clip1;
        else
            wormAttack.audioSource.clip = wormAttack.clip3;

        wormAttack.audioSource.PlayOneShot(wormAttack.audioSource.clip);

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
