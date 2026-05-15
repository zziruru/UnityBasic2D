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
        // CardSelector가 갖고 있던 Card[]을 PairMatchingGame 객체가 관리하도록
        // 코드를 수정
        public Card[] board; // 게임보드
        public GameObject ClearTextObject;

        private void Awake()
        {
            int[] indexBuffer = new int[board.Length];
            for(int i = 0; i< indexBuffer.Length; i++)
            {
                // board 의 인덱스를 넣어준다.
                indexBuffer[i] = i;
            }

            for (int i = indexBuffer.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                int temp = indexBuffer[i];
                indexBuffer[i] = indexBuffer[j];
                indexBuffer[j] = temp;
            }

            // 계속 변화되는 배열에서 랜덤하게 가져오는거 없나
            // (카드 갯수 / 2 )개 종류를 가져와서 
            for (int i = 0; i < indexBuffer.Length; i++)
            {
                //board[indexBuffer[i]] =
            }

            //for (int i = 0; i < board.Length; i+=2)
            //{
            // Unity에서 랜덤을 사용하는 방법
            // .Net의 랜덤을 사용해도 되긴 하다만, 더 편한 방법이 있음
            //int randNum = Random.Range((int)Card.Number.Two, (int)Card.Number.Ace + 1);
            // UnityEngine 의 Random을 사용해야함
            // Random.Range(최소값, 최대값) : 최소값 ~ 최대값 범위 중 임의의 수를 반환함
            // 주의! 최대값은 해당 범위에 포함되지 않음


            //    board[i].myNumber = (Card.Number)randNum;
            //    board[i + 1].myNumber = (Card.Number)randNum;
            //}
            // 셔플은 게임 오브젝트 연결해놓은게 다 섞이니까 커서 위치 이동이 이상해짐
            // 카드를 섞으면 안되고 데이터를 섞어야함
            // TODO: 차라리 Card.Number를 순서대로 돌고, 배당할 카드 인덱스를 랜덤하게 뽑기?
            // 뽑았으면 제거도 해야함
            // 셔플 돈 indexBuffer 를 이용해서 해도 됨

            CardSelector.SetBoard(board);
            ClearTextObject.SetActive(false);
        }

        // 모든 게임오브젝트의 Update()가 실행되고 나서 실행되는 LateUpdate()에서 트리거 값을
        // 조회해야 안전하게 작업을 할수 있다
        private void LateUpdate() 
        {
            if (CardSelector.wasSelectionCompleted)
            {
                Card[] selectedCards = CardSelector.GetSelectedCards();
                bool isPair = CheckPair(selectedCards[0], selectedCards[1]);
                if (isPair)
                {
                    DeleteCard(selectedCards[0]);
                    DeleteCard(selectedCards[1]);

                    pairMatchingCount += 2;
                    CheckGameEnd();
                }
                else
                    FlipCards(selectedCards[0], selectedCards[1]);

                CardSelector.Clear();
            }
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

        // 객체를 안전하게 삭제하는 기능을 넣자
        private void DeleteCard(Card target)
        {
            // 선형탐색을 이용해서 target의 위치를 찾는다
            for (int i = 0; i<board.Length; i++)
            {
                if (board[i] == null) continue;

                if (board[i].Equals(target)) // Equals(매개변수) 함수는 "==" 과 동일하다
                {
                    board[i] = null; // null 할당을 해주지 않으면 쓰레기값을 가리킨다
                    // 이제 해당 오브젝트가 삭제되면 Missing 이 아닌 None 이 뜨게 바뀐다

                    Destroy(target.gameObject); // Scene에서 삭제
                }
            }
        }

        // 게임이 끝났는지 여부를 검사하는 함수
        private int pairMatchingCount = 0; // pairMatching 이 성공한 순간에 +2 되는 변수
        

        private bool CheckGameEnd()
        {
            bool isEnd = pairMatchingCount == board.Length;

            // ~.SetActive(bool 매개변수)
            // 해당 게임오브젝트의 활성화/비활성화를 제어하는 함수
            // gameObject.SetActive(true) : 활성화
            // gameObject.SetActive(false): 비활성화
            ClearTextObject.SetActive(isEnd);
            return isEnd;
        }
    }
}
