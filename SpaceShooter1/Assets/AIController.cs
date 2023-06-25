using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol,
            PointsPatrol
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;
        [SerializeField] private AIPointPatrol m_PatrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private Projectile m_ProjectileBase;

        [SerializeField] private float m_EvadeRayLength;
        private Rigidbody2D m_Rigid;

        private SpaceShip m_SpaceShip;


        private Vector3 m_MovePosition;

        private Destructible m_SelectedTarget;
        private Timer RandomizeDirectionTimer;
        private Timer FireTimer;
        private Timer FindNewTargetTimer;
        private Timer ByPassing;
        [SerializeField] private float TimeByPassing;

        private void Start()
        {
            m_PatrolPoint = FindObjectOfType<AIPointPatrol>();
            m_Rigid = GetComponent<Rigidbody2D>();
            m_SpaceShip = GetComponent<SpaceShip>();
            InitTimers();
            
        }

        private void Update()
        {
            UpdateTimers();
            UpdateAI();
        }
        
        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Null)
            {

            }

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
            if(m_AIBehaviour==AIBehaviour.PointsPatrol)
            {
                UpdateBehaviourPatrol();
            }
        }

       
        private void UpdateBehaviourPatrol()
        {
            ActionEvadeCollision();
            ActionFindNewPosition();
            AcrionControllShip();
            ActionFindNewAttackTarget();
            ActionFire();
            
        }
        private int numpoint;

        private void UpdatePoint()
        {
                numpoint++;
                if (numpoint == m_PatrolPoint.TargetPoint.Length) numpoint = 0;
            
        }
        private void MakeLead()
        {
            float projectileVelocity = m_ProjectileBase.Velocity;

            float dist = Vector3.Distance(m_SelectedTarget.transform.position, transform.position);
            float timePJcurrent = dist / projectileVelocity;
            Vector3 futuredir = m_SelectedTarget.transform.position + (m_SelectedTarget.transform.up * m_SelectedTarget.GetComponent<Rigidbody2D>().velocity.magnitude * timePJcurrent);
            float nextdist = Vector3.Distance(futuredir, transform.position);

            Vector3 puintfuture = m_SelectedTarget.transform.position + (m_SelectedTarget.transform.up * m_SelectedTarget.GetComponent<Rigidbody2D>().velocity.magnitude /**  time*/);
            m_MovePosition = puintfuture;
        }
        private void ActionFindNewPosition()
        {
            if (m_AIBehaviour == AIBehaviour.PointsPatrol)
            {
                if (m_SelectedTarget != null)
                {
                    
                    MakeLead();
                }
                else 
                {
                    if (ByPassing.IsFinished)
                    {
                        m_MovePosition = m_PatrolPoint.TargetPoint[numpoint].position;


                        if (Vector3.Distance(m_MovePosition, transform.position) <= 0.5f)
                        {
                            UpdatePoint();
                        }
                    }
                }
            }
            if(m_AIBehaviour==AIBehaviour.Patrol)
            {
                if(m_SelectedTarget!=null)
                {
                    
                        MakeLead();
                    
                }
                else
                {
                    if(m_PatrolPoint!=null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;
                        if(isInsidePatrolZone==true)
                        {
                            if (RandomizeDirectionTimer.IsFinished==true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;
                                m_MovePosition = newPoint;
                                RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }
        }

        private void ActionEvadeCollision()
        {
            if(Physics2D.Raycast(transform.position,transform.up,m_EvadeRayLength)==true)
            {
                m_MovePosition = transform.position + transform.right * 500.0f;
                ByPassing.Start(TimeByPassing);
            }
        }
       private void AcrionControllShip()
       {
            m_SpaceShip.thrustControl = m_NavigationLinear;

            m_SpaceShip.TorgueControl= ComputeAliginTorgueNormalized(m_MovePosition,m_SpaceShip.transform)*m_NavigationLinear;
       }
        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAliginTorgueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;
            return -angle;
        }
        private void ActionFindNewAttackTarget()
        {
            if (FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestructibleTarget();
                FindNewTargetTimer.Start(m_ShootDelay);
            }
        }
       private void ActionFire()
       {
            if(m_SelectedTarget!=null)
            {
                if(FireTimer.IsFinished==true)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);
                    FireTimer.Start(m_ShootDelay);
                }
            }
       }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue;
            Destructible potentialTarget = null;
            foreach(var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;
                if (v.TeamId == Destructible.TeamIdNeutral) continue;
                if (v.TeamId == m_SpaceShip.TeamId) continue;
                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);
                if(dist<maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }
            return potentialTarget;
        }

        #region Timers
        private void InitTimers()
        {
            RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            FireTimer = new Timer(m_ShootDelay);
            FindNewTargetTimer = new Timer(m_FindNewTargetTime);
            ByPassing = new Timer(TimeByPassing);
        }
        
        private void UpdateTimers()
        {
            RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            FireTimer.RemoveTime(Time.deltaTime);
            FindNewTargetTimer.RemoveTime(Time.deltaTime);
            ByPassing.RemoveTime(Time.deltaTime);

        }

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }
        #endregion
    }
}