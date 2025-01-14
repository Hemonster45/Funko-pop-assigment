﻿using System;
using ClassLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _1_DataEntry : System.Web.UI.Page
{
    Int32 FunkoNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        FunkoNo = Convert.ToInt32(Session["FunkoNo"]);
        if (IsPostBack == false)
        {
            if (FunkoNo != -1)
            {
                DisplayOrder();
            }
        }
    }

    void DisplayOrder()
    {
        clsOrderCollection Order = new clsOrderCollection();
        Order.ThisOrder.Find(FunkoNo);
        txtFunkoNo.Text = Order.ThisOrder.FunkoNo.ToString();
        txtPrice.Text = Order.ThisOrder.Price.ToString();
        txtOrderNo.Text = Order.ThisOrder.OrderNo.ToString();
        txtFunkoName.Text = Order.ThisOrder.FunkoName;
        txtDateAdded.Text = Order.ThisOrder.DateAdded.ToString();
        chkAvailable.Checked = Order.ThisOrder.Available;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //create a new instance of clsOrder
        clsOrder AnOrder = new clsOrder();
        //capture the order number
        String FunkoName = txtFunkoName.Text;
        String Price = txtPrice.Text;
        String DateAdded = txtDateAdded.Text;
        String OrderNo = txtOrderNo.Text;

        string Error = "";
        Error = AnOrder.Valid(FunkoName, Price, OrderNo, DateAdded);

        if (Error == "")
        {
            AnOrder.FunkoNo = FunkoNo;
            AnOrder.FunkoName = FunkoName;
            AnOrder.Available = chkAvailable.Checked;
            AnOrder.Price = Convert.ToInt32(txtPrice.Text);
            AnOrder.DateAdded = Convert.ToDateTime(DateAdded);
            AnOrder.OrderNo = Convert.ToInt32(txtOrderNo.Text);
            clsOrderCollection OrderList = new clsOrderCollection();

            if (FunkoNo == -1)
            {
                OrderList.ThisOrder = AnOrder;
                OrderList.Add();
        
            }
            else
            {
                OrderList.ThisOrder.Find(FunkoNo);
                OrderList.ThisOrder = AnOrder;
                OrderList.Update();
            }
            Response.Redirect("OrderList.aspx");
        }
        else
        {
            lblerror.Text = Error;
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        {
            //create an instance of the order class
            clsOrder AnOrder = new clsOrder();
            //variable to store the primary key
            Int32 FunkoNo;
            //variable to store the result of the find operation
            Boolean Found = false;

            String FunkoNumber = txtFunkoNo.Text;

            string Error = "";
            Error = AnOrder.Valid2(FunkoNumber);

            if (Error == "")
            {
                AnOrder.FunkoNo = Convert.ToInt32(txtFunkoNo.Text);

                //get the primary key entered by the user
                FunkoNo = Convert.ToInt32(txtFunkoNo.Text);
                //find the record
                Found = AnOrder.Find(FunkoNo);
                //if found
                if (Found == true)
                {
                    //display the values of the properties in the form
                    txtFunkoNo.Text = AnOrder.FunkoNo.ToString();
                    txtFunkoName.Text = AnOrder.FunkoName;
                    txtDateAdded.Text = AnOrder.DateAdded.ToString();
                    txtPrice.Text = AnOrder.Price.ToString();
                    txtOrderNo.Text = AnOrder.OrderNo.ToString();
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderList.aspx");
    }
}