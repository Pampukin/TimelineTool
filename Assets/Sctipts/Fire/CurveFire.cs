using System.ComponentModel;
using UnityEngine;

[System.Serializable]
[DisplayName("Fire/CurveFire")]
public class CurveFire : AbstractFireMarker
{
    [SerializeField] private float _angle;
    public override void Fire()
    {
        base.Fire();
        
        var startPos = GameObject.FindWithTag("Enemy").transform;
        var radian = (_angle + 270) * Mathf.Deg2Rad;
        var direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        
        var bulletObject = Instantiate(_bullet, startPos.position, Quaternion.identity);
        bulletObject.GetComponent<Rigidbody2D>().AddForce(direction * _fireData.Speed, ForceMode2D.Impulse);
    }
}
