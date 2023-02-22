using System;
using System.Data.Odbc;
using ReportOHKServlerss.Dtos;

namespace ReportOHKServlerss.Repositories
{
	public class OHKDataBaseRepository
	{
        private readonly string _connectionString;

        public OHKDataBaseRepository()
		{
            _connectionString = "Driver={Microsoft Access Driver (*.mdb, *.accdb)}; Dbq=C:/Users/panupongnunchukun/Work/Database/db22_9_2006_Evening.mdb;";
        }

        public string GetPeopleQuery()
        {
            var query = $@"SELECT Part_Code FROM Stock_OHK";
            return query;
        }

        public void RunQuery(string query)
        {
            OdbcCommand command = new OdbcCommand(query);

            using (OdbcConnection connection = new OdbcConnection(_connectionString))
            {
                command.Connection = connection;
                connection.Open();
                var reader = command.ExecuteReader();
            }
        }

        public List<StockDto> GetPeople(string query)
        {
            var people = new List<StockDto>();
            OdbcCommand command = new OdbcCommand(query);

            using (OdbcConnection connection = new OdbcConnection(_connectionString))
            {
                command.Connection = connection;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var person = new StockDto();
                        person.PartCode = reader.GetString(0);
                        people.Add(person);
                    }
                };
            }
            return people;
        }

        public List<StockDto> GetPeople()
        {
            string query = GetPeopleQuery();
            return GetPeople(query);

        }

    }
}

