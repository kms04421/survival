using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;


namespace MainSSM
{
    public class Enemy : MonoBehaviour, IHitListener
    {
        private Rigidbody2D target; // player Rigidbody2D
        private Animator animator; // 몬스터 에니메이터
        [HideInInspector]public EnemyData enemydata; // 몬스터 기본 정보 HP,Speed,Damage등
        private BoxCollider2D boxCollider; // 몬스터 박스콜라이더
        private WaitForSeconds knockbackDelay; // 넉백 대기 시간
        private Rigidbody2D rigid; // 몬스터 Rigidbody2D
        private SpriteRenderer spriteRenderer; // 몬스터 SpriteRenderer 
        private EnemyState currentState; // 현재 상태
        private GameManager gamemanager; // 게임메니저 싱글톤 GameManager.Instance 저장용
        private Vector2 Speed; // 몬스터 스피드
        private float lastHitTime = 0f; // 마지막 피격 시간을 추적
        private float hitCooldown = 1f; // 1초 간격
        public List<ItemData> itemList; // 드랍 아이템 목록
        [HideInInspector]public int spawnIndex = 0;
        private enum EnemyState
        {
            Chase,// 추적
            Hit, // 히트
            Die // 사망
        }
        private void Awake()
        {           
            knockbackDelay = new WaitForSeconds(0.2f);
            gamemanager = GameManager.Instance;
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
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
            PerformAction();
        }
        void UpdateState()//상태 업데이트 
        {
            switch (currentState)
            {
                case EnemyState.Chase:
                    if (enemydata.HP <= 0)
                    {
                        gamemanager.IncreaseMonstersKilled();
                        currentState = EnemyState.Die;
                        animator.SetBool("Dead", true);
                        boxCollider.isTrigger = true;
                        Invoke("OnDie", 3);
                        rigid.linearVelocity = Vector2.zero;
                        DropItems();
                    }
                    break;
                case EnemyState.Hit:
                    StartCoroutine(WaitForKnockback());
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
                case EnemyState.Hit:
                    rigid.linearVelocity = Vector2.zero;
                    break;

                case EnemyState.Die:

                    break;
            }

        }
        private void LateUpdate()
        {
            if (currentState == EnemyState.Die || currentState == EnemyState.Hit) return;
            spriteRenderer.flipX = target.position.x < rigid.position.x;
        }
        public void OnHit(int num)//몬스터 히트 시
        {       
            if (currentState == EnemyState.Die) return;
            enemydata.TakeDamage(num);
            animator.SetTrigger("Hit");
            currentState = EnemyState.Hit;
            /*   Vector2 knockbackDirection = (rigid.position - (Vector2)gamemanager.PlayerPosition.position).normalized;
               rigid.AddForce(knockbackDirection * 0.3f, ForceMode2D.Impulse);*/ //밀어내는 로직
        }
        public void OnEnable()
        {
            InIt();
        }

        private void InIt()//초기화
        {
            Speed = new Vector2(enemydata.Speed, rigid.linearVelocityY);
            rigid.linearVelocity = Speed;
            boxCollider.isTrigger = false;
            enemydata.HpSet();
            currentState = EnemyState.Chase;
        }
        public void OnDie()//몬스터 사망 시 3초후 제거
        {
            gameObject.SetActive(false);

            MonsterSpawner.Instance.EnqueueMonster(gameObject , spawnIndex);
        }
        IEnumerator WaitForKnockback() // 몬스터 경직
        {
            yield return knockbackDelay;
            if (currentState != EnemyState.Die)
            {
                rigid.linearVelocity = Speed;
                currentState = EnemyState.Chase;
            }

        }

        private void DropItems()
        {
            foreach (ItemData item in itemList)
            {
                if (Random.value < item.dropChance)
                {
                    DropItem(item);
                }
            }
        }

        private void DropItem(ItemData itemData)
        {
            if (itemData.itemPrefab != null)
            {
                GameObject go = ItemManager.Instance.GetItem(itemData.itemPrefab.name);
                Vector3 randomOffset = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0) * 0.5f;
                Vector3 dropPosition = transform.position + randomOffset;
                go.transform.position = dropPosition;
                go.SetActive(true);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (currentState == EnemyState.Die) return;
            if (collision.collider.gameObject.layer == 9)
            {
                if (Time.time - lastHitTime >= hitCooldown)
                {
                    collision.collider.GetComponent<IHitListener>().OnHit(enemydata.Damage);

                    // 마지막 피격 시간 갱신
                    lastHitTime = Time.time;
                }
            }else if (collision.collider.gameObject.layer != 10)
            {
                collision.gameObject.SetActive(false);
            }
        }
    }

}

