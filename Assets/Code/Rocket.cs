using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 15f; // Tên lửa bay rất nhanh
    public float destroyX = -15f;

    void Update()
    {
        // Di chuyển thẳng sang bên trái
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Tự hủy khi bay mất khỏi màn hình bên trái
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}