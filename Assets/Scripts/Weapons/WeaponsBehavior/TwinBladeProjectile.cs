using UnityEngine;

public class TwinBladeProjectile : Projectile
{
    private float rotationSpeed = 360f; // Tốc độ xoay vòng tròn
    private float duration = 2f; // Thời gian tồn tại của hiệu ứng
    private float timer = 0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        // Xoay vòng tròn sát thương
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Kiểm tra thời gian tồn tại
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }

    public void SetRange(float rangeMultiplier)
    {
        transform.localScale *= rangeMultiplier;
    }
}
