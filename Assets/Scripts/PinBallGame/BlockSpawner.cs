using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class BlockSpawner : MonoBehaviourPun
{
    [Header("�� ����")]
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
    #region ������
    private void CreateBlock()
    {
        
        // -------------------------------------
        //Pos.x = -7.5f; (ī�޶� ���� ����)
        Pos.x = -14f;

        // ī�޶� �������� ��ġ (����)
        //Pos.y = 0.9f;
        // ī�޶� �������� ��ġ (�׽�Ʈ)
        Pos.y = 28.4f; 
        // -------------------------------------

        for (int y = 0; y < BlockposYnum; y++)
        {

            // -------------------------------------
            //Pos.y += 0.9f;
            Pos.y += 1.5f;
            //Pos.x = -7.5f;  // �ٽ� x�� �ʱ�ȭ
            Pos.x = -14f;  // �ٽ� x�� �ʱ�ȭ
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

        //���� ������ ��ġ

        // -------------------------------------
        //Vector2 Pos = new Vector2(-7.5f, 5.4f);  (ī�޶� ������ ���� )
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
        // ����� array�� ����
        GameObject[] _blockLine = new GameObject[5];

        #region ù��° ������
        // ù��° �������� �ٱ�������
        if (blockLine[0].transform.childCount == 0)
        {
            // ���� �����ĵ� ������ ���縦 �س���
            _blockLine[4] = blockLine[0];
            _blockLine[0] = blockLine[1];
            _blockLine[1] = blockLine[2];
            _blockLine[2] = blockLine[3];
            _blockLine[3] = blockLine[4];



            // 5���� �������� ������
            for (int i = 0; i < blockLine.Length; i++)
            {
                // -------------------------------------
                //blockLine[i].transform.position -= new Vector3(0, 0.9f);
                blockLine[i].transform.position -= new Vector3(0, 1.5f);
                // -------------------------------------
            }


            // ���� 2���� �迭�� ������
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

            // ��Ȱ��ȭ �� �������� ������ �ø��� ���� �����
            //CreateBlock_X(4, Pos);
            recycle(0);

            // �����ο� �迭 ���� ������
            blockLine[0] = _blockLine[0];
            blockLine[1] = _blockLine[1];
            blockLine[2] = _blockLine[2];
            blockLine[3] = _blockLine[3];
            blockLine[4] = _blockLine[4];
        }

        #endregion
        #region �ι�° ������
        if (blockLine[1].transform.childCount == 0)
        {
            // 4���� �������� ������

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

            // ���� 2���� �迭�� ������
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
            //Vector2 Pos = new Vector2(-7.5f, 5.4f);  (ī�޶� ������ ���� )
            //Vector2 Pos = new Vector2(-14f, 26.9f);
            Vector2 Pos = new Vector2(-14f, 34.4f);
            // -------------------------------------


            // ��Ȱ��ȭ �� �������� ������ �ø��� ���� �����
            //CreateBlock_X(4, Pos);
            recycle(1);

            // �����ο� �迭 ���ڰ� �ٲ�

            blockLine[1] = _blockLine[1];
            blockLine[2] = _blockLine[2];
            blockLine[3] = _blockLine[3];
            blockLine[4] = _blockLine[4];

        }
        #endregion
        #region ����° ������
        if (blockLine[2].transform.childCount == 0)
        {
            // 3���� �������� ������

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

            // ���� 2���� �迭�� ������
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
            //Vector2 Pos = new Vector2(-7.5f, 5.4f);  (ī�޶� ������ ���� )
            //Vector2 Pos = new Vector2(-14f, 26.9f);
            Vector2 Pos = new Vector2(-14f, 34.4f);
            // -------------------------------------

            // ��Ȱ��ȭ �� �������� ������ �ø��� ���� �����
            //CreateBlock_X(4, Pos);
            recycle(2);

            // �����ο� �迭 ���ڰ� �ٲ�

            blockLine[2] = _blockLine[2];
            blockLine[3] = _blockLine[3];
            blockLine[4] = _blockLine[4];

        }
        #endregion
        #region �׹�° ������
        if (blockLine[3].transform.childCount == 0)
        {
            // 2���� �������� ������


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

            // ���� 2���� �迭�� ������
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
            //Vector2 Pos = new Vector2(-7.5f, 5.4f);  (ī�޶� ������ ���� )
            //Vector2 Pos = new Vector2(-14f, 26.9f);
            Vector2 Pos = new Vector2(-14f, 34.4f);
            // -------------------------------------


            //Vector2 Pos = new Vector2(-7.5f, 5.4f);

            // ��Ȱ��ȭ �� �������� ������ �ø��� ���� �����
            //CreateBlock_X(4, Pos);
            recycle(3);

            // �����ο� �迭 ���ڰ� �ٲ�

            blockLine[3] = _blockLine[3];
            blockLine[4] = _blockLine[4];

        }
        #endregion

    }

    #region ��pool
    Queue<Block> pool = new Queue<Block>();
    

    public Block BackBlock(Vector3 pos)
    {
        Block instblock;

        // ������ ��� �߿��� ��Ȱ��ȭ �� ���� ã�´�.
        // ���� ť������ �� �տ� �ִ� �� �ϳ��� ������ �ָ�
        instblock = pool.Dequeue();
        instblock.transform.parent = null;
        instblock.transform.SetPositionAndRotation(pos, Quaternion.identity);
        instblock.gameObject.SetActive(true);
        return instblock;
    }

    
    //���� ������Ʈ�� ����
    public void Destroyblock(Block block)
    {
        block.transform.parent = ObjectPool.transform;
        block.gameObject.SetActive(false);
        pool.Enqueue(block); // pool �� 1�� �þ��.
    }
    #endregion

}
