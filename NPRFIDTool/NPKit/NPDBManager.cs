using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace NPRFIDTool.NPKit
{
    enum  TableType { TableTypeRemain, TableTypeCheck };

    class NPDBManager
    {
       
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private Dictionary<string,string> remindData;
        private Dictionary<string,string> checkedData;

        // 连接本地数据库
        public void connectDataBase()
        {
            string connStr = "server = localhost; user = root; database = rfid_db; port = 3306; password = 910123";
            conn = new MySqlConnection(connStr);
            try {
                conn.Open();
                Console.WriteLine("Connect DB success");
            }catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        // 断开数据库连接
        public void disconnectDataBase()
        {
            conn.Close();
        }

        // 根据类型查询数据库中对应表的数据
        public Dictionary<string,string> queryDataBase( TableType type )
        {
            string tableName = type == TableType.TableTypeRemain ? "remain" : "check";
            string queryStr = String.Format("SELECT * FROM {0}", tableName);
            MySqlDataAdapter adapter = new MySqlDataAdapter(queryStr, conn);
            MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(adapter);
            DataSet tableData = new DataSet();
            adapter.Fill(tableData, tableName);
            Dictionary<string, string> tableDict = processTableDataSet(tableData);
            return tableDict;
        }

        // 往数据库中插入数据
        public void appendDataToDataBase ( TableType type, Dictionary<string,string> dataDict )
        {
            if (dataDict == null) return;
            string tableName = type == TableType.TableTypeRemain ? "remain" : "checked";
            string queryStr = String.Format("SELECT * FROM {0}", tableName);
            MySqlDataAdapter adapter = new MySqlDataAdapter(queryStr, conn);
            MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(adapter);
            DataSet tableData = new DataSet();
            adapter.Fill(tableData, tableName);
            DataTable table = tableData.Tables[0];
            foreach (string key in dataDict.Keys)
            {
                bool isNew = !updateTableIfNeeded(table, key, dataDict[key]);
                if (isNew)
                {
                    DataRow row = table.NewRow();
                    row[0] = key;
                    row[1] = dataDict[key];
                    table.Rows.Add(row);
                }
            }
            adapter.Update(tableData, tableName);
        }

        // 清空数据库
        public void clearDataBase ( TableType type )
        {
            string tableName = type == TableType.TableTypeRemain ? "remain" : "check";
            string deleteStr = String.Format("truncate table {0}", tableName);
            MySqlCommand cmd = new MySqlCommand(deleteStr, conn);
            cmd.ExecuteNonQuery();
        }

        // 处理数据库返回的数据
        private Dictionary<string, string> processTableDataSet( DataSet dataset )
        {
            if (dataset == null) return null;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                dict.Add(row[0].ToString(), row[1].ToString());
            }
            return dict;
        }

        // 判断table中是否已包含制定数据,若包含则更新，更新成功返回true
        private bool updateTableIfNeeded (DataTable table, string key, string value)
        {
            bool contain = false;
            foreach (DataRow row in table.Rows)
            {
                if (row[0] == key)
                {
                    contain = true;
                    row[1] = value;
                }
            }
            return contain;
        }
    }
}
