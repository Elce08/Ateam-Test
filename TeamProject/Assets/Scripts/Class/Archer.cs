using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Archer : CharacterBase
{
    PlayerInputAction inputAction;
    public GameObject attackEffect;
    public GameObject skillEffect;
    WaitForSeconds attackWait;
    WaitForSeconds skillWait;
    int choiceAttack;
    float powershot;

    protected virtual void Status()
    {
        MaxHp = 100.0f;
        MaxMp = 100.0f;
        Strike = 1.0f;
        Intelligent = 1.0f;
        Agility = 1.0f;
        Defence = 1.0f;
        Anti = 1.0f;
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
        inputAction.Player.ChoiceEnemy.performed += OnChoiceEnemy;
    }



    private void OnDisable()
    {
        inputAction.Player.ChoiceEnemy.performed -= OnChoiceEnemy;
        inputAction.Player.Skill.performed -= OnSkill;
        inputAction.Player.NumberPad.performed -= OnAttack;
        inputAction.Player.Disable();
    }
        
    private void OnAttack(InputAction.CallbackContext value)    // �Ϲݰ��� ���ý�(Ű���� 1)
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
        if (choiceAttack == 1)         // �� ���ý� �ൿ ������ 1�̾��ٸ�
        {
            Attack();                   // �⺻ ���� ����
            Debug.Log("�� ����");
            choiceAttack = 0;           // �ൿ ���� �ʱ�ȭ
        }
        else if (choiceAttack == 2)      // �� ������ �ൿ ������ 2�̾��ٸ�
        {
            Skill();                    // ��ų ���� ����
            Debug.Log("������ ��ų");
            choiceAttack = 0;           // �ൿ ���� �ʱ�ȭ
        }
        else                            // �ൿ ������ ����� ���� �ȵǾ�����
        {
            Debug.Log("�ൿ�� �����ϼ���");
        }
    }

    private void Skill()
    {
        if (Random.Range(0, 100) < Agility)
        {
            powershot = (Agility * StrikeMultiple) * Critical;
        }
        else powershot = (Agility * StrikeMultiple);
        StartCoroutine(SkillEffect());
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
