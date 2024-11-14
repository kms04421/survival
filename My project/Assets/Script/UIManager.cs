using MainSSM;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : SingletonBehaviour<UIManager>
{
    public Slider timeBar;
    public WaitForSeconds waitForSeconds;
    public delegate void TimeEndEvet(); // 타이머 종료시 함수들 담는용
    public TimeEndEvet timeEndEvet; // 타이머 종료시 함수들 담는용
    public Slider playerHPBar;
    public TextMeshProUGUI day;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI dfsText;
    public TextMeshProUGUI apsText;
    public TextMeshProUGUI monsterTotalAmount;
    public DragSlot dragSlot;
    public PlayerData playerData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        waitForSeconds = new WaitForSeconds(0.01f);
        ADDtimeEndEvet();
     
    }
    private void Start()
    {
        playerData = FindAnyObjectByType<Player>().playerData;
    }
    public void ADDtimeEndEvet()// 이벤트 저장
    {
   
        timeEndEvet += SetTimeBarMaxValue; // 타임바 다시채우기
        timeEndEvet += GameManager.Instance.roundManager.NextRound; // 다음라운드로
        timeEndEvet += SetDay;// day텍스트 업데이트
    }
    private void SetTimeBarMaxValue()//슬라이더 값 초기화용
    {
        timeBar.value = 1;
    }
    private void SetDay() // 라운드 레벨 텍스트에 세팅
    {
        day.text = GameManager.Instance.roundManager.currentRound.ToString()+"Day";
    }
    public void ActivateTimer() // 타이머 코루틴 작동
    {
        int time = GameManager.Instance.roundManager.roundTime;
        StartCoroutine(StartTimer(time));

    }
    IEnumerator StartTimer(int MaxTime)
    {

        float Count = MaxTime;
        while (Count > 0)
        {
            Count = Count - 0.01f;
            timeBar.value = (float)Count / MaxTime;
            yield return waitForSeconds;
        }
        timeEndEvet?.Invoke();
    }
    public void HpBarSet(ICharacterData characterData) // uiManager로 이동
    {

        playerHPBar.value = (float)characterData.Hp / characterData.MaxHp;

    }


    public void MenuTextUpdate() // 추후 view로 
    {
        hpText.text = playerData.Hp.ToString();
        atkText.text = playerData.Damage.ToString();
        dfsText.text = playerData.DFS.ToString();
        apsText.text = playerData.APS.ToString();
    }
}

