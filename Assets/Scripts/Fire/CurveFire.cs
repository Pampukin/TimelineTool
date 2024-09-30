using System.ComponentModel;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

[System.Serializable]
[DisplayName("Fire/CurveFire")]
public class CurveFire : AbstractFireMarker
{
    public Direction Direction;
    public override void Fire()
    {
        base.Fire();

        var startPos = GameObject.FindWithTag("Enemy").transform;
        var bulletObject = Instantiate(_bullet, startPos.position, Quaternion.identity);
        var curveMonoBehaviour = bulletObject.AddComponent<CurveMonoBehaviour>();
        curveMonoBehaviour.CurvePower = _bulletData.CurvePower;
        curveMonoBehaviour.Direction = Direction;
        curveMonoBehaviour.Initialize();
        bulletObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1 * _bulletData.Speed), ForceMode2D.Impulse);
    }

    public override void Copy(ref AbstractFireMarker copyMarker, AbstractFireMarker origin)
    {
        if (origin is CurveFire originCurve)
        {
            if (copyMarker is CurveFire outMakerCurve)
            {
                outMakerCurve.Direction = originCurve.Direction;
                copyMarker = outMakerCurve;
            }
        }
    }
}

public class CurveMonoBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float CurvePower;
    public Direction Direction;
    private Vector2 _direction;
    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        switch (Direction)
        {
            case Direction.None:
                _direction = Vector2.zero;
                break;
            case Direction.Left:
                _direction = Vector2.left;
                break;
            case Direction.Right:
                _direction = Vector2.right;
                break;
        }
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_direction * CurvePower);
    }
}

public enum Direction
{
    None,
    Left,
    Right,
}
