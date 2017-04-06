using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;
using Shuhari.Framework.Wpf.Models;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// List view to show log
    /// </summary>
    public class LogListView : ListView
    {
        /// <summary>
        /// Initialize
        /// </summary>
        static LogListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LogListView),
                new FrameworkPropertyMetadata(typeof(LogListView)));
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public LogListView()
        {
            var gv = new GridView();
            gv.Columns.Add(new GridViewColumn
            {
                Width = 200,
                Header = ResourceRegistry.GetUiString("Time"),
                DisplayMemberBinding = new Binding(nameof(LogItem.Time)) { StringFormat = "{0:yy-MM-dd HH:mm:ss}" }
            });
            gv.Columns.Add(new GridViewColumn
            {
                Width = 800,
                Header = ResourceRegistry.GetUiString("Content"),
                DisplayMemberBinding = new Binding(nameof(LogItem.Content))
            });
            this.View = gv;

            _logs = new ObservableCollection<LogItem>();
            ItemsSource = _logs;
        }

        private readonly ObservableCollection<LogItem> _logs;

        /// <summary>
        /// 最大日志数量
        /// </summary>
        private const long MAX_COUNT = 3000;

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="log"></param>
        public void AddLog(LogItem log)
        {
            Expect.IsNotNull(log, nameof(log));

            if (_logs.Count > MAX_COUNT)
                _logs.RemoveAt(0);
            _logs.Add(log);
            SelectedItem = log;
            ScrollIntoView(log);
        }

        /// <summary>
        /// 清除日志
        /// </summary>
        public void Clear()
        {
            _logs.Clear();
        }
    }
}
