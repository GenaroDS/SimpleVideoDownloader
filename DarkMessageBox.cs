using System.Drawing;
using System.Windows.Forms;

public class DarkMessageBox : Form
{
    private Label lblMessage;
    private Button btnOk;

    public DarkMessageBox(string message, string title)
    {
        Text = title;
        Size = new Size(300, 130);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        lblMessage = new Label
        {
            Text = message,
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };

        btnOk = new Button
        {
            Text = "Accept",
            Dock = DockStyle.Bottom,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(69, 73, 74),
            FlatStyle = FlatStyle.Flat,
            DialogResult = DialogResult.OK
        };

        btnOk.FlatAppearance.BorderSize = 0;

        Controls.Add(lblMessage);
        Controls.Add(btnOk);

        // Aplicar Dark Mode
        new BlueMystic.DarkModeCS(this, true, true);
    }

    public static DialogResult Show(string message, string title)
    {
        using (var msgBox = new DarkMessageBox(message, title))
        {
            return msgBox.ShowDialog();
        }
    }
}
