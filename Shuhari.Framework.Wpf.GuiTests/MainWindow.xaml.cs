using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Wpf.Controls;
using static Shuhari.Framework.Win32.ShellApi;

namespace Shuhari.Framework.Wpf.GuiTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int index = 0;
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_RETURNONLYFSDIRS", BIF_RETURNONLYFSDIRS);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_DONTGOBELOWDOMAIN", BIF_DONTGOBELOWDOMAIN);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_STATUSTEXT", BIF_STATUSTEXT);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_RETURNFSANCESTORS", BIF_RETURNFSANCESTORS);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_EDITBOX", BIF_EDITBOX);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_VALIDATE", BIF_VALIDATE);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_NEWDIALOGSTYLE", BIF_NEWDIALOGSTYLE, true);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_USENEWUI", BIF_USENEWUI);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_BROWSEINCLUDEURLS", BIF_BROWSEINCLUDEURLS);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_UAHINT", BIF_UAHINT);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_NONEWFOLDERBUTTON", BIF_NONEWFOLDERBUTTON);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_NOTRANSLATETARGETS", BIF_NOTRANSLATETARGETS);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_BROWSEFORCOMPUTER", BIF_BROWSEFORCOMPUTER);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_BROWSEFORPRINTER", BIF_BROWSEFORPRINTER);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_BROWSEINCLUDEFILES", BIF_BROWSEINCLUDEFILES);
            AddOptionCheckBox(browseFlagGrid, ref index, "BIF_SHAREABLE", BIF_SHAREABLE);
        }

        private void AddOptionCheckBox(Grid grid, ref int index, string title, uint value, bool selected = false)
        {
            int colCount = grid.ColumnDefinitions.Count;
            var cb = new CheckBox
            {
                Content = title,
                Tag = value,
                VerticalAlignment = VerticalAlignment.Center,
                IsChecked = selected,
            };
            Grid.SetRow(cb, index / colCount);
            Grid.SetColumn(cb, index % colCount);
            grid.Children.Add(cb);

            index++;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            uint flags = 0;
            foreach (var cb in browseFlagGrid.Children.OfType<CheckBox>())
            {
                if (cb.IsChecked == true)
                    flags |= Convert.ToUInt32(cb.Tag);
            }
            txtFolderName.Text = this.BrowseForFolder("标题", txtFolderName.Text, flags);
        }

        private void btnTestDialog_Click(object sender, RoutedEventArgs e)
        {
            new TestDialog().ShowDialog(this);
        }

        private void btnShowInfo_Click(object sender, RoutedEventArgs e)
        {
            statusLabel.ShowInfo("Show info");
        }

        private void btnShowError_Click(object sender, RoutedEventArgs e)
        {
            statusLabel.ShowError("Show error");
        }

        private void btnLogInfo_Click(object sender, RoutedEventArgs e)
        {
            logList.AddLog(new LogItem(LogType.Info, DateTime.Now, "info log"));
        }

        private void btnLogError_Click(object sender, RoutedEventArgs e)
        {
            logList.AddLog(new LogItem(LogType.Error, DateTime.Now, "error log"));
        }

        private void btnLogClear_Click(object sender, RoutedEventArgs e)
        {
            logList.Clear();
        }
    }
}
