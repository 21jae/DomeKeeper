using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip Lazer;
    public AudioClip LoopingBackGround;
    public AudioClip LoopingSwordMove;
    public AudioClip LazerHit;
    public AudioClip LazerMove;
    public AudioClip SwordMove;
    public AudioClip SwordHit;
    public AudioClip Skillpc;
    public AudioClip Playpc;
    public AudioClip SubTowerAtk;
    public AudioClip FireTowerAtk;
    public AudioClip StunTowerAtk;
    public AudioClip AutoTowerAtk;
    public AudioClip digSound;
    public AudioClip groundCrack;
    public AudioClip mineralCrack;
    public AudioClip computerOn;
    public AudioClip computerOff;
    public AudioClip teleportSound;
    public AudioClip domeIn;
    public AudioClip domeOut;
    public AudioClip skillUp;
    public AudioClip skillOpen;
    public AudioClip jemSave;
    public AudioClip BigLazer;
    public AudioClip clearSound;
    public AudioClip deadSound;




    AudioSource myAudio; //AudioSorce 컴포넌트를 변수로 담습니다.
    void Awake() //Start보다도 먼저, 객체가 생성될때 호출됩니다
    {
        if (SoundManager.instance == null) //incetance가 비어있는지 검사합니다.
        {
            SoundManager.instance = this; //자기자신을 담습니다.
        }
    }
    void Start()
    {
        myAudio = this.gameObject.GetComponent<AudioSource>(); //AudioSource 오브젝트를 변수로 담습니다.

    }
    public void PlayLazer()
    {
        myAudio.PlayOneShot(Lazer); //soundExplosion을 재생합니다.
    }
    public void PlayLazerHit()
    {
        myAudio.PlayOneShot(LazerHit); //soundExplosion을 재생합니다.
    }
    public void PlayLazerMove()
    {
        myAudio.PlayOneShot(LazerMove); //soundExplosion을 재생합니다.
    }
  
    public void PlaySwordHit()
    {
        myAudio.PlayOneShot(SwordHit); // 소리 재생
    }
    public void PlayBigLazer()
    {
        myAudio.PlayOneShot(BigLazer); // 소리 재생
    }
    public void PlayPc()
    {
        myAudio.PlayOneShot(Playpc); //soundExplosion을 재생합니다.
    }
    public void PlaySkillPc()
    {
        myAudio.PlayOneShot(Skillpc); //soundExplosion을 재생합니다.
    }
    public void PlaySubTower()
    {
        myAudio.PlayOneShot(SubTowerAtk); //soundExplosion을 재생합니다.
    }
    public void PlayStunTower()
    {
        myAudio.PlayOneShot(StunTowerAtk); //soundExplosion을 재생합니다.
    }
    public void PlayFireTower()
    {
        myAudio.PlayOneShot(FireTowerAtk); //soundExplosion을 재생합니다.
    }
    public void PlayAutoTower()
    {
        myAudio.PlayOneShot(AutoTowerAtk); //soundExplosion을 재생합니다.
    }
    public void PlayGroundCrack()
    {
        myAudio.PlayOneShot(groundCrack);
    }
    public void PlayMineralCrack()
    {
        myAudio.PlayOneShot(mineralCrack);

    }
    public void PlayComputerOn()
    {
        myAudio.PlayOneShot(computerOn);

    }
    public void PlayComputerOff()
    {
        myAudio.PlayOneShot(computerOff);

    }
    public void PlayUseTeleport()
    {
        myAudio.PlayOneShot(teleportSound);

    }
    public void PlayDomeIn()
    {
        myAudio.PlayOneShot(domeIn);

    }
    public void PlayDomeOut()
    {
        myAudio.PlayOneShot(domeOut);

    }
    public void PlaySkillUp()
    {
        myAudio.PlayOneShot(skillUp);

    }
    public void PlaySkillOpen()
    {
        myAudio.PlayOneShot(skillOpen);

    }
    public void PlayDigSound()
    {
        myAudio.PlayOneShot(digSound);

    }
    public void PlayJemSave()
    {
        myAudio.PlayOneShot(jemSave);

    }
    public void PlayClear()
    {
        myAudio.PlayOneShot(clearSound);

    }
    public void PlayDead()
    {
        myAudio.PlayOneShot(deadSound);

    }



    // 루프 재생 여부를 나타내는 변수
    private bool isLoopingBack = false;
    private bool isLoopingSwordMove = false;

    void Update()
    {
  
        if (isLoopingBack && !myAudio.isPlaying)
        {
            myAudio.clip = LoopingBackGround;
            myAudio.loop = true;
            myAudio.Play();
        }

        if(isLoopingSwordMove && !myAudio.isPlaying)
        {
            myAudio.clip = LoopingSwordMove;
            myAudio.loop = true;
            myAudio.Play();
        }
    }
    

    public void StartLooping()
    {
        isLoopingBack = true;
        myAudio.clip = LoopingBackGround;
        myAudio.loop = true;
        myAudio.Play();
    }
    public void StopLooping()
    {
        isLoopingBack = false;
        myAudio.Stop();
    }

    public void StartLoopingSwordMove()
    {
        isLoopingSwordMove = true;
        myAudio.clip = LoopingSwordMove;
        myAudio.loop = true;
        myAudio.Play();
    }

}

