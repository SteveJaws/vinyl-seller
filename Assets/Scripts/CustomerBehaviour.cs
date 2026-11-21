using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    private GameObject spawn = null;
    private GameObject target = null;

    private bool moving = false;

    private Vector3? movingToVector = null;

    private CharacterController cc;

    [SerializeField] private float moveSpeed = 2f;        // units per seconde
    [SerializeField] private float stopDistance = 0.1f;

    // Awake i.p.v. Start: setSpawnAndTarget wordt vaak direct na Instantiate aangeroepen, vóór Start
    void Awake()
    {
        cc = GetComponent<CharacterController>();
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

        cc.Move(toTarget.normalized * moveSpeed * Time.fixedDeltaTime);
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
        cc.enabled = false;
        transform.position = spawn.transform.position;
        cc.enabled = true;
    }
}
