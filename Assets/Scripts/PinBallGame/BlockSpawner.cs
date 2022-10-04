using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class BlockSpawner : MonoBehaviourPun
{
    [Header("블럭 정보")]
    [SerializeField] List<Scriptable_PinBallBlock> blockDatas;
    [SerializeField] GameObject[] blockLine;

    public Scriptable_PinBallBlock GetBlockData(int idx) { return blockDatas[idx]; }


    Block[,] block;

    [SerializeField] int BlockposYnum = 5;
    [SerializeField] int BlockposXnum = 11;
    //GameObject BlockPosChange;

    Vector3 Pos = Vector3.zero;

    bool isReArrangement = false;
    private void Awake()
    {
        //blockDatas = new List<Scriptable_PinBallBlock>();
        block = new Block[BlockposYnum, BlockposXnum];
        //BlockPosChange = new GameObject("CapyBlockLine");
        //BlockPosChange.transform.parent = this.transform;
        //Block block = FindObjectOfType<Block>();  


    }

    void Start()
    {

        //CreateBlock();
        if (PhotonNetwork.IsMasterClient)
        {
            //Debug.Log("IsMasterClient ");
            //photonView.RPC("CreateBlock", RpcTarget.All);
            CreateBlock();

        }
    }

    #region 블럭생성
    public void CreateBlock()
    {
        #region
        // -------------------------------------
        //Pos.x = -7.5f; //(카메라 비춘 기준)
        // 카메라 비춰지는 위치 (기존)
        //Pos.y = 0.9f;
        //Pos.y = 2f;
        // 카메라 비춰지는 위치 (테스트)
        //Pos.y = 28.4f;
        // -------------------------------------
        #endregion

        // 생청 하는 첫 블럭
        Pos.x = -14f;
        Pos.y = 3f;

        for (int y = 0; y < BlockposYnum; y++)
        {
            // ------------------------------------
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
                    block[y, x] = PhotonNetwork.Instantiate("Block", Pos, Quaternion.identity).GetComponent<Block>();

                    //blockLine;
                    //photonView.RPC("SetBlockData", RpcTarget.Others);
                    //block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    block[y, x].blockData = blockDatas[0];
                    block[y, x].RPC_BlockDatas(0);

                    Debug.Log("RPC_BlockDatas");
                    //photonView.RPC("Pun_SetBlockData1", RpcTarget.All);
                }
                else if (random < 2.5f)
                {
                    block[y, x] = PhotonNetwork.Instantiate("Block", Pos, Quaternion.identity).GetComponent<Block>();
                    //block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    //block[y, x].transform.SetParent(blockLine[1].transform);

                    block[y, x].blockData = blockDatas[1];
                    block[y, x].RPC_BlockDatas(1);

                    //photonView.RPC("Pun_SetBlockData1", RpcTarget.All);

                }
                else if (random < 3f)
                {
                    block[y, x] = PhotonNetwork.Instantiate("Block", Pos, Quaternion.identity).GetComponent<Block>();
                    //block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    //photonView.RPC("Pun_SetBlockData1", RpcTarget.All);
                    block[y, x].blockData = blockDatas[2];
                    block[y, x].RPC_BlockDatas(2);

                    //block[y, x] = photonView.RPC("SetBlockData", RpcTarget.All);
                }
                else if (random < 3.5f)
                {
                    block[y, x] = PhotonNetwork.Instantiate("Block", Pos, Quaternion.identity).GetComponent<Block>();
                    //block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    //photonView.RPC("Pun_SetBlockData1", RpcTarget.All);
                    block[y, x].blockData = blockDatas[3];
                    block[y, x].RPC_BlockDatas(3);
                }
                else if (random < 4f)
                {
                    block[y, x] = PhotonNetwork.Instantiate("Block", Pos, Quaternion.identity).GetComponent<Block>();
                    //block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    //photonView.RPC("Pun_SetBlockData1", RpcTarget.All);
                    block[y, x].blockData = blockDatas[4];
                    block[y, x].RPC_BlockDatas(4);
                }
                else if (random < 4.5f)
                {
                    block[y, x] = PhotonNetwork.Instantiate("Block", Pos, Quaternion.identity).GetComponent<Block>();
                    //block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    //photonView.RPC("Pun_SetBlockData1", RpcTarget.All);
                    block[y, x].blockData = blockDatas[6];
                    block[y, x].RPC_BlockDatas(6);
                }
                //else if(random < 5f)
                //{
                //    block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);

                //    block[y, x].blockData = blockDatas[7];
                //}
                else
                {
                    block[y, x] = PhotonNetwork.Instantiate("Block", Pos, Quaternion.identity).GetComponent<Block>();
                    //block[y, x] = Instantiate(BlockPrefab, Pos, Quaternion.identity);
                    //photonView.RPC("SetBlockData", RpcTarget.All);
                    block[y, x].blockData = blockDatas[5];
                    block[y, x].RPC_BlockDatas(5);
                    //Debug.Log("RPC_BlockDatas : ");
                }

                block[y, x].transform.parent = blockLine[y].transform;


                //block[y, x].transform.SetParent(blockLine[y].transform);

                // -------------------------------------
                //Pos.x += 1.5f;
                Pos.x += 2.7f;
                // -------------------------------------
            }
        }
    }
    #endregion


    //[PunRPC]
    public Block BackBlock(Vector3 pos)
    {
        Block instblock = null;

        //    // 기존의 목록 중에서 비활성화 된 것을 찾는다.
        //    // 지금 큐에서는 맨 앞에 있는 것 하나를 전달해 주면
        //    //instblock = pool.Dequeue();
        //    //instblock.transform.parent = null;

        instblock.transform.SetPositionAndRotation(pos, Quaternion.identity);
        instblock.gameObject.SetActive(true);
        return instblock;
    }



    //public Block BackBlock(Vector3 pos)
    //{
    //    Block instblock;

    //    // 기존의 목록 중에서 비활성화 된 것을 찾는다.
    //    // 지금 큐에서는 맨 앞에 있는 것 하나를 전달해 주면
    //    //instblock = pool.Dequeue();
    //    //instblock.transform.parent = null;
    //    instblock.transform.SetPositionAndRotation(pos, Quaternion.identity);
    //    instblock.gameObject.SetActive(true);
    //    return instblock;
    //}

    public void recycle(int i)
    {

        //Vector2 PosyY = new Vector2(-14f, 35.9f);
        //Vector2 PosyY = new Vector2(-14f, 0.9f);
        //Pos.x = -14f;
        //Pos.y = 3f;

        Vector2 PosyY = new Vector2(-14f, 10.5f);


        //블럭이 생성될 위치

        // -------------------------------------
        //Vector2 Pos = new Vector2(-7.5f, 5.4f);  (카메라 비추진 기존 )
        // -------------------------------------

        for (int x = 0; x < BlockposXnum; x++)
        {
            //block[4, x] = Pos;
            float random = Random.Range(0f, 10f);

            if (random < 2f)
            {
                block[4, x] = PhotonNetwork.Instantiate("Block", PosyY, Quaternion.identity).GetComponent<Block>();
                block[4, x].blockData = blockDatas[0];
                block[4, x].RPC_BlockDatas(0);
            }
            else if (random < 2.5f)
            {
                block[4, x] = PhotonNetwork.Instantiate("Block", PosyY, Quaternion.identity).GetComponent<Block>();
                block[4, x].blockData = blockDatas[1];
                block[4, x].RPC_BlockDatas(1);
            }
            else if (random < 3f)
            {
                block[4, x] = PhotonNetwork.Instantiate("Block", PosyY, Quaternion.identity).GetComponent<Block>();
                block[4, x].blockData = blockDatas[2];
                block[4, x].RPC_BlockDatas(2);
            }
            else if (random < 3.5f)
            {
                block[4, x] = PhotonNetwork.Instantiate("Block", PosyY, Quaternion.identity).GetComponent<Block>();
                block[4, x].blockData = blockDatas[3];
                block[4, x].RPC_BlockDatas(3);
            }
            else if (random < 4f)
            {
                block[4, x] = PhotonNetwork.Instantiate("Block", PosyY, Quaternion.identity).GetComponent<Block>();
                block[4, x].blockData = blockDatas[4];
                block[4, x].RPC_BlockDatas(4);
            }
            else if (random < 4.5f)
            {
                block[4, x] = PhotonNetwork.Instantiate("Block", PosyY, Quaternion.identity).GetComponent<Block>();
                block[4, x].blockData = blockDatas[6];
                block[4, x].RPC_BlockDatas(6);
            }
            //else if(random < 4.5f)
            //{
            //    block[4, x] = PhotonNetwork.Instantiate("Block", PosyY, Quaternion.identity).GetComponent<Block>();
            //    block[4, x].blockData = blockDatas[7];
            //    block[4, x].RPC_BlockDatas();
            //}
            else
            {
                block[4, x] = PhotonNetwork.Instantiate("Block", PosyY, Quaternion.identity).GetComponent<Block>();
                block[4, x].blockData = blockDatas[5];
                block[4, x].RPC_BlockDatas(5);
            }

            // -------------------------------------
            //Pos.x += 1.5f;
            PosyY.x += 2.7f;
            // -------------------------------------


            block[4, x].transform.parent = blockLine[i].transform;

            // 바뀐 뒤 시점에서 블럭의 포톤뷰에 접근해서 블럭의 포지션을 동기화한다.
            //block[4, x].photonView.RPC("RPC_BlockPos", RpcTarget.Others, block[4, x].transform.position);


        }
    }


    void Update()
    {
        if (PhotonNetwork.IsMasterClient == false)
            return;

        if (PhotonNetwork.IsMasterClient)
        {
            // 복사용 array를 생성
            GameObject[] _blockLine = new GameObject[5];

            #region 첫번째 블럭라인
            // 첫번째 블럭라인이 다깨졌을때
            if (blockLine[0].transform.childCount == 0)
            {
                isReArrangement = true;
                //Debug.Log("움직이니?");

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

                    //block[4, x].photonView.RPC("RPC_BlockPos", RpcTarget.Others, block[4, x].transform.position);

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

                //photonView.RPC("recycle", RpcTarget.All, 0);
                //photonView.RPC("Sum", RpcTarget.Others, "a" );
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
                isReArrangement = true;
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
                //Vector2 Pos = new Vector2(-14f, 34.4f);
                //Vector2 PosyY = new Vector2(-14f, 3f);
                Vector2 PosyY = new Vector2(-14f, 10.5f);

                // -------------------------------------


                // 비활성화 된 블럭라인은 맨위로 올린뒤 블럭을 재생성
                //CreateBlock_X(4, Pos);

                recycle(1);

                //photonView.RPC("recycle", RpcTarget.All, 1);

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
                isReArrangement = true;
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
                //Vector2 Pos = new Vector2(-7.5f, 5.4f);  //(카메라 비추진 기존 )
                //Vector2 PosyY = new Vector2(-14f, 3f);
                Vector2 PosyY = new Vector2(-14f, 10.5f);


                //Vector2 Pos = new Vector2(-14f, 26.9f);
                //Vector2 Pos = new Vector2(-14f, 34.4f);
                //Vector2 PosyY = new Vector2(-14f, 0.9f);

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
                isReArrangement = true;
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
                //Vector2 Pos = new Vector2(-7.5f, 5.4f);  //(카메라 비추진 기존 )
                //Vector2 PosyY = new Vector2(-14f, 3f);
                Vector2 PosyY = new Vector2(-14f, 10.5f);

                //Vector2 Pos = new Vector2(-14f, 26.9f);
                //Vector2 Pos = new Vector2(-14f, 34.4f);
                // -------------------------------------
                //Vector2 Pos = new Vector2(-7.5f, 5.4f);

                // 비활성화 된 블럭라인은 맨위로 올린뒤 블럭을 재생성
                //CreateBlock_X(4, Pos);
                recycle(3);
                photonView.RPC("recycle", RpcTarget.All, 3);

                // 블럭라인에 배열 숫자가 바뀜

                blockLine[3] = _blockLine[3];
                blockLine[4] = _blockLine[4];
            }
            #endregion

            if (isReArrangement == true)
            {
                Block[] blocks = FindObjectsOfType<Block>();
                for (int i = 0; i < blocks.Length; i++)
                {
                    blocks[i].photonView.RPC("BlockTransFormChange", RpcTarget.Others, blocks[i].gameObject.transform.position);
                }

                isReArrangement = false;
            }
        }

    }



    // 보강이 POOl 활용 대체
    #region 블럭pool
    //Queue<Block> pool = new Queue<Block>();

    //블럭에 오브젝트가 들어옴
    //[PunRPC]
    //public void Destroyblock(Block block)
    //{
    //    block.transform.parent = ObjectPool.transform;
    //    block.gameObject.SetActive(false);

    //    pool.Enqueue(block); // pool 에 1개 늘어난다.
    //}



    #endregion

}
