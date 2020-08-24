using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayer2
{
    public partial class Form1 : Form
    {
        string[] path;
        string[] files;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectSongs_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                files = ofd.SafeFileNames;
                path = ofd.FileNames;

                for (int i = 0; i < files.Length; i++)
                    listBoxSongs.Items.Add(files[i]);
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (listBoxSongs.SelectedItems.Count < 1)
                return;
            
            int index = listBoxSongs.Items.IndexOf(listBoxSongs.SelectedItems[0]);
            player.SoundLocation = path[index];
            player.Play();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
