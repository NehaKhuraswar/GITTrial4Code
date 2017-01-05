using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using RAP.Core.DataModels;

namespace RAP.Business.Helper
{
    internal class DataTableHelper
    {
        static dynamic CheckNullValue(dynamic value)
        {
            return ((value == null) ? DBNull.Value : value);
        }

        static dynamic CheckEmptyStringValue(dynamic value)
        {
            return (string.IsNullOrEmpty(value) ? DBNull.Value : value);
        }

        static dynamic CheckZeroNullValue(dynamic value)
        {
            return ((value == null) || (value == 0) ? DBNull.Value : value);
        }

        public static DataTable CreateIDTable(List<int> lst)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            if (lst != null)
            {
                foreach (int id in lst)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = id;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public static DataTable CreateCodeTable(List<string> lst)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Code", typeof(string));
            if (lst != null)
            {
                foreach (string code in lst)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = code;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public static DataTable CreateNotesTable(List<Notes> lst)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("itemid", typeof(int));
            dt.Columns.Add("timestamp", typeof(DateTime));
            dt.Columns.Add("context", typeof(string));
            dt.Columns.Add("notedescription", typeof(string));
            dt.Columns.Add("createdby", typeof(string));
            if (lst != null)
            {
                foreach (Notes note in lst)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = note.ItemID;
                    dr[1] = note.TimeStamp;
                    dr[2] = note.Context;
                    dr[3] = note.NoteDescription;
                    dr[4] = note.CreatedBy;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

    //    public static DataTable CreateDocumentsTable(List<Document> lst)
    //    {
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("DocID", typeof(int));
    //        dt.Columns.Add("SectionID", typeof(int));
    //        dt.Columns.Add("KeyID", typeof(int));
    //        dt.Columns.Add("AdditionalKey", typeof(string));
    //        dt.Columns.Add("CategoryID", typeof(int));
    //        dt.Columns.Add("Description", typeof(string));
    //        dt.Columns.Add("FileName", typeof(string));
    //        dt.Columns.Add("Content", typeof(byte[]));
    //        if (lst != null)
    //        {
    //            foreach (Document item in lst)
    //            {
    //                DataRow dr = dt.NewRow();
    //                dr[0] = CheckZeroNullValue(item.DocID);
    //                dr[1] = item.Section.ID;
    //                dr[2] = item.KeyID;
    //                dr[3] = item.AdditionalKey;
    //                if (item.Category != null)
    //                {
    //                    dr[4] = item.Category.ID;
    //                }
    //                else
    //                {
    //                    dr[4] = 0;
    //                }
    //                dr[5] = CheckEmptyStringValue(item.Description);
    //                dr[6] = item.FileName;
    //                dr[7] = item.Content;
    //                dt.Rows.Add(dr);
    //            }
    //        }
    //        return dt;
    //    }
    }
}
