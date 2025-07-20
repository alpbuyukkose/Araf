using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private float rotateSpeed = 180f;

    void Update()
    {
        Vector3 targetDirection = Vector3.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        Vector3 currentEuler = arrow.rotation.eulerAngles;
        Vector3 targetEuler = targetRotation.eulerAngles;

        float newY = Mathf.MoveTowardsAngle(currentEuler.y, targetEuler.y, rotateSpeed * Time.deltaTime);
        arrow.rotation = Quaternion.Euler(currentEuler.x, newY, currentEuler.z);
    }
}
