using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using System.Data;

namespace NPRFIDTool.NPKit
{
    enum  TableType { TableTypeRemain, TableTypeCheck };

    class NPDBManager
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private JObject remindData;
        private JObject checkedData;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;

        public NPDBManager(DBConfig dbConfig)
        {
            string[] adArray = dbConfig.dbAddress.Split(':');
            this.server = adArray[0];
            this.port = adArray[1];
            this.database = dbConfig.dbName;
            this.user = dbConfig.username;
            this.password = dbConfig.password;
        }

        // 连接本地数据库
        public void connectDataBase()
        {
            //dbname = rfid_db
            string connStr = String.Format("server = {0}; user = {1}; database = {2}; port = {3}; password = {4}",server,user,database,port,password);
            conn = new MySqlConnection(connStr);
            conn.Open();
        }

        // 断开数据库连接
        public void disconnectDataBase()
        {
            if (conn.State != ConnectionState.Connecting) return;
            conn.Close();
        }

        // 根据类型查询数据库中对应表的数据
        public JObject queryDataBase( TableType type )
        {
            string tableName = type == TableType.TableTypeRemain ? "remain" : "checked";
            string queryStr = String.Format("SELECT * FROM {0}", tableName);
            MySqlDataAdapter adapter = new MySqlDataAdapter(queryStr, conn);
            MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(adapter);
            DataSet tableData = new DataSet();
            adapter.Fill(tableData, tableName);
            JObject tableDict = processTableDataSet(tableData);
            return tableDict;
        }

        // 刷新表的数据
        public void refreshTableWithData (TableType type, JObject dataDict)
        {
            clearDataBase(type);
            appendDataToDataBase(type, dataDict);
        }


        // 往数据库中插入数据
        public void appendDataToDataBase ( TableType type, JObject dataDict )
        {
            if (dataDict == null) return;
            string tableName = type == TableType.TableTypeRemain ? "remain" : "checked";
            string queryStr = String.Format("SELECT * FROM {0}", tableName);
            MySqlDataAdapter adapter = new MySqlDataAdapter(queryStr, conn);
            MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(adapter);
            DataSet tableData = new DataSet();
            adapter.Fill(tableData, tableName);
            DataTable table = tableData.Tables[0];
            foreach (var item in dataDict)
            {
                bool isNew = !updateTableIfNeeded(table, item.Key, item.Value.ToString());
                if (isNew)
                {
                    DataRow row = table.NewRow();
                    row[0] = item.Key;
                    row[1] = item.Value.ToString();
                    table.Rows.Add(row);
                }
            }
            adapter.Update(tableData, tableName);
        }

        // 清空数据库
        public void clearDataBase ( TableType type )
        {
            string tableName = type == TableType.TableTypeRemain ? "remain" : "checked";
            string deleteStr = String.Format("truncate table {0}", tableName);
            MySqlCommand cmd = new MySqlCommand(deleteStr, conn);
            cmd.ExecuteNonQuery();
        }

        // 处理数据库返回的数据
        private JObject processTableDataSet( DataSet dataset )
        {
            if (dataset == null) return null;
            JObject obj = new JObject();
            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                obj.Add(row[0].ToString(), row[1].ToString());
            }
            return obj;
        }

        // 判断table中是否已包含制定数据,若包含则更新，更新成功返回true
        private bool updateTableIfNeeded (DataTable table, string key, string value)
        {
            bool contain = false;
            foreach (DataRow row in table.Rows)
            {
                if (row[0].ToString() == key)
                {
                    contain = true;
                    row[1] = value;
                }
            }
            return contain;
        }
    }
}
