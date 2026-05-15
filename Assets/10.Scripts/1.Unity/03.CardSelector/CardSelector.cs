using UnityEngine;
using UnityEngine.InputSystem;

namespace Study.CardSelector
{
    public class CardSelector : MonoBehaviour
    {
        public Card[] cards;
        public UnityEngine.Transform cursor;
        public int currentIndex = 2;
        void Update()
        {
            if(Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                // 왼쪽키를 누를 경우
                // 인덱스를 차감하고
                // 해당되는 카드의 x 위치값을 가져와서 
                // cursor의 x 값에 대입해준다
                MoveCursor(true);
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                MoveCursor(false);
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                SelectCard();
            }

            // 카드는 최소 스무장 (4x5 사이즈)
            // 두개 선택햇다면
            // 카드가 동일한지 체크하고, 선택된 카드배열 제거
            // 카드가 제거되면 인덱스 어떻게 해야할지
        }

        private void DeleteCard(Card a, Card b)
        {
            Destroy(a.gameObject);
            Destroy(b.gameObject);
        }

        private void SelectCard()
        {
            cards[currentIndex].Flip();
        }

        private void MoveCursor(bool isLeft)
        {
            currentIndex += isLeft ? -1 : +1;

            // Case 1 : 범위 밖으로 움직이지 못하게
            //if (currentIndex < 0) currentIndex = 0;
            //if (currentIndex >= cards.Length) currentIndex = cards.Length - 1;

            // Case 1-1
            // Clamp : 범위를 만족하는 값. 고정된 범위 내에서만 움직임
            // Clamp(변환할 값, 최소값, 최대값)
            // : 변환할 값을 비교하여 최소값과 최대값 사이를 만족하는 값을 반환함
            // 변환할 값이 최소값보다 작다면 => 최소값이 반환됨
            // 변환할 값이 최대값보다 크다면 => 최대값이 반환됨
            //currentIndex = Mathf.Clamp(currentIndex, 0, cards.Length - 1);

            // Case 2 : 최대값 보다 클경우 최소값으로,
            // 최소값보다 작을경우 최대값으로
            if (currentIndex < 0) currentIndex = cards.Length - 1;
            if (currentIndex >= cards.Length) currentIndex = 0;


            float cardX = cards[currentIndex].transform.position.x;
            cursor.position = new Vector3(cardX, cursor.position.y);
        }
    }
}
