﻿using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class clsOrderCollection
    {
        List<clsOrder> mOrderList = new List<clsOrder>();

        clsOrder mThisOrder = new clsOrder();

        public List<clsOrder> OrderList
        {
            get
            {
                return mOrderList;
            }
            set
            {
                mOrderList = value;
            }
        }

        public int Count
        {
            get
            {
                return mOrderList.Count;
            }
            set
            {

            }
        }

        public clsOrder ThisOrder
        {
            get
            {
                return mThisOrder;
            }
            set
            {
                mThisOrder = value;
            }
        }

        public clsOrderCollection()
        {
            Int32 Index = 0;
            Int32 RecordCount = 0;
            clsDataConnection DB = new clsDataConnection();
            DB.Execute("sproc_tblOrder_SelectAll");
            RecordCount = DB.Count;
            while (Index < RecordCount)
            {
                clsOrder AnOrder = new clsOrder();
                AnOrder.Available = Convert.ToBoolean(DB.DataTable.Rows[Index]["Available"]);
                AnOrder.FunkoNo = Convert.ToInt32(DB.DataTable.Rows[Index]["FunkoNo"]);
                AnOrder.OrderNo = Convert.ToInt32(DB.DataTable.Rows[Index]["OrderNo"]);
                AnOrder.DateAdded = Convert.ToDateTime(DB.DataTable.Rows[Index]["DateAdded"]);
                AnOrder.FunkoName = Convert.ToString(DB.DataTable.Rows[Index]["FunkoName"]);
                AnOrder.Price = Convert.ToInt32(DB.DataTable.Rows[Index]["Price"]);
                mOrderList.Add(AnOrder);
                Index++;
            }
        }

        public int Add()
        {
            clsDataConnection DB = new clsDataConnection();
            DB.AddParameter("OrderNo", mThisOrder.OrderNo);
            DB.AddParameter("FunkoName", mThisOrder.FunkoName);
            DB.AddParameter("Price", mThisOrder.Price);
            DB.AddParameter("DateAdded", mThisOrder.DateAdded);
            DB.AddParameter("Available", mThisOrder.Available);

            return DB.Execute("sproc_tblOrder_Insert");
        }
    }
}