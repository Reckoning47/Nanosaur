using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;


public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField]
    private float normalSensitivity;
    [SerializeField]
    private float aimSensitivity;
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();

    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform bulletProjectile;
    [SerializeField] private Transform spawnBulletPos;

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private Animator animator;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mouseWorldPos = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        Transform hitScanTransform = null;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;

            mouseWorldPos = raycastHit.point;

            hitScanTransform = raycastHit.transform;

        }

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAim = mouseWorldPos;
            worldAim.y = transform.position.y;
            Vector3 aimDirection = (worldAim - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }


        if (starterAssetsInputs.shoot)
        {
            // hitscan shooting
            //if(hitScanTransform != null)
            //{
            //    if (hitTransform.GetComponent<BulletTarget>() != null)
            //    {
            //        // Hit target
            //        Instantiate(vfxHitGreen, raycastHit.point, Quaternion.identity);
            //    }
            //    else
            //    {
            //        // Hit something else
            //        Instantiate(vfxHitRed, raycastHit.point, Quaternion.identity);
            //    }
            //}

            // projectile based shooting
            Vector3 aimDir = (mouseWorldPos - spawnBulletPos.position).normalized;
            Instantiate(bulletProjectile, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));

            starterAssetsInputs.shoot = false;

        }

    }
}
