﻿using System;

namespace ClassLibrary
{
    public class clsStock
    {
        private Int32 mIdNumber;
        private Int32 mItemQTY;
        private String mItemTag;
        private String mItemDesc;
        private bool minstock;
        private DateTime mDate;

        public int IdNum
        {
            get
            {
                return mIdNumber;

            }
            set
            {
                mIdNumber = value;
            }
        }
        public bool InStock
        {
            get
            {
                return minstock;
            }
            set
            {
                minstock = value;
            }
        }
        public DateTime DateAdded
        {
            get
            {
                return mDate;
            }
            set
            {
                mDate = value;
            }
        }
        public int ItemQty
        {
            get
            {
                return mItemQTY;
            }
            set
            {
                mItemQTY = value;
            }
        }
        public string ItemTag
        {
            get
            {
                return mItemTag;
            }
            set
            {
                mItemTag = value;
            }
        }
        public string ItemDesc
        {
            get
            {
                return mItemDesc;
            }
            set
            {
                mItemDesc = value;
            }
        }

        public bool Find(int itemID)
        {
            clsDataConnection DB = new clsDataConnection();

            DB.AddParameter("@ItemID", itemID);

            DB.Execute("sproc_tblStock_FilterByItemID");

            if (DB.Count == 1)
            {
                mIdNumber = Convert.ToInt32(DB.DataTable.Rows[0]["itemID"]);
                mItemQTY = Convert.ToInt32(DB.DataTable.Rows[0]["itemQTY"]);
                mItemTag = Convert.ToString(DB.DataTable.Rows[0]["itemTag"]);
                mItemDesc = Convert.ToString(DB.DataTable.Rows[0]["itemDesc"]);
                minstock = Convert.ToBoolean(DB.DataTable.Rows[0]["inStockItem"]);
                mDate = Convert.ToDateTime(DB.DataTable.Rows[0]["itemLastStock"]);

                return true;
            }

            else
            {
                return false;
            }
        }

        public string Valid(string itemQT, string tagItem, string dateAdded, string descItem)
        {
            String Error = "";
            DateTime DateAddedTemp;
            int itemQTtemp;
           // int numIdTemp;


            if (tagItem.Length == 0)
            {
                Error = Error + "This field should not be empty";
            }
            if (tagItem.Length > 50)
            {
                Error = Error + "Cant be more then 50 ";

            }

            if (descItem.Length == 0)
            {
                Error = Error + "This field should not be empty";
            }
            if (descItem.Length > 50)
            {
                Error = Error + "Cant be more then 50 ";

            }


            try
            {
                itemQTtemp = Convert.ToInt32(itemQT);
                if (itemQTtemp < 0)
                {
                    Error = Error + "Cant go lower then 0";
                }

            } catch {
                Error = Error + "Invalid type itemqt";
            }

           
            
          //  try
      //     {
         //       numIdTemp = Convert.ToInt32(numId);
       //         if (numIdTemp < 0)
       //         {
       //             Error = Error + "Cant go lower then 0";
       //         }
       //
       //     } catch
        //    {
        //        Error = Error + "Invalid type idnum";
      //      }

           
            try
            {
                DateAddedTemp = Convert.ToDateTime(dateAdded);

                if (DateAddedTemp > DateTime.Now.Date)
                {
                    Error = Error + "Date cannot be in future";
                }

            }catch
            {
                Error = Error + "Invalid type date";
            }

            return Error;

        }

    }
}