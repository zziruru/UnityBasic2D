using UnityEngine;
using UnityEngine.InputSystem;

namespace Study.CardSelector
{
    public class Card : MonoBehaviour
    {
        public enum Number
        {
           Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Ace
        }

        public enum State
        {
            Back,
            Front
        }

        [Header("공용데이터")]
        public Sprite backImage;
        public Sprite[] numberImages;

        [Header("My Data")]
        public SpriteRenderer cardRenderer;
        public Number myNumber;
        public State myState;

        #region Unity Event Method
        void Awake()
        {
            cardRenderer = gameObject.GetComponent<SpriteRenderer>();
            // gameObject 멤버변수는 컴포넌트 객체가 부착된 GameObject에 접근할 수 있다

            // GetComponent<가져올타입>();
            // 부착된 게임오브젝트에서 {가져올타입}의 객체를 가져옴
            // 만약 부착되어있지 않다면 null 을 반환함
            SetState(State.Back);
        }

        void Start()
        {
        }
        #endregion

        #region Method
        /// <summary>
        /// 카드의 출력 상태를 설정한다
        /// </summary>
        public void SetState(State state)
        {
            // 나의 현재 상태(myState)를 갱신한다
            // cardRenderer의 이미지를 알맞게 바꿔준다

            if (state == State.Back)
            {
                cardRenderer.sprite = backImage;
            }
            else
            {
                cardRenderer.sprite = numberImages[(int)myNumber];
            }

            myState = state;
        }

        /// <summary>
        /// 카드의 상태를 반전합니다. Back <=> Front
        /// </summary>
        public void Flip()
        {
            if (myState == State.Back) SetState(State.Front);
            else SetState(State.Back);
        }
        #endregion
    }
}
