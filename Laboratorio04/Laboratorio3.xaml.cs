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
    /// Lógica de interacción para Laboratorio3.xaml
    /// </summary>
    public partial class Laboratorio3 : Window
    {
        private string connectionString = "Data Source=LAPTOP-R79EK4NG\\SQLEXPRESS2017;Initial Catalog=Neptuno;Integrated Security=True; TrustServerCertificate=True; Encrypt=True";
        public Laboratorio3()
        {
            InitializeComponent();
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            if (dpDesde.SelectedDate == null || dpHasta.SelectedDate == null)
            {
                MessageBox.Show("Selecciona ambas fechas.");
                return;
            }

            DateTime fechaInicio = dpDesde.SelectedDate.Value;
            DateTime fechaFin = dpHasta.SelectedDate.Value;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("ListarPedidosConDetallesPorFecha", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgResultados.ItemsSource = dt.DefaultView;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error de SQL: " + ex.Message);
                }
            }
        }
        private void BtnAtras_Click(object sender, RoutedEventArgs e)
        {
            Laboratorio2 v2 = new Laboratorio2();
            v2.Show();
            this.Close();
        }

    }
}
