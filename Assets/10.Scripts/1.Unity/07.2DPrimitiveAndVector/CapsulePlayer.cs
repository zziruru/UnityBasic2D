using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Study.PrimitiveAndVector
{
    public class CapsulePlayer : MonoBehaviour
    {
        // capsule player
        // 1. 화살표(좌,우)를 이용한 이동 및 표현
        // 2. Space 버튼 이용한 점프
        // 3. Platform 이라는 지형 위에서 움직여야 함

        public enum State // 점프는 요 값들에 중첩되는 상태이므로 여기에 선언하지 않음
        {
            Idle = 0, // 대기 상태
            Left, // 왼쪽으로 가는 상태
            Right // 오른쪽으로 가는 상태
        }

        public GameObject[] SunGlasses;
        private State currentState = State.Idle;

        private Rigidbody2D rBody;
        private Collider2D col;

        private void Update()
        {
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                SetSunGlassState(State.Left);
                Move(Vector3.left);
            }
            else if (Keyboard.current.rightArrowKey.isPressed)
            {
                SetSunGlassState(State.Right);
                Move(Vector3.right);
            }
            else
            {
                SetSunGlassState(State.Idle);
            }

            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }
        }

        public float jumpPower = 3;

        private void Jump()
        {
            // AddForce를 사용하지 않고 점프를 구현해보세요
            // MoveVector 에 수평적 움직임과 점프의 수직적 움직임을통합하면 됨
            rBody.AddForceY(jumpPower, ForceMode2D.Impulse);
        }

        private void SetSunGlassState(State state)
        {
            // 상태가 전환될때만 아래 로직이 실행되도록
            // 예외처리를 합니다
            if (currentState == state) return;

            SunGlasses[(int)currentState].SetActive(false);
            SunGlasses[(int)state].SetActive(true);
            currentState = state;
        }

        public float speed = 2.0f;

        private void Awake()
        {
            rBody = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
        }

        private void Move(Vector3 dir)
        {
            //transform.Translate(dir * speed * Time.deltaTime);

            // 이번 프레임에 움직일 벡터의 크기 : 이동량
            Vector3 moveVector = dir * speed * Time.deltaTime;
           
            // 내 위치와 이동량을 더해준다
            rBody.MovePosition(transform.position + moveVector);
        }
    }
}