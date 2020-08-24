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
using System.Windows.Media;

namespace MusicPlayer2
{
    public partial class Form1 : Form
    {
        string[] path;
        string[] files;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        int songProgress;
        int dsec;
        int sec;
        int min;
        int tickCounter;
        int timerInterval = 50;

        public Form1()
        {
            InitializeComponent();
            SetStartParameters();
            label1.Visible = false;
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

            setSongSlider(path[index]);           
            player.Play();
            SetStartParameters();
            SongTimer.Interval = timerInterval;
            SongTimer.Start();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            player.Stop();
            SongTimer.Stop();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int AudioDuration(string path)
        {
            TagLib.File file = TagLib.File.Create(path);
            int s_time = (int)file.Properties.Duration.TotalMilliseconds;          
            return s_time;
        }

        private void setSongSlider(string path)
        {
            SongSlider.Maximum = AudioDuration(path);
            label1.Text = AudioDuration(path).ToString();
        }

        private void SongTimer_Tick(object sender, EventArgs e)
        {
            SliderMove();
            ShowPlayedTime();
        }

        private void SliderMove()
        {
            songProgress += 50;
            if (songProgress < SongSlider.Maximum)
                SongSlider.Value = songProgress;
            else
                SongSlider.Value = SongSlider.Maximum;
        }

        private void ShowPlayedTime()
        {
            tickCounter++;
            dsec = (int)((tickCounter * timerInterval) / 100);
            if (dsec == 10)
            {
                dsec = 0;
                sec += 1;
                tickCounter = 0;
            }
            if (sec == 60)
            {
                sec = 0;
                min += 1;
            }            
            label1.Text = min.ToString("00") + " : " +
                sec.ToString("00") + " : " +
                dsec.ToString("0");
        }

        private void SetStartParameters()
        {
            songProgress = 0;
            dsec = 0;
            sec = 0;
            min = 0;
            tickCounter = 0;
            label1.Text = "00 : 00 : 0";
            label1.Visible = true;
        }
    }
}
