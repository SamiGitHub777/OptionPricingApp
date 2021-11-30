using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;

namespace WPFHelper
{
    public abstract class HamburgerMenuItemViewModelBase
    {
        public HamburgerMenuItemCollection MenuItems { get; set; }
        public HamburgerMenuItemCollection MenuOptionItems { get; set; }

        public string Title { get; set; }
        public string AppVersion { get; set; }
        public string Author { get; set; }
        public HamburgerMenuItemViewModelBase()
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();
        }

        protected void AddHamburgerMenuIconItem(string label, string toolTip, PackIconMaterialKind icon, object tag)
        {
            MenuItems.Add(new HamburgerMenuIconItem
            {
                Icon = new PackIconMaterial { Kind = icon },
                Label = label,
                ToolTip = toolTip,
                Tag = tag
            });
        }

        protected void AddHamburgerMenuIconOptionItem(string label, string toolTip, PackIconMaterialKind icon, object tag)
        {
            MenuOptionItems.Add(new HamburgerMenuIconItem
            {
                Icon = new PackIconMaterial { Kind = icon },
                Label = label,
                ToolTip = toolTip,
                Tag = tag
            });
        }
    }
}
