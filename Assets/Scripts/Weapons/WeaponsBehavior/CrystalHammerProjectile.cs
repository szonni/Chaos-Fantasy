using UnityEngine;

public class CrystalHammerProjectile : Projectile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Điều chỉnh vị trí ban đầu để tạo hiệu ứng động
        transform.position += direction * -0.2f;
    }

    public void SetRange(float rangeMultiplier)
    {
        // Tăng kích thước để mở rộng phạm vi tấn công
        transform.localScale *= rangeMultiplier;
    }
}
