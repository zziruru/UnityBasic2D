using UnityEngine;
using UnityEngine.InputSystem;

namespace Study.PairMatchingGame
{
    public class CardSelector : MonoBehaviour
    {
        public enum Direction
        {
            Up, Down, Left, Right
        }

        private Card[,] cards;// 외부(PairMatchingGame)에서 주입 받습니다
        public Transform cursor;
        public int currentRowIndex = 2;
        public int currentColumnIndex = 2;

        [Header("Settings")]
        public float cursorYOffset = 0.5f;

        private Card selectedCardA;
        private Card selectedCardB;

        // 이번 프레임에 카드 선택이 완료되었는지 체크하는 bool 변수
        public bool wasSelectionCompleted = false; // 트리거 변수

        public bool canInput = true;

        // cards배열에 존재하지 않는 개체를 건너뛰는 기능

        /// <summary>
        /// 0번은 첫번째로 선택한 카드, 1번은 두번째로 선택한 카드
        /// </summary>
        /// <returns></returns>
        public Card[] GetSelectedCards() // Tuple 반환도 괜찮음
        {
            return new[] { selectedCardA, selectedCardB };
        }

        public void SetBoard(Card[,] cards)
        {
            this.cards = cards;
        }

        void Update()
        {
            wasSelectionCompleted = false;
            if (!canInput) return;

            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                MoveCursor(Direction.Left);
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                MoveCursor(Direction.Right);
            }
            else if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                MoveCursor(Direction.Up);
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                MoveCursor(Direction.Down);
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                SelectCard();
            }

            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                Debug.Log($"A: {selectedCardA}, B: {selectedCardB}");
            }
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                Clear();
            }
        }

        private void DeleteCard(Card a, Card b)
        {
            Destroy(a.gameObject);
            Destroy(b.gameObject);
        }

        private void SelectCard()
        {
            Card currentCard = cards[currentRowIndex, currentColumnIndex];

            if (selectedCardA == null) selectedCardA = currentCard;
            else if(selectedCardA == currentCard) return;
            else
            {
                selectedCardB = currentCard;
                wasSelectionCompleted = true;
            }

            cards[currentRowIndex, currentColumnIndex].Flip();
        }

        public void Clear()
        {
            selectedCardA = null;
            selectedCardB = null;
        }

        // cards 배열에 존재하지 않는 개체를 건너뛰어야함
        // currentIndex 를 적용하기 전에 해당 배열이 null 인지 체크
        private void MoveCursorVertically(bool isUp)
        {
            // temp 라는 임시변수에 미리 더해봅시다
            int temp = currentRowIndex;

            for (int i = 0; i < cards.GetLength(0); i++)
            {
                temp += isUp ? -1 : +1;

                if (temp < 0) temp = cards.GetLength(0) - 1;
                if (temp >= cards.GetLength(0)) temp = 0;

                if (cards[temp, currentColumnIndex] == null)
                {
                    continue;
                } else
                {
                    currentRowIndex = temp;
                    float cardX = cards[currentRowIndex, currentColumnIndex].transform.position.x;
                    float cardY = cards[currentRowIndex, currentColumnIndex].transform.position.y - cursorYOffset;
                    cursor.position = new Vector3(cardX, cardY);
                    return;
                }
            }

            currentRowIndex = -1;
        }

        private void MoveCursorHorizontally(bool isLeft)
        {
            int temp = currentColumnIndex;

            for (int i = 0; i < cards.GetLength(1); i++)
            {
                temp += isLeft ? -1 : +1;

                if (temp < 0) temp = cards.GetLength(1) - 1;
                if (temp >= cards.GetLength(1)) temp = 0;

                if (cards[currentRowIndex, temp] == null)
                {
                    continue;
                } else
                {
                    currentColumnIndex = temp;
                    float cardX = cards[currentRowIndex, currentColumnIndex].transform.position.x;
                    float cardY = cards[currentRowIndex, currentColumnIndex].transform.position.y - cursorYOffset;
                    cursor.position = new Vector3(cardX, cardY);
                    return;
                }
            }

            currentColumnIndex = -1;
        }

        // 함수 오버로딩
        // - 같은 이름의 함수더라도, 사용하는 매개변수가 다르다면
        // 중복정의가 가능함
        // (반환자료형-함수이름-매개변수) => 함수의 시그니처
        // 함수의 시그니처가 다르면 다르다고 인식함
        private void MoveCursor(Direction direction)
        {
            switch(direction)
            {
                case Direction.Up:
                    MoveCursorVertically(true);
                    break;

                case Direction.Down:
                    MoveCursorVertically(false);
                    break;

                case Direction.Left:
                    MoveCursorHorizontally(true);
                    break;

                case Direction.Right:
                    MoveCursorHorizontally(false);
                    break;
            }
        }
    }
}
