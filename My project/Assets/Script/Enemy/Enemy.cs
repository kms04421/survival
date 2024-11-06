using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Rigidbody2D target;
    public float speed;
    public bool die = false;
    public EnemyData enemydata;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private EnemyState currentState;
    private enum EnemyState
    {
        Chase,// ����
        Die // ���
    }
    private void Update()
    {
        UpdateState();
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentState = EnemyState.Chase;
    }
    private void FixedUpdate()
    {
        if (currentState == EnemyState.Die) return;

        PerformAction();
    }
    void UpdateState()//���� ������Ʈ 
    {
        switch (currentState)
        {
            case EnemyState.Chase:
                if (enemydata.Hp <= 0)
                {
                    currentState = EnemyState.Die;
                }
                break;
            case EnemyState.Die:

                break;
        }
    }

    void PerformAction()//���¿� ���� ���� ���� 
    {
        switch (currentState)
        {
            case EnemyState.Chase:
                Vector2 dirVec = target.position - rigid.position;
                Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

                rigid.MovePosition(rigid.position + nextVec);
                break;

            case EnemyState.Die:
                break;
        }

    }
    private void LateUpdate()
    {
        if (currentState == EnemyState.Die) return;
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }
}
