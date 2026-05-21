using UnityEngine;
using UnityEngine.InputSystem;

namespace Study.PrimitiveAndVector
{
    // ! 주의사항 : 현재는 2D 프로젝트를 진행하지만, 가르칠때는 3D를 가르침
    // 2D Vector 는 3D Vector 의 하위개념이라 Vector3만 사용하면 2D도 자동적으로
    // 사용 가능함

    // # Vector
    // 3차원 공간에서 위치, 방향(or 크기)를 나타내는데 사용되는 구조체(Struct)
    // 마치 지도에서 특정 지점의 좌표를 표시하거나, 어떤 방향으로 어떤 속도만큼
    // 이동하는지를 표현하는데에 사용된다
    // Vector 는 게임엔진의 모든 3D 계산의 기본이 되며, GameObject 의 위치, 이동방향
    // 속도, 힘, 작용 반작용 등등을 표현하는데에 필수적으로 사용된다

    public class Study_Vector : MonoBehaviour
    {
        // # 3D 그래픽스 (게임) 엔진에서 자주 사용하는 Vector3 관련 정적 필드와 메서드
        // Vector3.zero : (0,0,0)을 담아둔 변수(원점, 크기가 없는 상태)
        // Vector3.one : (1,1,1)을 담아둔 변수(유니티 게임오브젝트의 기본 Scale 값)

        // # 방향
        // Vector3.forward : (0,0,1)을 나타냄 (z축의 양의 방향)
        // Vector3.back : (0,0,-1)을 나타냄 (z축의 음의 방향)
        // Vector3.up : (0,1,0) 을 나타냄 (y축의 양의 방향)
        // Vector3.down : (0, -1, 0) 을 나타냄 ( y축의 음의 방향)
        // Vector3.right : (1, 0, 0) 을 나타냄 (x축의 양의 방향)
        // Vector3.left : (-1, 0, 0) 을 나타냄 (x축의 음의 방향)

        // # 길이 (vector 이 소문자인 이유는 해당 객체를 통해 호출하라는거)
        // vector.magnitude : 벡터의 길이(크기)를 반환함 (원점으로부터 얼마나 떨어졌는지)
        // (원점으로부터 얼마나 떨어졌는지)
        // vector.sqrMagnitude : 벡터의 길이의 제곱을 반환함
        //  magnitude 보다 연산비용이 적어서 단순 비교 시 사용함
        // vector.normalized : 길이가 1인 단위 벡터 (Unit Vector)를 반환함
        //  방향을 나타낼 때 사용

        private void Start()
        {
        }

        private void Update()
        {
            Vector();
        }

        public void Vector()
        {
            // # Vector 의 합연산
            // - Vector 끼리의 합연산은 가능
            //      - 의미 : 방향을 변환시키고자 할 때
            // - 단일 값과의 합연산은 불가능하다


            // # Vector 의 곱연산
            // - Vector 끼리의 곱연산 나중에 배움( 스칼라, 내적, 외적 등)ㅈ
            //      - 의미 : 길이를 변환시키고자 할 때
            // - 단일 값과의 곱연산은 가능

            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                // transform 은 모든 게임 오브젝트에 존재함
                transform.position += Vector3.one;
            }
            if (Keyboard.current.wKey.wasPressedThisFrame)
            {
                Vector3 angle = new Vector3(0, 0, 30);
                transform.rotation *= Quaternion.Euler(angle);
            }
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                transform.localScale += Vector3.one;
            }
        }
    }
}