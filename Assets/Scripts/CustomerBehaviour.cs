using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    private GameObject spawn = null;
    private GameObject target = null;

    private bool moving = false;

    private Vector3? movingToVector = null;

    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 2f;        // units per seconde
    [SerializeField] private float stopDistance = 0.1f;

    // Awake i.p.v. Start: setSpawnAndTarget wordt vaak direct na Instantiate aangeroepen, vóór Start
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!moving || movingToVector == null) return;

        Vector3 toTarget = movingToVector.Value - transform.position;
        toTarget.y = 0f; // alleen horizontaal bewegen

        if (toTarget.magnitude <= stopDistance)
        {
            moving = false;
            return;
        }

        // cc.Move(toTarget.normalized * moveSpeed * Time.fixedDeltaTime);
        rb.AddForce(toTarget.normalized * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
        transform.position = Vector3.MoveTowards(transform.position, toTarget.normalized * moveSpeed * Time.fixedDeltaTime, 0.1f);
        // rb.Appl
    }

    public void customerMoveToTarget()
    {
        movingToVector = target.transform.position;
        moving = true;
    }

    public void setSpawnAndTarget(GameObject spawnParam, GameObject targetParam)
    {
        spawn = spawnParam;
        target = targetParam;

        // CharacterController overschrijft transform.position anders soms meteen weer
        // rb.enabled = false;
        transform.position = spawn.transform.position;
        // cc.enabled = true;
    }
}
