using System;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.ComponentModel;
using View = System.Windows.Forms.View;
using System.IO.Compression;

/*
 * how to set values in another form
 * https://www.youtube.com/watch?v=t-4caAZKLJw
*/

namespace IPS_Patch_Creator
{
    public partial class Wildcards : Form
    {
        public static string mydb = ("tools/wildcards.db");
        public static string mydatabase = ("Data Source=" + mydb);
        readonly string url = "https://github.com/mrdude2478/patches/releases/download/1/wildcards.zip";

        public static Wildcards instance;
        public Wildcards()
        {
            InitializeComponent();
            instance = this;
            populate_text(); //fill the text boxes with data from the Main WCO tab textboxes.
            sqlversion();
            checkdatabase();
        }

        private void populate_text()
        {
            try
            {
                textBox_es_patch.Text = Main.instance.tb_espatch.Text;
                textBox_fs_patch1.Text = Main.instance.tb_fs_patch1.Text;
                textBox_fs_patch2.Text = Main.instance.tb_fs_patch2.Text;
                textBox_nfim_patch1.Text = Main.instance.tb_nfim_patch1.Text;
                textBox_nfim_patch2.Text = Main.instance.tb_nfim_patch2.Text;
                textBox_es_override.Text = Main.instance.tb_es_override.Text;
                textBox_fs1_override.Text = Main.instance.tb_fs1_override.Text;
                textBox_fs2_override.Text = Main.instance.tb_fs2_override.Text;
                textBox_nfim_override.Text = Main.instance.tb_nfim_override.Text;
                richTextBox_about.Text = "The wilcards and patches above are taken from the current values shown in the WCO tab from the main menu.";
            }

            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            try
            {
                send_to_wco();
            }

            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sqlversion()
        {
            try
            {
                //test database connection and version
                string database = "Data Source=:memory:";
                string stm = "SELECT SQLITE_VERSION()";
                using var con = new SQLiteConnection(database);
                con.Open();
                using var cmd = new SQLiteCommand(stm, con);
                string version = cmd.ExecuteScalar().ToString();
                this.Text = ($"Wildcard Overrides Database - Powered by SQLite version: {version}");
                con.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void checkdatabase()
        {
            if (File.Exists(mydb))
            {
                populatelist();
            }
            else
            {
                MessageBox.Show("No database found, creating a basic database", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                SQLiteConnection.CreateFile(mydb);
                //best make some tables now....
                database_make_tables();
                populatelist();
            }
        }

        private void database_make_tables()
        {
            try
            {
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);

                //create the empty tables
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS wildcards(id INTEGER, FW_Version Text, es_patch Text, fs_patch1 Text, fs_patch2 Text, nfim_patch1 Text, nfim_patch2 Text, es_override Text, fs1_override Text, fs2_override Text, nfim_override Text, info Text)";
                cmd.ExecuteNonQuery();

                //populate empty tables with default values
                cmd.CommandText = "INSERT INTO wildcards(id, FW_Version, es_patch, fs_patch1, fs_patch2, nfim_patch1, nfim_patch2, es_override, fs1_override, fs2_override, nfim_override, info) VALUES(@id, @FW_Version, @es_patch, @fs_patch1, @fs_patch2, @nfim_patch1, @nfim_patch2, @es_override, @fs1_override, @fs2_override, @nfim_override, @info)";
                cmd.Parameters.AddWithValue("@id", 1);
                cmd.Parameters.AddWithValue("@FW_Version", textBox_FW_Version.Text);
                cmd.Parameters.AddWithValue("@es_patch", textBox_es_patch.Text);
                cmd.Parameters.AddWithValue("@fs_patch1", textBox_fs_patch1.Text);
                cmd.Parameters.AddWithValue("@fs_patch2", textBox_fs_patch2.Text);
                cmd.Parameters.AddWithValue("@nfim_patch1", textBox_nfim_patch1.Text);
                cmd.Parameters.AddWithValue("@nfim_patch2", textBox_nfim_patch2.Text);
                cmd.Parameters.AddWithValue("@es_override", textBox_es_override.Text);
                cmd.Parameters.AddWithValue("@fs1_override", textBox_fs1_override.Text);
                cmd.Parameters.AddWithValue("@fs2_override", textBox_fs2_override.Text);
                cmd.Parameters.AddWithValue("@nfim_override", textBox_nfim_override.Text);
                cmd.Parameters.AddWithValue("@info", richTextBox_about.Text);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

                con.Close(); //close the database...

            }

            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void populatelist()
        {
            try
            {
                listView1.Clear(); //reset

                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con)
                {
                    CommandText = "SELECT * from wildcards ORDER BY FW_Version COLLATE NOCASE ASC"
                };

                SQLiteDataReader sqReader = cmd.ExecuteReader();

                listView1.View = View.Details;
                listView1.Columns.Add("SDK Version");

                while (sqReader.Read())
                {
                    int listid = (sqReader.GetInt32(0));
                    string id = listid.ToString();

                    string FW_Version = (sqReader.GetString(1));
                    string es_patch = (sqReader.GetString(2));
                    string fs_patch1 = (sqReader.GetString(3));
                    string fs_patch2 = (sqReader.GetString(4));
                    string nfim_patch1 = (sqReader.GetString(5));
                    string nfim_patch2 = (sqReader.GetString(6));
                    string es_override = (sqReader.GetString(7));
                    string fs1_override = (sqReader.GetString(8));
                    string fs2_override = (sqReader.GetString(9));
                    string nfim_override = (sqReader.GetString(10));
                    string info = (sqReader.GetString(11));
                    listView1.Items.Add(new ListViewItem(new string[] { FW_Version, es_patch, fs_patch1, fs_patch2, nfim_patch1, nfim_patch2, es_override, fs1_override, fs2_override, nfim_override, info, id }));
                }

                con.Close();
                con.Dispose();

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sdk_click();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string FW_Version = textBox_FW_Version.Text;
                string es_patch = textBox_es_patch.Text;
                string fs_patch1 = textBox_fs_patch1.Text;
                string fs_patch2 = textBox_fs_patch2.Text;
                string nfim_patch1 = textBox_nfim_patch1.Text;
                string nfim_patch2 = textBox_nfim_patch2.Text;
                string es_override = textBox_es_override.Text;
                string fs1_override = textBox_fs1_override.Text;
                string fs2_override = textBox_fs2_override.Text;
                string nfim_override = textBox_nfim_override.Text;
                string info = richTextBox_about.Text;

                if (FW_Version == "" || FW_Version == String.Empty || textBox_FW_Version.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the SDK Version box");
                    return;
                }
                if (es_patch == "" || es_patch == String.Empty || textBox_es_patch.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the ES Patch box");
                    return;
                }
                if (fs_patch1 == "" || fs_patch1 == String.Empty || textBox_fs_patch1.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the FS Patch 1 box");
                    return;
                }
                if (fs_patch2 == "" || fs_patch2 == String.Empty || textBox_fs_patch2.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the FS Patch 2 box");
                    return;
                }
                if (nfim_patch1 == "" || nfim_patch1 == String.Empty || textBox_nfim_patch1.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the NFIM Patch 1 box");
                    return;
                }
                if (nfim_patch2 == "" || nfim_patch2== String.Empty || textBox_nfim_patch2.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the NFIM Patch 2 box");
                    return;
                }
                if (es_override == "" || es_override == String.Empty || textBox_es_override.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the ES Wildcard box");
                    return;
                }
                if (fs1_override == "" || fs1_override == String.Empty || textBox_fs1_override.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the FS Patch 1 Wildcard box");
                    return;
                }
                if (fs2_override == "" || fs2_override == String.Empty || textBox_fs2_override.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the FS Patch 2 Wildcard box");
                    return;
                }
                if (nfim_override == "" || nfim_override == String.Empty || textBox_nfim_override.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the NFIM Wildcard box");
                    return;
                }

                else
                {
                    using var con = new SQLiteConnection(mydatabase);
                    con.Open();
                    using var cmd = new SQLiteCommand(con);

                    cmd.CommandText = "SELECT MAX(rowid) FROM wildcards";
                    int i = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.CommandText = "INSERT INTO wildcards(id, FW_Version, es_patch, fs_patch1, fs_patch2, nfim_patch1, nfim_patch2, es_override, fs1_override, fs2_override, nfim_override, info) VALUES(@id, @FW_Version, @es_patch, @fs_patch1, @fs_patch2, @nfim_patch1, @nfim_patch2, @es_override, @fs1_override, @fs2_override, @nfim_override, @info)";
                    cmd.Parameters.AddWithValue("@id", i+1);
                    cmd.Parameters.AddWithValue("@FW_Version", FW_Version);
                    cmd.Parameters.AddWithValue("@es_patch", es_patch);
                    cmd.Parameters.AddWithValue("@fs_patch1", fs_patch1);
                    cmd.Parameters.AddWithValue("@fs_patch2", fs_patch2);
                    cmd.Parameters.AddWithValue("@nfim_patch1", nfim_patch1);
                    cmd.Parameters.AddWithValue("@nfim_patch2", nfim_patch2);
                    cmd.Parameters.AddWithValue("@es_override", es_override);
                    cmd.Parameters.AddWithValue("@fs1_override", fs1_override);
                    cmd.Parameters.AddWithValue("@fs2_override", fs2_override);
                    cmd.Parameters.AddWithValue("@nfim_override", nfim_override);
                    cmd.Parameters.AddWithValue("@info", info);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    con.Close(); //close the database...
                    con.Dispose();

                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    populatelist();
                    MessageBox.Show(FW_Version + " added to the database", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            
            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sdk_click()
        {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                    return;
                ListViewItem item = listView1.SelectedItems[0];
                //fill the text boxes
                textBox_FW_Version.Text = item.SubItems[0].Text;
                textBox_es_patch.Text = item.SubItems[1].Text;
                textBox_fs_patch1.Text = item.SubItems[2].Text;
                textBox_fs_patch2.Text = item.SubItems[3].Text;
                textBox_nfim_patch1.Text = item.SubItems[4].Text;
                textBox_nfim_patch2.Text = item.SubItems[5].Text;
                textBox_es_override.Text = item.SubItems[6].Text;
                textBox_fs1_override.Text = item.SubItems[7].Text;
                textBox_fs2_override.Text = item.SubItems[8].Text;
                textBox_nfim_override.Text = item.SubItems[9].Text;
                richTextBox_about.Text = item.SubItems[10].Text;
            }

            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                string FW_Version = textBox_FW_Version.Text;
                string es_patch = textBox_es_patch.Text;
                string fs_patch1 = textBox_fs_patch1.Text;
                string fs_patch2 = textBox_fs_patch2.Text;
                string nfim_patch1 = textBox_nfim_patch1.Text;
                string nfim_patch2 = textBox_nfim_patch2.Text;
                string es_override = textBox_es_override.Text;
                string fs1_override = textBox_fs1_override.Text;
                string fs2_override = textBox_fs2_override.Text;
                string nfim_override = textBox_nfim_override.Text;
                string info = richTextBox_about.Text;

                if (FW_Version == "" || FW_Version == String.Empty || textBox_FW_Version.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the SDK Version box");
                    return;
                }
                if (es_patch == "" || es_patch == String.Empty || textBox_es_patch.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the ES Patch box");
                    return;
                }
                if (fs_patch1 == "" || fs_patch1 == String.Empty || textBox_fs_patch1.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the FS Patch 1 box");
                    return;
                }
                if (fs_patch2 == "" || fs_patch2 == String.Empty || textBox_fs_patch2.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the FS Patch 2 box");
                    return;
                }
                if (nfim_patch1 == "" || nfim_patch1 == String.Empty || textBox_nfim_patch1.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the NFIM Patch 1 box");
                    return;
                }
                if (nfim_patch2 == "" || nfim_patch2 == String.Empty || textBox_nfim_patch2.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the NFIM Patch 2 box");
                    return;
                }
                if (es_override == "" || es_override == String.Empty || textBox_es_override.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the ES Wildcard box");
                    return;
                }
                if (fs1_override == "" || fs1_override == String.Empty || textBox_fs1_override.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the FS Patch 1 Wildcard box");
                    return;
                }
                if (fs2_override == "" || fs2_override == String.Empty || textBox_fs2_override.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the FS Patch 2 Wildcard box");
                    return;
                }
                if (nfim_override == "" || nfim_override == String.Empty || textBox_nfim_override.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the NFIM Wildcard box");
                    return;
                }

                else
                {
                    if (listView1.SelectedItems.Count == 0)
                        return;
                    if (MessageBox.Show("Update " + FW_Version + " on the database?", "Update!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using var con = new SQLiteConnection(mydatabase);
                        con.Open();
                        using var cmd = new SQLiteCommand(con);

                        //get selected item
                        ListViewItem item = listView1.SelectedItems[0];
                        string ver = item.SubItems[11].Text;
                        //MessageBox.Show(ver);
                        int id = Int32.Parse(ver);

                        if (ver == "1") //don't mod the original database entry
                        {
                            MessageBox.Show("This database item has been locked by MrDude, you can't remove or update it!", "Dude!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        cmd.CommandText = ("UPDATE wildcards set FW_Version = '" + FW_Version + "', es_patch = '" + es_patch + "', fs_patch1 = '" + fs_patch1 + "', fs_patch2 = '" + fs_patch2 + "', nfim_patch1 = '" + nfim_patch1 + "', nfim_patch2 = '" + nfim_patch2 + "', es_override = '" + es_override + "', fs1_override = '" + fs1_override + "', fs2_override = '" + fs2_override + "', nfim_override = '" + nfim_override + "', info = '" + info + "' WHERE id = " + id + "");

                        cmd.Prepare();
                        cmd.ExecuteNonQuery();

                        con.Close(); //close the database...
                        con.Dispose();

                        listView1.Items.Clear();
                        listView1.Columns.Clear();
                        populatelist();
                        MessageBox.Show(FW_Version + " updated on the database", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            try
            {
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);

                if (listView1.SelectedItems.Count == 0)
                    return;

                ListViewItem item = listView1.SelectedItems[0];
                string myid = (item.SubItems[11].Text);

                if  (myid == "1") //don't delete the original database entry
                {
                    return;
                }

                if (MessageBox.Show("Remove " + item.SubItems[0].Text + " from the database?", "Remove from database!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd.CommandText = "DELETE FROM wildcards WHERE id = " + myid + ";";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();

                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    populatelist();

                    MessageBox.Show(item.SubItems[0].Text + " removed from the database", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

                else
                {
                    return;
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void send_to_wco()
        {
            try
            {
                Main.instance.tb_espatch.Text = textBox_es_patch.Text;
                Main.instance.tb_fs_patch1.Text = textBox_fs_patch1.Text;
                Main.instance.tb_fs_patch2.Text = textBox_fs_patch2.Text;
                Main.instance.tb_nfim_patch1.Text = textBox_nfim_patch1.Text;
                Main.instance.tb_nfim_patch2.Text = textBox_nfim_patch2.Text;
                Main.instance.tb_es_override.Text = textBox_es_override.Text;
                Main.instance.tb_fs1_override.Text = textBox_fs1_override.Text;
                Main.instance.tb_fs2_override.Text = textBox_fs2_override.Text;
                Main.instance.tb_nfim_override.Text = textBox_nfim_override.Text;

                //show message to say the value have been changed in the wco tab in the main menu.
                MessageBox.Show("Wilcard patterns in the WCO tab have been updated.", "WCO Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            send_to_wco();
            this.Close();
        }

        private void updateDatabaseFromInternetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string zipfile = "tools/wildcard.zip";
                if (File.Exists(zipfile))
                {
                    File.Delete(zipfile);
                }

                System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
                webRequest.Method = "HEAD";
                try
                {
                    using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)webRequest.GetResponse())
                    {
                        if (response.StatusCode.ToString() == "OK")
                        {
                            richTextBox_about.Text = "Trying to download the new database";
                            DownLoadFileInBackground(url, zipfile);
                        }
                        else
                        {
                            MessageBox.Show("The download link can't be reached just now, try later!", "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Http error - can't get the database just now!", "Oh No!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DownLoadFileInBackground(string address, string location)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri(address);

            // Specify a progress notification handler.
            // client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
            // Call DownloadFileCallback when the download completes.
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);
            //start downloading
            client.DownloadFileAsync(uri, location);
            client.Dispose();
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                (string)e.UserState,
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage);
        }

        private void DownloadFileCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }

            download_done();
        }

        public void download_done()
        {
            try
            {
                richTextBox_about.Text += "\n" + "Database downloaded!";

                if (MessageBox.Show("Would you like to unzip the new database and overwrite your old one?", "Update Database!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String myzip = "tools/wildcard.zip";
                    String mydb = "tools/wildcards.db";
                    if (File.Exists(myzip))
                    {
                        //kill in use database
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        
                        File.Delete(mydb);
                        ZipFile.ExtractToDirectory(myzip, "tools/");
                        richTextBox_about.Text += "\n" + "Zip extracted and new database loaded!";
                        //
                        listView1.Items.Clear();
                        listView1.Columns.Clear();
                        //populate_text();
                        populatelist();
                        //cleanup....
                        File.Delete(myzip);
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("The error is: " + error.Message, "Eeeeek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
