using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace WindowMessageLogger
{
    public class WindowsMessageLoggerApplicationContext : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;

        public WindowsMessageLoggerApplicationContext()
        {
            var components = new Container();
            ContextMenu menu = new ContextMenu(new[] { new MenuItem("Exit", OnClick)});
            //ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            //contextMenuStrip.Items.Add(new ExitButton(this));
            _notifyIcon = new NotifyIcon(components)
            {
                ContextMenu = menu,
                //ContextMenuStrip = contextMenuStrip,
                Visible = true,
                Icon = new Icon(Assembly.GetEntryAssembly().GetManifestResourceStream("WindowMessageLogger.assets.icon.ico")),
                Text = "WindowMessageLogger"
            };
        }

        private void OnClick(object sender, EventArgs e)
        {
            ExitThread();
        }

        protected override void ExitThreadCore()
        {
            _notifyIcon.Visible = false;
            base.ExitThreadCore();
        }

        private class ExitButton : ToolStripButton
        {
            private readonly ApplicationContext _applicationContext;

            public ExitButton(ApplicationContext applicationContext) 
                : base("Exit")
            {
                _applicationContext = applicationContext;
            }

            protected override void OnClick(EventArgs e)
            {
                _applicationContext.ExitThread();
                base.OnClick(e);
            }
        }
    }
}