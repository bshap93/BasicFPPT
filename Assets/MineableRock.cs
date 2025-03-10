using UnityEngine;

using RayFire;
using UnityEngine.Serialization;

public class MineableRock : MonoBehaviour, IInteractable
{
    private RayfireRigid _rfRigid;
    public float explosionForce = 100f;
    public float explosionRadius = 1f;
    // default value of 1f or no change
    public float successiveExplosionForceMultiplier = 1f;
    public bool fragShouldDestroy; 

    void OnEnable()
    {
        GameObject myGameObject = gameObject;
        _rfRigid = myGameObject.GetComponent<RayfireRigid>();
        _rfRigid.demolitionEvent.LocalEvent += OnFragmentCreated;

    }



    public void Interact()
    {

        if (_rfRigid != null)
        {
            _rfRigid.Demolish(); // Trigger fragmentation instead of just destroying
            
            ApplyExplosionForce();
            
            RaycastHit hit;
            if (Camera.main != null)
            {
                Vector3 rayOrigin = Camera.main.transform.position; // Start from the camera
                Vector3 rayDirection = Camera.main.transform.TransformDirection(Vector3.forward); // Cast forward from camera

                if (Physics.Raycast(rayOrigin, rayDirection, out hit, 5f))
                {
                    RayfireRigid hitFragment = hit.collider.GetComponent<RayfireRigid>();
                    if (hitFragment != null)
                    {
                        Destroy(hitFragment.gameObject);
                        Debug.Log("Destroyed aimed fragment: " + hitFragment.gameObject.name);
                    }
                }
            }



            // if (_rfRigid.fragments.Count > 1)
            // {
            //     // Destroy one at random
            //     Destroy(_rfRigid.fragments[UnityEngine.Random.Range(0, _rfRigid.fragments.Count)].gameObject);
            // }
            
            
            
            
        }


        Debug.Log("Rock mined");
    }

    // This method runs when new fragments are created
    private void OnFragmentCreated(RayfireRigid fragment)
    {
        if (fragment != null)
        {
            var fragments = fragment.fragments;
            foreach (var frag in fragments)
            {
                var rock = frag.gameObject.AddComponent<MineableRock>();
                rock.explosionForce = explosionForce * successiveExplosionForceMultiplier;
                rock.fragShouldDestroy = true;
                
                Debug.Log("Fragment created");
            }

        }
    }
    
    // Adds force to each fragment after demolition
    void ApplyExplosionForce()
    {
        foreach (RayfireRigid fragment in _rfRigid.fragments)
        {
            if (fragment != null && fragment.physics.rb != null)
            {
                Vector3 explosionPos = transform.position; // Center of explosion


                fragment.physics.rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
            }
        }
    }
    
    void ApplyPushForce()
    {
        Vector3 pushDirection = transform.forward; // Pushes fragments forward

        foreach (RayfireRigid fragment in _rfRigid.fragments)
        {
            if (fragment != null && fragment.physics.rb != null)
            {
                fragment.physics.rb.AddForce(pushDirection * 10f, ForceMode.Impulse);
            }
        }
    }
}


public interface IInteractable
{
    void Interact();
}
