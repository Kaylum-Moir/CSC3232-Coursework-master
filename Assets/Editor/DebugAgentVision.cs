using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AISensor))]
public class DebugAgentVision : Editor
{
    private void OnSceneGUI()
    {
        AISensor sensor = (AISensor)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(sensor.transform.position, Vector3.up, Vector3.forward, 360, sensor.maxRadius);

        Vector3 viewAngleLeft = AngleDirection(sensor.transform.eulerAngles.y, -sensor.maxAngle / 2);
        Vector3 viewAngleRight = AngleDirection(sensor.transform.eulerAngles.y, sensor.maxAngle / 2);

        Handles.color = Color.red;
        Handles.DrawLine(sensor.transform.position, sensor.transform.position + viewAngleLeft * sensor.maxRadius);
        Handles.DrawLine(sensor.transform.position, sensor.transform.position + viewAngleRight * sensor.maxRadius);
    }

    private Vector3 AngleDirection(float eulerY, float angle)
    {
        angle += eulerY;

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
