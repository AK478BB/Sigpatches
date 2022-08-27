using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace IPS_Patch_Creator
{
    public partial class FTP : Form
    {
        public static string mydb = ("tools/ftp.db");
        public static string mydatabase = ("Data Source=" + mydb);
        string ip;
        string port;
        string user;
        string password;
        public FTP()
        {
            InitializeComponent();

            //check the database exists + check tables
            run_first();
        }

        private void run_first()
        {
            try
            {
                if (!File.Exists(mydb))
                {
                    MessageBox.Show("No default ftp settings found, creating a basic ftp database", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    SQLiteConnection.CreateFile(mydb);
                    database_make_tables();
                    Fill_textbox();
                }
                else
                {
                    Fill_textbox();
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
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
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS ftp(ip Text, port Text, user Text, pass Text)";
                cmd.ExecuteNonQuery();

                //populate empty tables with default values
                cmd.CommandText = "INSERT INTO ftp(ip, port, user, pass) VALUES(@ip, @port, @user, @pass)";
                cmd.Parameters.AddWithValue("@ip", "192.168.0.100");
                cmd.Parameters.AddWithValue("@port", "21");
                cmd.Parameters.AddWithValue("@user", "anonymous");
                cmd.Parameters.AddWithValue("@pass", "anonymous");
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                con.Close(); //close the database...

            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            user = textBox_user.Text;
            password = textBox_password.Text;
            ip = textBox_ip.Text;
            port = textBox_port.Text;

            if (user == "" || user == String.Empty || user.Length == 0)
            {
                user = "anonymous";
            }
            if (password == "" || password == String.Empty || password.Length == 0)
            {
                password = "anonymous";
            }
            if (ip == "" || ip == String.Empty || ip.Length == 0)
            {
                MessageBox.Show("No adrress set, You can't connect to an empty address", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //set default IP
                ip = "192.168.0.100";
                textBox_ip.Text = ip;
            }
            if (port == "" || port == String.Empty || port.Length == 0)
            {
                MessageBox.Show("No port value set, You can't connect to an empty port", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //set default port
                port = "21";
                textBox_port.Text = port;
            }

            if (port != "" && ip != "" && user != "" && password != "")
            {
                //save settings to the database now we have the info we need
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);
                cmd.CommandText = ("UPDATE ftp SET ip = '" + ip + "', port = '" + port + "', user = '" + user + "', pass = '" + password + "' WHERE rowid = 1");
                cmd.ExecuteNonQuery();
                con.Close();

                this.Close();
            }
        }

        private void Fill_textbox()
        {
            try
            {
                //read the database and populate the text box's
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);
                cmd.CommandText = "SELECT * from ftp";

                SQLiteDataReader sqReader = cmd.ExecuteReader();
                while (sqReader.Read())
                {
                    string ip = (sqReader.GetString(0));
                    string port = (sqReader.GetString(1));
                    string user = (sqReader.GetString(2));
                    string pass = (sqReader.GetString(3));
                    textBox_ip.Text = ip;
                    textBox_port.Text = port;
                    textBox_user.Text = user;
                    textBox_password.Text = pass;
                }
                sqReader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }
    }
}
