using UnityEngine;

public class TwinBlade : Weapon
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
                rangeMultiplier = 1f; // Phạm vi nhỏ
                SpawnBladeCircle(rangeMultiplier);
                break;
            case 2:
                rangeMultiplier = 1.2f; // Phạm vi tăng
                SpawnBladeCircle(rangeMultiplier);
                break;
            case 3:
                rangeMultiplier = 1.5f; // Phạm vi lớn
                SpawnBladeCircle(rangeMultiplier);
                break;
            case 4:
                rangeMultiplier = 2f; // Phạm vi rất lớn
                SpawnBladeCircle(rangeMultiplier);
                break;
            case 5:
                rangeMultiplier = 2.5f; // Phạm vi tối đa
                SpawnBladeCircle(rangeMultiplier);
                break;
        }
    }

    private void SpawnBladeCircle(float rangeMultiplier)
    {
        GameObject blade = Instantiate(weaponData.prefab);
        TwinBladeProjectile projectile = blade.GetComponent<TwinBladeProjectile>();

        if (projectile != null)
        {
            projectile.SetRange(rangeMultiplier);
        }

        blade.transform.position = this.transform.position;
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        return true;
    }
}
