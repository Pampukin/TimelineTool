using System.ComponentModel;
using UnityEngine;

[System.Serializable]
[DisplayName("Fire/DirectionFire")]
public class DirectionFire : AbstractFireMarker
{
    public float Angle;
    public override void Fire()
    {
        base.Fire();

        var startPos = GameObject.FindWithTag("Enemy").transform;
        float radian = (Angle + 270) * Mathf.Deg2Rad;
        var direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

        var bulletObject = Instantiate(_bullet, startPos.position, Quaternion.identity);
        bulletObject.GetComponent<Rigidbody2D>().AddForce(direction * _bulletData.Speed, ForceMode2D.Impulse);
    }

    public override void Copy(ref AbstractFireMarker copyMarker, AbstractFireMarker origin)
    {
        if (origin is DirectionFire originCurve)
        {
            if (copyMarker is DirectionFire outMakerCurve)
            {
                outMakerCurve.Angle = originCurve.Angle;
                copyMarker = outMakerCurve;
            }
        }
    }
}
