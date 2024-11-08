using MainSSM;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Slider timeBar;
    public WaitForSeconds WaitForSeconds;
    public delegate void TimeEndEvet(); // Ÿ�̸� ����� �Լ��� ��¿�
    public TimeEndEvet timeEndEvet; // Ÿ�̸� ����� �Լ��� ��¿�
    private RoundManager roundManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundManager = GameManager.Instance.roundManager;
        WaitForSeconds = new WaitForSeconds(0.01f);
        ActivateTimer();
        ADDtimeEndEvet(); 
    }
    public void ADDtimeEndEvet()// �̺�Ʈ ����
    {
        timeEndEvet += ActivateTimer;
        timeEndEvet += roundManager.NextRound;
    }
    public void ActivateTimer() // Ÿ�̸� �ڷ�ƾ �۵�
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
