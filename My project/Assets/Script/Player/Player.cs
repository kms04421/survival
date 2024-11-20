
using UnityEngine;
using System.Collections.Generic;

namespace MainSSM
{
    public class Player : MonoBehaviour, IHitListener
    {
        public FloatingJoystick movejoy;//움직임 조이스틱
        public FloatingJoystick rotejoy;//무기 조이스틱

        private Animator animator;  //플레이어 에니메이터
        private Rigidbody2D rigid; //플레이어 rigdboy
        private Vector2 movement; //움지임 저장용 xy
        private GameObject weapoon; // 무기    
        [HideInInspector]public WeaponController weaponController; // 무기 컨트롤러
        [HideInInspector]public SpriteRenderer spriteRenderer; // 플레이어 sprite
        [HideInInspector]public Vector2 rote; // 플레이어 무기 회전각 저장용 xy
        float isMoving = 0; //에니메이션 속도 용

        private Inventory inventory; // 인벤토리

        public PlayerData playerData;
        private PlayerState currentState; // 현재 상태
        private enum PlayerState
        {
            Idle,// 기본
            Menu, // 메뉴 오픈상태
            Die // 사망
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
                spriteRenderer.flipX = false; // 오른쪽 방향
            }
            else if (movement.x < 0)
            {
                spriteRenderer.flipX = true; // 왼쪽 방향
            }

            isMoving = movement.magnitude;
            animator.SetFloat("Speed", isMoving);
        }
        private void UpdateState() // 업데이트 값받기
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
        private void PerformAction() // 물리효과 업데이트 
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

    

        public void OnHit(float dam) // IHitListener히트용 인터페이스 몬스터 플레이어 적용 히트시 작동
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