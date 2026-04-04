using UnityEngine;

public class Hearts : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 3f;
    [SerializeField] private float floatAmplitude = 0.4f;
    [SerializeField] private float boostMultiplier = 2f;
    [SerializeField] private float boostDuration = 2f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        RemoteControlCarController car = other.GetComponent<RemoteControlCarController>();

        if (car != null)
        {
            car.ApplySpeedBoost(boostMultiplier, boostDuration);
            Destroy(gameObject);
        }
    }
}
