﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

public partial class _1_DataEntry : System.Web.UI.Page
{
    //variable to store the primary key with page level scope
    Int32 CustomerId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //get the number of the address to be processed
        CustomerId = Convert.ToInt32(Session["CustomerId"]);
        if(IsPostBack == false)
        {
            //if there is not a new record
            if(CustomerId != -1)
            {
                //display the current data for the record
                DisplayCustomer();
            }
        }
    }

    private void DisplayCustomer()
    {
        //create an instance of clsCustomerCollection
        clsCustomerCollection CustomerBook = new clsCustomerCollection();
        //find the record to update
        CustomerBook.ThisCustomer.Find(CustomerId);
        //display the data for this record
        txtCustomerId.Text = CustomerBook.ThisCustomer.CustomerId.ToString();
        txtFirstName.Text = CustomerBook.ThisCustomer.FirstName;
        txtLastName.Text = CustomerBook.ThisCustomer.LastName;
        txtEmail.Text = CustomerBook.ThisCustomer.Email;
        txtDateOfBirth.Text = CustomerBook.ThisCustomer.DateOfBirth.ToString();
        chkActive.Checked = CustomerBook.ThisCustomer.Active;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //create a new instance of clsCustomer
        clsCustomer AnCustomer = new clsCustomer();
        //capture the first name
        String FirstName = txtFirstName.Text;
        //capture the last name
        String LastName = txtLastName.Text;
        //capture the Email
        String Email = txtEmail.Text;
        //capture the date of birth
        String DateOfBirth = txtDateOfBirth.Text;
        //varible to stor any error messages
        String Error = "";
        //validate the data
        Error = AnCustomer.Valid(FirstName, LastName, Email, DateOfBirth);
        if (Error == "")
        {
            //capture the customer ID
            AnCustomer.CustomerId = CustomerId;
            //capture the first name
            AnCustomer.FirstName = FirstName;
            //capture the last name
            AnCustomer.LastName = LastName;
            //capture the email
            AnCustomer.Email = Email;
            //capture the date of birth
            AnCustomer.DateOfBirth = Convert.ToDateTime(DateOfBirth);
            //capture active
            AnCustomer.Active = chkActive.Checked;
            //create a new instance of the address collection
            clsCustomerCollection CustomerList = new clsCustomerCollection();

            //if this is a new record i.e CustomerNumber = -1 then add the data
            if(CustomerId == -1)
            {
                //set the ThisCustomer property
                CustomerList.ThisCustomer = AnCustomer;
                //Add the new record
                CustomerList.Add();
            }
            //otherwise it must be an update
            else
            {
                //find the record to update
                CustomerList.ThisCustomer.Find(CustomerId);
                //set the ThisCustomer property
                CustomerList.ThisCustomer = AnCustomer;
                //update the record
                CustomerList.Update();
            }
            //redirect back to the listpage
            Response.Redirect("CustomerList.aspx");
        }
        else
        {
            //display the error message
            lblError.Text = Error;
        }
    }


    protected void btnFind_Click(object sender, EventArgs e)
    {
        //create an instance of the custome class
        clsCustomer AnCustomer = new clsCustomer();
        //varible to store primary key
        Int32 CustomerId;
        //varible to store the result of the find operation
        Boolean Found = false;
        //get the primary key enterd by the user
        CustomerId = Convert.ToInt32(txtCustomerId.Text);
        //find the recod
        Found = AnCustomer.Find(CustomerId);
        //if found
        if (Found == true)
        {
            //display the values of the properties in the form
            txtCustomerId.Text = AnCustomer.CustomerId.ToString();
            txtFirstName.Text = AnCustomer.FirstName;
            txtLastName.Text = AnCustomer.LastName;
            txtEmail.Text = AnCustomer.Email;
            txtDateOfBirth.Text = AnCustomer.DateOfBirth.ToString();
        }
    }
}