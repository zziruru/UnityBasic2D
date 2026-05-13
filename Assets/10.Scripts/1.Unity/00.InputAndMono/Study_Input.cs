using System;
using UnityEngine;
using UnityEngine.InputSystem;

// 클래스명 중복방지를 위해 namespace를 사용
namespace Study.Input
{
    public class Study_Input : MonoBehaviour
    {
        // 매 프레임마다 한번씩 실행시켜줌
        private void Update()
        {
            SpaceInputTest();
            ArrowInputTest();

            // 이전 방식
            //if(UnityEngine.Input.GetKeyDown(KeyCode.V))
            //{

            //}
        }

        private void SpaceInputTest()
        {
            // Keyboard 에Space Input 받아보기
            //bool isPressed = Keyboard.current.spaceKey.isPressed;
            //Debug.Log($"isPressed = {isPressed}");

            // 키보드의 입력이 시작되었을때 True반환
            if (Keyboard.current.spaceKey.wasPressedThisFrame) // 상태전환 체크
            {
                // 안눌린 상태에서 눌린상태로 바뀌는 첫프레임
                Debug.Log("스페이스 입력이 시작되었습니다!");
            }
            else if (Keyboard.current.spaceKey.isPressed) // 조회만함
            {
                // Keyboard의 Spacekey가 눌려있다면
                //Debug.Log("스페이스키가 눌려있습니다!");
            }
            // 키보드 입력이 종료되었을때 True 반환
            else if (Keyboard.current.spaceKey.wasReleasedThisFrame) // 상태전환 체크
            {
                // 눌린상태에서 안눌린 상태로 바뀌는 첫 프레임
                Debug.Log("스페이스 입력이 종료되었습니다!");
            }
            
            // wasReleasedThisFrame, wasPressedThisFrame 순서바꼈을때 왜 안찍히는건지 
        }

        // 유니티에서는 public 접근제한자를 이용해서 다른 객체에 대한 의존성을 Inspector에서
        // 부여할 수 있다. (의존성주입DI 가 아님!)
        public GameObject Target;

        private void ArrowInputTest()
        {
            // 왼쪽 이동
            Target.transform.position += new Vector3(-1, 0, 0);
            // 오른쪽 이동
            Target.transform.position += new Vector3(1, 0, 0);
            // 위쪽 이동
            Target.transform.position += new Vector3(0, 1, 0);
            // 아래쪽 이동
            Target.transform.position += new Vector3(0, -1, 0);

            // # 실습
            // 키보드의 화살표를 이용해서 Target 게임 오브젝트의 위치를 변경되는 코드 만들기

            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                Target.transform.position += new Vector3(0, -1, 0);
            } 
            else if(Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                Target.transform.position += new Vector3(0, 1, 0);
            }
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                Target.transform.position += new Vector3(-1, 0, 0);
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                Target.transform.position += new Vector3(1, 0, 0);
            }
        }
    }
}
