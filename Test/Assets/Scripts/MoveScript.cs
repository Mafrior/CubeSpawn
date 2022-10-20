using UnityEngine;

public class MoveScript : MonoBehaviour
{
    Vector3 direction = Vector3.right;

    [HideInInspector]
    public float speed;

    [HideInInspector]
    public float distance;

    private Vector3 startposition;

    private void Awake() {
        startposition = transform.position;
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }

    // move the cube untill it covers entire distance
    private void FixedUpdate() {
        if (transform.position.x - startposition.x < distance) {
            GetComponent<Rigidbody>().MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
            return;
        }
        // and then destroy
        Destroy(gameObject, 2);
    }
}
