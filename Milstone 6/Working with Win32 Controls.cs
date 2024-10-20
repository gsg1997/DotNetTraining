using System;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

public class MyForm : Form
{
    private ListView listView;
    private Button addButton;
    private Button removeButton;

    public MyForm()
    {
        listView = new ListView { Dock = DockStyle.Top, Height = 200 };
        addButton = new Button { Text = "Add Item", Dock = DockStyle.Left };
        removeButton = new Button { Text = "Remove Item", Dock = DockStyle.Right };

        addButton.Click += AddButton_Click;
        removeButton.Click += RemoveButton_Click;

        Controls.Add(listView);
        Controls.Add(addButton);
        Controls.Add(removeButton);
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        listView.Items.Add("New Item");
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        if (listView.SelectedItems.Count > 0)
        {
            listView.Items.Remove(listView.SelectedItems[0]);
        }
        else
        {
            MessageBox.Show("No item selected.");
        }
    }

    [STAThread]
    static void Main()
    {
        Application.Run(new MyForm());
    }
}
