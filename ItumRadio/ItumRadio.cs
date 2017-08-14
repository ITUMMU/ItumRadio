using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;

namespace itumRadio
{
    public partial class ItumRadio : Form
    {
        private class Item
        {
            public IceCast2.StreamInfo Stream;
            public int Value;
            public Item(IceCast2.StreamInfo stream, int value)
            {
                try
                {
                    Stream = stream;
                    Value = value;
                }
                catch (Exception ex)
                {
                    using (eventLogger ev = new eventLogger("itumRadio"))
                    {
                        ev.writeLog(String.Format("Exception in Item:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                    }
                }

            }
            public override string ToString()
            {
                try
                {
                    return Stream.streamName + ", " + Stream.streamCity + " - " + Stream.streamDescription;
                }
                catch (Exception ex)
                {
                    using (eventLogger ev = new eventLogger("itumRadio"))
                    {
                        ev.writeLog(String.Format("Exception in Item.ToString:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                    }
                    return String.Empty;
                }
            }
        }

        public Int32 getVolumeIndex()
        {
            try
            {
                return Properties.UserSettings.Default.volumeIndex;
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in getVolumeIndex:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }

                return 50;
            }
        }

        public Int32 getStationIndex()
        {
            try
            {
                return Properties.UserSettings.Default.stationIndex;
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in getStationIndex:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }

                return 0;
            }
        }

        public void setVolumeIndex(Int32 Index)
        {
            try
            {
                Properties.UserSettings.Default.volumeIndex = Index;
                Properties.UserSettings.Default.Save();
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in setVolumeIndex:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        public void setStationIndex(Int32 Index)
        {
            try
            {
                Properties.UserSettings.Default.stationIndex = Index;
                Properties.UserSettings.Default.Save();
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in setStationIndex:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        public IceCast2 iceCast2;
        private MediaPlayer mediaPlayer = new MediaPlayer();

        private void exitApplication(Int32 ErrorLevel = 0)
        {
            if (Application.MessageLoop)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog("Fin de l'application", System.Diagnostics.EventLogEntryType.Information);
                }
                Application.Exit();
            }
            else
            {
                Environment.Exit(ErrorLevel);
            }
        }

        public ItumRadio()
        {
            try
            {
                Int32 i = 0;

                this.Icon = itumRadio.Properties.Resources.ItumRadio;
                InitializeComponent();
                iceCast2 = new IceCast2("http://radio.conseil.itum.net:8000/");
                volumeControl.Minimum = 0;
                volumeControl.Maximum = 100;
                volumeControl.SmallChange = 1;
                volumeControl.LargeChange = 10;
                volumeControl.Value = getVolumeIndex();

                mediaPlayer.BufferingEnded += new System.EventHandler(BufferingEnded_Event);
                mediaPlayer.BufferingStarted += new System.EventHandler(BufferingStarted_Event);
                mediaPlayer.MediaOpened += new System.EventHandler(MediaOpened_Event);

                foreach (IceCast2.StreamInfo stream in iceCast2.streamInfoList)
                {
                    stationList.Items.Add(new Item(stream, i++));
                }

                if(getStationIndex() == -1)
                {
                        stationList.SelectedIndex = stationList.FindString(Properties.Settings.Default.defaultStation);
                } else
                {
                    stationList.SelectedIndex = getStationIndex();
                }

                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog("Démarrage de l'application", System.Diagnostics.EventLogEntryType.Information);
                }
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in ItumRadio:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }

                MessageBox.Show("Une erreur est survenue.", "itumRadio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                exitApplication(1);
            }

        }

        private void playRadio()
        {
            try
            {
                Item selectedItem = (Item)stationList.SelectedItem;
                setStationIndex(stationList.SelectedIndex);
                stopRadio();
                setPlayerStatus(false);
                StatusLabel.Text = "Chargement...";
                progressBar.Visible = true;
                stationLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                stationLogo.BorderStyle = BorderStyle.Fixed3D;
                stationLogo.ImageLocation = selectedItem.Stream.uriLogo.Uri.ToString();

                mediaPlayer.Open(selectedItem.Stream.uriListen.Uri);
                mediaPlayer.Play();
                mediaPlayer.Volume = (double)(volumeControl.Value / 100.0);
                this.Text = selectedItem.ToString();
            }
            catch (Exception ex)
            {
                setPlayerStatus(true);
                progressBar.Visible = false;
                StatusLabel.Text = "Erreur de lecture...";

                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in playRadio:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        

        }

        private void stopRadio()
        {
            try
            {
                mediaPlayer.Stop();
                mediaPlayer.Close();
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in stopRadio:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            try
            {
                playRadio();
            }
            catch (Exception ex)
            {
                using(eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in playButton_Click:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                stopRadio();
                setPlayerStatus(true);
                progressBar.Visible = false;
                StatusLabel.Text = String.Empty;
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in stopButton_Click:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            try
            {
                if ((stationList.SelectedIndex + 1) == stationList.Items.Count)
                {
                    stationList.SelectedIndex = 0;
                }
                else
                {
                    stationList.SelectedIndex++;
                }
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in forwardButton_Click:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void reverseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (stationList.SelectedIndex == 0)
                {
                    stationList.SelectedIndex = stationList.Items.Count - 1;
                }
                else
                {
                    stationList.SelectedIndex--;
                }
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in reverseButton_Click:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void volumeControl_Scroll(object sender, EventArgs e)
        {
            try
            {
                setVolumeIndex(volumeControl.Value);
                mediaPlayer.Volume = (double)(volumeControl.Value / 100.0);
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in volumeControl_Scroll:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void stationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                playRadio();
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in stationList_SelectedIndexChanged:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void setPlayerStatus(Boolean Status)
        {
            try
            {
                playButton.Enabled = Status;
                //stopButtonbutton.Enabled = Status;
                forwardButton.Enabled = Status;
                reverseButton.Enabled = Status;
                stationList.Enabled = Status;
                volumeControl.Enabled = Status;
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in setPlayerStatus:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void MediaOpened_Event(object sender, System.EventArgs e)
        {
            try
            {
                setPlayerStatus(true);
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in MediaOpened_Event:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void BufferingStarted_Event(object sender, System.EventArgs e)
        {
            try
            {
                setPlayerStatus(false);
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in BufferingStarted_Event:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void BufferingEnded_Event(object sender, System.EventArgs e)
        {
            try
            {
                setPlayerStatus(true);
                progressBar.Visible = false;
                StatusLabel.Text = "Lecture";
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in BufferingEnded_Event:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }

        private void ItumRadio_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (eventLogger ev = new eventLogger("itumRadio"))
            {
                ev.writeLog("Fin normale de l'application", System.Diagnostics.EventLogEntryType.Information);
            }
        }
    }
}
