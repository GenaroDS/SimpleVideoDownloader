using System.Diagnostics;
using System.Drawing;
namespace YouTubeDownloader
{
    public partial class Window : Form
    {

        private string selectedFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private static Window? instance;

        private Button? btnDownloadBottom;
        public static bool IsOpen => instance != null;

        public Window()
        {
            instance = this;
            FormClosed += (s, e) => instance = null; // Limpia la instancia al cerrar
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Icon = System.Drawing.Icon.FromHandle(SimpleVideoDownloader.Resources.UNO.GetHicon());
            InitializeComponent();

            // Cargar la carpeta guardada o usar Desktop como predeterminada
            selectedFolder = LoadFolderPathFromConfig();
            txtFolderPath.Text = selectedFolder;

            txtFolderPath.TextChanged += TxtFolderPath_TextChanged;

            btnDownloadBottom = new Button
            {
                Text = "Download",
                Dock = DockStyle.Bottom,
                Height = 25,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(69, 73, 74),
                FlatStyle = FlatStyle.Flat
            };
            btnDownloadBottom.FlatAppearance.BorderSize = 0;

            btnDownloadBottom.Click += btnDownload_Click;

            Controls.Add(btnDownloadBottom);
            new BlueMystic.DarkModeCS(this, true, true);
        }


        private void ComboBoxFormat_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox combo = sender as ComboBox;

            // Fondo del elemento
            e.DrawBackground();
            Brush textBrush = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? Brushes.White // Color de texto para elemento seleccionado
                : Brushes.WhiteSmoke; // Color de texto para elementos no seleccionados

            Brush backgroundBrush = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? new SolidBrush(Color.FromArgb(69, 73, 74)) // Fondo seleccionado
                : new SolidBrush(Color.FromArgb(43, 43, 43)); // Fondo no seleccionado

            // Dibujar el fondo
            e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

            // Dibujar el texto
            e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds);

            e.DrawFocusRectangle();
        }

        private void ReactivateControls()
        {
            // Reactiva todos los controles
            foreach (Control control in Controls)
            {
                control.Enabled = true;
            }

            // Reactivar texto y botón
            btnDownloadBottom.Text = "Download";
            btnDownloadBottom.Enabled = true;
        }

        private string LoadFolderPathFromConfig()
        {
            string configPath = GetConfigFilePath();
            if (File.Exists(configPath))
            {
                var config = System.Text.Json.JsonSerializer.Deserialize<Config>(File.ReadAllText(configPath));
                return config?.FolderPath ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void SaveFolderPathToConfig(string folderPath)
        {
            var config = new Config { FolderPath = folderPath };
            string configPath = GetConfigFilePath();
            File.WriteAllText(configPath, System.Text.Json.JsonSerializer.Serialize(config));
        }

        private string GetConfigFilePath()
        {
            string tempFolder = Path.GetTempPath();
            return Path.Combine(tempFolder, "SVDappsettings.json");
        }

        public class Config
        {
            public string FolderPath { get; set; }
        }

        private void TxtFolderPath_TextChanged(object? sender, EventArgs e)
        {
            selectedFolder = txtFolderPath.Text.Trim(); // Sincronizar la carpeta seleccionada
        }
        private void btnSelectFolder_Click(object? sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select folder";
                folderBrowserDialog.ShowNewFolderButton = true;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolder = folderBrowserDialog.SelectedPath;
                    txtFolderPath.Text = selectedFolder; // Sincronizar el TextBox
                    SaveFolderPathToConfig(selectedFolder); // Guardar en el archivo JSON
                }
            }
        }


        private async void btnDownload_Click(object? sender, EventArgs e)
        {
            // Desactiva la pantalla y el botón al comienzo
            btnDownloadBottom.Text = "Downloading...";
            btnDownloadBottom.Enabled = false;

            // Desactiva solo las interacciones del formulario (permite minimizar)
            foreach (Control control in Controls)
            {
                control.Enabled = false;
            }

            string link = txtUrl.Text.Trim();
            if (string.IsNullOrWhiteSpace(link))
            {
                btnDownloadBottom.Text = "Download";
                DarkMessageBox.Show("No link detected.", "Error");
                ReactivateControls(); // Reactivar controles en caso de error
                return;
            }

            string randomSuffix = new Random().Next(100000, 999999).ToString();
            string outputPath = Path.Combine(selectedFolder, $"%(title)s_{randomSuffix}.%(ext)s");

            string formatOption = GetSelectedFormatOption();
            string? ytdlpPath = null;
            string? ffmpegPath = null;

            try
            {
                // Extraer los recursos asíncronamente
                ytdlpPath = await Task.Run(() => ExtractResourceToTempFile("yt_dlp", "yt-dlp.exe"));
                ffmpegPath = await Task.Run(() => ExtractResourceToTempFile("ffmpeg", "ffmpeg.exe"));

                var result = await Task.Run(() =>
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = ytdlpPath,
                            Arguments = $"-o \"{outputPath}\" -f \"{formatOption}\" {link}",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        }
                    };

                    process.StartInfo.EnvironmentVariables["PATH"] =
                        Path.GetDirectoryName(ffmpegPath) + ";" + process.StartInfo.EnvironmentVariables["PATH"];

                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    return new { ExitCode = process.ExitCode, Error = error };
                });

                if (result.ExitCode == 0)
                {
                    btnDownloadBottom.Text = "Success";
                    DarkMessageBox.Show("Download completed successfully.", "Success");
                    btnDownloadBottom.Text = "Download";
                }
                else
                {
                    btnDownloadBottom.Text = "Error";
                    DarkMessageBox.Show($"Error", "Error");
                    btnDownloadBottom.Text = "Download";
                }
            }
            catch (Exception ex)
            {
                btnDownloadBottom.Text = "Error";
                DarkMessageBox.Show($"Error: {ex.Message}", "Error");
                btnDownloadBottom.Text = "Download";
            }
            finally
            {
                ReactivateControls();

                // Eliminar los archivos temporales si fueron creados
                if (ytdlpPath != null && File.Exists(ytdlpPath)) File.Delete(ytdlpPath);
                if (ffmpegPath != null && File.Exists(ffmpegPath)) File.Delete(ffmpegPath);
            }
        }
        private string ExtractResourceToTempFile(string resourceName, string fileName)
        {
            // Obtener el recurso desde Resources
            var resource = SimpleVideoDownloader.Resources.ResourceManager.GetObject(resourceName);

            // Validar si el recurso existe
            if (resource == null)
            {
                throw new Exception($"Recurso '{resourceName}' no encontrado en Resources.");
            }

            string tempFolder = Path.GetTempPath();
            string filePath = Path.Combine(tempFolder, fileName);

            // Extraer el recurso al archivo
            using (var resourceStream = new MemoryStream((byte[])resource))
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                resourceStream.CopyTo(fileStream);
            }

            return filePath;
        }


        private string GetSelectedFormatOption()
        {
            if (radioButton1.Checked) return "bestaudio"; // MP3
            if (radioButton2.Checked) return "bestvideo[height<=480][ext=mp4]+bestaudio[ext=m4a]/best[height<=480][ext=mp4]";
            if (radioButton3.Checked) return "bestvideo[height<=720][ext=mp4]+bestaudio[ext=m4a]/best[height<=720][ext=mp4]";
            if (radioButton4.Checked) return "bestvideo[height<=1080][ext=mp4]+bestaudio[ext=m4a]/best[height<=1080][ext=mp4]";
            return "best[ext=mp4]";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDownload_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtFolderPath_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Window_Load(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }
    }
}