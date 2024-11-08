using MainSSM;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public int EnemyCount { get; private set; } //���� ���� ���� ��ü ����

    public int currentRound = 1; // ���� ����

    private int baseMonsterHP = 10; // �⺻ ���� HP

    private float hpMultiplier = 1.2f; // HP�� ���� ����

    public int roundTime = 100; // �������� �ð�
    public void SetRoundParameters()// ���庰 ���� ����
    {
        EnemyCount = currentRound * 5; // ��: ���忡 ���� �� �� ����

    }
    public void NextRound()//���� ����� ���� �Լ�
    {
        currentRound++;
        SetRoundParameters();
        MonsterSpawner.Instance.SpawnMonster(); // ���� ����
    }
    public int GetMonsterHPForCurrentRound()// ���庰 ���� hp
    {
        return Mathf.FloorToInt(baseMonsterHP * Mathf.Pow(hpMultiplier, currentRound - 1));
    }

    
}
