using System;
using Microsoft.Win32;
using System.Text.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using YouTubeDownloader;



namespace SimpleVideoDownloader
{

    class Program
    {
        static bool stopPingTest = false;
        static NotifyIcon? trayIcon;
        [STAThread]
        static void Main(string[] args)
        {

            bool isNewInstance;
            using (Mutex mutex = new Mutex(true, "SimpleVideoDownloader", out isNewInstance))
            {
                if (!isNewInstance)
                {
                    // Si ya hay una instancia en ejecución, salir sin mostrar mensaje
                    return;
                }

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                SetSysTrayIcon();

                trayIcon.ContextMenuStrip = new ContextMenuStrip();
                trayIcon.ContextMenuStrip.Renderer = new DarkModeRenderer();
                trayIcon.ContextMenuStrip.ShowImageMargin = false;

                trayIcon.ContextMenuStrip.Items.Add("Download video", null, onDownloadClick);
                trayIcon.ContextMenuStrip.Items.Add("Like the app? Buy me a coffee!", null, onCoffeeClick);
                trayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
                trayIcon.ContextMenuStrip.Items.Add("Exit", null, OnExitClick);

                Application.Run();
            }

            static void SetSysTrayIcon()
            {
                trayIcon = new NotifyIcon
                {
                    Icon = Icon.FromHandle(SimpleVideoDownloader.Resources.UNO.GetHicon()), 
                    Text = "SimpleVideoDownloader",
                    Visible = true
                };
            }


            static void onDownloadClick(object? sender, EventArgs e)
            {
                if (Window.IsOpen)
                {
                    //MessageBox.Show("La ventana ya está abierta.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Window form1 = new Window();
                form1.ShowDialog();
            }


            static void onCoffeeClick(object? sender, EventArgs e)
            {
                string url = "https://ko-fi.com/gdsdev";
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }

            static void OnExitClick(object? sender, EventArgs e)
            {
                stopPingTest = true;
                Application.Exit();
            }

            
        }


        class DarkModeRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#4b4b4b")), e.Item.ContentRectangle);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#2b2b2b")), e.Item.ContentRectangle);
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = Color.White;
                base.OnRenderItemText(e);
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = Color.White;
                base.OnRenderArrow(e);
            }
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
            }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#2b2b2b")), e.AffectedBounds);
            }

            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
                e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#2b2b2b")), e.AffectedBounds);
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                int separatorThickness = 1; // Thickness of the separator line in pixels
                int separatorMargin = 6; // Margin on the left and right of the separator line

                int x1 = e.Item.ContentRectangle.Left + separatorMargin;
                int x2 = e.Item.ContentRectangle.Right - separatorMargin;
                int y = e.Item.ContentRectangle.Top + (e.Item.ContentRectangle.Height - separatorThickness) / 2;

                e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#6b6b6b")), x1, y, x2 - x1, separatorThickness);
            }

        }

    }
}