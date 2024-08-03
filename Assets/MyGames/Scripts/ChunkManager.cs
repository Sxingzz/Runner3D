using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header("Elements")]
    [SerializeField] private LevelSO[] levels;
    
    private GameObject finishLine;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        Generatelevels();
        finishLine = GameObject.FindWithTag("Finish");
    }

    private void CreateLevels(Chunk[] levelChunks)
    {
        Vector3 chunkPosition = Vector3.zero;

        // thuật toán đặt các khối với pivot nằm giữa nối tiếp nhau
        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chunk chunkToCreate = levelChunks[i];

            if (i > 0) // kiểm tra nếu là khối thứ 2 trở đi thì chạy code trong if
            {                                                     // tính toán điểm ở giữa của khối,
                chunkPosition.z += chunkToCreate.GetLength() / 2; // ví dụ khối thứ 2: z = 20,
                                                                  // thì z = 5 + 20/2 = 15 ( là điểm giữa của khối thứ 2),
                                                                  // vậy khối thứ 2 sinh ra từ 5 đến 25 có 15 ở giữa
            }
            // sinh ra ở vị trí chunk position = (0,0,0) =>  z = 0
            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);// ví dụ khối 1 dài z = 10 đơn vị,
                                                                                                            // trục của khối nằm ở giữa cho nên vị trí của trục sinh ra ở vị trí 0
                                                                                                            // cho nên khối đầu tiên dài từ -5 đến 5 do z = 10 đơn vị và ở giữa là 0
            chunkPosition.z += chunkInstance.GetLength() / 2;// khối 2: z = 0 + 10/2 = 5                                                       
                                                             // khối 3: z = 15 + 20/2 = 25 nếu có khối 3 chạy tiếp trong if với z mới = 25                                             

        }
    }

    private void Generatelevels()
    {
        int runnerLevel = GetLevels(); // Lấy cấp độ hiện tại;

        runnerLevel = runnerLevel % levels.Length;// đảm bảo cấp độ luôn nằm trong khoảng levels.Length 
        LevelSO level = levels[runnerLevel]; // Lấy thông tin cấp độ hiện tại

        CreateLevels(level.chunks);
    }

    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevels()
    {
        return PlayerPrefs.GetInt("Level", 0);
    }
   
}
