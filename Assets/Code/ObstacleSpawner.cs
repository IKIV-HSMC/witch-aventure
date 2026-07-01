using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2f;
    public float minY = -4f;
    public float maxY = 4f;
    public float paddingX = 2f;

    [Header("Cấu hình Nhắm mục tiêu")]
    public Transform playerTransform;   // Kéo GameObject Player vào đây trong Inspector
    [Range(0f, 100f)]
    public float targetPlayerChance = 70f; // 70% tỉ lệ chướng ngại vật sinh ra đối diện Player

    private float nextSpawnTime;
    private float spawnX;

    void Start()
    {
        // Tính toán vị trí trục X nằm ngoài cùng bên phải màn hình
        if (Camera.main != null)
        {
            Vector3 topRightScreen = new Vector3(Screen.width, Screen.height, 0);
            Vector3 topRightWorld = Camera.main.ScreenToWorldPoint(topRightScreen);
            spawnX = topRightWorld.x + paddingX;
        }
        else
        {
            spawnX = 12f;
        }

        // Tự động tìm Player nếu bạn quên kéo vào bảng Inspector
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnObstacle()
    {
        float finalY = 0f;

        // 1. Tung xúc xắc ngẫu nhiên từ 0 đến 100
        float randomRoll = Random.Range(0f, 100f);

        // 2. Nếu trúng tỷ lệ nhắm vào Player VÀ tìm thấy Player trong game
        if (randomRoll <= targetPlayerChance && playerTransform != null)
        {
            // Lấy luôn tọa độ Y hiện tại của Player
            finalY = playerTransform.position.y;

            // Cẩn thận: Giới hạn lại finalY để phòng trường hợp Player đang ở vị trí đặc biệt
            finalY = Mathf.Clamp(finalY, minY, maxY);
        }
        else
        {
            // 3. Nếu không trúng tỷ lệ, sinh ra ngẫu nhiên như cũ
            finalY = Random.Range(minY, maxY);
        }

        // Tạo vị trí và sinh chướng ngại vật
        Vector3 spawnPosition = new Vector3(spawnX, finalY, transform.position.z);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}