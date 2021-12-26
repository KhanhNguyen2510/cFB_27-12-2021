using System.Collections.Generic;

namespace cFB.Data.Entites
{
    public class RoleManager
    {
        public string ManagerId { get; set; }
        public string ManagerName { get; set; }
        public IEnumerable<AdministrativeDivision> AdministrativeDivision { get; set; }
    }
}
