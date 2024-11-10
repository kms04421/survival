using MainSSM;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public int EnemyCount { get; private set; } //현재 라운드 몬스터 전체 수량

    public int currentRound = 1; // 현재 라운드

    private int baseMonsterHP = 10; // 기본 몬스터 HP

    private float hpMultiplier = 1.2f; // HP가 증가 비율

    public int roundTime = 100; // 스테이지 시간
    public void SetRoundParameters()// 라운드별 몬스터 수량
    {
        EnemyCount = currentRound * 5; // 예: 라운드에 따라 적 수 증가

    }
    public void NextRound()//다음 라운드로 진행 함수
    {
        currentRound++;
        SetRoundParameters();
        MonsterSpawner.Instance.SpawnMonster(); // 몬스터 스폰
    }
    public int GetMonsterHPForCurrentRound()// 라운드별 몬스터 hp
    {
        return Mathf.FloorToInt(baseMonsterHP * Mathf.Pow(hpMultiplier, currentRound - 1));
    }

    
}
