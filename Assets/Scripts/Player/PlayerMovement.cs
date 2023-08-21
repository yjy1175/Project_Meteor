using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Sgt
    private static PlayerMovement instance;
    public static PlayerMovement GetInstance() => instance;

    private void Awake()
    {
        instance = null;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    #region Event
    public event EventManager.SingleVecter2 updatePlayerHpPosition;
    #endregion

    private bool isJump = false;

    public void Move(int value)
    {
        updatePlayerHpPosition.Invoke(transform.position);
        if (value == 0)
        {
            GetComponent<PlayerAnimation>().PlayMove(false);
            transform.GetChild(1).gameObject.SetActive(false);
            return;
        }

        GetComponent<PlayerAnimation>().PlayMove(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.localScale = new Vector3(transform.localScale.y *value, transform.localScale.y, transform.localScale.z);
        transform.Translate(Vector2.right * value * GetComponent<PlayerStats>().MoveSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (!isJump)
        {
            isJump = true;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * GetComponent<PlayerStats>().JumpPower, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            isJump = false;
        }
    }
}
