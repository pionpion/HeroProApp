using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

using Microsoft.Phone.Controls;

using MyToolkit.Paging;

using AppStudio.Data;
using AppStudio.Services;

namespace AppStudio
{
    public partial class GoProTipsDetail
    {
        private bool _isDeepLink = false;

        public GoProTipsDetail()
        {
            InitializeComponent();
        }

        public GoProTipsViewModel GoProTipsModel { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GoProTipsModel = NavigationServices.CurrentViewModel as GoProTipsViewModel;
            if (e.NavigationMode == NavigationMode.New && NavigationContext.QueryString.ContainsKey("id"))
            {
                string id = NavigationContext.QueryString["id"];
                if (!String.IsNullOrEmpty(id))
                {
                    _isDeepLink = true;
                    GoProTipsModel = new GoProTipsViewModel();
                    NavigationServices.CurrentViewModel = GoProTipsModel;
                    GoProTipsModel.LoadItem(id);
                }
            }
            if (GoProTipsModel != null)
            {
                GoProTipsModel.ViewType = ViewTypes.Detail;
            }
            DataContext = GoProTipsModel;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationServices.CurrentViewModel = null;
            }
            SpeechServices.Stop();
            base.OnNavigatedFrom(e);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (_isDeepLink)
            {
                _isDeepLink = false;
                NavigationServices.NavigateToPage("MainPage");
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpeechServices.Stop();
        }
    }
}