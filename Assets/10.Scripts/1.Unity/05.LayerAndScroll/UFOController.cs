using System.Collections;
using UnityEngine;


public class UFOController : MonoBehaviour
{
    public int CoinCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 속도 * 프레임당 이동량을 구해준다
        //moveVector = moveVector * Speed * Time.deltaTime;
    }

    // 충돌중일때는 충돌되지 않게 하기 위한 조건 검사용 변수
    private bool isColliding = false;

    // OnTriggerEnter2D 이벤트 함수는
    // Rigidbody 를 가진 GameObject 가 Collider(IsTrigger 가 True) 개체와
    // 접촉했을때 호출됨
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // CompareTag() || GameObject.CompareTag()
        // - 매개변수로 전달된 값(문자열, 태그)과 지정된 gameObject 가 가지고 있는 Tag
        // 를 비교하여 True/False bool 타입으로 반환함
        // - 전달된 값과 동일하면 True, 아니라면 False 반환
        if(collider.gameObject.CompareTag("Coin"))
        {
            Destroy(collider.gameObject);
            CoinCount += 1;
        }
        else if ((isColliding == false) && (collider.gameObject.CompareTag("Obstacle")))
        {
            // 지형과의 충돌 로직을 작성
            isColliding = true;
            StartCoroutine(CollisionEffect()); // Coroutine 함수를 호출하는 방법
        }
    }

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 게임오브젝트에 부착된 SpriteRenderer 컴포넌트를 가져옵니다
    }

    // # Coroutine?
    // 시간의 흐름에 따라 순차적으로 진행되는 로직을 Update함수 없이 깔끔하게 작성
    // 하게 해주는 유니티의 강력한 기능
    // - 함수의 실행을 잠시 멈추고, 특정 조건 후 멈춘 지점부터 다시 시작할 수 있는 함수
    // - Coroutine 으로 선언된 함수는 그냥 실행할 수 없으며, StartCoroutine() 이라는 함수를 
    //   통해서만 실행할 수 있다
    private IEnumerator CollisionEffect()
    {
        Debug.LogWarning("충돌 시작!");

        Color origin = spriteRenderer.color;
        Color effect = spriteRenderer.color;
        effect.a = 0.2f; // 투명도를 20%로 낮춰줌

        float term = 0.15f;


        for (int i = 0; i < 4; i ++)
        {
            spriteRenderer.color = effect;
            yield return new WaitForSeconds(term);
            spriteRenderer.color = origin;
            yield return new WaitForSeconds(term);
        }
        //yield return new WaitForSeconds(2.0f); // yield 양보하다 미루다
        Debug.LogWarning("충돌 종료!");
        isColliding = false;
    }
}
