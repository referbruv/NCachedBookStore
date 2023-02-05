using Alachisoft.NCache.Runtime.Caching;
using Alachisoft.NCache.Runtime.DatasourceProviders;
using NCachedBookStore.Contracts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NCachedBookStore.CachingProviders.Providers
{
    public class NCachedWriteThruProvider : IWriteThruProvider
    {
        private SqlConnection _connection;

        public NCachedWriteThruProvider()
        {
        }

        public void Dispose()
        {
            // final cleanup
            if (_connection != null)
            {
                _connection.Close();
            }
        }

        public void Init(IDictionary parameters, string cacheId)
        {
            // initialization functionality
            try
            {
                string connString = GetConnectionString(parameters);

                if (!string.IsNullOrEmpty(connString))
                {
                    _connection = new SqlConnection(connString);
                    _connection.Open();
                }
            }
            catch (Exception)
            {
                // Handle exception
            }
        }

        public OperationResult WriteToDataSource(WriteOperation operation)
        {
            ProviderCacheItem cacheItem = operation.ProviderItem;
            Book book = cacheItem.GetValue<Book>();

            var dtBook = new DataTable();
            // transform book object into a database

            switch (operation.OperationType)
            {
                case WriteOperationType.Add:
                    {
                        // Insert logic for any Add operation
                        var commandText = string.Format(
                            "INSERT INTO dbo.Books (Name, Description, ISBN, Price, AuthorName) VALUES ({0}, {1}, {2}, {3}, {4})",
                            book.Name, book.Description, book.ISBN, book.Price, book.AuthorName);

                        new SqlCommand(commandText, _connection).ExecuteNonQuery();
                    }
                    break;
                case WriteOperationType.Delete:
                    {
                        // Insert logic for any Delete operation
                        var commandText = string.Format(
                            "DELETE FROM dbo.Books WHERE Id = {0}", book.Id);

                        new SqlCommand(commandText, _connection).ExecuteNonQuery();
                    }
                    break;
                case WriteOperationType.Update:
                    {
                        // Insert logic for any Update operation
                        // Insert logic for any Add operation
                        var commandText = string.Format(
                            "UPDATE dbo.Books SET Name = {0}, Description = {1}, ISBN = {2}, Price = {3}, AuthorName = {4} WHERE Id = {5}",
                            book.Name, book.Description, book.ISBN, book.Price, book.AuthorName, book.Id);

                        new SqlCommand(commandText, _connection).ExecuteNonQuery();
                    }
                    break;
            }

            // Write Thru operation status can be set according to the result. 
            return new OperationResult(operation, OperationResult.Status.Success);
        }

        public ICollection<OperationResult> WriteToDataSource(ICollection<WriteOperation> operations)
        {
            var operationResult = new List<OperationResult>();
            foreach (WriteOperation operation in operations)
            {
                // Write Thru operation status can be set according to the result
                operationResult.Add(WriteToDataSource(operation));
            }
            return operationResult;
        }

        public ICollection<OperationResult> WriteToDataSource(ICollection<DataTypeWriteOperation> dataTypeWriteOperations)
        {
            var operationResult = new List<OperationResult>();
            foreach (DataTypeWriteOperation operation in dataTypeWriteOperations)
            {
                var list = new List<Book>();
                ProviderDataTypeItem<object> cacheItem = operation.ProviderItem;
                Book book = (Book)cacheItem.Data;

                switch (operation.OperationType)
                {
                    case DatastructureOperationType.CreateDataType:
                        // Insert logic for creating a new List
                        IList myList = new List<Book>();
                        myList.Add(book.Id);
                        break;
                    case DatastructureOperationType.AddToDataType:
                        // Insert logic for any Add operation 
                        list.Add(book);
                        break;
                    case DatastructureOperationType.DeleteFromDataType:
                        // Insert logic for any Remove operation
                        list.Remove(book);
                        break;
                    case DatastructureOperationType.UpdateDataType:
                        // Insert logic for any Update operation 
                        list.Insert(0, book);
                        break;
                }
                // Write Thru operation status can be set according to the result. 
                operationResult.Add(new OperationResult(operation, OperationResult.Status.Success));
            }
            return operationResult;
        }

        // Parameters specified in Manager are passed to this method
        // These parameters make the connection string
        private string GetConnectionString(IDictionary parameters)
        {
            string connectionString = string.Empty;
            string server = parameters["server"] as string, database = parameters["database"] as string;
            string userName = parameters["username"] as string, password = parameters["password"] as string;
            try
            {
                connectionString = string.IsNullOrEmpty(server) ? "" : "Server=" + server + ";";
                connectionString = string.IsNullOrEmpty(database) ? "" : "Database=" + database + ";";
                connectionString += "User ID=";
                connectionString += string.IsNullOrEmpty(userName) ? "" : userName;
                connectionString += ";";
                connectionString += "Password=";
                connectionString += string.IsNullOrEmpty(password) ? "" : password;
                connectionString += ";";
            }
            catch (Exception)
            {
                // Handle exception
            }

            return connectionString;
        }
        // Deploy this class on cache
    }
}
