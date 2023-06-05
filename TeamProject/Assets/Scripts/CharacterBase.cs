using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //데미지 애니매이션
    GameObject Hit;
    //최종 데미지
    float Damage;
    //기본 스탯
    float Strike;
    float Intelligent;
    float Agility;
    float Defence;
    float Anti;
    float hp;
    float mp;
    //최대 마나체력
    public float MaxHp = 100f;
    public float MaxMp = 100f;
    //스탯 배수 설정
    public float StrikeMultiple;
    public float IntelligentMultiple;
    public float DefenceMultiple;
    public float AntiMultiple;
    //치명타 배수
    public float Critical = 2f;
    //임시 데미지 이펙트 시간
    public float AnimationTime = 1f;
    //마나 체력 받기
    public float HP
    {
        get => MaxHp;
        protected set
        {
            if(hp != value)
            {
                hp = value;
                if(hp < 0)
                {
                    hp = 0;
                    Die();
                }
            }
        }
    }
    public float MP
    {
        get => MaxMp;
        protected set
        {
            if(mp != value)
            {
                mp = value;
            }
        }
    }
    
    /// <summary>
    /// 캐릭터 스탯
    /// </summary>
    protected virtual void Awake()
    {
        Hit = transform.GetChild(0).gameObject;
        Hit.SetActive(false);
    }

    /// <summary>
    /// 캐릭터 스탯
    /// </summary>
    protected virtual void CharacterStats
    {
        Strike = 1f;
        Intelligent = 50f;
        Agility = 1f;
        Defence = 1f * DefenceMultiple;
        Anti = 1f * AntiMultiple;
    }
    
    /// <summary>
    /// 주는 데미지
    /// </summary>
    /// <returns>주는 데미지 리턴</returns>
    protected virtual float Attack()
    {
        if (Random.Range(0, 100) < Agility)
        {
            Damage = (Strike * StrikeMultiple + Intelligent * IntelligentMultiple) * Critical;
        }
        else Damage = (Strike * StrikeMultiple + Intelligent * IntelligentMultiple);
        return Damage;
    }

    /// <summary>
    /// 받는 데미지
    /// </summary>
    /// <param name="getDamage">데미지</param>
    /// <param name="DamageSort">받는 데미지 종류</param>
    protected virtual void getDemage(float getDamage, int DamageSort)
    {
        if(DamageSort == 0) HP -= getDamage / Defence;
        else if(DamageSort == 1) HP -= getDamage / Anti;
        StartCoroutine(hit());
    }

    /// <summary>
    /// 으앙주금
    /// </summary>
    protected virtual void Die()
    {
        Debug.Log("Die");
    }

    /// <summary>
    /// 데이지 애니메이션 실행
    /// </summary>
    /// <returns>AnimationTime시간 만큼 실행</returns>
    IEnumerator hit()
    {
        Hit.SetActive(true);
        yield return AnimationTime;
        Hit.SetActive(false);
    }
}
