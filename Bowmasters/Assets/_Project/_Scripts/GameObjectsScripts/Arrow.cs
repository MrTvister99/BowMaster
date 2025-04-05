using Spine.Unity;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _force;
    private bool hasHit;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (hasHit == false)
        {
            float angle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void Shoot(Vector2 direction, float force)
    {
        _rigidbody.velocity = direction * force;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasHit = true;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.isKinematic = false;
    }
}
