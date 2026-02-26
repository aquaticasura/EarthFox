using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
public class ArmAndGunScript : MonoBehaviour
{
    [SerializeField] private Transform ArmTransgoon;
    private Vector3 worldPos;
    private Camera mainCam;
    private bool isCooldown;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCam == null || ArmTransgoon == null)
        {
            return;
        }

        Vector2 mousePos = Mouse.current.position.ReadValue();
        worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));
        worldPos.z = ArmTransgoon.position.z;
        
    }
    void FixedUpdate()
    {
        if (ArmTransgoon == null)
        {
            return;
        }

        Vector3 dir = worldPos - ArmTransgoon.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        ArmTransgoon.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    void OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed && !isCooldown)
        {
            isCooldown = true;
            StartCoroutine(Cooldown());
        }
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isCooldown = false;
    }

}
