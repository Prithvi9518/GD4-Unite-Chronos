using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    [System.Serializable]
    public class TeamMember
    {
        public string Name;
        public List<DepartmentRole> roles = new();
    }
}
