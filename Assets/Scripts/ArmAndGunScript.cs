using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
public class ArmAndGunScript : MonoBehaviour
{
    [SerializeField] private Transform ArmTransgoon;
    [SerializeField] private GameObject Bullet;
    private Vector3 worldPos;
    private Camera mainCam;
    private bool isCooldown;
    public float shootForce = 10f;
    public float muzzleOffset = 0.3f;
    public float bulletDamage = 10f;
    public Vector2 mousePos;

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


        mousePos = Mouse.current.position.ReadValue();
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
    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed && !isCooldown)
        {
            isCooldown = true;
            Vector3 spawnPos = ArmTransgoon.position + ArmTransgoon.right * muzzleOffset;
            GameObject bullet = Instantiate(Bullet, spawnPos, ArmTransgoon.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDamage(bulletDamage);
            Collider2D shooterCollider = GetComponentInParent<Collider2D>();
            bulletScript.IgnoreShooterCollider(shooterCollider);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = ArmTransgoon.right * shootForce;
            
            StartCoroutine(Cooldown());
        }
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isCooldown = false;
    }

}
