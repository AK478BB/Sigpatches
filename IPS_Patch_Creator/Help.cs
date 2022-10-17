using System;
using System.Net;
using System.Windows.Forms;
namespace IPS_Patch_Creator
{
    public partial class Help : Form
    {
        readonly string pastebin = "https://pastebin.com/raw/Z99AZKhN";
        readonly string gists = "https://gist.githubusercontent.com/mrdude2478/aefe3e83dbcf143f2e02781b8442c3ed/raw/8439e0da9fb44716c6cdb954a7c06818681e8b72/gistfile1.txt";

        public Help()
        {
            InitializeComponent();
            read_from_net();
        }

        public void read_from_net()
        {
            try
            {
                //still to do - add timeout if website is down.......

                // create a new instance of WebClient
                WebClient client = new WebClient();

                // set the user agent to IE6
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)");

                bool url = URLExists(pastebin);
                bool alturl = URLExists(gists);
                
                if (url)
                {
                    string ret = client.DownloadString(pastebin);
                    richTextBox1.Text = ret;
                }
                
                else
                {
                    //backup url to use if pastebin is down.
                    if (alturl)
                    {
                        string ret = client.DownloadString(gists);
                        richTextBox1.Text = ret;
                    }
                    
                    //if both url's are down, mabey we have an internet issue? - show unreachable
                    else
                    {
                        richTextBox1.Text = "URL unreachable";
                    }
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        public bool URLExists(string url)
        {
            System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
            webRequest.Method = "HEAD";
            try
            {
                using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)webRequest.GetResponse())
                {
                    if (response.StatusCode.ToString() == "OK")
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            richTextBox1.Copy();
        }
    }
}
