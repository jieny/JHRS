using CommonServiceLocator;
using JHRS.Constants;
using JHRS.Core.Events;
using Prism.Events;
using Prism.Ioc;
using Prism.Navigation.Regions;
using System.Windows;
using System.Windows.Controls;

namespace JHRS.Shell.Views.Dialogs
{
    /// <summary>
    /// CommonDialogPage.xaml 的交互逻辑
    /// </summary>
    public partial class CommonDialogPage : Page
    {
        public CommonDialogPage(IContainerProvider containerProvider)
        {
            InitializeComponent();

            RegionManager.SetRegionName(pages, RegionNames.DialogRegin);
            IRegionManager manager = ServiceLocator.Current.GetInstance<IRegionManager>();
            manager.Regions.Remove("DialogRegin");
            RegionManager.SetRegionManager(pages, manager);

            ConstrolStateEvent controlEvent = containerProvider.Resolve<IEventAggregator>().GetEvent<ConstrolStateEvent>();
            controlEvent.Subscriptions.Clear();
            controlEvent.Subscribe((state) => { SaveButton.IsEnabled = state.IsEnabled; });

            DisableDialogPageButtonEvent disableEvent = containerProvider.Resolve<IEventAggregator>().GetEvent<DisableDialogPageButtonEvent>();
            disableEvent.Subscriptions.Clear();
            disableEvent.Subscribe(() => { saveArea.Visibility = Visibility.Collapsed; });
        }
    }
}
