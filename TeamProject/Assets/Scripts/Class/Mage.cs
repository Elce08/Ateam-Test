using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mage : CharacterBase
{
    float meteor;
    PlayerInputAction inputAction;
    int choiceAttack;
    public GameObject attackEffect;
    public GameObject skillEffect;
    WaitForSeconds attackWait;
    WaitForSeconds skillWait;

    private void Skill()
    {
        if (Random.Range(0, 100) < Agility)
        {
            meteor = (Strike * StrikeMultiple + Intelligent * IntelligentMultiple) * Critical;
        }
        else meteor = (Strike * StrikeMultiple + Intelligent * IntelligentMultiple);
        StartCoroutine(SkillEffect());
    }

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(AttackEffect());
    }

    protected override void Awake()
    {
        base.Awake();
        inputAction = new PlayerInputAction();
        attackEffect = transform.GetChild(1).gameObject;
        skillEffect = transform.GetChild(2).gameObject;
        attackWait = new WaitForSeconds(1);
        skillWait = new WaitForSeconds(1);

    }

    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.Attack.performed += OnAttack;
        inputAction.Player.Skill.performed += OnSkill;
        inputAction.Player.ChoiceEnemy.performed += OnChoiceEnemy; ;
    }

    

    private void OnDisable()
    { 
        inputAction.Player.ChoiceEnemy.performed -= OnAttack;
        inputAction.Player.Skill.performed -= OnSkill;
        inputAction.Player.NumberPad.performed -= OnAttack;
        inputAction.Player.Disable();
    }
  
    private void OnAttack(InputAction.CallbackContext value)
    {
        choiceAttack = 1;
        Debug.Log("�Ϲ� ������ ���õǾ����ϴ�.");
    }

    private void OnSkill(InputAction.CallbackContext value)
    {
        choiceAttack = 2;
        Debug.Log("��ų�� ���õǾ����ϴ�.");
    }

    private void OnChoiceEnemy(InputAction.CallbackContext value)
    {
        if ( choiceAttack == 1)
        {
            Attack();
            Debug.Log("�� ����");
            choiceAttack = 0;
        }
        else if(choiceAttack == 2)
        {
            Skill();
            Debug.Log("������ ��ų");
            choiceAttack = 0;
        }
        else
        {
            Debug.Log("�ൿ�� �����ϼ���");
        }
    }

    IEnumerator AttackEffect()
    {
        attackEffect.SetActive(true);
        yield return attackWait;
        attackEffect.SetActive(false);
    }

    IEnumerator SkillEffect()
    {
        skillEffect.SetActive(true);
        yield return skillWait;
        skillEffect.SetActive(false);
    }

}