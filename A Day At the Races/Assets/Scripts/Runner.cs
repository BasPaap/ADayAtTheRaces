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

    private Horse horse;
    private RunningPhase currentRunningPhase;
    private float currentRunningPhaseStartTime;
    private NavMeshAgent navMeshAgent;
    private bool isInLastPhase = false;
    private int lapsRun = 0;
    private bool hasStartedRunning;

    public float maxSpeed = 15.0f;

    public Vector3 StartingLinePosition { get; set; }
    public Vector3 FirstCornerPosition { get; set; }
    public Vector3 SecondCornerPosition { get; set; }
    public Vector3 ThirdCornerPosition { get; set; }
    public Vector3 FinishLinePosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(Horse horse, 
        Vector3 startingLinePosition, 
        Vector3 firstCornerPosition, 
        Vector3 secondCornerPosition, 
        Vector3 thirdCornerPosition, 
        Vector3 finishLinePosition)
    {
        this.horse = horse;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        this.navMeshAgent.Warp(transform.position);

        StartingLinePosition = startingLinePosition;
        FirstCornerPosition = firstCornerPosition;
        SecondCornerPosition = secondCornerPosition;
        ThirdCornerPosition = thirdCornerPosition;
        FinishLinePosition = finishLinePosition;

        var cycleOffset = UnityEngine.Random.value;
        SetAnimationControllerCycleOffset(horsePath, cycleOffset);
        SetAnimationControllerCycleOffset(jockeyPath, cycleOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (horse != null)
        {
            UpdateRunningPhase();
            UpdateDestination();
        }       
    }    
        
    private void UpdateDestination()
    {
        if (navMeshAgent != null && !navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            if (!hasStartedRunning && navMeshAgent.speed > 0.0f)
            {
                SetSpeed(0.0f);
            }
            else
            {
                if (Vector3.Distance(navMeshAgent.destination, FirstCornerPosition) < 0.02f)
                {
                    navMeshAgent.destination = SecondCornerPosition;
                }
                else if (Vector3.Distance(navMeshAgent.destination, SecondCornerPosition) < 0.02f)
                {
                    navMeshAgent.destination = ThirdCornerPosition;
                }
                else if (Vector3.Distance(navMeshAgent.destination, ThirdCornerPosition) < 0.02)
                {
                    navMeshAgent.destination = (this.lapsRun < totalLapsToRun - 1) ? FirstCornerPosition : FinishLinePosition;
                    this.lapsRun++;
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
    }
    
    private void UpdateRunningPhase()
    {
        if (hasStartedRunning)
        {
            if (currentRunningPhase == null)
            {
                SetRunningPhase(horse.RunningPhases.First());
            }

            if (!isInLastPhase)
            {
                if (currentRunningPhase.Duration.TotalSeconds <= (Time.time - currentRunningPhaseStartTime))
                {
                    int nextRunningPhaseIndex;

                    if (currentRunningPhase == horse.RunningPhases.Last())
                    {
                        nextRunningPhaseIndex = 0;
                        isInLastPhase = true;
                    }
                    else
                    {
                        nextRunningPhaseIndex = horse.RunningPhases.IndexOf(currentRunningPhase) + 1;
                    }

                    SetRunningPhase(horse.RunningPhases[nextRunningPhaseIndex]);
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
        Debug.Log($"{gameObject.name} speed set to {runningPhase.Speed} for {runningPhase.Duration.TotalSeconds} seconds (index {this.horse.RunningPhases.IndexOf(runningPhase)})");
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
