using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class BlockSpawner : MonoBehaviourPun
{
    [Header("블럭 정보")]
    [SerializeField] List<Scriptable_PinBallBlock> blockDatas = new List<Scriptable_PinBallBlock>();
    [SerializeField] Block BlockPrefab;
    [SerializeField] GameObject[] blockLine;

    Block[,] block;

    [SerializeField] int BlockposYnum = 5;
    [SerializeField] int BlockposXnum = 11;
    GameObject ObjectPool;
    
    Vector3 Pos = Vector3.zero;

    private void Awake()
    {
        block = new Block[BlockposYnum, BlockposXnum];
        ObjectPool = new GameObject("CapyBlockLine");
    }
    void Start()
    {
        CreateBlock();
        if(PhotonNetwork.IsMasterClient)
        {
            CreateBlock();
        }
    }
    #region 블럭생성
    private void CreateBlock()
    {
        
        // -------------------------------------
        //Pos.x = -7.5f; (카메라 비춘 기준)
        Pos.x = -14f;

        // 카메라 비춰지는 위치 (기존)
        //Pos.y = 0.9f;
        // 카메라 비춰지는 위치 (테스트)
        Pos.y = 28.4f; 
        // -------------------------------------

        for (int y = 0; y < BlockposYnum; y++)
        {

            // -------------------------------------
            //Pos.y += 0.9f;
            Pos.y += 1.5f;
            //Pos.x = -7.5f;  // 다시 x축 초기화
            Pos.x = -14f;  // 다시 x축 초기화
            // -------------------------------------

            for (int x = 0; x < BlockposXnum; x++)
            {
                float random = Random.Range(0f, 10f);

                if (random < 2f)
                {
                    
                    block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);

                    block[y, x].blockData = blockDatas[0];
                }
                else if (random < 2.5f)
                {

                    block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    block[y, x].blockData = blockDatas[1];
                }
                else if (random < 3f)
                {

                    block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    block[y, x].blockData = blockDatas[2];
                }
                else if (random < 3.5f)
                {

                    block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    block[y, x].blockData = blockDatas[3];
                }
                else if (random < 4f)
                {

                    block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    block[y, x].blockData = blockDatas[4];
                }
                else if(random < 4.5f)
                {
                    block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    block[y, x].blockData = blockDatas[6];
                }
                //else if(random < 5f)
                //{
                //    block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                //    block[y, x].blockData = blockDatas[7];
                //}
                else
                {

                    block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);   
                    block[y, x].blockData = blockDatas[5];
                }

                block[y, x].transform.parent = blockLine[y].transform;

                // -------------------------------------
                //Pos.x += 1.5f;
                Pos.x += 2.7f;
                // -------------------------------------
            }
        }        
    }
#endregion

    void recycle(int i)
    {

        //블럭이 생성될 위치

        // -------------------------------------
        //Vector2 Pos = new Vector2(-7.5f, 5.4f);  (카메라 비추진 기존 )
        Vector2 Pos = new Vector2(-14f, 35.9f);
        // -------------------------------------

        for (int x = 0; x < BlockposXnum; x++)
        {
            block[4, x] = BackBlock(Pos);

            float random = Random.Range(0f, 10f);

            if (random < 2f)
            {
                block[4, x].blockData = blockDatas[0];
            }
            else if (random < 2.5f)
            {
                block[4, x].blockData = blockDatas[1];
            }
            else if (random < 3f)
            {
                block[4, x].blockData = blockDatas[2];
            }
            else if (random < 3.5f)
            {
                block[4, x].blockData = blockDatas[3];
            }
            else if (random < 4f)
            {
                block[4, x].blockData = blockDatas[4];
            }
            else if (random < 4.5f)
            {
                block[4, x].blockData = blockDatas[6];
            }
            //else if(random < 4.5f)
            //{
            //    block[4, x].blockData = blockDatas[7];
            //}
            else
            {
                block[4, x].blockData = blockDatas[5];
            }

            // -------------------------------------
            //Pos.x += 1.5f;
            Pos.x += 2.7f;
            // -------------------------------------

            block[4, x].transform.parent = blockLine[i].transform;
        }
    }



    void Update()
    {
        // 복사용 array를 생성
        GameObject[] _blockLine = new GameObject[5];

        #region 첫번째 블럭라인
        // 첫번째 블럭라인이 다깨졌을때
        if (blockLine[0].transform.childCount == 0)
        {
            // 블럭이 재정렬될 순서로 복사를 해놓음
            _blockLine[4] = blockLine[0];
            _blockLine[0] = blockLine[1];
            _blockLine[1] = blockLine[2];
            _blockLine[2] = blockLine[3];
            _blockLine[3] = blockLine[4];



            // 5개의 블럭라인이 내려옴
            for (int i = 0; i < blockLine.Length; i++)
            {
                // -------------------------------------
                //blockLine[i].transform.position -= new Vector3(0, 0.9f);
                blockLine[i].transform.position -= new Vector3(0, 1.5f);
                // -------------------------------------
            }


            // 블럭의 2차원 배열을 재정렬
            for (int y = 0; y < BlockposYnum; y++)
            {
                for (int x = 0; x < BlockposXnum; x++)
                {
                    if (y > 0)
                    {
                        block[y, x] = block[y - 1, x];
                    }
                    if (y == 0)
                    {
                        block[y, x] = block[4, x];
                    }
                }
            }

            // 비활성화 된 블럭라인은 맨위로 올린뒤 블럭을 재생성
            //CreateBlock_X(4, Pos);
            recycle(0);

            // 블럭라인에 배열 숫자 재정렬
            blockLine[0] = _blockLine[0];
            blockLine[1] = _blockLine[1];
            blockLine[2] = _blockLine[2];
            blockLine[3] = _blockLine[3];
            blockLine[4] = _blockLine[4];
        }

        #endregion
        #region 두번째 블럭라인
        if (blockLine[1].transform.childCount == 0)
        {
            // 4개의 블럭라인이 내려옴

            _blockLine[4] = blockLine[1];
            _blockLine[1] = blockLine[2];
            _blockLine[2] = blockLine[3];
            _blockLine[3] = blockLine[4];

            for (int i = 1; i < blockLine.Length; i++)
            {
                // -------------------------------------
                //blockLine[i].transform.position -= new Vector3(0, 0.9f);
                blockLine[i].transform.position -= new Vector3(0, 1.5f);
                // -------------------------------------

                //blockLine[i].transform.position -= new Vector3(0, 0.9f);
            }

            // 블럭의 2차원 배열을 재정렬
            for (int y = 1; y < BlockposYnum; y++)
            {
                for (int x = 0; x < BlockposXnum; x++)
                {
                    if (y > 1)
                    {
                        block[y, x] = block[y - 1, x];
                    }
                    if (y == 1)
                    {
                        block[y, x] = block[4, x];
                    }
                }
            }
            // -------------------------------------
            //Vector2 Pos = new Vector2(-7.5f, 5.4f);  (카메라 비추진 기존 )
            //Vector2 Pos = new Vector2(-14f, 26.9f);
            Vector2 Pos = new Vector2(-14f, 34.4f);
            // -------------------------------------


            // 비활성화 된 블럭라인은 맨위로 올린뒤 블럭을 재생성
            //CreateBlock_X(4, Pos);
            recycle(1);

            // 블럭라인에 배열 숫자가 바뀜

            blockLine[1] = _blockLine[1];
            blockLine[2] = _blockLine[2];
            blockLine[3] = _blockLine[3];
            blockLine[4] = _blockLine[4];

        }
        #endregion
        #region 세번째 블럭라인
        if (blockLine[2].transform.childCount == 0)
        {
            // 3개의 블럭라인이 내려옴

            _blockLine[4] = blockLine[2];
            _blockLine[2] = blockLine[3];
            _blockLine[3] = blockLine[4];


            for (int i = 2; i < blockLine.Length; i++)
            {
                // -------------------------------------
                //blockLine[i].transform.position -= new Vector3(0, 0.9f);
                blockLine[i].transform.position -= new Vector3(0, 1.5f);
                // -------------------------------------

                //blockLine[i].transform.position -= new Vector3(0, 0.9f);
            }

            // 블럭의 2차원 배열을 재정렬
            for (int y = 2; y < BlockposYnum; y++)
            {
                for (int x = 0; x < BlockposXnum; x++)
                {
                    if (y > 2)
                    {
                        block[y, x] = block[y - 1, x];
                    }
                    if (y == 2)
                    {
                        block[y, x] = block[4, x];
                    }
                }
            }

            // -------------------------------------
            //Vector2 Pos = new Vector2(-7.5f, 5.4f);  (카메라 비추진 기존 )
            //Vector2 Pos = new Vector2(-14f, 26.9f);
            Vector2 Pos = new Vector2(-14f, 34.4f);
            // -------------------------------------

            // 비활성화 된 블럭라인은 맨위로 올린뒤 블럭을 재생성
            //CreateBlock_X(4, Pos);
            recycle(2);

            // 블럭라인에 배열 숫자가 바뀜

            blockLine[2] = _blockLine[2];
            blockLine[3] = _blockLine[3];
            blockLine[4] = _blockLine[4];

        }
        #endregion
        #region 네번째 블럭라인
        if (blockLine[3].transform.childCount == 0)
        {
            // 2개의 블럭라인이 내려옴


            _blockLine[4] = blockLine[3];
            _blockLine[3] = blockLine[4];


            for (int i = 3; i < blockLine.Length; i++)
            {
                // -------------------------------------
                //blockLine[i].transform.position -= new Vector3(0, 0.9f);
                blockLine[i].transform.position -= new Vector3(0, 1.5f);
                // -------------------------------------

                //blockLine[i].transform.position -= new Vector3(0, 0.9f);
            }

            // 블럭의 2차원 배열을 재정렬
            for (int y = 3; y < BlockposYnum; y++)
            {
                for (int x = 0; x < BlockposXnum; x++)
                {
                    if (y > 3)
                    {
                        block[y, x] = block[y - 1, x];
                    }
                    if (y == 3)
                    {
                        block[y, x] = block[4, x];
                    }
                }
            }
            // -------------------------------------
            //Vector2 Pos = new Vector2(-7.5f, 5.4f);  (카메라 비추진 기존 )
            //Vector2 Pos = new Vector2(-14f, 26.9f);
            Vector2 Pos = new Vector2(-14f, 34.4f);
            // -------------------------------------


            //Vector2 Pos = new Vector2(-7.5f, 5.4f);

            // 비활성화 된 블럭라인은 맨위로 올린뒤 블럭을 재생성
            //CreateBlock_X(4, Pos);
            recycle(3);

            // 블럭라인에 배열 숫자가 바뀜

            blockLine[3] = _blockLine[3];
            blockLine[4] = _blockLine[4];

        }
        #endregion

    }

    #region 블럭pool
    Queue<Block> pool = new Queue<Block>();
    

    public Block BackBlock(Vector3 pos)
    {
        Block instblock;

        // 기존의 목록 중에서 비활성화 된 것을 찾는다.
        // 지금 큐에서는 맨 앞에 있는 것 하나를 전달해 주면
        instblock = pool.Dequeue();
        instblock.transform.parent = null;
        instblock.transform.SetPositionAndRotation(pos, Quaternion.identity);
        instblock.gameObject.SetActive(true);
        return instblock;
    }

    
    //블럭에 오브젝트가 들어옴
    public void Destroyblock(Block block)
    {
        block.transform.parent = ObjectPool.transform;
        block.gameObject.SetActive(false);
        pool.Enqueue(block); // pool 에 1개 늘어난다.
    }
    #endregion

}
