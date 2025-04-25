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

            List<Pedido> listaPedidos = new List<Pedido>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("ListarPedidosConDetallesPorFecha", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Pedido pedidoActual = null;

                    while (reader.Read())
                    {
                        int idPedido = reader.GetInt32(0);

                        
                        if (pedidoActual == null || pedidoActual.IdPedido != idPedido)
                        {
                            if (pedidoActual != null)
                            {
                                listaPedidos.Add(pedidoActual); 
                            }

                            pedidoActual = new Pedido
                            {
                                IdPedido = idPedido,
                                IdCliente = reader.GetString(1),
                                IdEmpleado = reader.GetInt32(2),
                                FechaPedido = reader.GetDateTime(3),
                                FechaEntrega = reader.GetDateTime(4),
                                FechaEnvio = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                                FormaEnvio = reader.GetInt32(6),
                                Cargo = reader.GetDecimal(7),
                                Destinatario = reader.GetString(8),
                                DireccionDestinatario = reader.GetString(9),
                                CiudadDestinatario = reader.GetString(10),
                                RegionDestinatario = reader.IsDBNull(11) ? null : reader.GetString(11),
                                CodPostalDestinatario = reader.IsDBNull(12) ? null : reader.GetString(12),
                                PaisDestinatario = reader.GetString(13),
                                Detalles = new List<DetallePedido>()
                            };
                        }

                        
                        DetallePedido detalle = new DetallePedido
                        {
                            IdProducto = reader.GetInt32(14),
                            PrecioUnidad = reader.GetDecimal(15),
                            Cantidad = reader.GetInt32(16),
                            Descuento = reader.GetDecimal(17)
                        };

                        pedidoActual.Detalles.Add(detalle);
                    }

                    
                    if (pedidoActual != null)
                    {
                        listaPedidos.Add(pedidoActual);
                    }

                    reader.Close();
                    dgResultados.ItemsSource = listaPedidos;
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
