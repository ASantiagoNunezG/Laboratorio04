using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace Laboratorio04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source=LAPTOP-R79EK4NG\\SQLEXPRESS2017;Initial Catalog=Neptuno;Integrated Security=True; TrustServerCertificate=True; Encrypt=True"; 
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnCargar_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("ListarProductos", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgProductos.ItemsSource = dt.DefaultView;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error de SQL: " + ex.Message);
                }
            }
        }
        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Laboratorio2 v2 = new Laboratorio2();
            v2.Show();
            this.Close();
        }
    }
}