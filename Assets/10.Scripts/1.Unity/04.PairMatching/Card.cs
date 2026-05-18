using UnityEngine;

namespace Study.PairMatchingGame
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

        void Awake()
        {
            cardRenderer = gameObject.GetComponent<SpriteRenderer>();
            SetState(State.Back);
        }

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
        public void Flip()
        {
            if (myState == State.Back) SetState(State.Front);
            else SetState(State.Back);
        }
    }
}