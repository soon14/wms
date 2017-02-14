using System;
using System.Data.SQLite;

namespace Nodes.Update.Engine
{
    public class SqliteHelper
    {
        private static string DBConnectString = "Data Source=manifest.dat;Version=3;FailIfMissing=True;";

        public static VersionInfo LoadVersion(string appId)
        {
            VersionInfo val = null;

            using (SQLiteConnection cnn = new SQLiteConnection(DBConnectString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "select id, ver from VersionInfo where id = @id";
                    cmd.Parameters.AddWithValue("@id", appId);

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        val = new VersionInfo();
                        val.ID = reader.GetString(0);
                        val.VER = reader.GetString(1);
                    }

                    reader.Close();
                }

                cnn.Close();
            }

            return val;
        }

        public static void UpdateVersion(string appId, string newVer)
        {
            //create table VersionInfo(id varchar(4), ver varchar(16), modifiedDate varchar(24))
            using (SQLiteConnection cnn = new SQLiteConnection(DBConnectString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "update VersionInfo set ver = @NewVersion, modifiedDate = @Now where id = @id";

                    cmd.Parameters.AddWithValue("@NewVersion", newVer);
                    cmd.Parameters.AddWithValue("@Now", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    cmd.Parameters.AddWithValue("@id", appId);

                    cmd.ExecuteNonQuery();
                }

                cnn.Close();
            }
        }
    }
}
