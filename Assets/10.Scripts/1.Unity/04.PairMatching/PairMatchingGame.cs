using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Study.PairMatchingGame
{
    public class PairMatchingGame : MonoBehaviour
    {
        [Header("Ref Object")]
        // Card Selector에 의해 선택된 두 카드를 비교합니다
        // 같으면 지우고, 다르면 뒤집습니다
        public CardSelector CardSelector;
        public Transform[] rowRoots;
        // CardSelector가 갖고 있던 Card[]을 PairMatchingGame 객체가 관리하도록
        // 코드를 수정
        private Card[,] board; // 게임보드
        public GameObject ClearTextObject;

        private bool isChecking;

        private void Awake()
        {
            Card.Number[] numbers = new Card.Number[20];
            int pointer = 0;
            foreach(Card.Number number in System.Enum.GetValues(typeof(Card.Number)))
            {
                numbers[pointer++] = number;
                numbers[pointer++] = number;
            }
            
            for (int i = numbers.Length - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);
                Card.Number temp = numbers[i];
                numbers[i] = numbers[randomIndex];
                numbers[randomIndex] = temp;
            }

            board = new Card[4, 5];
            for (int row = 0; row < rowRoots.Length; row++)
            {
                for (int col = 0; col < rowRoots[row].childCount; col++)
                {
                    Card card = rowRoots[row].GetChild(col).GetComponent<Card>();
                    board[row, col] = card;
                }
            }

            int pointer2 = 0;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col].myNumber = numbers[pointer2++];
                }
            }

            CardSelector.SetBoard(board);
            ClearTextObject.SetActive(false);
        }

        // 모든 게임오브젝트의 Update()가 실행되고 나서 실행되는 LateUpdate()에서 트리거 값을
        // 조회해야 안전하게 작업을 할수 있다
        private void LateUpdate() 
        {
            if (CardSelector.wasSelectionCompleted && !isChecking)
            {
                StartCoroutine(CheckPairRoutine());
            }
        }

        private IEnumerator CheckPairRoutine()
        {
            CardSelector.canInput = false;
            isChecking = true;
            yield return new WaitForSeconds(0.7f);

            Card[] selectedCards = CardSelector.GetSelectedCards();
            Debug.Log($"selectedCards[0]: {selectedCards[0]}, selectedCards[1]: {selectedCards[1]}");
            bool isPair = CheckPair(selectedCards[0], selectedCards[1]);

            if (isPair)
            {
                DeleteCard(selectedCards[0]);
                DeleteCard(selectedCards[1]);

                pairMatchingCount += 2;
                CheckGameEnd();
            }
            else
            {
                FlipCards(selectedCards[0], selectedCards[1]);
            }

            CardSelector.Clear();
            isChecking = false;
            CardSelector.canInput = true;
        }

        private bool CheckPair(Card a, Card b)
        {
            return a.myNumber == b.myNumber;
        }

        /// <summary>
        /// a와 b 가 동일한 카드(숫자가 같을경우)일 경우 호출하세요
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private void DeleteCards(Card a, Card b)
        {
            Destroy(a.gameObject); // a 컴포넌트가 부착된 게임오브젝트를 제거합니다
            Destroy(b.gameObject);

            Debug.Log("두 카드가 같습니다");
        }

        private void FlipCards(Card a, Card b) {
            a.Flip();
            b.Flip();
            Debug.Log("두 카드가 다릅니다");
        }

        private void DeleteCard(Card target)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == null) continue;

                    if (board[i, j].Equals(target)) // Equals(매개변수) 함수는 "==" 과 동일하다
                    {
                        board[i, j] = null; // null 할당을 해주지 않으면 쓰레기값을 가리킨다
                        // 이제 해당 오브젝트가 삭제되면 Missing 이 아닌 None 이 뜨게 바뀐다

                        Destroy(target.gameObject); // Scene에서 삭제
                    }
                }
            }
        }

        // 게임이 끝났는지 여부를 검사하는 함수
        private int pairMatchingCount = 0; // pairMatching 이 성공한 순간에 +2 되는 변수
        

        private bool CheckGameEnd()
        {
            bool isEnd = pairMatchingCount == (board.GetLength(0) * board.GetLength(1));

            // ~.SetActive(bool 매개변수)
            // 해당 게임오브젝트의 활성화/비활성화를 제어하는 함수
            // gameObject.SetActive(true) : 활성화
            // gameObject.SetActive(false): 비활성화
            ClearTextObject.SetActive(isEnd);
            return isEnd;
        }
    }
}
