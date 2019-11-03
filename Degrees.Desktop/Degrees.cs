using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Degrees.Engine;
using static Degrees.Engine.Engine;

namespace Degrees.Desktop
{
    public partial class Degrees : Form
    {
        private Database _db;

        public Degrees()
        {
            InitializeComponent();

            _db = new Database();

            this.Load += Degrees_Load;
            _goButton.Click += _goButton_Click;
        }

        private async void _goButton_Click(object sender, EventArgs e)
        {
            _actor1Combo.Enabled = false;
            _actor2Combo.Enabled = false;
            _goButton.Enabled = false;

            var actor1 = _db.Actors.First(a => a.Name == _actor1Combo.Text);
            var actor2 = _db.Actors.First(a => a.Name == _actor2Combo.Text);

            _outputTextBox.Text = $"Finding shorted path from {actor1.Name} to {actor2.Name}...";

            var degrees = await Task.Run(() =>
            {
                return Engine.Engine.FindFewestDegrees(actor1, actor2, (c, d) =>
                {
                    if (c % 100 == 0)
                    {
                        Invoke(new Action(() =>
                        {
                            _outputTextBox.Text = $"Checked {c} (depth {d})";
                        }));
                    }
                });
            });

            var builder = new StringBuilder();
            builder.AppendLine($"{actor1.Name}");
            int depth = 0;
            foreach (var degree in degrees)
            {
                builder.AppendLine($"{++depth})\tin \"{degree.Movie.Title}\" with {degree.Costar.Name}");
            }
            _outputTextBox.Text = builder.ToString();

            _actor1Combo.Enabled = true;
            _actor2Combo.Enabled = true;
            _goButton.Enabled = true;
        }

        private async void Degrees_Load(object sender, EventArgs e)
        {
            const string titleSourcePath = @"..\..\..\..\title.basics.tsv";
            const string nameSourcePath = @"..\..\..\..\name.basics.tsv";
            const string principalSourcePath = @"..\..\..\..\title.principals.tsv";

            _actor1Combo.Enabled = false;
            _actor2Combo.Enabled = false;
            _goButton.Enabled = false;

            await Task.Run(() =>
            {
                using (var titleSource = new StreamReader(titleSourcePath))
                {
                    _db.LoadTitles(titleSource, (m, p, a) =>
                    {
                        Invoke(new Action(() =>
                        {
                            _outputTextBox.Text = $"{p}) {m} : {a} lines";
                        }));
                    });
                }
                using (var nameSource = new StreamReader(nameSourcePath))
                {
                    _db.LoadNames(nameSource, (m, p, a) =>
                    {
                        Invoke(new Action(() =>
                        {
                            _outputTextBox.Text = $"{p}) {m} : {a} lines";
                        }));
                    });
                }
                using (var principalSource = new StreamReader(principalSourcePath))
                {
                    _db.LoadPrincipals(principalSource, (m, p, a) =>
                    {
                        Invoke(new Action(() =>
                        {
                            _outputTextBox.Text = $"{p}) {m} : {a} lines";
                        }));
                    });
                }
            });

            _outputTextBox.Text = "Ready...";

            _actor1Combo.Enabled = true;
            _actor2Combo.Enabled = true;
            _goButton.Enabled = true;

            //var actorNames = _db.Actors.Select(a => a.Name).ToArray();
            //_actor1Combo.Items.AddRange(actorNames);
            //_actor2Combo.Items.AddRange(actorNames);
        }
    }
}
