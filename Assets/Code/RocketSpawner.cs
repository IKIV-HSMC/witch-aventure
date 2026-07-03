using UnityEngine;


public class RocketSpawner : MonoBehaviour
{
    [Header("Đối tượng")]
    public GameObject warningPrefab;   // Kéo Sprite/UI dấu cảnh báo vào đây
    public GameObject rocketPrefab;    // Kéo Prefab tên lửa vào đây
    public Transform playerTransform;   // Kéo Player vào đây

    [Header("Thời gian xuất hiện tên lửa")]
    public float spawnCooldown = 5f;    // Cứ mỗi 5 giây lại có 1 đợt tên lửa
    
    [Header("Thời gian cảnh báo (Giảm dần)")]
    public float initialWarningTime = 3f; // Thời gian chờ ban đầu là 3 giây
    public float minWarningTime = 0.8f;   // Không được giảm thấp hơn 0.8 giây (quá nhanh không né kịp)
    public float difficultyFactor = 0.02f; // Mỗi giây trôi qua, thời gian cảnh báo giảm 0.02s

    private float nextSpawnTime;
    private float spawnX;
    public float paddingX = 1.5f;

    void Start()
    {
        // Tính toán rìa phải màn hình để đặt cảnh báo và tên lửa
        if (Camera.main != null)
        {
            Vector3 topRightWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            spawnX = topRightWorld.x; 
        }
        else
        {
            spawnX = 10f;
        }

        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player")?.transform;
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            // Bắt đầu chuỗi kích hoạt tên lửa
            StartCoroutine(SpawnRocketSequence());
            nextSpawnTime = Time.time + spawnCooldown;
        }
    }

    System.Collections.IEnumerator SpawnRocketSequence()
    {
        if (playerTransform == null) yield break;

        // 1. Xác định độ cao Y của player TẠI THỜI ĐIỂM BẮT ĐẦU CẢNH BÁO
        float targetY = playerTransform.position.y;
        Vector3 spawnPos = new Vector3(spawnX - 1f, targetY, 0f); // Hiện bên trong rìa màn hình tí để dễ thấy

        // 2. Tạo dấu cảnh báo
        GameObject warningSign = Instantiate(warningPrefab, spawnPos, Quaternion.identity);

        // 3. Tính toán thời gian cảnh báo hiện tại (giảm dần theo thời gian chơi thực tế Time.time)
        float currentWarningTime = initialWarningTime - (Time.time * difficultyFactor);
        currentWarningTime = Mathf.Max(currentWarningTime, minWarningTime); // Giữ cho không nhỏ hơn minWarningTime

        // 4. Chờ hết thời gian cảnh báo
        yield return new WaitForSeconds(currentWarningTime);

        // 5. Xóa dấu cảnh báo đi
        Destroy(warningSign);

        // 6. Bắn tên lửa lao ra từ rìa màn hình (ở ngoài màn hình một chút để mượt hơn)
        Vector3 rocketSpawnPos = new Vector3(spawnX + paddingX, targetY, 0f);
        Instantiate(rocketPrefab, rocketSpawnPos, Quaternion.identity);
    }
}