using UnityEngine;
using System.Collections.Generic;

namespace Study_DataStructure
{
    // # Queue<T> 는 "줄 서 있는 사람들" => 대기열
    // 1. 선입선출 ( FIFO - First In, First Out ) : 가장 먼저 줄을 선 사람이 가장 먼저
    // 서비스를 받는 구조, 새로운 데이터는 줄의 맨 뒤에 추가되고, 데이터를 꺼낼때는 줄의 맨 앞에서 꺼냅니다
    // 2. 한 종류만 담을 수 있다. "학생들 줄", "주문 대기열" 처럼 한 종류의 데이터만 
    // 담을 수 있다
    public class Study_Queue : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Queue();
        }

        private void Queue()
        {
            // 1. Queue 의 생성 : 어떤 종류의 데이터를 담을지 알려주기
            // string(문자열)만 담을 수 있는 Queue를 만들어보자
            // 이름은 "waitingLine"
            Queue<string> waitingLine; // Queue의 선언
            waitingLine = new Queue<string>();

            Debug.Log("1. Queue 생성 직후");
            Debug.Log($"대기열의 사람 수 : {waitingLine}");

            // 2. 데이터 추가 : .Enqueue()
            waitingLine.Enqueue("앨리스");

            // 3. 데이터 확인 : .Peek()
            // 데이터를 꺼내지 않고, 줄의 맨 앞에 어떤 데이터가 있는지
            // 살짝 확인한다
            Debug.Log("3. 줄 맨 앞의 사람 확인하기");
            Debug.Log($"줄 맨 앞의 사람 확인하기 {waitingLine.Peek()}");
            Debug.Log($"대기열의 사람 수 : {waitingLine.Count}");

            // 4. 데이터 꺼내기 : .Dequeue()
            // 줄의 맨 앞 데이터를 꺼낸다. 꺼낸 데이터는 Queue에서 사라짐
            Debug.Log("4. 대기열에서 데이터 꺼내기");
            string servedPerson = waitingLine.Dequeue();

            // 큐에 더 꺼낼 것이 없을때는? 
            waitingLine.Dequeue();

            // 5. 모든 데이터를 꺼내지 않고 확인해야할 경우에는?
            foreach (string person in waitingLine)
            {

            }

            // 정리 : Queue<T> 언제 사용할까?
            // 1. 작업 처리 순서가 중요한 경우
            // - 프린터의 인쇄 대기열 : 먼저 인쇄 요청을 보낸 문서가 먼저 인쇄된다
            // - 운영체제의 작업 스케쥴링 : CPU가 처리해야할 작업들이 Queue에 쌓여 순서대로 처리됨
            // - 온라인 게임의 접속 대기열 : 먼저 접속을 대기한 유저부터 게임에 입장함

            // 2. 버퍼링 (buffering)
            // - 데이터를 일시적으로 저장하고 순서대로 처리해야 할 때 사용됨
            //   예를 들어, 네트워크에서 데이터를 수신할 때

            // 3. 너비 우선 탐색 (BFS- Breath-First-Search)
            // - 그래프나 트리를 탐색할 때, 현재 노드와 연결된 모든 노드를 먼저 방문하고 다음 깊이로 
            // 넘어가는 BFS 알고리즘 에서 사용함
        }   
    }
}

