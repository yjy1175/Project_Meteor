using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private EnumClass.ItemType type;
    [SerializeField] private int amount;
    [SerializeField] private bool isGet = false;
    [SerializeField] private Transform target;

    private void Update()
    {
        if(target != null)
        {
            Vector2 relativePos = target.transform.position - transform.position;
            float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle - 90);
            transform.Translate(transform.up * PlayerStats.GetInstance().MoveSpeed * 2f * Time.deltaTime, Space.World);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetItem(int amount)
    {
        this.amount = amount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isGet)
        {
            isGet = true;
            switch (type)
            {
                case EnumClass.ItemType.Dregs:
                    PlayerStats.GetInstance().AddDregs(amount);
                    break;
                case EnumClass.ItemType.PerfectDregs:
                    PlayerStats.GetInstance().AddPerfectDregs(amount);
                    break;
                case EnumClass.ItemType.Heart:
                    PlayerStats.GetInstance().AddHp(amount);
                    break;
            }
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.PLAYER_GET_ITEM);
            Destroy(gameObject);
        }
    }
}
