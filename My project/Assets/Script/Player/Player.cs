
using UnityEngine;
using System.Collections.Generic;

namespace MainSSM
{
    public class Player : MonoBehaviour, IHitListener
    {
        public FloatingJoystick movejoy;//������ ���̽�ƽ
        public FloatingJoystick rotejoy;//���� ���̽�ƽ

        private Animator animator;  //�÷��̾� ���ϸ�����
        private Rigidbody2D rigid; //�÷��̾� rigdboy
        private Vector2 movement; //������ ����� xy
        private GameObject weapoon; // ����    
        [HideInInspector]public WeaponController weaponController; // ���� ��Ʈ�ѷ�
        [HideInInspector]public SpriteRenderer spriteRenderer; // �÷��̾� sprite
        [HideInInspector]public Vector2 rote; // �÷��̾� ���� ȸ���� ����� xy
        float isMoving = 0; //���ϸ��̼� �ӵ� ��

        private Inventory inventory; // �κ��丮

        public PlayerData playerData;
        private PlayerState currentState; // ���� ����
        private enum PlayerState
        {
            Idle,// �⺻
            Menu, // �޴� ���»���
            Die // ���
        }
        // Start is called before the first frame update
        void Awake()
        {
            playerData = new PlayerData(100, 3.5f, 100,1,2.5f);         
            weapoon = transform.Find("Weapon").gameObject;
            weaponController = weapoon.GetComponent<WeaponController>();          
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody2D>();
            inventory = GetComponent<Inventory>();
        }
        private void Update()
        {
            UpdateState();
        }
        private void FixedUpdate()
        {
            PerformAction();
        }
        private void LateUpdate()
        {
            if (currentState == PlayerState.Die) return;
            if (movement.x > 0)
            {
                spriteRenderer.flipX = false; // ������ ����
            }
            else if (movement.x < 0)
            {
                spriteRenderer.flipX = true; // ���� ����
            }

            isMoving = movement.magnitude;
            animator.SetFloat("Speed", isMoving);
        }
        private void UpdateState() // ������Ʈ ���ޱ�
        {
            switch (currentState)
            {
                case PlayerState.Idle:
                    movement.x = movejoy.Horizontal;
                    movement.y = movejoy.Vertical;
                    rote.x = rotejoy.Horizontal;
                    rote.y = rotejoy.Vertical;
                    break;
                case PlayerState.Menu:
                    break;
                case PlayerState.Die:
                    animator.SetTrigger("Dead");
                    UIManager.Instance.GameOver();
                    AudioManager.Instance.BackGroundAudioPlay(2);
                    weapoon.SetActive(false);
                    rigid.bodyType = RigidbodyType2D.Kinematic;
                    break;
            }
        }
        private void PerformAction() // ����ȿ�� ������Ʈ 
        {
            switch (currentState)
            {
                case PlayerState.Idle:
                    rigid.MovePosition(rigid.position + movement * playerData.BaseMovementSpeed * Time.fixedDeltaTime);
                    weaponController.Attack();
                    break;
                case PlayerState.Menu:
                    break;
                case PlayerState.Die:
                    break;
            }


        }

    

        public void OnHit(float dam) // IHitListener��Ʈ�� �������̽� ���� �÷��̾� ���� ��Ʈ�� �۵�
        {
            if (currentState == PlayerState.Die) return;
            playerData.TakeDamage(dam);
            UIManager.Instance.HpBarSet();
            if (playerData.BaseHp <= 0)
            {
                currentState = PlayerState.Die;
            }
        }
      
        private void OnTriggerEnter2D(Collider2D collision) 
        {
            if (collision.gameObject.layer == 6)
            {
                Item item = collision.GetComponent<Item>();
                if (item != null)
                {
                    inventory.AddItem(item.itemData);
                    item.PickupItem();
                }
            }
        }

    }
}