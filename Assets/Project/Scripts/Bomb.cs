using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float duration = 2f;
    public float radius = 3f;
    public float explosionDuration = 0.4f;
    public GameObject explosionModel;

    private float explosionTimer;
    private bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        explosionTimer = duration;
        explosionModel.transform.localScale = Vector3.one * radius;
        explosionModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        explosionTimer -= Time.deltaTime;
        if (!exploded && explosionTimer <= 0f) {
            exploded = true;
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider collider in hitObjects) {
                if (collider.GetComponent<Enemy>() != null) {
                    collider.GetComponent<Enemy>().Hit();
                }
            }
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode() {
        Debug.Log("Bomb->Explode()!");
        explosionModel.SetActive(true);
        yield return new WaitForSeconds(explosionDuration);
        Destroy(gameObject);
    }
}
