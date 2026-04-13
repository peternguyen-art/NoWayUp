using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera mainCamera;
    public Transform followTarget;

    Vector2 startingPos;
    float startingZ;

    Vector2 CameraMoveSinceStart => (Vector2)mainCamera.transform.position - startingPos;
    
    float ZDistanceFromTarget => transform.position.z - followTarget.position.z;

    float ClippingPlane => (mainCamera.transform.position.z + (ZDistanceFromTarget > 0 ? mainCamera.farClipPlane : mainCamera.nearClipPlane));

    float ParallaxFactor => Mathf.Abs(ZDistanceFromTarget) / ClippingPlane;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPos = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = startingPos + CameraMoveSinceStart * ParallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startingZ);
    }
}
