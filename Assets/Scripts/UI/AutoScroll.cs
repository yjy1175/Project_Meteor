using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    [SerializeField] private EnumClass.SCROLL_DIR type;
    [SerializeField] private RectTransform con;
    [SerializeField] private float speed;
    [SerializeField] private float max;
    [SerializeField] private bool isMove = true;

    private void Update()
    {
        if (con != null && isMove)
        {
            switch (type)
            {
                case EnumClass.SCROLL_DIR.Horizontal:
                    con.Translate(Vector2.left * speed * Time.deltaTime);
                    if (con.anchoredPosition.x <= max)
                    {
                        con.anchoredPosition = new Vector2(0, con.anchoredPosition.y);
                    }
                    break;
                case EnumClass.SCROLL_DIR.Vertical:
                    con.Translate(Vector2.up * speed * Time.deltaTime);
                    if (con.anchoredPosition.y >= max)
                    {
                        con.anchoredPosition = new Vector2(con.anchoredPosition.x, 0);
                    }
                    break;
            }
        }
    }

    public void ToggleMove(bool isMove)
    {
        this.isMove = isMove;
    }
}
