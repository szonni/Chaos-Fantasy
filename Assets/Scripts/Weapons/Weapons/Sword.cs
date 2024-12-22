using UnityEngine;

public class Sword : Weapon
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
                SpawnSlash(pm.ShootDir, rangeMultiplier);
                break;
            case 2:
                rangeMultiplier = 1.3f;
                SpawnSlash(pm.ShootDir, rangeMultiplier);
                break;
            case 3:
                rangeMultiplier = 1.3f; // Phạm vi tăng
                SpawnSlash(pm.ShootDir, rangeMultiplier);
                SpawnSlash(-pm.ShootDir, rangeMultiplier);
                break;
            case 4:
                rangeMultiplier = 1.3f; // Phạm vi lớn hơn
                SpawnSlash(Vector2.up, rangeMultiplier);
                SpawnSlash(Vector2.down, rangeMultiplier);
                SpawnSlash(Vector2.left, rangeMultiplier);
                SpawnSlash(Vector2.right, rangeMultiplier);
                break;
            case 5:
                rangeMultiplier = 1.6f; // Phạm vi lớn hơn
                SpawnSlash(Vector2.up, rangeMultiplier);
                SpawnSlash(Vector2.down, rangeMultiplier);
                SpawnSlash(Vector2.left, rangeMultiplier);
                SpawnSlash(Vector2.right, rangeMultiplier);
                break;

        }
    }


    private void SpawnSlash(Vector2 direction, float rangeMultiplier = 1f)
    {
        GameObject slash = Instantiate(weaponData.prefab);
        SwordProjectile projectile = slash.GetComponent<SwordProjectile>();

        if (projectile != null)
        {
            projectile.CheckDirection(direction);
            projectile.SetRange(rangeMultiplier); // Điều chỉnh phạm vi
        }

        slash.transform.position = this.transform.position;
    }



    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        return true;
    }
}
