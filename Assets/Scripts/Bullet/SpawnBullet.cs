using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnTriggerAction()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void OnDieAction()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteor"))
        {
            collision.GetComponent<Meteor>().Die();
        }

        if (collision.CompareTag("Bullet"))
        {
            collision.GetComponent<Bullet>().Die(false);
        }
    }
}
