﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

public partial class _1_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if this is the first time the page is displayed
        if (IsPostBack == false)
        {
            //Update the list box
            DisplayCustomers();
        }
    }

    void DisplayCustomers()
    {
        //create an instance of the customer collection
        clsCustomerCollection Customers = new clsCustomerCollection();
        //set the data source to list of customers in collection
        lstCustomerList.DataSource = Customers.CustomerList;
        //set the name of the primary key
        lstCustomerList.DataValueField = "CustomerId";
        //set the data feild to be displayed
        lstCustomerList.DataTextField = "FirstName";
        //bind the data to the list
        lstCustomerList.DataBind();
    }


    protected void lstCustomerList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //store -1 into the session object to indicate this is a new record
        Session["CustomerId"] = -1;
        //redirect to the data entry page
        Response.Redirect("CustomerDataEntry.aspx");

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //var to store the primary key value of the record to be edited
        Int32 CustomerId;
        //if a record has been selected from the list
        if(lstCustomerList.SelectedIndex != -1)
        {
            //get the primary key value of the record to edit
            CustomerId = Convert.ToInt32(lstCustomerList.SelectedValue);
            //store the data in the session object
            Session["CustomerId"] = CustomerId;
            //redirect to the edit page
            Response.Redirect("CustomerDataEntry.aspx");
        }
        else//if no record has been selected
        {
            lblError.Text = "Please select a record to edit from the list";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //var to store the primary key od the record to be edited
        Int32 CustomerId;
        //if a record has be selected from the list
        if(lstCustomerList.SelectedIndex != -1)
        {
            //get the primary key value of the record to edit
            CustomerId = Convert.ToInt32(lstCustomerList.SelectedValue);
            //store the data in the session object
            Session["CustomerId"] = CustomerId;
            //redirectto the delete page
            Response.Redirect("CustomerConfirmDelete.aspx");
        }
        else//if no record has been selected
        {
            lblError.Text = "Please select a record to delete from the list";
        }
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        //create an instance of the Customer collection
        clsCustomerCollection Customers = new clsCustomerCollection();
        Customers.ReportByFirstName(txtFilter.Text);
        lstCustomerList.DataSource = Customers.CustomerList;
        //set the name of the primary key
        lstCustomerList.DataValueField = "CustomerId";
        //set the name of the feild to display
        lstCustomerList.DataTextField = "FirstName";
        //bind the data to the list
        lstCustomerList.DataBind();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //create an instance of the customer collection
        clsCustomerCollection Customers = new clsCustomerCollection();
        Customers.ReportByFirstName("");
        //clear any existing filter to tidy up the interface
        txtFilter.Text = "";
        lstCustomerList.DataSource = Customers.CustomerList;
        //set the name of the primary key
        lstCustomerList.DataTextField = "CustomerId";
        //set the name of the field to be display
        lstCustomerList.DataTextField = "FirstName";
        //bind the data to the list
        lstCustomerList.DataBind();
    }
}