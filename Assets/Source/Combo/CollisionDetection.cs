using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Vector3 _size;

    private List<Collider> _damagedColliders;
    private bool _isDetecting;

    private void Awake()
    {
        _damagedColliders = new List<Collider>();
        _isDetecting = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_damagedColliders.Count <= 0)
                Debug.Log("No collisions");
            else
                foreach (var collider in _damagedColliders)
                    Debug.Log(collider.name);
        }
        else if (Input.GetKeyDown(KeyCode.C))
            _damagedColliders.Clear();
    }

    private void FixedUpdate()
    {
        if (_isDetecting == false)
            return;

        StartDetect();
    }

    public void StartDetect()
    {
        _isDetecting = true;

        Collider[] collidersToDamage = Physics.OverlapBox(transform.position, _size, transform.rotation, _layerMask);

        for (int i = 0; i < collidersToDamage.Length; i++)
        {
            if (_damagedColliders.Contains(collidersToDamage[i]) == false)
                _damagedColliders.Add(collidersToDamage[i]);
        }
    }

    public void StopDetect()
    {
        _isDetecting = false;
        _damagedColliders.Clear();
    }

    private void OnDrawGizmos()
    {
        const float multiplier = 2.0f;
        Gizmos.color = Color.red;
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, _size * multiplier);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = oldMatrix;
    }
}