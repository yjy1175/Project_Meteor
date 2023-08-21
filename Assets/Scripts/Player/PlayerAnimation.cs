using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animatorPlayer;
    private Animator animatorGun;

    private void Awake()
    {
        animatorPlayer = GetComponent<Animator>();
        animatorGun = transform.GetChild(0).GetComponent<Animator>();
    }

    public void PlayMove(bool isMove)
    {
        animatorPlayer.SetBool("isRun", isMove);
        animatorGun.SetBool("isRun", isMove);
    }

    public void PlayDamaged()
    {
        animatorPlayer.SetTrigger("damaged");
        animatorGun.SetTrigger("damaged");
    }

    public void PlayAttack()
    {
        animatorGun.SetTrigger("attack");
    }

    public void PlayGameover()
    {
        animatorPlayer.SetTrigger("gameover");
        animatorGun.SetTrigger("gameover");
    }
}
