using System;
using MySql.Data;
using System.Text;
using System.Data;

namespace CreateMySqlTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            String connectionString = "database=gaia;server=127.0.0.1;uid=root;password=root;Charset=utf8;Convert Zero Datetime=True;";
            /*创建MYSQL定时器*/
            StringBuilder createMySqlTimer_command = new StringBuilder("use gaia ; DROP EVENT IF EXISTS E_HIS_TATCH_DA_OUT_BACK; ");
            createMySqlTimer_command.Append(" CREATE EVENT E_HIS_TATCH_DA_OUT_BACK ON SCHEDULE EVERY 1 DAY ");
            createMySqlTimer_command.Append(" STARTS 	DATE_ADD(	DATE(CURDATE() + 1),	INTERVAL 1 HOUR) ");
            createMySqlTimer_command.Append(" ON COMPLETION PRESERVE DISABLE  DO ");
            createMySqlTimer_command.Append(" SELECT '测试代码创建定时器'; ");
            MySqlHelper helper = new MySqlHelper(connectionString);
            helper.ExecuteScalar(CommandType.Text, createMySqlTimer_command.ToString(), null);

        }
    }
}
