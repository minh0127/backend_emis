using Dapper;
using MISA.Web06.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>, IDisposable
    {
        #region Properties
        protected string ConnectionString;
        protected MySqlConnection MySqlConnection;
        protected string TableName;
        #endregion

        /// <summary>
        /// Hàm khởi tạo: Kết nối database
        /// </summary>
        /// CreatedBy: MINHBUI (04/08/2022)
        public BaseRepository()
        {
            ConnectionString = "Host=3.0.89.182;" +
                    " Port=3306;" +
                    " Database=MISA.WEB06.BTMINH;" +
                    " User Id=dev;" +
                    " Password=12345678;" + 
                    " default command timeout=300;";
            TableName = typeof(MISAEntity).Name;
        }

        public IEnumerable<MISAEntity> Get()
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"SELECT * FROM {TableName}";

                var entity = MySqlConnection.Query<MISAEntity>(sqlCommand);

                return entity;
            }
        }

        public MISAEntity GetById(Guid id)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"SELECT * FROM {TableName} WHERE {TableName}ID = @{TableName}ID";
                var parameters = new DynamicParameters();
                parameters.Add($"@{TableName}ID", id);

                var res = MySqlConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: parameters);

                return res;
            }
        }

        public int Insert(MISAEntity entity)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"Proc_Insert{TableName}";

                var res = MySqlConnection.Execute(sqlCommand, param: entity, commandType: System.Data.CommandType.StoredProcedure);

                return res;
            }
        }

        public int Update(MISAEntity entity)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"Proc_Update{TableName}";

                var res = MySqlConnection.Execute(sqlCommand, param: entity, commandType: System.Data.CommandType.StoredProcedure);

                return res;
            }
        }

        public int Delete(Guid id)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"DELETE FROM {TableName} WHERE {TableName}ID = @{TableName}ID";
                var parameters = new DynamicParameters();
                parameters.Add($"@{TableName}ID", id);

                var res = MySqlConnection.Execute(sqlCommand, param: parameters);

                return res;
            }
        }

        public void Dispose()
        {
            MySqlConnection.Close();
            MySqlConnection.Dispose();
        }
    }
}
