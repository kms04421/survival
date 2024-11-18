using MainSSM;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : SingletonBehaviour<UIManager>
{
    public Slider timeBar;
    public WaitForSeconds waitForSeconds;
    public delegate void TimeEndEvet(); // Ÿ�̸� ����� �Լ��� ��¿�
    public TimeEndEvet timeEndEvet; // Ÿ�̸� ����� �Լ��� ��¿�
    public Slider playerHPBar;
    public TextMeshProUGUI day;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI dfsText;
    public TextMeshProUGUI apsText;
    public TextMeshProUGUI monsterTotalAmount;
    public DragSlot dragSlot;
    public PlayerData playerData;
    public GameObject deadUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        waitForSeconds = new WaitForSeconds(0.01f);
        ADDtimeEndEvet();



    }
    private void Start()
    {
        playerData = FindAnyObjectByType<Player>().playerData;
        MenuTextUpdate();
    }
    public void ADDtimeEndEvet()// Ÿ�̸� ����� �߻��� �̺�Ʈ ����
    {

        timeEndEvet += SetTimeBarMaxValue; // Ÿ�ӹ� �ٽ�ä���
        timeEndEvet += GameManager.Instance.roundManager.NextRound; // ���������
        timeEndEvet += SetDay;// day�ؽ�Ʈ ������Ʈ
    }
    private void SetTimeBarMaxValue()//�����̴� �� �ʱ�ȭ��
    {
        timeBar.value = 1;
    }
    private void SetDay() // ���� ���� �ؽ�Ʈ�� ����
    {
        day.text = GameManager.Instance.roundManager.currentRound.ToString() + "Day";
    }
    public void ActivateTimer() // Ÿ�̸� �ڷ�ƾ �۵�
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
    public void HpBarSet() // uiManager�� �̵�
    {

        playerHPBar.value = (float)playerData.BaseHp / playerData.MaxHp;
        MenuTextUpdate();
    }
 

    public void MenuTextUpdate() // ���� view�� 
    {
        hpText.text = playerData.BaseHp.ToString() + " / " + playerData.MaxHp.ToString("F1");
        atkText.text = (playerData.AdditionalAttackPower + playerData.BaseAttackPower).ToString("F1");
        dfsText.text = (playerData.AdditionalDefensePower + playerData.BaseDefensePower).ToString("F1");
        apsText.text = (playerData.AdditionalAttackSpeed+ playerData.BaseAttackSpeed).ToString("F1");
    }

    
    public void GameOver()//���ӿ���
    {
        deadUI.SetActive(true);
    }
}

