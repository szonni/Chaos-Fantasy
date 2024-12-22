using UnityEngine;

public class BowProjectile : Projectile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += currentSpeed * Time.deltaTime * direction;
    }
}
