using System.Collections.Generic;
using UnityEngine;

namespace Study.LayerAndScroll
{
    // Layer 의 속도를 조절해 볼 겁니다
    public class ScrollController : MonoBehaviour
    {
        public enum ScrollDirection { Left, Right, Up, Down }

        [Header("Scroll Settings")]
        public float speed = 1.0f;
        public ScrollDirection direction = ScrollDirection.Left;
        public Vector3 moveDirection;

        [Header("Resources")]
        public GameObject[] layerPrefabs;

        [Header("Ref Objects")]
        public GameObject startLayer;
        public Transform endPivot;
        public Transform spawnPivot;

        private List<GameObject> enableLayerList = new List<GameObject>();

        void Start()
        {
            if (layerPrefabs.Length == 0)
            {
                Debug.LogError($"{gameObject.name}의 layerPrefebs.Length 가 0 입니다");
                return;
            }
            enableLayerList.Add(startLayer); // enableLayerList = {startLayer};
        }

        private void MoveLayerList()
        {
            // (speed * Time.deltaTime) 초당 speed의 속도로 뭔가를 하겠다는 표현
            Vector3 dir = GetMoveDirection(direction);
            Vector3 moveVector = dir * (speed * Time.deltaTime);

            // 활성화된 모든 레이어를 moveVector만큼 옮겨준다
            for (int i = 0; i < enableLayerList.Count; i++) 
            {
                enableLayerList[i].transform.Translate(moveVector);
            }
        }

        private void CheckDestroyableLayer()
        {
            // 2. 가장 첫번째 Layer(enableLayerList[0])가 
            // EndPivot의 경계를 넘어간다면 (x 값보다 작아진다면)
            // 삭제한다

            GameObject headLayer = enableLayerList[0];
            // 가장 앞에 있는 Layer 오브젝트를 가져온다

            bool check = false;

            switch(direction)
            {
                case ScrollDirection.Left:
                    // headLayer의 x 가 endPivot의 x 보다 작다면
                    check = headLayer.transform.position.x <= endPivot.position.x;
                    break;
                case ScrollDirection.Right:
                    // headLayer의 x 가 endPivot의 x 보다 크다면
                    check = headLayer.transform.position.x >= endPivot.position.x;
                    break;
                case ScrollDirection.Up:
                    // headLayer의 y 가 endPivot의 y 보다 크다면
                    check = headLayer.transform.position.y <= endPivot.position.y;
                    break;
                case ScrollDirection.Down:
                    // headLayer의 y 가 endPivot의 y 보다 작다면
                    check = headLayer.transform.position.y <= endPivot.position.y;
                    break;
            }

            if(check)
            {
                enableLayerList.RemoveAt(0);
                Destroy(headLayer);
            }


            if (headLayer.transform.position.x <= endPivot.position.x)
            {
                // 안전하게 지우기
                enableLayerList.RemoveAt(0);
                Destroy(headLayer);
            }
        }

        private void CheckInstantiateLayer()
        {
            // 3. Layer 생성. 현재 필요한 LayerObject 2~3개임.
            // GameObject.Instantiate(GameObject object) || .Instantiate(GameObject object)
            // .Instantiate() : 실행중(런타임)에 게임오브젝트를 생성하는 함수
            // 생성한 객체는 생성할 객체의 타입으로 반환받을 수 있다

            while (enableLayerList.Count <= 2)
            {
                GameObject instance = Instantiate(layerPrefabs[0], // layerPrefabs[0] 개체의 사본을 전달
                    spawnPivot.transform.position, spawnPivot.rotation);
                // spawnPivot 의 위치, spawnPivot 의 회전값
                enableLayerList.Add(instance);
            }
        }

        private void Update()
        {
            MoveLayerList(); 
            CheckDestroyableLayer();
            CheckInstantiateLayer();
        }

        private Vector3 GetMoveDirection(ScrollDirection dir)
        {
            switch(dir)
            {
                case ScrollDirection.Left:
                    return Vector3.left;
                case ScrollDirection.Right:
                    return Vector3.right;
                case ScrollDirection.Up:
                    return Vector3.up;
                case ScrollDirection.Down:
                    return Vector3.down;
                default:
                    return Vector3.left;
            }
        }
    }
}

