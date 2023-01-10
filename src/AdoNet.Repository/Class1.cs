using System.Data.SqlClient;
using System.Reflection;
using System.Security.Principal;
using System.Text;

namespace AdoNet.Repository
{
    internal class AdoRepository<T>
           where T : IEntity
    {
        private readonly string _connectionString;

        private readonly List<PropertyInfo> _properties = new List<PropertyInfo>(typeof(T).GetProperties());

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoRepository{T}"/> class.
        /// </summary>
        /// <param name="connectionString">A connection string.</param>
        public AdoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <inheritdoc cref="IRepository{T}.Create(T)"/>
        public async Task<int> Create(T item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                int newId = 0;

                StringBuilder queryString = new StringBuilder("INSERT INTO [" + typeof(T).Name + "] ");

                queryString.Append("(" + string.Join(",", _properties.Skip(1).Select(obj => $"[{obj.Name}]")) + ") ");
                queryString.Append("VALUES (" + string.Join(",", _properties.Skip(1).Select(obj => $"@{obj.Name}")) + ")");
                queryString.Append(";SELECT CAST(scope_identity() AS int);");

                _properties.ForEach(property => sqlCommand.Parameters.AddWithValue("@" + property.Name, property.GetValue(item)));

                sqlCommand.CommandText = queryString.ToString();

                try
                {
                    await connection.OpenAsync();
                    newId = Convert.ToInt32(await sqlCommand.ExecuteScalarAsync());
                }
                catch (SqlException ex)
                {
                    throw new CustomException(typeof(T).Name + $"Id = {item.Id} cannot be Created. " + ex.Message);
                }

                return newId;
            }
        }

        /// <inheritdoc cref="IRepository{T}.Delete(int)"/>
        public async Task Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;

                string queryString = "DELETE FROM " + typeof(T).Name + " WHERE Id = @Id";
                sqlCommand.CommandText = queryString;

                sqlCommand.Parameters.AddWithValue("@Id", id);

                try
                {
                    await connection.OpenAsync();
                    await sqlCommand.ExecuteNonQueryAsync();
                }
                catch (SqlException ex)
                {
                    throw new CustomException(typeof(T).Name + $"Id = {id} cannot be Deleted. " + ex.Message);
                }
            }
        }

        /// <inheritdoc cref="IRepository{T}.GetAll"/>
        public IQueryable<T> GetAll()
        {
            var data = new List<T>();

            Type typeOf = typeof(T);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;

                StringBuilder queryString = new StringBuilder("SELECT ");

                queryString.Append(string.Join(",", _properties.Select(obj => $"[{obj.Name}]")));
                queryString.Append(" FROM " + typeof(T).Name + ";");

                sqlCommand.CommandText = queryString.ToString();

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var obj = Activator.CreateInstance(typeOf);

                        for (int i = 0; i < _properties.Count; i++)
                        {
                            var fieldName = reader.GetName(i);
                            var propInfo = typeOf.GetProperty(fieldName);
                            propInfo?.SetValue(obj, reader.GetValue(i));
                        }

                        data.Add((T)obj);
                    }
                }
                catch (SqlException ex)
                {
                    throw new CustomException(typeof(T).Name + $" cannot be read all. " + ex.Message);
                }
            }

            return data.AsQueryable();
        }

        /// <inheritdoc cref="IRepository{T}.GetById(int)"/>
        public async Task<T> GetById(int id)
        {
            Type typeOf = typeof(T);
            T obj = default(T);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;

                StringBuilder queryString = new StringBuilder("SELECT ");

                queryString.Append(string.Join(",", _properties.Select(obj => $"[{obj.Name}]")));
                queryString.Append(" FROM " + typeof(T).Name + " WHERE [Id] = @id;");

                sqlCommand.CommandText = queryString.ToString();

                sqlCommand.Parameters.AddWithValue("@id", id);

                try
                {
                    await connection.OpenAsync();

                    var reader = await sqlCommand.ExecuteReaderAsync();

                    var count = reader.FieldCount;

                    if (reader.Read())
                    {
                        obj = Activator.CreateInstance<T>();

                        for (int i = 0; i < count; i++)
                        {
                            var fieldName = reader.GetName(i);
                            var propInfo = typeOf.GetProperty(fieldName);
                            propInfo?.SetValue(obj, reader.GetValue(i));
                        }
                    }

                    connection.Close();
                }
                catch (SqlException ex)
                {
                    throw new CustomException(typeof(T).Name + $"(Id = {id} cannot be Read. " + ex.Message);
                }
            }

            return obj;
        }

        /// <inheritdoc cref="IRepository{T}.Update(T)"/>
        public async Task Update(T item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;

                StringBuilder queryString = new StringBuilder("UPDATE " + typeof(T).Name + " SET ");

                foreach (var property in _properties)
                {
                    if (property.Name == "Id")
                    {
                        continue;
                    }

                    queryString.Append("[" + property.Name + "] =" + "@" + property.Name + ", ");
                }

                queryString.Remove(queryString.Length - 2, 1);

                queryString.Append(" WHERE [Id] = @" + nameof(item.Id) + "; ");

                sqlCommand.CommandText = queryString.ToString();

                _properties.ForEach(property => sqlCommand.Parameters.AddWithValue("@" + property.Name, property.GetValue(item)));

                try
                {
                    await connection.OpenAsync();

                    await sqlCommand.ExecuteNonQueryAsync();
                }
                catch (SqlException ex)
                {
                    throw new CustomException(typeof(T).Name + $"(Id = {item.Id} cannot be Updated. " + ex.Message);
                }
            }
        }
    }
}