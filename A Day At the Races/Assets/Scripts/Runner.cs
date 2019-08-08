using Assets.Scripts;
using Bas.ADayAtTheRaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Runner : MonoBehaviour
{
    private const int totalLapsToRun = 2;
    private const string horsePath = "Horse_With_Jockey_PBR/Horse_PBR/horse";
    private const string jockeyPath = "Horse_With_Jockey_PBR/Jockey_PBR/jockey";
        
    private RunningPhase currentRunningPhase;
    private float currentRunningPhaseStartTime;
    private NavMeshAgent navMeshAgent;
    private bool isInLastPhase = false;
    private bool isFinished = false;
    private int lapsRun = 0;
    private bool hasStartedRunning;

    private TimeSpan trotAwayTime = TimeSpan.Zero;
    private bool IsTimeToTrotAway => this.trotAwayTime != TimeSpan.Zero && this.trotAwayTime <= DateTime.Now.TimeOfDay;
    private bool IsNearDestination => navMeshAgent != null && !navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f;

    public float maxSpeed = 15.0f;

    public Horse Horse { get; private set; }
    public Vector3 StartingLinePosition { get; set; }
    public Vector3 FirstCornerPosition { get; set; }
    public Vector3 SecondCornerPosition { get; set; }
    public Vector3 ThirdCornerPosition { get; set; }
    public Vector3 FinishLinePosition { get; set; }
    public Vector3 ExitPosition { get; set; }

    public event EventHandler ArrivedAtStartingLine;
    public event EventHandler Finished;
    public event EventHandler ArrivedAtExitPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(Horse horse, 
        Vector3 startingLinePosition, 
        Vector3 firstCornerPosition, 
        Vector3 secondCornerPosition, 
        Vector3 thirdCornerPosition, 
        Vector3 finishLinePosition,
        Vector3 exitPosition)
    {
        this.Horse = horse;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        this.navMeshAgent.Warp(transform.position);

        StartingLinePosition = startingLinePosition;
        FirstCornerPosition = firstCornerPosition;
        SecondCornerPosition = secondCornerPosition;
        ThirdCornerPosition = thirdCornerPosition;
        FinishLinePosition = finishLinePosition;
        ExitPosition = exitPosition;

        var cycleOffset = UnityEngine.Random.value;
        SetAnimationControllerCycleOffset(horsePath, cycleOffset);
        SetAnimationControllerCycleOffset(jockeyPath, cycleOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (Horse != null)
        {
            UpdateRunningPhase();
            UpdateDestination();

            if (IsTimeToTrotAway)
            {
                TrotAway();
            }
        }       
    }    
    
    private void UpdateDestination()
    {
        if (IsNearDestination)
        {
            if (!hasStartedRunning && navMeshAgent.speed > 0.0f)
            {
                SetSpeed(0.0f);
                ArrivedAtStartingLine?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if (navMeshAgent.IsDestination(FirstCornerPosition))
                {
                    navMeshAgent.destination = SecondCornerPosition;
                }
                else if (navMeshAgent.IsDestination(SecondCornerPosition))
                {
                    navMeshAgent.destination = ThirdCornerPosition;
                }
                else if (navMeshAgent.IsDestination(ThirdCornerPosition))
                {
                    navMeshAgent.destination = (this.lapsRun < totalLapsToRun - 1) ? FirstCornerPosition : FinishLinePosition;
                    this.lapsRun++;
                }
                else if (navMeshAgent.IsDestination(FinishLinePosition, 0.05f))
                {
                    if (!this.isFinished)
                    {
                        this.isFinished = true;
                        Finished?.Invoke(this, EventArgs.Empty);
                        this.trotAwayTime = DateTime.Now.TimeOfDay + TimeSpan.FromSeconds(0.5);
                    }                    
                }
                else if (navMeshAgent.IsDestination(ExitPosition))
                {
                    ArrivedAtExitPosition?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public void WalkToStartingLine()
    {
        navMeshAgent.destination = new Vector3(StartingLinePosition.x - 1.1f, transform.position.y, transform.position.z);
        SetSpeed(0.1f);
    }

    public void Run()
    {
        hasStartedRunning = true;
        navMeshAgent.destination = FirstCornerPosition;

        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void TrotAway()
    {
        this.trotAwayTime = TimeSpan.Zero;
        navMeshAgent.destination = ExitPosition;
        SetSpeed(0.3f);

        var audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }
    
    private void UpdateRunningPhase()
    {
        if (hasStartedRunning)
        {
            if (currentRunningPhase == null)
            {
                SetRunningPhase(Horse.RunningPhases.First());
            }

            if (!isInLastPhase)
            {
                if (currentRunningPhase.Duration.TotalSeconds <= (Time.time - currentRunningPhaseStartTime))
                {
                    int nextRunningPhaseIndex;

                    if (currentRunningPhase == Horse.RunningPhases.Last())
                    {
                        nextRunningPhaseIndex = 0;
                        isInLastPhase = true;
                    }
                    else
                    {
                        nextRunningPhaseIndex = Horse.RunningPhases.IndexOf(currentRunningPhase) + 1;
                    }

                    SetRunningPhase(Horse.RunningPhases[nextRunningPhaseIndex]);
                }
            }
        }        
    }
        
    private void SetSpeed(float relativeSpeed)
    {
        navMeshAgent.speed = relativeSpeed * maxSpeed;
        SetAnimationControllerSpeedParameters(horsePath, relativeSpeed);
        SetAnimationControllerSpeedParameters(jockeyPath, relativeSpeed);
    }

    private void SetRunningPhase(RunningPhase runningPhase)
    {
        currentRunningPhaseStartTime = Time.time;
        currentRunningPhase = runningPhase;

        SetSpeed(currentRunningPhase.Speed);        
        Debug.Log($"{gameObject.name} speed set to {runningPhase.Speed} for {runningPhase.Duration.TotalSeconds} seconds (index {this.Horse.RunningPhases.IndexOf(runningPhase)})");
    }

    private void SetAnimationControllerSpeedParameters(string path, float relativeSpeed)
    {
        var animator = gameObject.transform.Find(path).GetComponentInChildren<Animator>();
        animator.SetInteger("SpeedPercentage", (int)(relativeSpeed * 100));
        animator.SetFloat("AnimationSpeedMultiplier", relativeSpeed);
    }

    private void SetAnimationControllerCycleOffset(string path, float cycleOffset)
    {
        var animator = gameObject.transform.Find(path).GetComponentInChildren<Animator>();
        animator.SetFloat("CycleOffset", cycleOffset);
    }
}
