using UnityEngine;

public static class Helpers 
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    
    public static Vector3 MouseToWorldPostion()
    {
        var _mouseInput = Input.mousePosition;

        var ray = Camera.main.ScreenPointToRay(_mouseInput);

        var planeVector = new Vector3(0, 1.4f, 0);
        
        var groundPlane = new Plane(Vector3.up, planeVector);

        if (groundPlane.Raycast(ray, out float distance))
        {
            var pos = ray.GetPoint(distance);
            
            return pos;
        }

        return Vector3.zero;
    }
}