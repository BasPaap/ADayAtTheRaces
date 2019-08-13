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
    private float firstLapSpeed = 1.0f;
    private float secondLapSpeed = 1.0f;
    private Vector3 startingLinePosition;
    private Vector3 firstCornerPosition;
    private Vector3 secondCornerPosition;
    private Vector3 thirdCornerPosition;
    private Vector3 finishLinePosition;
    private Vector3 exitPosition;
    private TimeSpan trotAwayTime = TimeSpan.Zero;

    private bool IsTimeToTrotAway => this.trotAwayTime != TimeSpan.Zero && this.trotAwayTime <= DateTime.Now.TimeOfDay;
    private bool IsNearDestination => navMeshAgent != null && !navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f;
        
    public Horse Horse { get; private set; }

    public float maxSpeed = 15.0f;

    public event EventHandler ArrivedAtStartingLine;
    public event EventHandler Finished;
    public event EventHandler ArrivedAtExitPosition;
    
    public void Initialize(Horse horse, 
        float firstLapSpeed,
        float secondLapSpeed,
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
        this.firstLapSpeed = firstLapSpeed;
        this.secondLapSpeed = secondLapSpeed;

        Debug.Log($"{horse.Name} set with first lap speed of {firstLapSpeed} and second lap speed of {secondLapSpeed}.");

        this.startingLinePosition = startingLinePosition;
        this.firstCornerPosition = firstCornerPosition;
        this.secondCornerPosition = secondCornerPosition;
        this.thirdCornerPosition = thirdCornerPosition;
        this.finishLinePosition = finishLinePosition;
        this.exitPosition = exitPosition;

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
                if (navMeshAgent.IsDestination(firstCornerPosition))
                {
                    if (this.lapsRun > 0)
                    {
                        Debug.Log($"Lap! setting next lap speed to {secondLapSpeed}");
                        SetSpeed(currentRunningPhase.Speed * secondLapSpeed);
                    }

                    navMeshAgent.destination = secondCornerPosition;
                }
                else if (navMeshAgent.IsDestination(secondCornerPosition))
                {
                    navMeshAgent.destination = thirdCornerPosition;
                }
                else if (navMeshAgent.IsDestination(thirdCornerPosition))
                {
                    navMeshAgent.destination = (this.lapsRun < totalLapsToRun - 1) ? firstCornerPosition : finishLinePosition;
                    this.lapsRun++;
                }
                else if (navMeshAgent.IsDestination(finishLinePosition, 0.05f))
                {
                    if (!this.isFinished)
                    {
                        Finish();
                    }
                }
                else if (navMeshAgent.IsDestination(exitPosition))
                {
                    ArrivedAtExitPosition?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    private void Finish()
    {
        this.isFinished = true;
        Finished?.Invoke(this, EventArgs.Empty);
        this.trotAwayTime = DateTime.Now.TimeOfDay + TimeSpan.FromSeconds(0.5);
    }

    public void WalkToStartingLine()
    {
        navMeshAgent.destination = new Vector3(startingLinePosition.x - 1.1f, transform.position.y, transform.position.z);
        SetSpeed(0.1f);
    }

    public void Run()
    {
        hasStartedRunning = true;
        navMeshAgent.destination = firstCornerPosition;

        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void TrotAway()
    {
        this.trotAwayTime = TimeSpan.Zero;
        navMeshAgent.destination = exitPosition;
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

        var adjustedSpeed = currentRunningPhase.Speed * (lapsRun == 0 ? firstLapSpeed : secondLapSpeed);
        SetSpeed(adjustedSpeed);        
        Debug.Log($"{gameObject.name} speed set to {adjustedSpeed} for {runningPhase.Duration.TotalSeconds} seconds (index {this.Horse.RunningPhases.IndexOf(runningPhase)})");
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
