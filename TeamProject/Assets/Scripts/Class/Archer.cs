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
        
    private void OnAttack(InputAction.CallbackContext value)    // 일반공격 선택시(키보드 1)
    {
        choiceAttack = 1;
        Debug.Log("일반 공격이 선택되었습니다.");
    }

    private void OnSkill(InputAction.CallbackContext value)
    {
        choiceAttack = 2;
        Debug.Log("스킬이 선택되었습니다.");
    }

    private void OnChoiceEnemy(InputAction.CallbackContext value)
    {
        if (choiceAttack == 1)         // 적 선택시 행동 선택이 1이었다면
        {
            Attack();                   // 기본 공격 실행
            Debug.Log("적 공격");
            choiceAttack = 0;           // 행동 선택 초기화
        }
        else if (choiceAttack == 2)      // 적 선택히 행동 선택이 2이었다면
        {
            Skill();                    // 스킬 공격 실행
            Debug.Log("적한테 스킬");
            choiceAttack = 0;           // 행동 선택 초기화
        }
        else                            // 행동 선택이 제대로 선택 안되었을때
        {
            Debug.Log("행동을 선택하세요");
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
