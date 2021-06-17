using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using Monogame_Sokobon.LevelThings;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using Monogame_Sokobon.TerminalSokobon;


namespace Monogame_Sokobon {
    public partial class LevelEditor : Form {
        public LevelEditor() {
            InitializeComponent();
        }

        public Vector2 size = new Vector2();
        public String name = "";
        public Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
        private Button[,] buttonMatrix;
        private Button player;
        private int box = 0;
        private int goal = 0;

        private void SizeY_TextChanged(object sender, EventArgs e) {
            if (!SizeY.Enabled) {
                return;
            }
            SizeY.Enabled = false;
            if (int.TryParse(SizeY.Text, out int n)) {
                size.Y = n;
            }
            else {
                SizeY.Text = "0";
            }
            SizeY.Enabled = true;
            drawButtons();
        }

        private void SizeX_TextChanged(object sender, EventArgs e) {
            if (!SizeX.Enabled) {
                return;
            }
            SizeX.Enabled = false;
            if (int.TryParse(SizeX.Text, out int n)) {
                size.X = n;
            }
            else {
                SizeX.Text = "0";
            }
            SizeX.Enabled = true;
            drawButtons();
        }

        private void drawButtons() {
            if (buttonMatrix != null) {
                foreach (Button button in buttonMatrix) {
                    button.Dispose();
                }
            }
            buttonMatrix = new Button[(int)size.Y + 2, (int)size.X + 2];

            //InitializeMatrix(ref matrix);  //Corresponds to the real matrix
            int celNr = 1;

            for (int y = 0; y < size.Y + 2; y++) {
                for (int x = 0; x < size.X + 2; x++) {
                    buttonMatrix[y, x] = new Button() {
                        Width = Height = 20,
                        Text = ((y == 0 || x == 0) || (y == size.Y + 1 || x == size.X + 1) ? "8" : "0"),
                        BackColor = ((y == 0 || x == 0) || (y == size.Y + 1 || x == size.X + 1) ? Color.Black : panel1.BackColor),
                        Location = new Point(x * 20 + 10,
                                              y * 20 + 10),  // <-- You might want to tweak this
                        Parent = panel1,
                        AllowDrop = true
                    };
                    buttonMatrix[y, x].Tag = celNr++;
                    buttonMatrix[y, x].DragOver += matrixClick;
                    buttonMatrix[y, x].MouseDown += mousedown;
                }
            }
        }
        private void mousedown(object sender, EventArgs e) {
            if (sender is Button) {
                Button b = sender as Button;
                int.TryParse(b.Text, out int n);
                int.TryParse(b.Tag.ToString(), out int k);
                switch (n) {
                    case 0:
                        entities.Add(k, new Entity("Wall", (b.Location.X - 10) / 20 - 1, (b.Location.Y - 10) / 20 - 1));
                        b.Text = "1";
                        b.BackColor = Color.Black;
                        break;
                    case 1:
                        entities[k] = new Entity("Goal", (b.Location.X - 10) / 20 - 1, (b.Location.Y - 10) / 20 - 1);
                        b.Text = "2";
                        b.BackColor = Color.Green;
                        goal++;
                        break;
                    case 2:
                        entities[k] = new Entity("Box", (b.Location.X - 10) / 20 - 1, (b.Location.Y - 10) / 20 - 1);
                        b.Text = "3";
                        b.BackColor = Color.Brown;
                        goal--;
                        box++;
                        break;
                    case 3:
                        entities[k] = new Entity("Player", (b.Location.X - 10) / 20 - 1, (b.Location.Y - 10) / 20 - 1);
                        if (player != null) {
                            int.TryParse(player.Tag.ToString(), out int j);
                            player.Text = "0";
                            player.BackColor = panel1.BackColor;
                            entities.Remove(j);
                        }
                        b.Text = "4";
                        b.BackColor = Color.Red;
                        player = b;
                        box--;
                        break;
                    case 4:
                        player = null;
                        entities.Remove(k);
                        b.Text = "0";
                        b.BackColor = panel1.BackColor;
                        break;
                }
                int.TryParse(b.Text, out n);
                b.DoDragDrop("" + n, DragDropEffects.All);
            }
        }
        private void matrixClick(object sender, DragEventArgs e) {
            if (sender is Button) {
                Button b = sender as Button;
                int.TryParse(b.Text, out int n);
                int.TryParse(e.Data.GetData(DataFormats.Text).ToString(), out int x);
                if (n == x) {
                    return;
                }
                int.TryParse(b.Tag.ToString(), out int k);
                switch (n) {
                    case 0:
                        entities.Add(k, new Entity("Wall", (b.Location.X - 10) / 20 - 1, (b.Location.Y - 10) / 20 - 1));
                        b.Text = "1";
                        b.BackColor = Color.Black;
                        break;
                    case 1:
                        entities[k] = new Entity("Goal", (b.Location.X - 10) / 20 - 1, (b.Location.Y - 10) / 20 - 1);
                        b.Text = "2";
                        b.BackColor = Color.Green;
                        goal++;
                        break;
                    case 2:
                        entities[k] = new Entity("Box", (b.Location.X - 10) / 20 - 1, (b.Location.Y - 10) / 20 - 1);
                        b.Text = "3";
                        b.BackColor = Color.Brown;
                        goal--;
                        box++;
                        break;
                    case 3:
                        entities[k] = new Entity("Player", (b.Location.X - 10) / 20 - 1, (b.Location.Y - 10) / 20 - 1);
                        if (player != null) {
                            int.TryParse(player.Tag.ToString(), out int j);
                            player.Text = "0";
                            player.BackColor = panel1.BackColor;
                            entities.Remove(j);
                        }
                        b.Text = "4";
                        b.BackColor = Color.Red;
                        player = b;
                        box++;
                        break;
                    case 4:
                        player = null;
                        entities.Remove(k);
                        b.Text = "0";
                        b.BackColor = panel1.BackColor;
                        break;
                }
            }
        }

        private void LevelEditor_ResizeEnd(object sender, EventArgs e) {
            panel1.Height = LevelEditor.ActiveForm.Height - 63;
            panel1.Width = LevelEditor.ActiveForm.Width - 128;
            //drawButtons();
        }

        private void LevelName_TextChanged(object sender, EventArgs e) {
            name = LevelName.Text;
        }

        private void Export_Click(object sender, EventArgs e) {
            if (box == 0 || goal == 0 || player == null) {
                MessageBox.Show("Missing object: Boxes = " + box + ". (Needs at least 1), Goals = " + goal + ". (Needs at least 1), Player = " + (player == null ? 0 : 1) + ". (Needs exactly 1)");
                return;
            }
            LevelData level = new LevelData((int)size.Y, (int)size.X, new List<LayersDum>() { new LayersDum(new List<Entity>()) });
            foreach (Entity entity in entities.Values) {
                level.layers[0].entities.Add(entity);
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save Level Files";
            //saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.FileName = name;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.Filter = "json files (*.json)|*.json";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName)) {
                    writer.Write(JsonSerializer.Serialize(level));
                }
            }
            SokobonGame.isLevelEditor = false;
            Soko.loadSokoban(level);
            SokobonGame.levelType = LevelType.Custom;
            LevelEditor.ActiveForm.Dispose();
        }

        private void button1_Click(object sender, EventArgs e) {
            drawButtons();
        }

        private void Load_Click(object sender, EventArgs e) {
            if (box == 0 || goal == 0 || player == null) {
                MessageBox.Show("Missing object: Boxes = " + box + ". (Needs at least 1), Goals = " + goal + ". (Needs at least 1), Player = " + (player == null ? 0 : 1) + ". (Needs exactly 1)");
                return;
            }
            LevelData level = new LevelData((int)size.Y, (int)size.X, new List<LayersDum>() { new LayersDum(new List<Entity>()) });
            foreach (Entity entity in entities.Values) {
                level.layers[0].entities.Add(entity);
            }
            SokobonGame.isLevelEditor = false;
            Soko.loadSokoban(level);
            SokobonGame.levelType = LevelType.Custom;
            LevelEditor.ActiveForm.Dispose();
        }

        private void SizeX_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) {
                SizeX_TextChanged(sender, e);
            }
        }

        private void SizeY_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) {
                SizeY_TextChanged(sender, e);
            }
        }
        private void Closing(object sender, EventArgs e) {
            SokobonGame.isLevelEditor = false;
            LevelEditor.ActiveForm.Dispose();
        }
    }
}
