using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f, attackDelay = 1f;

    [SerializeField]
    private float attackDistance = 1.5f;

    //Inputs sent from the Enemy AI to the Enemy controller
    //public UnityEvent OnAttackPressed;
    //public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    private ContextSolver movementDirectionSolver;
    [SerializeField]
    private Transform weapon;
    [SerializeField]
    private Transform slash;
    private NpcCell cell;
    bool following = false;

    private void Start()
    {
        //Detecting Player and Obstacles around
        InvokeRepeating("PerformDetection", 0.1f, detectionDelay);
        cell = GetComponent<NpcCell>();
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    public void HandleUpdate()
    {
        //Enemy AI movement based on Target availability
        if (aiData.currentTarget != null)
        {
            //Looking at the Target
            if (following == false)
            {
                following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aiData.GetTargetsCount() > 0)
        {
            //Target acquisition logic
            aiData.currentTarget = aiData.targets[0];
        }
        //Moving the Agent
        cell.MovementInput(movementInput);
        //rb2.velocity = movementInput * speed * Time.deltaTime;
    }

    private IEnumerator ChaseAndAttack()
    {
        if (aiData.currentTarget == null)
        {
            //Stopping Logic
            movementInput = Vector2.zero;
            following = false;
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            if (distance < attackDistance)
            {
                //Attack logic
                movementInput = Vector2.zero;
                cell.Attack();
                //float anglez = Vector2.Angle(aiData.currentTarget.position, -Vector2.up);
                //VisualEffect ve = slash.GetComponent<VisualEffect>();
                //if (aiData.currentTarget.position.x < transform.position.x)
                //{
                //    ve.SetVector3("InitializeAngle", new Vector3(0, 0, -anglez));
                //    weapon.eulerAngles = new Vector3(0, 0, -anglez);
                //}
                //else
                //{
                //    ve.SetVector3("InitializeAngle", new Vector3(0, 0, anglez));
                //    weapon.eulerAngles = new Vector3(0, 0, anglez);
                //}
                //ve.Play();
                //weapon.up = movementInput;
                yield return new WaitForSeconds(attackDelay);
                StartCoroutine(ChaseAndAttack());
            }
            else
            {
                //Chase logic
                movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
                //weapon.up = movementInput;
                
                
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(ChaseAndAttack());
            }
        }

    }
}
