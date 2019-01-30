//using MachineCommon;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace DOD.MachineClass
//{
//    public class clsIO
//    {
//        public bool Update(bool state)
//        {
//            string value = "0";
//            if (state)
//                value = "1";
//            DBCom db = new DBCom();
//            StringBuilder sbSql = new StringBuilder();
//            sbSql.AppendLine("update qe_productionstate set `value`=@value where `key` = 'inplace'");

//            db.AddParameter("@value", value);
//            try
//            {
//                db.ExecuteQuery(sbSql.ToString());
//            }
//            catch (Exception ex)
//            {

//            }
//            return true;

//        }
        
//    }
//    public static class dbIO
//    {
//        public static bool Query()
//        {
//            DBCom db = new DBCom();
//            StringBuilder sbSql = new StringBuilder();
//            sbSql.AppendLine("select  `value` from  qe_productionstate where `key` = 'sendtocenter'");

//            try
//            {
//                ///1 表示发送数据， 0 表示不发送数据，也就是关闭
//                string s = db.ExecuteScalar(sbSql.ToString()).ToString();
//                if (s == "1")
//                    return false;
//                else
//                    return true;
//            }
//            catch (Exception ex)
//            {

//            }
//            return true;

//        }
//    }
//}
