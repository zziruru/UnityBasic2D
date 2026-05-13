using UnityEngine;

namespace Study.Mono
{
    // # LifeCycle
    //
    // [오브젝트 생성]
    //    ↓
    // Awake()      (딱 한 번)
    //    ↓
    // OnEnable()    (활성화될 때마다)
    //    ↓
    // Start()      (딱 한 번) 
    //    ↓
    // ┌───────── ┐
    // │  (게임 루프 시작) │
    // │      ↓           │
    // │ FixedUpdate()     │   (물리 업데이트)
    // │      ↓           │
    // │   Update()        │   (프레임 업데이트)
    // │      ↓           │
    // │ LateUpdate()      │   (모든 Update 후)
    // │      ↓           │
    // └───────── ┘  (게임이 실행되는 동안 반복)
    //    ↓
    // OnDisable()    (비활성화될 때마다)
    //    ↓
    // OnDestroy()    (오브젝트 파괴 시)

    // MonoBehaviour ? 
    // 유니티에서 생성하는 모든 스크립트가 기본적으로 상속받는 베이스 클래스
    // 게임 오브젝트에 컴포넌트 형태로 부착될 수 있게 해주며, 
    // 다양한 생명주기(LifeCycle) 이벤트 메시지를 수신할 수 있는 권한을 부여함

    // 한가지의 행위를 정의하는데 쓰십쇼
    // 프로그래머가 정의한 하나의 Component
    public class Study_MonoBehaviour: MonoBehaviour
    {
        // 스크립트 객체(인스턴스) 로드 시 1회 호출됨
        // 메모리에 할당될 때 호출된다고 생각하면 됨
        // 객체(컴포넌트)가 비활성 상태여도 호출됨
        private void Awake()
        {
            Debug.Log("Awake() 가 호출됨");
        }

        // 첫 Update(=첫 프레임) 직전 1회 호출됨
        // 비활성화 상태일때는 호출되지 않음
        // Scene(장면)의 다른 객체상태를 참고하여
        // 본인 초기화 로직에 사용하려고 할 때 사용함
        private void Start()
        {
            Debug.Log("Start() 가 호출됨");
        }

        // Update()를 어떻게 활용하는지
        // public 접근제한자를 사용하면 인스펙터에 노출됨
        public float speed = 2.0f;
        public float countDown = 5;
        public float waitTime = 0.0f;

        public GameObject Triangle;

        // 매 프레임마다 호출되는 이벤트 함수
        // 프레임 속도(결국 사양)에 따라 호출빈도와 주기가 다름
        // Scene 에 활성화될때만(등장할때만) 호출이 됨
        // GameObject 와 컴포넌트가 모두 활성화일 때 호출됨
        private void Update()
        {
            Debug.Log("Update()");

            // Scene에 있는 Triangle 이 지정된 시간이 지나면
            // 위로 상승하는 간단한 로직 구현해보기
            // 

            waitTime += Time.deltaTime;
            if(waitTime > countDown)
            {
                Triangle.transform.position += (Vector3.up * speed * Time.deltaTime);
            }
        }

        // 고정된 시간 간격마다 호출됨
        // 
        private void FixedUpdate()
        {
            Debug.Log("FixedUpdate()가 호출되었습니다!");
        }

        // 모든 Mono의 Update가 종료된 후에 호출됨
        // 
        private void LateUpdate()
        {
            Debug.Log("LateUpdate()가 호출되었습니다!");
        }

        // 객체가 활성화 상태로 전환될 때마다 호출됨
        private void OnEnable()
        {
            Debug.Log("onEnable()가 호출되었습니다!");
        }

        // 객체가 비활성화 상태로 전환될 때마다 호출됨 (전환될때 딱 한번만)
        private void OnDisable()
        {
            Debug.Log("onDisable()가 호출되었습니다!");
        }
        private void OnDestroy()
        {
            Debug.Log("OnDestroy()가 호출되었습니다!");
        }
    }
}