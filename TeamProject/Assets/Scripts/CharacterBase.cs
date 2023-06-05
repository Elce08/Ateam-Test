using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //������ �ִϸ��̼�
    GameObject Hit;
    //���� ������
    float Damage;
    //�⺻ ����
    float Strike;
    float Intelligent;
    float Agility;
    float Defence;
    float Anti;
    float hp;
    float mp;
    //�ִ� ����ü��
    public float MaxHp = 100f;
    public float MaxMp = 100f;
    //���� ��� ����
    public float StrikeTime;
    public float IntelligentTime;
    public float DefenceTime;
    public float AntiTime;
    //ġ��Ÿ ���
    public float Critical = 2f;
    //�ӽ� ������ ����Ʈ �ð�
    public float AnimationTime = 1f;
    //���� ü�� �ޱ�
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
    /// ĳ���� ����
    /// </summary>
    protected virtual void Awake()
    {
        Strike = 1f;
        Intelligent = 50f;
        Agility = 1f;
        Defence = 1f * DefenceTime;
        Anti = 1f * AntiTime;
        Hit = transform.GetChild(0).gameObject;
        Hit.SetActive(false);
    }
    
    /// <summary>
    /// �ִ� ������
    /// </summary>
    /// <returns>�ִ� ������ ����</returns>
    protected virtual float Attack()
    {
        if (Random.Range(0, 100) < Agility)
        {
            Damage = (Strike * StrikeTime + Intelligent * IntelligentTime) * Critical;
        }
        else Damage = (Strike * StrikeTime + Intelligent * IntelligentTime);
        return Damage;
    }

    /// <summary>
    /// �޴� ������
    /// </summary>
    /// <param name="getDamage">������</param>
    /// <param name="DamageSort">�޴� ������ ����</param>
    protected virtual void getDemage(float getDamage, int DamageSort)
    {
        if(DamageSort == 0) HP -= getDamage / Defence;
        else if(DamageSort == 1) HP -= getDamage / Anti;
        StartCoroutine(hit());
    }

    /// <summary>
    /// �����ֱ�
    /// </summary>
    protected virtual void Die()
    {
        Debug.Log("Die");
    }

    /// <summary>
    /// ������ �ִϸ��̼� ����
    /// </summary>
    /// <returns>AnimationTime�ð� ��ŭ ����</returns>
    IEnumerator hit()
    {
        Hit.SetActive(true);
        yield return AnimationTime;
        Hit.SetActive(false);
    }
}