using EgyptianeInvoicing.Signer.Services.Abstractions;
using EgyptianeInvoicing.WPF.Pages;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace EgyptianeInvoicing.WPF
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private readonly ITokenSigner _tokenSigner;

        private ObservableCollection<string> messages;
        private DispatcherTimer timer;
        public HomeWindow(ITokenSigner tokenSigner)
        {

            InitializeComponent();
            _ = SetupMenu();
            InitializeMessages();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
            _tokenSigner = tokenSigner;
        }
        private void InitializeMessages()
        {

            messages = new ObservableCollection<string>
            {
            "مرحبًا بك في نظام فاتورة الكترونية الخاص بنا. يرجى الاتصال بفريق الدعم إذا كنت بحاجة إلى مساعدة.",
            "تمت معالجة الطلب بنجاح. لا تتردد في إرسال أي استفسارات أو ملاحظات.",
            "شكرًا لاستخدامك نظام فاتورة الكترونية الخاص بنا. نتطلع إلى خدمتك مرة أخرى قريبًا.",

            };




        }

        private Random random = new Random();

        private void Timer_Tick(object sender, EventArgs e)
        {
            int randomIndex = random.Next(messages.Count);

            MessageSlider.Text = messages[randomIndex];
        }

        public async Task SetupMenu()
        {
            var companies = new MenuItem { Name = "الشركات", Identifier = "companies", Icon = PackIconKind.Domain, IsClickable = true };
            var bulkImport = new MenuItem { Name = "استيراد Excel", Identifier = "bulkImport", Icon = PackIconKind.FileUpload, IsClickable = true };
            var recentInvoices = new MenuItem { Name = "الفواتير", Identifier = "recentInvoices", Icon = PackIconKind.Receipt, IsClickable = true };

            var menuItems = new ObservableCollection<MenuItem>
            {
                companies,
                bulkImport,
                recentInvoices
            };

            menuTreeView.ItemsSource = menuItems;
            var homeItem = menuItems.FirstOrDefault(item => item.Identifier == "bulkImport");
            if (homeItem != null)
            {
                SelectItem(menuTreeView, homeItem);
            }
        }




        private void SelectItem(TreeView treeView, MenuItem itemToSelect)
        {
            if (treeView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                var container = (TreeViewItem)treeView.ItemContainerGenerator.ContainerFromItem(itemToSelect);
                if (container != null)
                {
                    container.IsSelected = true;
                    return;
                }
            }

            treeView.ItemContainerGenerator.StatusChanged += (sender, e) =>
            {
                if (treeView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                {
                    var container = (TreeViewItem)treeView.ItemContainerGenerator.ContainerFromItem(itemToSelect);
                    if (container != null)
                    {
                        container.IsSelected = true;
                    }
                }
            };
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            notificationPopup.IsOpen = true;
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            profilePopup.IsOpen = true;
        }
        private async Task HandleSelectedMenuItem(MenuItem selectedMenuItem)
        {
            if (selectedMenuItem.IsClickable)
            {


                UserControl newControl = CreateControlForMenuItem(selectedMenuItem);

                if (newControl != null)
                {
                    TabItem existingTab = tabControlRenderPages.Items.OfType<TabItem>().FirstOrDefault(t => t.Tag as string == selectedMenuItem.Identifier);
                    if (existingTab != null)
                    {
                        tabControlRenderPages.SelectedItem = existingTab;
                    }
                    else
                    {
                        loadingProgressBar.Visibility = Visibility.Visible;
                        RenderPages.IsEnabled = false;
                        TabItem tabItem = new TabItem
                        {

                            Name = "page", 
                            Header = selectedMenuItem.Name,
                            Content = newControl,
                            Tag = selectedMenuItem.Identifier
                        };

                        tabControlRenderPages.Items.Add(tabItem);
                        tabControlRenderPages.SelectedItem = tabItem;

                        await Task.Delay(2000);

                        loadingProgressBar.Visibility = Visibility.Collapsed;
                        RenderPages.IsEnabled = true;
                    }
                }


            }
        }

        private UserControl CreateControlForMenuItem(MenuItem selectedMenuItem)
        {
            UserControl newControl = null;
            switch (selectedMenuItem.Identifier)
            {
                case "companies":
                    newControl = new CompaniesPage();
                    break;
                case "bulkImport":
                    newControl = new BulkImportPage(_tokenSigner);
                    break;
                case "recentInvoices":
                    newControl = new RecentInvoicesPage();
                    break;
                default:
                    MessageBox.Show("Unknown menu item.");
                    break;
            }
            return newControl;
        }

        private async void menuTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (menuTreeView.SelectedItem != null && menuTreeView.SelectedItem is MenuItem selectedMenuItem)
            {
                await HandleSelectedMenuItem(selectedMenuItem);
            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void ShowHideKeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            if (keyboardContainer.Visibility == Visibility.Collapsed)
            {
                keyboardContainer.Visibility = Visibility.Visible;
            }
            else
            {
                keyboardContainer.Visibility = Visibility.Collapsed;
            }
        }

        private void ShowHideCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (calculatorContainer.Visibility == Visibility.Collapsed)
            {
                calculatorContainer.Visibility = Visibility.Visible;
            }
            else
            {
                calculatorContainer.Visibility = Visibility.Collapsed;
            }
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DetachTab_Click(object sender, RoutedEventArgs e)
        {
            FontAwesome.WPF.FontAwesome button = (FontAwesome.WPF.FontAwesome)sender;
            TabItem tabItem = FindParent<TabItem>(button);

            if (tabItem.Name == "page")
            {
                DetachTab(tabItem);
            }
            else
            {
                MessageBox.Show("هذا العنصر لا يمكن فصله.", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DetachTab(TabItem tab)
        {
            Window newWindow = new Window
            {
                Title = tab.Header.ToString(),
                WindowState = WindowState.Maximized,
                FlowDirection = FlowDirection.RightToLeft,
                Content = tab.Content 
            };

            newWindow.Closed += (sender, e) =>
            {
                TabItem newTabItem = new TabItem
                {
                    Name = "page", 
                    Header = tab.Header,
                    Content = tab.Content 
                };
                tabControlRenderPages.Items.Add(newTabItem);
                tabControlRenderPages.SelectedItem = newTabItem;
            };

            tabControlRenderPages.Items.Remove(tab);

            newWindow.Show();
        }




        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T parent = parentObject as T;
            return parent ?? FindParent<T>(parentObject);
        }

        private void CloseTab_Click(object sender, RoutedEventArgs e)
        {
            FontAwesome.WPF.FontAwesome button = (FontAwesome.WPF.FontAwesome)sender;
            TabItem tabItem = FindParent<TabItem>(button);

            if (tabItem.Name == "page")
            {
                CloseTab(tabItem);
            }
            else
            {
                MessageBox.Show("هذا العنصر لا يمكن إزالته.", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseTab(TabItem tabItem)
        {
            tabControlRenderPages.Items.Remove(tabItem);
        }


        private async void menuTreeView_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (menuTreeView.SelectedItem != null && menuTreeView.SelectedItem is MenuItem selectedMenuItem)
            {
                await HandleSelectedMenuItem(selectedMenuItem);
            }
        }
    }
}
