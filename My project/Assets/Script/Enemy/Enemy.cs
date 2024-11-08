using UnityEngine;


namespace MainSSM
{
    public class Enemy : MonoBehaviour,IHitListener
    {
        private Rigidbody2D target;
        private Animator animator;
        public bool die = false;
        public EnemyData enemydata;
        private BoxCollider2D boxCollider;

        private Rigidbody2D rigid;
        private SpriteRenderer spriteRenderer;
        private EnemyState currentState;
        private GameManager gamemanager;
        private enum EnemyState
        {
            Chase,// 추적
            Die // 사망
        }
        private void Awake()
        {
            gamemanager = GameManager.Instance;
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            enemydata = new EnemyData(10,3,1);
            target = gamemanager.PlayerPosition.GetComponent<Rigidbody2D>();
            rigid = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentState = EnemyState.Chase;
        }
        private void Update()
        {
            if (currentState == EnemyState.Die) return;
            UpdateState();
        }
        private void FixedUpdate()
        {
            if (currentState == EnemyState.Die) return;
            PerformAction();
        }
        void UpdateState()//상태 업데이트 
        {
            switch (currentState)
            {
                case EnemyState.Chase:
                    if (enemydata.Hp <= 0)
                    {
                        currentState = EnemyState.Die;
                        animator.SetBool("Dead",true);
                        boxCollider.isTrigger = true;
                        Invoke("OnDie", 3);
                    }
                    break;
                case EnemyState.Die:

                    break;
            }
        }

        void PerformAction()//상태에 따라 동작 수행 
        {
            switch (currentState)
            {
                case EnemyState.Chase:
                    Vector2 dirVec = target.position - rigid.position;
                    Vector2 nextVec = dirVec.normalized * enemydata.Speed * Time.fixedDeltaTime;

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
        public void OnHit(int num)//몬스터 히트 시
        {
            if (currentState == EnemyState.Die) return;
            enemydata.TakeDamage(num);
            animator.SetTrigger("Hit");

        }
        public void OnEnable()
        {
            boxCollider.isTrigger = false;
            enemydata.HpSet();
        }
        public void OnDie()//몬스터 사망 시 3초후 제거
        {
            gameObject.SetActive(false);
            gamemanager.IncreaseMonstersKilled();
            MonsterSpawner.Instance.EnqueueMonster(gameObject);
        }
    }
} 