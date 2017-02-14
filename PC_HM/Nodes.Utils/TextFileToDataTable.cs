using System.Data;
using System.IO;

namespace Nodes.Utils
{
    public class TextFileToDataTable
    {
        /// <summary>
        /// 将文本文件数据读取到内存表中
        /// 规则：第一行是字段名称；从第二行起是数据；空行自动略过
        /// </summary>
        /// <param name="File">文件名</param>
        /// <param name="TableName">内存表 名称</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns></returns>
        public static DataTable ReadTextFile(string File, string TableName, string delimiter)
        {
            DataTable result = new DataTable(TableName);

            string emptyColumnName = "__empty__";

            //注意字符集，防止中文乱码
            StreamReader s = new StreamReader(File, System.Text.Encoding.Default);

            //读取表头（列名）
            string[] columns = s.ReadLine().Split(delimiter.ToCharArray());
            foreach (string col in columns)
            {
                bool added = false;
                string next = "";
                int i = 0;
                while (!added)
                {
                    //Build the column name and remove any unwanted characters.
                    string columnname = col + next;
                    columnname = columnname.Replace("'", "");
                    columnname = columnname.Replace("&", "");
                    if (string.IsNullOrEmpty(columnname))
                        columnname = emptyColumnName;

                    //See if the column already exists
                    if (!result.Columns.Contains(columnname))
                    {
                        //if it doesn't then we add it here and mark it as added
                        result.Columns.Add(columnname);
                        added = true;
                    }
                    else
                    {
                        //if it did exist then we increment the sequencer and try again.
                        i++;
                        next = "_" + i.ToString();
                    }
                }
            }

            //Read the rest of the data in the file.        
            string AllData = s.ReadToEnd();

            //读取完成后，立即释放StreanReader
            s.Close();
            s.Dispose();

            //Split off each row at the Carriage Return/Line Feed
            //Default line ending in most windows exports.  
            //You may have to edit this to match your particular file.
            //This will work for Excel, Access, etc. default exports.
            string[] rows = AllData.Split("\r\n".ToCharArray());

            //Now add each row to the DataSet        
            foreach (string r in rows)
            {
                //空行要略过，无效数据
                if (string.IsNullOrEmpty(r))
                    continue;

                //Split the row at the delimiter.
                string[] items = r.Split(delimiter.ToCharArray());
                result.Rows.Add(items);
            }

            //去除空的列，空列是有Split函数导致
            if (result.Columns.Contains(emptyColumnName))
                result.Columns.Remove(emptyColumnName);

            return result;
        }
    }
}
