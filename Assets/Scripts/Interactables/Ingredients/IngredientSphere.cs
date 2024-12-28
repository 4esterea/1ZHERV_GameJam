using UnityEngine;

public class IngredientSphere : Ingredient
{
    private SphereCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    protected override void Update()
    {
        base.Update();
        Interaction();
    }

     public override void Interaction()
    {
        if (_isHeld)
        {
            _rb.isKinematic = true;
            _collider.enabled = false;
            transform.position = _player.transform.position + holdingOffset + _player.transform.forward;
            Quaternion rotation = Quaternion.LookRotation(_player.transform.forward, Vector3.up);
            transform.rotation = rotation;
        }
        else
        {
            _collider.enabled = true;
            _rb.isKinematic = false;
            if (!_wasTossed)
            {
                _rb.AddForce((transform.forward + Vector3.up*2) * 6f, ForceMode.Impulse);
                _wasTossed = true;
            }
        }
    }
}
