using UnityEngine;

using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 10f;        // Tốc độ ban đầu
    public float destroyX = -12f;    // Tọa độ X để tự hủy

    private float timeCounter = 0f;  // Biến đếm thời gian từ lúc sinh ra
    public float maxTime = 200f;     // Giới hạn thời gian tối đa để tăng tốc

    void Update()
    {
        // 1. Tăng thời gian timeCounter lên theo mỗi giây thực tế
        if (timeCounter < maxTime)
        {
            timeCounter += Time.deltaTime;
        }

        // 2. Tính toán tốc độ thực tế (tăng dần theo timeCounter)
        // Ví dụ: Tốc độ tăng thêm dựa trên (timeCounter / 100)
        float currentSpeed = speed * (1f + timeCounter / 100f);

        // 3. Di chuyển sang bên trái với tốc độ đã tính
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);

        // 4. Nếu bay quá màn hình bên trái thì tự hủy
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}