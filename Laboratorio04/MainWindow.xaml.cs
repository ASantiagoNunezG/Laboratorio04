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
            List<Producto> listaProductos = new List<Producto>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("ListarProductos", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto prod = new Producto
                        {
                            IdProducto = reader.GetInt32(0),
                            NombreProducto = reader.IsDBNull(1) ? null : reader.GetString(1),
                            IdProveedor = reader.GetInt32(2),
                            IdCategoria = reader.GetInt32(3),
                            CantidadPorUnidad = reader.IsDBNull(4) ? null : reader.GetString(4),
                            PrecioUnidad = reader.GetDecimal(5),
                            UnidadesEnExistencia = reader.GetInt16(6),
                            UnidadesEnPedido = reader.GetInt16(7),
                            NivelNuevoPedido = reader.GetInt16(8),
                            Suspendido = reader.GetInt16(9),
                            CategoriaProducto = reader.IsDBNull(10) ? null : reader.GetString(10)
                        };

                        listaProductos.Add(prod);
                    }

                    reader.Close();
                    dgProductos.ItemsSource = listaProductos;
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