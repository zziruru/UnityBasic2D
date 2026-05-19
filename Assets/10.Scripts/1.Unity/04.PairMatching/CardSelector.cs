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
                FindNearestCard(Direction.Left);
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                FindNearestCard(Direction.Right);
            }
            else if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                FindNearestCard(Direction.Up);
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                FindNearestCard(Direction.Down);
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

        private void FindNearestCard(Direction direction)
        {
            Card currentCard = cards[currentRowIndex, currentColumnIndex];

            Card nearestCard = null;
            int nearestRow = currentRowIndex;
            int nearestColumn = currentColumnIndex;
            float nearestDistance = float.MaxValue;

            Vector3 currentPosition = currentCard.transform.position;

            for (int row = 0; row < cards.GetLength(0); row++)
            {
                for (int col = 0; col < cards.GetLength(1); col++)
                {
                    Card candidate = cards[row, col];

                    if (candidate == null || candidate == currentCard)
                        continue;

                    Vector3 diff = candidate.transform.position - currentPosition;

                    // 가려는 방향과 반대에 있는것은 후보군에서 제외
                    if (direction == Direction.Up && diff.y <= 0) continue;
                    if (direction == Direction.Down && diff.y >= 0) continue;
                    if (direction == Direction.Left && diff.x >= 0) continue;
                    if (direction == Direction.Right && diff.x <= 0) continue;

                    float distance = diff.sqrMagnitude;

                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestCard = candidate;
                        nearestRow = row;
                        nearestColumn = col;
                    }
                }
            }

            if (nearestCard == null)
            {
                return;
            }

            currentRowIndex = nearestRow;
            currentColumnIndex = nearestColumn;

            MoveCursorToCurrentCard();
        }

        private void MoveCursorToCurrentCard()
        {
            Card currentCard = cards[currentRowIndex, currentColumnIndex];

            float cardX = currentCard.transform.position.x;
            float cardY = currentCard.transform.position.y - cursorYOffset;

            cursor.position = new Vector3(cardX, cardY);
        }

        public void MoveCursorToNearestAliveCardFrom(Vector3 basePosition)
        {
            Card nearestCard = null;
            int nearestRow = -1;
            int nearestColumn = -1;
            float nearestDistance = float.MaxValue;

            for (int row = 0; row < cards.GetLength(0); row++)
            {
                for (int col = 0; col < cards.GetLength(1); col++)
                {
                    Card candidate = cards[row, col];

                    if (candidate == null)
                        continue;

                    Vector3 diff = candidate.transform.position - basePosition;
                    float distance = diff.sqrMagnitude;

                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestCard = candidate;
                        nearestRow = row;
                        nearestColumn = col;
                    }
                }
            }

            if (nearestCard == null)
            {
                cursor.gameObject.SetActive(false);
                return;
            }

            currentRowIndex = nearestRow;
            currentColumnIndex = nearestColumn;

            MoveCursorToCurrentCard();
        }
    }
}
