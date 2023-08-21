using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float dirValue;
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isMove = false;
    [SerializeField] private float moveSpeed;

    [SerializeField] private bool isOnlyPlayerTrigger = false;

    private void Update()
    {
        if (isMove)
        {
            transform.Translate(Vector2.right * dirValue * moveSpeed * Time.deltaTime);

            if(transform.position.x > 10 || transform.position.x < -10)
            {
                Die(false);
            }
        }
    }

    public void SetBullet(int damage, float dirValue)
    {
        this.damage = damage;
        this.dirValue = dirValue;
        animator = GetComponent<Animator>();
        transform.localScale = new Vector3(transform.localScale.x * dirValue, transform.localScale.y, transform.localScale.z);
        isMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMove)
        {
            if (collision.CompareTag("Meteor") && !isOnlyPlayerTrigger)
            {
                Die(true);
                collision.GetComponent<Meteor>().TakeDamage(damage);
            }
            else if (collision.CompareTag("Player") && isOnlyPlayerTrigger)
            {
                Die(false);
                collision.GetComponent<PlayerStats>().TakeDamage(1); 
            }
            else if (collision.CompareTag("Shield") && isOnlyPlayerTrigger)
            {
                Die(false);
            }
            else if (collision.CompareTag("BlackHole") && !isOnlyPlayerTrigger)
            {
                isOnlyPlayerTrigger = true;
                transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            }
        }
    }

    public void Die(bool isMeteor)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        isMove = false;
        if (isMeteor)
        {
            animator.SetTrigger("die");
        }
        else
        {
            animator.SetTrigger("diePlayer");
        }
    }

    public void DieAction()
    {
        Destroy(gameObject);
    }
}
