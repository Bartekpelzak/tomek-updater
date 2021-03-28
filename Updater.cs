using System;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using Ionic.Zip;
using System.Security.AccessControl;

namespace tomek_updater
{
    public partial class Updater : Form
    {
        private readonly WebClient webClient = new WebClient();
        private readonly string gameDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".tomek");

        public Updater()
        {
            InitializeComponent();
        }

        private void WebClientDownload(Uri uri, string path)
        {
            Thread thread = new Thread(() =>
            {
                WebClient webClient = new WebClient();
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClientDownloadProgress);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(WebClientDownloadCompleted);
                webClient.DownloadFileAsync(uri, path);
            });

            thread.Start();
        }

        private void WebClientDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                // Progress bar
                var bytesDownloaded = double.Parse(e.BytesReceived.ToString());
                var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                var percentageDownloaded = bytesDownloaded / totalBytes * 100;

                // Label
                var megabytesDownloaded = bytesDownloaded / 1024 / 1024;
                var totalMegabytes = totalBytes / 1024 / 1024;

                // string.Format("Downloaded {0:0.00}/{1:0.00} MB ({2:0.0}%, {3:0.0} MB/s)", megabytesDownloaded, totalMegabytes, percentageDownloaded, downloadSpeed);

                downloadProgress.Value = int.Parse(Math.Truncate(percentageDownloaded).ToString());
                downloadStatusLabel.Text = string.Format("Downloaded {0:0.00}/{1:0.00} MB ({2:0.0}%)", megabytesDownloaded, totalMegabytes, percentageDownloaded);
            });
        }

        private void WebClientDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                downloadStatusLabel.Text = "Extracting...";
                Wait(50);
                UpdateGame(gameDirectory);
                StartGameProcess("tomek.exe", gameDirectory);
                Environment.Exit(0);
            });
        }
        public void Wait(int milliseconds)
        {
            var timer = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer.Interval = milliseconds;
            timer.Enabled = true;
            timer.Start();

            timer.Tick += (s, e) =>
            {
                timer.Enabled = false;
                timer.Stop();
            };

            while (timer.Enabled)
            {
                Application.DoEvents();
            }
        }
        private bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                MessageBox.Show("Connect to the Internet!", "Updating tomek.exe");
                return false;
            }
        }
        private bool CheckGameVersion(Uri versionUri, string gameDirectory, string existingVersionFile)
        {
            try
            {
                // Download version string from the internet
                var server_version = new WebClient().DownloadString(versionUri);
                var local_version = System.IO.File.ReadAllText(Path.Combine(gameDirectory, existingVersionFile));

                downloadStatusLabel.Text = "Checking for updates...";

                Wait(1000);

                // Return true or false depending if version is the same or different
                if (server_version == local_version)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool CreateGameDirectory(string directoryName)
        {
            try
            {
                System.IO.Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), directoryName));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create game directory. Check permissions for the .tomek folder in %AppData%. " + ex.Message, "Updating tomek.exe");
                return false;
            }
        }

        private bool StartGameProcess(string gameExecutable, string gameDirectory)
        {
            try
            {
                using (Process gameProcess = new Process())
                {
                    gameProcess.StartInfo.WorkingDirectory = gameDirectory; // Sets the working directory so that the game doesnt crash trying to find sfx/gfx folders
                    gameProcess.StartInfo.FileName = Path.Combine(gameDirectory, gameExecutable);
                    gameProcess.StartInfo.UseShellExecute = true;
                    gameProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

                    gameProcess.Start();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start game. Re-launch the updater. " + ex.Message, "Updating tomek.exe");
                return false;
            }
        }

        private bool DownloadGameUpdate(Uri packageUri, string gameDirectory, string packageName)
        {
            try
            {
                WebClientDownload(packageUri, Path.Combine(gameDirectory, packageName));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to download update package. Are you connected to the internet? " + ex.Message, "Updating tomek.exe");
                return false;
            }
        }

        private bool UpdateGame(string gameDirectory)
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(gameDirectory, "Update"));

                using (ZipFile packageZip = ZipFile.Read(Path.Combine(gameDirectory, "package.zip")))
                {
                    packageZip.ExtractAll(Path.Combine(gameDirectory, "Update"), ExtractExistingFileAction.OverwriteSilently);
                }

                using (ZipFile zip = ZipFile.Read(Path.Combine(gameDirectory, "Update", "app.zip")))
                {
                    zip.ExtractAll(Path.Combine(gameDirectory), ExtractExistingFileAction.OverwriteSilently);
                }

                using (ZipFile zip = ZipFile.Read(Path.Combine(gameDirectory, "Update", "gfx.zip")))
                {
                    zip.ExtractAll(Path.Combine(gameDirectory), ExtractExistingFileAction.OverwriteSilently);
                }

                using (ZipFile zip = ZipFile.Read(Path.Combine(gameDirectory, "Update", "sfx.zip")))
                {
                    zip.ExtractAll(Path.Combine(gameDirectory), ExtractExistingFileAction.OverwriteSilently);
                }

                Directory.Delete(Path.Combine(gameDirectory, "Update"), true);
                File.Delete(Path.Combine(gameDirectory, "package.zip"));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to extract package. Check permissions for the .tomek folder in %AppData%. " + ex.Message, "Updating tomek.exe");
                return false;
            }
        }
        private bool CheckIfGameDirectoryIsReadOnly(string gameDirectory)
        {
            var gameDirectoryInfo = new DirectoryInfo(gameDirectory);

            if (gameDirectoryInfo.Attributes.HasFlag(FileAttributes.ReadOnly))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool FixGameDirectoryPermissions(string gameDirectory)
        {
            try
            {
                DirectorySecurity securityRules = new DirectorySecurity();
                securityRules.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));

                Directory.SetAccessControl(gameDirectory, securityRules);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void Updater_Load(object sender, EventArgs e)
        {
            if (!CheckForInternetConnection())
            {
                Environment.Exit(1);
            }

            if (Directory.Exists(gameDirectory))
            {
                if (CheckIfGameDirectoryIsReadOnly(gameDirectory))
                {
                    FixGameDirectoryPermissions(gameDirectory);
                }

                if (CheckGameVersion(new System.Uri("https://moonware.xyz/other/tomek-patch/version.txt"), gameDirectory, "version.txt"))
                {
                    if (StartGameProcess("tomek.exe", gameDirectory))
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    DownloadGameUpdate(new System.Uri("https://moonware.xyz/other/tomek-patch/package.zip"), gameDirectory, "package.zip");
                }
            }
            else
            {
                CreateGameDirectory(".tomek");
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            webClient.CancelAsync();
            webClient.DownloadDataCompleted -= WebClientDownloadCompleted;
            Environment.Exit(0);
        }

        private void Updater_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            webClient.CancelAsync();
            webClient.DownloadDataCompleted -= WebClientDownloadCompleted;

            e.Cancel = false;
        }
    }
}
