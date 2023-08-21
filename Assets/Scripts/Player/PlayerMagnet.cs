using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] private LayerMask itemLayer;

    private void Update()
    {
        Collider2D[] items =
            Physics2D.OverlapCircleAll(transform.position, PlayerStats.GetInstance().MagnetRange, itemLayer);

        for(int i = 0; i < items.Length; i++)
        {
            if (items[i].GetComponent<Item>() != null)
            {
                items[i].GetComponent<Item>().SetTarget(transform);
            }
        }
    }
}
