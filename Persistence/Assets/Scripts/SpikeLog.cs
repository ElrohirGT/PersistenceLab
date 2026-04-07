using UnityEngine;

public class SpikeLog : MonoBehaviour
{
    [SerializeField] public float secondsAlive = 4;

    private Vector3 _originalPos;
    private float _originalAlive;
    private Rigidbody _rigidBody;

    private void Start()
    {
        _originalAlive = secondsAlive;
        _originalPos = transform.position;
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        secondsAlive -= Time.deltaTime;

        if (secondsAlive <= 0)
        {
            Reset();
        }
    }

    void Reset()
    {
        secondsAlive = _originalAlive;
        transform.position = _originalPos;
        transform.rotation = new Quaternion();
        _rigidBody.angularVelocity = Vector3.zero;
        _rigidBody.linearVelocity = Vector3.zero;
    }
}
