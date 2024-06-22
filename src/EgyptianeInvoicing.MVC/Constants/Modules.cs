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
                    PageName = "Dashboard",
                    RoleName = "Dashboard",
                    Path = "",
                    ControllerName = "Home",
                    ActionName = "Index",
                    Parent = 1,
                    IsShow = 1,
                    FaIcon = "fas fa-tachometer-alt"
                },
                new Modules
                {
                    PageName = "Documents",
                    RoleName = "Documents",
                    Path = "/Documents/Index", 
                    ControllerName = "Documents",
                    ActionName = "Index",
                    Parent = 1,
                    IsShow = 1,
                    FaIcon = "far fa-file-alt"
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
