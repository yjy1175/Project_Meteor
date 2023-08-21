using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Sgt
    private static PlayerController instance;
    public static PlayerController GetInstance() => instance;

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
    public event EventManager.SingleBool doubleShot;
    public event EventManager.SingleBool circleDiffuse;
    #endregion
    [SerializeField] private bool isMove = true;
    [SerializeField] private float attackTime;
    [SerializeField] private float doubleShotAttackTime;
    [SerializeField] private float circleDiffuseTime;

    [SerializeField] private GameObject baseBullet;
    [SerializeField] private GameObject doubleShotBullet;
    [SerializeField] private GameObject circleDiffuseBullet;
    [SerializeField] private GameObject lazerBullet;
    [SerializeField] private Transform lunchPosition;
    [SerializeField] private Transform bulletHolder;

    [SerializeField] private GameObject doubleShotAnimation;
    [SerializeField] private GameObject shieldAnimation;

    private void Start()
    {
        bulletHolder = GameObject.Find("Bullets").transform;
    }

    private void Update()
    {
        MoveControll();
        AttackControll();
    }

    private void MoveControll()
    {
        if (isMove)
        {
            GetComponent<PlayerMovement>().Move(InGameUiManager.GetInstance().curPed.GetHorizontalValue());

            if (Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<PlayerMovement>().Move(1);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<PlayerMovement>().Move(-1);
            }
        }
    }

    private void AttackControll()
    {
        if (isMove)
        {
            attackTime += Time.deltaTime;
            doubleShotAttackTime += Time.deltaTime;
            circleDiffuseTime += Time.deltaTime;

            if(doubleShotAttackTime > GetComponent<PlayerStats>().DoubleShotCoolTime)
            {
                doubleShot.Invoke(false);
            }

            if(circleDiffuseTime > GetComponent<PlayerStats>().CircleDiffuseCoolTime)
            {
                circleDiffuse.Invoke(false);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.Z))
            {
                Attack();
            }

            if (Input.GetKey(KeyCode.X))
            {
                DoubleShotAttack();
            }

            if (Input.GetKey(KeyCode.C))
            {
                CircleDiffuse();
            }
        }
    }

    public void IsMove(bool move)
    {
        isMove = move;
    }

    public void Jump()
    {
        GetComponent<PlayerMovement>().Jump();
    }

    public void Attack()
    {
        if(attackTime > GetComponent<PlayerStats>().AttackSpeed)
        {
            if (!Lazer())
            {
                GetComponent<PlayerAnimation>().PlayAttack();

                Bullet newBullet = Instantiate(baseBullet, lunchPosition.position, Quaternion.identity, bulletHolder).GetComponent<Bullet>();
                newBullet.SetBullet(GetComponent<PlayerStats>().Power, transform.localScale.x > 0 ? 1 : -1);

                attackTime = 0;

                SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.PLAYER_ATTACK);
            }
        }
    }

    public void DoubleShotAttack()
    {
        if (doubleShotAttackTime > GetComponent<PlayerStats>().DoubleShotCoolTime)
        {
            doubleShot.Invoke(true);
            doubleShotAttackTime = 0;

            StartCoroutine(CoDoubleShotAttack());
        }
    }

    IEnumerator CoDoubleShotAttack()
    {
        doubleShotAnimation.SetActive(true);
        SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.PLAYER_DOUBLE_SHOT);
        for (int i = 0; i < GetComponent<PlayerStats>().DoubleShotCount; i++)
        {
            Bullet newBullet0 = Instantiate(doubleShotBullet, lunchPosition.position + Vector3.up * 0.1f, Quaternion.identity, bulletHolder).GetComponent<Bullet>();
            newBullet0.SetBullet(GetComponent<PlayerStats>().Power * 2, transform.localScale.x > 0 ? 1 : -1);
            Bullet newBullet1 = Instantiate(doubleShotBullet, lunchPosition.position + Vector3.down * 0.1f, Quaternion.identity, bulletHolder).GetComponent<Bullet>();
            newBullet1.SetBullet(GetComponent<PlayerStats>().Power * 2, transform.localScale.x > 0 ? 1 : -1);

            yield return new WaitForSeconds(GetComponent<PlayerStats>().AttackSpeed * 2f);
        }
        yield return new WaitForSeconds(0.5f);
        doubleShotAnimation.SetActive(false);
    }

    public void CircleDiffuse()
    {
        if (circleDiffuseTime > GetComponent<PlayerStats>().CircleDiffuseCoolTime)
        {
            circleDiffuse.Invoke(true);
            circleDiffuseTime = 0;
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.PLAYER_CIRCLE_DIFFUSE);
            Instantiate(circleDiffuseBullet, transform.position, Quaternion.identity, transform);
        }
    }

    public bool Lazer()
    {
        int ran = Random.Range(0, 100);

        if(ran < PlayerStats.GetInstance().CriticalLaserChance)
        {
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.PLAYER_LASER);
            Instantiate(lazerBullet, lunchPosition.position, Quaternion.identity, transform);
            attackTime = -1f;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnShield()
    {
        if (!shieldAnimation.activeInHierarchy)
        {
            shieldAnimation.SetActive(true);
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.PLAYER_AVOIDANCE);
        }
    }
}
