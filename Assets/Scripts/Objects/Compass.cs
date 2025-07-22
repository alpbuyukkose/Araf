using UnityEngine;

public class Compass : MonoBehaviour
{
    [Header("Rotation Ring Control")]
    [SerializeField] private Transform rotatingRing;
    [SerializeField] private float rotationSpeed = 5f;
    private Transform playerTransform;
    private float targetY = 180f;
    private float easedStep;

    [Header("Arrow Head Control")]
    [SerializeField] private Transform arrowPivot;
    [SerializeField] private float pivotReturnSpeed = 5f;

    private float lastYaw;
    private float currentArrowY = 270f;
    private float desiredY = 270f;

    void Start()
    {
        if (playerTransform == null)
            playerTransform = GameObject.FindWithTag("Player").transform;

        lastYaw = playerTransform.eulerAngles.y;
    }

    void Update()
    {
        HandleRotationRing();
        HandleArrowHead();
    }

    private void HandleArrowHead()
    {
        float currentYaw = playerTransform.eulerAngles.y;
        float deltaYaw = Mathf.DeltaAngle(lastYaw, currentYaw);
        lastYaw = currentYaw;

        //Debug.Log(deltaYaw);

        if (deltaYaw > .5f)
        {
            if (deltaYaw > 5f)
            {
                desiredY = 250f;
            }
            else
            {
                desiredY = 260f;
            }
        }
        else if (deltaYaw < -.5f)
        {
            if (deltaYaw > -5f)
            {
                desiredY = 280f;
            }
            else
            {
                desiredY = 290f;
            }
        }
        else
        {
            desiredY = 270f;
        }

        currentArrowY = Mathf.MoveTowardsAngle(currentArrowY, desiredY, pivotReturnSpeed * Time.deltaTime);
        arrowPivot.localRotation = Quaternion.Euler(0f, currentArrowY, 0f);
    }

    private void HandleRotationRing()
    {
        float playerYaw = playerTransform.eulerAngles.y;
        // 0 is always means north, 90 is east, 180 is south, 270 is west. // This rotation is clockwise

        float targetY = -playerYaw + 180f; // 180 is for fixing north dir. -playerYaw is for rotatingRing is rotating counterclockwise

        float currentY = rotatingRing.localEulerAngles.y;
        // 180 is north, 90 is east, 0 is south, 270 is west. // This rotation is counter clock wise

        float angleDiff = Mathf.DeltaAngle(currentY, targetY); // angle diff of two vector  // like +70 or -70

        easedStep = rotationSpeed * Time.deltaTime * CustomEase(Mathf.Abs(angleDiff) / 180f);

        float newY = Mathf.MoveTowardsAngle(currentY, targetY, easedStep);
        rotatingRing.localRotation = Quaternion.Euler(0f, newY, 0f);
    }

    #region Easing Algorithms
    private float EaseInOutQuart(float t)
    {
        return t < 0.5f
            ? 8f * t * t * t * t
            : 1f - Mathf.Pow(-2f * t + 2f, 4f) / 2f;
    }

    private float EaseInOutSine(float t)
    {
        return -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f;
    }

    private float EaseMild(float t)
    {
        return Mathf.Lerp(t, t * t * (3f - 2f * t), 0.5f); // %50 smoothstep
    }

    private float CustomEase(float t)
    {
        if (t < 0.1f)
        {
            // Ease-in bölgesi: smoothstep aralığı
            float localT = t / 0.1f; // normalize to 0–1
            return localT * localT * (3f - 2f * localT) * 0.1f; // scale to 0–0.1
        }
        else if (t > 0.9f)
        {
            // Ease-out bölgesi: smoothstep tersten
            float localT = (t - 0.9f) / 0.1f; // normalize to 0–1
            float eased = localT * localT * (3f - 2f * localT); // smoothstep
            return 0.9f + eased * 0.1f;
        }
        else
        {
            // Orta bölge: linear
            return t;
        }
    }
    #endregion
}
