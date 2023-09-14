using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways] // to always execute code even not in play mode.
public class CameraRig : MonoBehaviour
{
    [SerializeField] Transform followTransform;
    [SerializeField] float armLength;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform cameraArm;
    [SerializeField] float turnSpeed;

    [SerializeField][Range(0, 1)] float followDamping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        cameraTransform.position = cameraArm.position - cameraTransform.forward * armLength;

        transform.position = Vector3.Lerp(transform.position, followTransform.position, (1 - followDamping) * Time.deltaTime * 20f);
    }
    public void AddYawInput(float amount)
    {
        transform.Rotate(Vector3.up, amount * Time.deltaTime * turnSpeed);
    }
}
