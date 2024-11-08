using MainSSM;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Slider timeBar;
    public WaitForSeconds WaitForSeconds;
    public delegate void TimeEndEvet(); // 타이머 종료시 함수들 담는용
    public TimeEndEvet timeEndEvet; // 타이머 종료시 함수들 담는용
    private RoundManager roundManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundManager = GameManager.Instance.roundManager;
        WaitForSeconds = new WaitForSeconds(0.01f);
        ActivateTimer();
        ADDtimeEndEvet(); 
    }
    public void ADDtimeEndEvet()// 이벤트 저장
    {
        timeEndEvet += ActivateTimer;
        timeEndEvet += roundManager.NextRound;
    }
    public void ActivateTimer() // 타이머 코루틴 작동
    {
        int time = roundManager.roundTime;
        StartCoroutine(StartTimer(time));

    }
    IEnumerator StartTimer(int MaxTime)
    {

        float Count = MaxTime;
        while (Count > 0)
        {
            Count = Count - 0.01f;
            timeBar.value = (float)Count / MaxTime;
            yield return WaitForSeconds;
        }
        timeEndEvet?.Invoke();
    }


}
