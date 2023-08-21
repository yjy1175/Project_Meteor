using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour
{
    [SerializeField] private Button doubleShotAttackButton;
    [SerializeField] private Button circleDiffuseButton;

    [SerializeField] private bool isBaseAttackDown = false;
    [SerializeField] private bool isJumpDown = false;

    private void Start()
    {
        PlayerController.GetInstance().doubleShot += ShowDoubleShotButton;
        PlayerController.GetInstance().circleDiffuse += ShowCircleDiffuseButton;

        doubleShotAttackButton.onClick.AddListener(() =>
        {
            PlayerController.GetInstance().DoubleShotAttack();
        });

        circleDiffuseButton.onClick.AddListener(() =>
        {
            PlayerController.GetInstance().CircleDiffuse();
        });
    }

    private void Update()
    {
        if (isBaseAttackDown)
        {
            PlayerController.GetInstance().Attack();
        }

        if (isJumpDown)
        {
            PlayerController.GetInstance().Jump();
        }
    }

    public void BaseAttackButtonDown()
    {
        isBaseAttackDown = true;
    }

    public void BaseAttackButtonUp()
    {
        isBaseAttackDown = false;
    }

    public void JumpButtonDown()
    {
        isJumpDown = true;
    }

    public void JumpButtonUp()
    {
        isJumpDown = false;
    }

    private void ShowDoubleShotButton(bool isShow)
    {
        doubleShotAttackButton.transform.GetChild(0).gameObject.SetActive(isShow);
    }

    private void ShowCircleDiffuseButton(bool isShow)
    {
        circleDiffuseButton.transform.GetChild(0).gameObject.SetActive(isShow);
    }
}
