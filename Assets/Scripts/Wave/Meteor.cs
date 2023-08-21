using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Meteor Stat")]
    [SerializeField] private EnumClass.MeteorSize type;
    [SerializeField] private int maxHp;
    public int MaxHp { get { return maxHp; } }
    [SerializeField] private int curHp;
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMoveStart = false;
    [SerializeField] private bool isDie = false;
    [SerializeField] private bool isTakeDamaged = false;
    [SerializeField] private float startPosX;

    [Header("Etc")]
    [SerializeField] private GameObject hpUI;
    [SerializeField] private CreateTextUI myUI;
    [SerializeField] private GameObject dregs;
    [SerializeField] private GameObject perfectDregs;
    [SerializeField] private GameObject hearts;
    [SerializeField] private Transform itemHolder;

    private void Start()
    {
        GetComponent<AudioSource>().volume = SoundManager.Instance.effectAudio.volume;
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (isMoveStart)
        {
            // 스폰 방향 반대방향으로 이동
            transform.Translate((startPosX > 0 ? Vector2.left : Vector2.right) * moveSpeed * Time.deltaTime);

            if(Mathf.Abs(transform.position.x - startPosX) > 16f)
            {
                isMoveStart = false;
                Die(true);
            }
        }
    }

    public void SetInfo(int baseHp, MeteorHpRate hpRate, float moveSpeed, int damage)
    {
        maxHp = baseHp * Random.Range(hpRate.minRate, hpRate.maxRate + 1);
        curHp = maxHp;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        startPosX = transform.position.x;
        transform.localScale = new Vector3(startPosX > 0 ? -transform.localScale.x : transform.localScale.x, transform.localScale.y, transform.localScale.z);
        myUI = Instantiate(hpUI, GameObject.Find("TextUI").transform).GetComponent<CreateTextUI>();
        myUI.SetNumber(curHp, transform);
        itemHolder = GameObject.Find("Items").transform;
        isMoveStart = true;
    }

    public void TakeDamage(int damage)
    {
        if (!isDie && !isTakeDamaged)
        {
            isTakeDamaged = true;
            curHp -= damage;
            StartCoroutine(CoTakeDamage());
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.METEOR_DAMAGED);
            myUI.UpdateNumber(curHp < 0 ? 0 : curHp);
            if (curHp <= 0)
            {
                curHp = 0;
                Die(true);
            }
        }
    }

    public void Die()
    {
        Die(true);
    }

    IEnumerator CoTakeDamage()
    {
        yield return new WaitForSeconds(DataManager.GetInstance().playerBaseData.GetData().baseAttackCoolTime * 0.5f);
        isTakeDamaged = false;
    }

    private void Die(bool isDrop)
    {
        if (!isDie)
        {
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.METEOR_DESTROYED);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            isDie = true;
            isMoveStart = false;
            Destroy(myUI.gameObject);
            if (isDrop)
            {
                GetComponent<Animator>().SetTrigger("dieDrop");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("die");
            }
        }
    }

    private void DropItem()
    {
        int dregsCount = DataManager.GetInstance().waveData.GetData().dropDregs[(int)type];
        int perfectDregsCount = DataManager.GetInstance().waveData.GetData().dropPerfectDregs[(int)type];
        int heartCount = DataManager.GetInstance().waveData.GetData().dropHearts[(int)type];

        int dregsRate = DataManager.GetInstance().waveData.GetData().dropRate[0];
        int ran = Random.Range(0, 100);

        if (perfectDregsCount > 0)
        {
            if(ran < dregsRate)
            {
                // 부스러기
                Item newItem = Instantiate(dregs, transform.position, Quaternion.identity, itemHolder).GetComponent<Item>();
                newItem.SetItem(dregsCount);
                if (DataManager.GetInstance().playerSaveData.GetData().isDoubleDropRate)
                {
                    Item plusItem = Instantiate(dregs, transform.position, Quaternion.identity, itemHolder).GetComponent<Item>();
                    plusItem.SetItem(dregsCount);
                }
            }
            else
            {
                // 온전한
                Item newItem = Instantiate(perfectDregs, transform.position, Quaternion.identity, itemHolder).GetComponent<Item>();
                newItem.SetItem(perfectDregsCount);
            }
        }
        else
        {
            if(ran < dregsRate)
            {
                // 부스러기
                Item newItem = Instantiate(dregs, transform.position, Quaternion.identity, itemHolder).GetComponent<Item>();
                newItem.SetItem(dregsCount);
                if (DataManager.GetInstance().playerSaveData.GetData().isDoubleDropRate)
                {
                    Item plusItem = Instantiate(dregs, transform.position, Quaternion.identity, itemHolder).GetComponent<Item>();
                    plusItem.SetItem(dregsCount);
                }
            }
            else
            {
                // 체력
                Item newItem = Instantiate(hearts, transform.position, Quaternion.identity, itemHolder).GetComponent<Item>();
                newItem.SetItem(heartCount);
            }

        }
    }

    public void DieDropAction()
    {
        // 아이템 드롭
        PlayerStats.GetInstance().AddDestroyCount(type, 1);
        DropItem();
        Destroy(gameObject);
    }

    public void DieAction()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerStats>().TakeDamage(damage);
            Die(false);
        }
        else if (collision.collider.CompareTag("Shield"))
        {
            Die(true);
        }
    }
}
