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
        private Animator animator; // ���� ���ϸ�����
        [HideInInspector]public EnemyData enemydata; // ���� �⺻ ���� HP,Speed,Damage��
        private BoxCollider2D boxCollider; // ���� �ڽ��ݶ��̴�
        private WaitForSeconds knockbackDelay; // �˹� ��� �ð�
        private Rigidbody2D rigid; // ���� Rigidbody2D
        private SpriteRenderer spriteRenderer; // ���� SpriteRenderer 
        private EnemyState currentState; // ���� ����
        private GameManager gamemanager; // ���Ӹ޴��� �̱��� GameManager.Instance �����
        private Vector2 Speed; // ���� ���ǵ�
        private float lastHitTime = 0f; // ������ �ǰ� �ð��� ����
        private float hitCooldown = 1f; // 1�� ����
        public List<ItemData> itemList; // ��� ������ ���
        [HideInInspector]public int spawnIndex = 0;
        private enum EnemyState
        {
            Chase,// ����
            Hit, // ��Ʈ
            Die // ���
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
        void UpdateState()//���� ������Ʈ 
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

        void PerformAction()//���¿� ���� ���� ���� 
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
        public void OnHit(int num)//���� ��Ʈ ��
        {       
            if (currentState == EnemyState.Die) return;
            enemydata.TakeDamage(num);
            animator.SetTrigger("Hit");
            currentState = EnemyState.Hit;
            /*   Vector2 knockbackDirection = (rigid.position - (Vector2)gamemanager.PlayerPosition.position).normalized;
               rigid.AddForce(knockbackDirection * 0.3f, ForceMode2D.Impulse);*/ //�о�� ����
        }
        public void OnEnable()
        {
            InIt();
        }

        private void InIt()//�ʱ�ȭ
        {
            Speed = new Vector2(enemydata.Speed, rigid.linearVelocityY);
            rigid.linearVelocity = Speed;
            boxCollider.isTrigger = false;
            enemydata.HpSet();
            currentState = EnemyState.Chase;
        }
        public void OnDie()//���� ��� �� 3���� ����
        {
            gameObject.SetActive(false);

            MonsterSpawner.Instance.EnqueueMonster(gameObject , spawnIndex);
        }
        IEnumerator WaitForKnockback() // ���� ����
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

                    // ������ �ǰ� �ð� ����
                    lastHitTime = Time.time;
                }
            }else if (collision.collider.gameObject.layer != 10)
            {
                collision.gameObject.SetActive(false);
            }
        }
    }

}

