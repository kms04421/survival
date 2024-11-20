using MainSSM;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public int EnemyCount { get; private set; } //���� ���� ���� ��ü ����

    public int currentRound = 1; // ���� ����

    private int baseMonsterHP = 10; // �⺻ ���� HP

    private int baseMonsterPower = 5; // �⺻ ���� ���ݷ�

    private float hpMultiplier = 1.2f; // HP�� ���� ����

    public int roundTime = 10; // �������� �ð�
    public void SetRoundParameters()// ���庰 ���� ����
    {
        EnemyCount = currentRound * 5; // ��: ���忡 ���� �� �� ����
        UIManager.Instance.monsterTotalAmount.text = EnemyCount.ToString();
    }
    public void NextRound()//���� ����� ���� �Լ�
    {
      
        currentRound++;
        SetRoundParameters();
        EnemySpawner.Instance.SpawnMonster(); // ���� ����
        
    }
    public int GetMonsterHPForCurrentRound()// ���庰 ���� hp
    {
        return Mathf.FloorToInt(baseMonsterHP * Mathf.Pow(hpMultiplier, currentRound - 1));
    }

    public int GetMonsterPowerForCurrentRound()// ���庰 ���� ���ݷ�
    {
        return Mathf.FloorToInt(baseMonsterPower * Mathf.Pow(hpMultiplier, currentRound - 1));
    }

}
