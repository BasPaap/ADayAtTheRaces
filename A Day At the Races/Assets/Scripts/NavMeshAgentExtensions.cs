using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public static class NavMeshAgentExtensions
    {
        public static bool IsDestination(this NavMeshAgent navMeshAgent, Vector3 position, float precision = 2.0f)
        {
            return Vector3.Distance(navMeshAgent.destination, position) < precision;
        }
    }
}
