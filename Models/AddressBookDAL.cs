using System.Data;
using System.Data.SqlClient;

namespace Practical_AddressBook1.Models
{
    public class AddressBookDAL
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        public static IConfiguration Configuration { get; set; }

        // Connect to Database
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        // Get Database View
        public List<AddressBookLogs> GetAll()
        {
            List<AddressBookLogs> addressClasses = new List<AddressBookLogs>();
            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[sp_DisplayInfo]";
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    AddressBookLogs Address = new AddressBookLogs();
                    Address.PersonID = (int)dr["PersonID"];
                    Address.PersonName = (string)dr["PersonName"];
                    Address.PersonAddress = (string)dr["PersonAddress"];
                    Address.City = (string)dr["City"];
                    addressClasses.Add(Address);
                }
                connection.Close();
            }
            return addressClasses;
        }
        // Insert address book
        public bool Insert(AddressBookLogs Address)
        {
            int flag = 0;

            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[DBO].[usp_InsertData]";

                command.Parameters.AddWithValue("@PersonID", Address.PersonID);
                command.Parameters.AddWithValue("@PersonName", Address.PersonName);
                command.Parameters.AddWithValue("@PersonAddress", Address.PersonAddress);
                command.Parameters.AddWithValue("@City", Address.City);

                connection.Open();

                flag = command.ExecuteNonQuery();

                connection.Close();
            }

            // For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command
            return flag > 0 ? true : false;
        }

        // Get Address by ID
        public AddressBookLogs GetPersonById(int id)
        {
            AddressBookLogs logs = new AddressBookLogs();
            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[DBO].[usp_GetPersonID]";
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    logs.PersonID = (int)dr["PersonID"];
                    logs.PersonName = (string)dr["PersonName"];
                    logs.PersonAddress = (string)dr["PersonAddress"];
                    logs.City = (string)dr["City"];
                }
                connection.Close();
            }
            return logs;
        }

        // Update address book
        public bool Update(AddressBookLogs abl)
        {
            int flag = 0;
            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[usp_UpdateAddress]";

                command.Parameters.AddWithValue("@PersonID", abl.PersonID);
                command.Parameters.AddWithValue("@PersonName", abl.PersonName);
                command.Parameters.AddWithValue("@PersonAddress", abl.PersonAddress);
                command.Parameters.AddWithValue("@City", abl.City);

                connection.Open();
                flag = command.ExecuteNonQuery();
                connection.Close();
            }
            return flag > 0 ? true : false;
        }

        // Delete address book
        public bool Delete(int id)
        {
            int flag = 0;

            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[usp_DeleteAddress]";
                command.Parameters.AddWithValue("@PersonID", id);
                connection.Open();

                flag = command.ExecuteNonQuery();

                connection.Close();
            }

            return flag > 0 ? true : false;
        }
    }
}
