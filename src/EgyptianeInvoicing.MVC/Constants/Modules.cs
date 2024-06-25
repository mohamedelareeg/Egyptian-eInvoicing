using System.Collections.Generic;

namespace EgyptianeInvoicing.MVC.Constants
{
    public class ModuleSeeding
    {
        public IEnumerable<Modules> GetModulesList()
        {
            return new List<Modules>
            {

                 new Modules
                {
                    PageName = "Company",
                    RoleName = "Company",
                    Path = "",
                    ControllerName = "Company",
                    ActionName = "Index",
                    Parent = 1,
                    IsShow = 1,
                    FaIcon = "fas fa-building"
                },
                new Modules
                {
                    PageName = "RecentDocument",
                    RoleName = "RecentDocument",
                    Path = "",
                    ControllerName = "Documents",
                    ActionName = "RecentDocument",
                    Parent = 1,
                    IsShow = 1,
                    FaIcon = "far fa-clock"
                },

                new Modules
                {
                    PageName = "SearchDocument",
                    RoleName = "SearchDocument",
                    Path = "",
                    ControllerName = "Documents",
                    ActionName = "SearchDocument",
                    Parent = 1,
                    IsShow = 1,
                    FaIcon = "fas fa-search"
                }

               
            };
        }
    }

    public class Modules
    {
        public long Id { get; set; }
        public string? PageName { get; set; }
        public string RoleName { get; set; }
        public string? Path { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public int Parent { get; set; }
        public byte IsShow { get; set; }
        public string? FaIcon { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsAction { get; set; } = false;
        public List<Modules>? Childs { get; set; }
        public string? Permission { get; set; }
    }
}
