using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Movement : MonoBehaviour
{
    public float JumpForce = 1f;
    public float speed = 5f;
    public float Hspeed = 50f;
    public LayerMask GroundLayer = 1;
    public bool key;

    private Rigidbody _rb;
    private CapsuleCollider _collider;

    private bool IsGrounded
    {
        get
        {
            var bottomCenterPoint = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);
            return Physics.CheckCapsule(_collider.bounds.center, bottomCenterPoint, _collider.bounds.size.x / 2 * 0.9f, GroundLayer);
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -Hspeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * Hspeed);
        }
    }
    private void Start()
    {
        _rb= GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        if (GroundLayer == gameObject.layer)
            //Слой сортировки игроков должен отличаться от слоя сортировки земли
            Debug.LogError("Player SortingLayer must be different from Ground SourtingLayer");
    }
    private void FixedUpdate()
    {
        JumpLogic();
        
    }
    private void JumpLogic()
    {
        if (IsGrounded && (Input.GetAxis("Jump") > 0))
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Key"))
        {
            key = true;
            other.gameObject.SetActive(false);
        }
        if (other.tag.Equals("DOOR"))
        {
            if (key)
            {
                Vector3 rotation = other.gameObject.transform.eulerAngles;
                rotation.y = -90;
                other.gameObject.transform.eulerAngles = rotation;
            }
        }
    }
}
