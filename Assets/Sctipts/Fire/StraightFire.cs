using System.ComponentModel;
using UnityEngine;

[System.Serializable]
[DisplayName("Fire/StraightFire")]
public class StraightFire : AbstractFireMarker
{
    public override void Fire()
    {
        base.Fire();
        
        var startPos = GameObject.FindWithTag("Enemy").transform;
        var bulletObject = Instantiate(_bullet, startPos.position, Quaternion.identity);
        bulletObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1 * _fireData.Speed), ForceMode2D.Impulse);
    }
}
