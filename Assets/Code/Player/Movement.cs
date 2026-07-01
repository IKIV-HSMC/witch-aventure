using UnityEngine;

public class PlayerMovementY : MonoBehaviour
{
    [Header("tăng tốc theo thời gian")]
    private float timeCounter = 0f;
    public float maxTime = 200f;

    [Header("Cài đặt di chuyển")]
    public float speed = 5f;          // Tốc độ di chuyển lên xuống

    [Header("Giới hạn biên độ (Y)")]
    public float minY = -4f;         // Tọa độ Y thấp nhất có thể đi
    public float maxY = 4f; // Tọa độ Y cao nhất có thể đi

    void Update()
    {
        if (timeCounter < maxTime)
        {
            timeCounter += Time.deltaTime;
        }
        float currentSpeed = speed * (1f + timeCounter / 100f);
        // 1. Lấy input từ bàn phím (Mũi tên Lên/Xuống hoặc phím W/S)
        float moveInput = Input.GetAxis("Vertical");

        // 2. Tính toán vị trí mới dựa trên input và tốc độ
        float newY = transform.position.y + moveInput * currentSpeed * Time.deltaTime;

        // 3. Giới hạn vị trí newY luôn nằm trong khoảng từ minY đến maxY
        newY = Mathf.Clamp(newY, minY, maxY);

        // 4. Cập nhật lại vị trí mới cho GameObject
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

    }
    // Hàm này tự động chạy khi có vật thể khác đụng vào người chơi
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem vật thể đụng trúng có tag là "Obstacle" hay không
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("GAME OVER! Bạn đã thua.");

            // Cách xử lý tạm thời: Đóng băng game lại
            Time.timeScale = 0f;

            // Bạn có thể thêm code hiện bảng Game Over hoặc Load lại cảnh ở đây
        }
    }
}