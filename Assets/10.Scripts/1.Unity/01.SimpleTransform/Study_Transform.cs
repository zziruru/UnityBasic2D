using UnityEngine;
using UnityEngine.InputSystem;

namespace Study.Transform
{
    public class Study_Transform: MonoBehaviour
    {
        // Transform 컴포넌트
        // - 좌표계 변환을 담당하는 객체
        // - 모든 GameObject가 반드시 하나씩 가지고 있다
        // - 위치(Position), 회전(Rotation), 크기(Scale) 정보를 담고 있음
        // - Scene 내에서 GameObject 의 좌표, 공간적인 정보를 정의함
        // - 계층구조 : Transform 은 부모 - 자식 관계를 가질 수 있으며, 
        // 자식 Transform은 부모 Transform 의 영향을 받음
        // (예 : 부모가 이동하면 자식도 이동함)

        // 기본적인 Transform 의 필드 구성
        // - Vector3      Position: 위치
        // - Quaternion       Rotation: 회전(량)
        // - Vector3        Scale: 크기
        // PS : 3D에 들어가기 전까지는 당분간 Rotation 은
        //      Vector3 형태의 Euler로 다룸

        public float moveSpeed = 1.0f;
        public float rotationSpeed = 60.0f;
        public float scaleSpeed = 1.0f;

        private void Update()
        {
            TransferPosition();
            TransferRotation();
            TransferScale();
        }
        
        //자주사용하게될 Vector3의 필드들
        
        //Vector3.up = (0, 1, 0);
        //Vector3.down = (0, -1, 0);
        //Vector3.right = (1, 0, 0);
        //Vector3.left = (-1, 0, 0);
        //Vector3.forward = (0, 0, 1);
        //Vector3.back = (0, 0, -1);
        //Vector3.zero = (0, 0, 0);
        //Vector3.one = (1, 1, 1);

        private void TransferPosition()
        {
            // 1초당 moveSpeed 만큼 y 축 방향으로 이동
            if(Keyboard.current.wKey.isPressed)
            {
                // transform : 소문자로 구성된 transform 단어를 가져오면
                // 멤버변수인 transform 객체에 접근할 수 있다

                // Translate(Vector3 자료형 매개변수) 함수는 매개변수로 전달된
                // Vector3(x, y, z) 의 값만큼 GameObject 를 이동시킨다
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
            else if(Keyboard.current.sKey.isPressed)
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            }
            else if (Keyboard.current.aKey.isPressed)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }

        private void TransferRotation()
        {
            // 보통 2D의 회전은 Z축을 기반으로 하는 것이 자연스럽게 나옴

            if (Keyboard.current.qKey.isPressed)
            {
                // Rotate(Vector3 자료형 매개변수) 함수는 해당 GameObject를
                // 회전시키는 함수
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            }
            else if (Keyboard.current.eKey.isPressed)
            {
                transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            }
        }

        private void TransferScale()
        {
            if (Keyboard.current.zKey.isPressed)
            {
                // Scale은 0이 될 경우 아무것도 보이지 않음. 따라서 Vector3.one 을 씀

                // 스케일 변화량
                float deltaScale = scaleSpeed * Time.deltaTime;
                transform.localScale += Vector3.one * deltaScale;
            }
            else if (Keyboard.current.xKey.isPressed)
            {
                float deltaScale = scaleSpeed * Time.deltaTime;
                transform.localScale -= Vector3.one * deltaScale;
                // localScale 값이 음수가 되는 때에 위아래가 뒤집힘
            }
        }
    }
}