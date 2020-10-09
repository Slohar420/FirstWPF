using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        Registration registration = new Registration();
        Welcome welcome = new Welcome();
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                MySqlConnection con = new MySqlConnection("Server = localhost; Port = 3307; Database = WPFDB; Uid = root; Pwd = lipimysql;");
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from Registration where Email='" + email + "'  and password='" + password + "'", con);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string username = dataSet.Tables[0].Rows[0]["FirstName"].ToString() + " " + dataSet.Tables[0].Rows[0]["LastName"].ToString();
                    welcome.TextBlockName.Text = username;//Sending value from one form to another form.  
                    welcome.Show();
                    Close();
                }
                else
                {
                    errormessage.Text = "Sorry! Please enter existing emailid/password.";
                }
                con.Close();
            }
        }
        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            registration.Show();
            Close();
        }
    }
}
