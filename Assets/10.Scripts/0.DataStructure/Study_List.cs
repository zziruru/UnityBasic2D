using System;
using System.Collections.Generic;
using UnityEngine;

namespace Study_DataStructure
{
    public class Study_List : MonoBehaviour
    {    
        private void Start()
        {
            List();
        }

        // List<T>는 타입이 정해진 마법의 쇼핑백
        // 1. 크기는 자유자재 : 처음에는 비어있지만, 물건(데이터)을 넣을수록 쇼핑백이 알아서 늘어남
        // 반대로 물건을 빼면 크기가 줄어듦. 배열(Array)처럼 크기를 미리 정해둘 필요가 없음
        // 2. 한 종류만 담는 규칙 : 이 쇼핑백은 특별해서, "과일만 담는 쇼핑백", "장난감"만 
        // 담은 쇼핑백처럼 한 종류의 물건만 담기로 약속해야합니다. 만약, 숫자를 
        // 담는 List를 만들었다면 문자열이나 다른 타입의 데이터는 넣을 수 없습니다.
        // 


        void List()
        {
            List<string> fruitBasket = new List<string>();

            Debug.Log($"쇼핑백 안의 과일 개수 : {fruitBasket.Count}");

            fruitBasket.Add("사과");
            fruitBasket.Add("딸기");
            fruitBasket.Add("바나나");

            Debug.Log($"쇼핑백 안의 과일 개수 : {fruitBasket.Count}");

            Debug.Log("과일 꺼내보기");
            Debug.Log($"{fruitBasket[1]}");

            for(int i = 0; i< fruitBasket.Count; i++)
            {
                Debug.Log($"fruitBasket[{i}] = {fruitBasket[i]}");
            }

            // Remove(), RemoveAt()
            fruitBasket.Remove("바나나"); // for 문을 돌면서 매개변수와 같은걸 찾아서 삭제함
            fruitBasket.RemoveAt(0); // 0번 값을 삭제

            fruitBasket.Add("파인애플");
            fruitBasket.Add("배");

            fruitBasket.Insert(1, "블루베리"); // 지정한 인덱스에 새로운 값 추가

            // 리스트 안에 해당 데이터가 잇는지
            bool hasBlueberry = fruitBasket.Contains("블루베리"); // 선형탐색으로 구현되어있음. for문 도는 방식
            if (hasBlueberry)
            {

            }
        }
    }

}

