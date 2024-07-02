using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;

namespace EgyptianeInvoicing.WPF
{
    public partial class HomeWindow
    {
        public class MenuItem
        {
            public string Name { get; set; }
            public string Identifier { get; set; }
            public PackIconKind Icon { get; set; }
            public ObservableCollection<MenuItem> Children { get; set; } = new ObservableCollection<MenuItem>();
            public string RequiredClaim { get; set; }
            public bool IsClickable { get; set; } = true;
        }
    }
}
