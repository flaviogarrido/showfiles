namespace showfiles;
public partial class ContentForm : Form
{
    private TextBox textBox;
    internal static ContentForm instance;

    public ContentForm(string filePath = "")
    {
        InitializeComponent();
        InitializeTextBox(filePath);
    }

    private void InitializeTextBox(string filePath)
    {
        textBox = new TextBox
        {
            Multiline = true,
            ScrollBars = ScrollBars.Both,
            Dock = DockStyle.Fill,
            Font = new Font("Consolas", 10),
            Text = !string.IsNullOrEmpty(filePath) ? File.ReadAllText(filePath) : ""
        };
        this.Controls.Add(textBox);
        this.KeyPreview = true;
        this.KeyDown += ContentForm_KeyDown;
    }

    public void LoadFile(string filePath)
    {
        textBox.Text = File.ReadAllText(filePath);
    }

    private void ContentForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.G)
        {
            string line = Prompt.ShowDialog("Enter line number:", "Go to Line");
            if (int.TryParse(line, out int lineNumber))
            {
                GoToLine(lineNumber);
            }
        }
        else if (e.Control && e.KeyCode == Keys.F)
        {
            string searchText = Prompt.ShowDialog("Enter text to find:", "Find Text");
            FindText(searchText);
        }
    }

    private void GoToLine(int lineNumber)
    {
        var lines = textBox.Lines;
        if (lineNumber > 0 && lineNumber <= lines.Length)
        {
            textBox.SelectionStart = textBox.GetFirstCharIndexFromLine(lineNumber - 1);
            textBox.SelectionLength = lines[lineNumber - 1].Length;
            textBox.ScrollToCaret();
        }
    }

    private void FindText(string searchText)
    {
        int startIndex = textBox.Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase);
        if (startIndex != -1)
        {
            textBox.SelectionStart = startIndex;
            textBox.SelectionLength = searchText.Length;
            textBox.ScrollToCaret();
        }
    }
}
