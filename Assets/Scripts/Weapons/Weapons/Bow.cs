using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public float doubleShotDelay = 0.005f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        if (currentLevel < 3)
        {
            // Level 1 và 2: chỉ bắn 1 lần
            SpawnArrows();
        }
        else
        {
            // Level 3 trở lên: bắn 2 lần liên tiếp
            StartCoroutine(DoubleShot());
        }
    }

    private IEnumerator DoubleShot()
    {
        // Lần bắn đầu tiên
        SpawnArrows();

        // Chờ delay giữa 2 lần bắn
        yield return new WaitForSeconds(doubleShotDelay);

        // Lần bắn thứ hai
        SpawnArrows();
    }   

    private void SpawnArrows()
    {
        switch (currentLevel)
        {
            case 1: // Level 1: Ném 1 dao
                SpawnArrow(pm.ShootDir);
                break;

            case 2: // Level 2: Ném 2 dao theo góc chéo
                SpawnArrow(pm.ShootDir);
                SpawnArrow(-pm.ShootDir);
                break;

            case 3: // Level 3: Ném 3 dao theo góc
                SpawnArrow(pm.ShootDir);
                SpawnArrow(Quaternion.Euler(0, 0, 30) * pm.ShootDir); // Dao lệch 30 độ
                SpawnArrow(Quaternion.Euler(0, 0, -30) * pm.ShootDir); // Dao lệch -30 độ
                break;
            case 4:
                SpawnArrow(pm.ShootDir);
                SpawnArrow(-pm.ShootDir);
                SpawnArrow(Quaternion.Euler(0, 0, 30) * pm.ShootDir); // Dao lệch 30 độ
                SpawnArrow(Quaternion.Euler(0, 0, -30) * pm.ShootDir); // Dao lệch -30 độ
                break;
            case 5: // Level 4: Ném 4 dao 4 hướng
                SpawnArrow(Vector2.up);
                SpawnArrow(Vector2.down);
                SpawnArrow(Vector2.left);
                SpawnArrow(Vector2.right);
                break;
        }
    }

    private void SpawnArrow(Vector2 direction)
    {
        GameObject arrow = Instantiate(weaponData.prefab);
        arrow.transform.position = this.transform.position;

        // Truyền hướng bắn cho projectile
        BowProjectile projectile = arrow.GetComponent<BowProjectile>();
        if (projectile != null)
        {
            projectile.CheckDirection(direction);
        }
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        return true;
    }
}

