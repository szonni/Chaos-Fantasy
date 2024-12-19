using System.Collections;
using UnityEngine;

public class ShurikenProjectile : Projectile
{
    private float rotationSpeed = 360f; // Tốc độ quay của Shuriken

    protected override void Start()
    {
        base.Start();
        StartCoroutine(RotateShuriken()); // Bắt đầu quay ngay khi tạo ra
    }

    // Phương thức để quay Shuriken
    private IEnumerator RotateShuriken()
    {
        while (true)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); // Quay Shuriken theo trục Z
            yield return null; // Tiếp tục quay mỗi frame
        }
    }

    // Phương thức khởi tạo hướng bay và góc quay cho Shuriken
    public void Initialize(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle); // Xoay Shuriken theo góc ban đầu
    }
}
