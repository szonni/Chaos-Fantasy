using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Shuriken : Weapon
{
    public GameObject shurikenPrefab; // Prefab của Shuriken
    public float radius = 2f; // Bán kính phát tán của Shuriken
    public int baseShurikenCount = 3; // Số lượng Shuriken mặc định

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        int shurikenCount = baseShurikenCount; // Mặc định là 3 Shuriken
        switch (currentLevel)
        {
            case 1:
                shurikenCount = 3; // Phát 3 Shuriken
                break;
            case 2:
                shurikenCount = 4; // Phát 4 Shuriken
                break;
            case 3:
                shurikenCount = 5; // Phát 5 Shuriken
                break;
            case 4:
                shurikenCount = 6; // Phát 6 Shuriken
                break;
            case 5:
                shurikenCount = 7; // Phát 7 Shuriken
                break;
        }

        // Tạo Shuriken xung quanh nhân vật
        CreateShurikens(shurikenCount);
    }

    private void CreateShurikens(int count)
    {
        float angleStep = 360f / count; // Tính góc phân bố Shuriken đều
        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep; // Tính góc cụ thể cho mỗi Shuriken
            Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius, 0);
            var shuriken = Instantiate(shurikenPrefab, transform.position + position, Quaternion.identity);
            shuriken.GetComponent<ShurikenProjectile>().Initialize(angle); // Khởi tạo hướng và góc cho Shuriken
        }
    }

    public override bool LevelUp()
    {
        if (!base.LevelUp()) return false;
        return true;
    }
}
