using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float multiplier = 1.4f;
    public float duration = 4f;

    public GameObject effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }

        IEnumerator Pickup(Collider player)
        {
            Instantiate(effect, transform.position, transform.rotation);

            PlayerMovement speed = player.GetComponent<PlayerMovement>();
            speed.moveSpeed *= multiplier;

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(duration);

            speed.moveSpeed /= multiplier;

            Destroy(gameObject);
        }
    }
}
