using System.Collections.Generic;

namespace Unite.Team
{
    [System.Serializable]
    public class TeamMember
    {
        public string Name;
        public List<DepartmentRole> roles = new();
    }
}
