using UnityEngine;

public class ObjectMagnet : MonoBehaviour {
    public float pullRadius;
    public float pullForce;

	void FixedUpdate () {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)){
            
            if (collider.CompareTag("lightorb")){

                Debug.Log(collider);
                // calculate direction from target to me
                Vector3 forceDirection = transform.position - collider.transform.position;

                // apply force on target towards me
                collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
            }
        }
    }
}
