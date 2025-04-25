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
            List<Categoria> listaCategorias = new List<Categoria>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("ListarCategorias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Categoria cat = new Categoria
                        {
                            IdCategoria = reader.GetInt32(0),
                            NombreCategoria = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Activo = reader.GetBoolean(3),
                            CodCategoria = reader.GetString(4)
                        };

                        listaCategorias.Add(cat);
                    }

                    reader.Close();
                    dgCategorias.ItemsSource = listaCategorias;
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
