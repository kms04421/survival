using TMPro;
using UnityEngine;

public class RandomStateUi : MonoBehaviour
{
    private TextMeshProUGUI stateValue;
    private Animator Animator;
    public float randomValue = 0;

    void Start()
    {
        Animator = GetComponent<Animator>();
        stateValue = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

    }

    public void SetRandomState() //·»´ý°ª ¼¼ÆÃ 
    {
        randomValue = Random.Range(-0.6f,1.1f);
        stateValue.text = randomValue.ToString("F1");
        Animator.SetBool("StateUp", true);
        

    }

    public void SelectState(int btnIndex)
    {
        UIManager.Instance.playerData.ModifyBaseStat(btnIndex, randomValue);
        Animator.SetBool("StateUp", false);
        UIManager.Instance.MenuTextUpdate();
    }

}