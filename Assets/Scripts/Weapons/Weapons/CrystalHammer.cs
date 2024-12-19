using UnityEngine;

public class CrystalHammer : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        float rangeMultiplier = 1f;
        switch (currentLevel)
        {
            case 1:
                rangeMultiplier = 1f; // Phạm vi bình thường
                SpawnHammer(pm.ShootDir, rangeMultiplier);
                break;
            case 2:
                rangeMultiplier = 1.3f; // Phạm vi tăng
                SpawnHammer(pm.ShootDir, rangeMultiplier);
                break;
            case 3:
                rangeMultiplier = 1.3f; // Phạm vi tăng + tấn công thêm hướng ngược lại
                SpawnHammer(pm.ShootDir, rangeMultiplier);
                SpawnHammer(-pm.ShootDir, rangeMultiplier);
                break;
            case 4:
                rangeMultiplier = 1.5f; // Phạm vi lớn hơn, tấn công nhiều hướng
                SpawnHammer(Vector2.up, rangeMultiplier);
                SpawnHammer(Vector2.down, rangeMultiplier);
                SpawnHammer(Vector2.left, rangeMultiplier);
                SpawnHammer(Vector2.right, rangeMultiplier);
                break;
            case 5:
                rangeMultiplier = 1.8f; // Phạm vi tối đa
                SpawnHammer(Vector2.up, rangeMultiplier);
                SpawnHammer(Vector2.down, rangeMultiplier);
                SpawnHammer(Vector2.left, rangeMultiplier);
                SpawnHammer(Vector2.right, rangeMultiplier);
                break;
        }
    }

    private void SpawnHammer(Vector2 direction, float rangeMultiplier = 1f)
    {
        // Tạo một instance của CrystalHammerProjectile
        GameObject hammer = Instantiate(weaponData.prefab);
        CrystalHammerProjectile projectile = hammer.GetComponent<CrystalHammerProjectile>();

        if (projectile != null)
        {
            projectile.CheckDirection(direction);
            projectile.SetRange(rangeMultiplier); // Điều chỉnh phạm vi
        }

        hammer.transform.position = this.transform.position; // Vị trí xuất hiện
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        return true;
    }
}
