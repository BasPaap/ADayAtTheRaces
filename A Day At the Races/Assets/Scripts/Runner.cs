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

    private Horse horse;
    private RunningPhase currentRunningPhase;
    private float currentRunningPhaseStartTime;
    private NavMeshAgent navMeshAgent;
    private bool isInLastPhase = false;
    private int lapsRun = 0;

    public float maxSpeed = 15.0f;

    public Vector3 FirstCornerPosition { get; set; }
    public Vector3 SecondCornerPosition { get; set; }
    public Vector3 ThirdCornerPosition { get; set; }
    public Vector3 FinishLinePosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (horse != null)
        {
            UpdateSpeed();
        }

        UpdateDestination();
    }    
        
    private void UpdateDestination()
    {
        if (navMeshAgent != null && !navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            if (Vector3.Distance(navMeshAgent.destination, FirstCornerPosition) < 0.02f)
            {
                navMeshAgent.destination = SecondCornerPosition;
                //Debug.Log($"{gameObject.name} set to second corner.");
            }
            else if (Vector3.Distance(navMeshAgent.destination, SecondCornerPosition) < 0.02f)
            {
                navMeshAgent.destination = ThirdCornerPosition;
                //Debug.Log($"{gameObject.name} set to third corner.");
            }
            else if (Vector3.Distance(navMeshAgent.destination, ThirdCornerPosition) < 0.02)
            {   
                navMeshAgent.destination = (this.lapsRun < totalLapsToRun - 1) ? FirstCornerPosition : FinishLinePosition;
                this.lapsRun++;
                //Debug.Log($"{gameObject.name} set to finish line.");
            }
        }
    }

    public void Run(Horse horse)
    {
        this.horse = horse;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.Warp(transform.position);
        navMeshAgent.destination = FirstCornerPosition;
    }

    private void UpdateSpeed()
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

    private void SetRunningPhase(RunningPhase runningPhase)
    {
        currentRunningPhaseStartTime = Time.time;
        currentRunningPhase = runningPhase;

        navMeshAgent.speed = currentRunningPhase.Speed * maxSpeed;

        gameObject.GetComponentInChildren<Animator>().SetInteger("SpeedPercentage", (int)(currentRunningPhase.Speed * 100));
        gameObject.GetComponentInChildren<Animator>().SetFloat("AnimationSpeedMultiplier", currentRunningPhase.Speed);

        //navMeshAgent.acceleration *= currentRunningPhase.Speed;
        //navMeshAgent.angularSpeed *= currentRunningPhase.Speed;

        Debug.Log($"{gameObject.name} speed set to {runningPhase.Speed} for {runningPhase.Duration.TotalSeconds} seconds (index {this.horse.RunningPhases.IndexOf(runningPhase)})");
    }
}
