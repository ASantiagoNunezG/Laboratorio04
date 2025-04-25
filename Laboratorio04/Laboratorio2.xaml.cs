using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace Laboratorio04
{
    /// <summary>
    /// Lógica de interacción para Laboratorio2.xaml
    /// </summary>
    public partial class Laboratorio2 : Window
    {
        private string connectionString = "Data Source=LAPTOP-R79EK4NG\\SQLEXPRESS2017;Initial Catalog=Neptuno;Integrated Security=True; TrustServerCertificate=True; Encrypt=True";
        public Laboratorio2()
        {
            InitializeComponent();
        }
        private void BtnCargarCategorias_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("ListarCategorias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgCategorias.ItemsSource = dt.DefaultView;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error de SQL: " + ex.Message);
                }
            }
        }
        private void BtnAtras_Click(object sender, RoutedEventArgs e)
        {
            MainWindow v1 = new MainWindow();
            v1.Show();
            this.Close();
        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Laboratorio3 v3 = new Laboratorio3();
            v3.Show();
            this.Close();
        }
    }
}
