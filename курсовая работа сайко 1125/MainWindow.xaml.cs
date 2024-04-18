using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CollegeAdmissionAutomation
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source=yourServer;Initial Catalog=yourDatabase;Integrated Security=True";
        private DataTable applicantsDataTable;

        public MainWindow()
        {
            InitializeComponent();
            LoadApplicants();
            applicantsDataGrid.ItemsSource = applicantsDataTable.DefaultView;
        }

        private void LoadApplicants()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Applicants";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                applicantsDataTable = new DataTable();
                adapter.Fill(applicantsDataTable);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                DataView applicantsView = applicantsDataTable.DefaultView;
                applicantsView.RowFilter = $"Name LIKE '%{searchTerm}%'";
                applicantsDataGrid.ItemsSource = applicantsView;
            }
            else
            {
                LoadApplicants();
                applicantsDataGrid.ItemsSource = applicantsDataTable.DefaultView;
            }
        }
    }
}