﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using Ionic.Zip;
using tool;

public partial class User_ShowPages : System.Web.UI.Page
{
    ArrayList SelectedFile = new ArrayList();
    Utility Util = new Utility();
    DBTool dbTool = new DBTool();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string command = Request.QueryString["command"];
            if (command == "0")
            {
                string bookid = Request.QueryString["bookid"];
                string filename = Request.QueryString["filename"];
                //"../E-library/" + Eval("bookid") + "/pages/" + Eval("filename")
                string ImageURL = Server.MapPath(String.Format("~/E-library/{0}/pages/{1}",bookid,filename));

                //Save file as dialog work Only JPEG File
                Response.ContentType = "image/jpeg";
                Response.AppendHeader("Content-Disposition", "attachment; filename="+filename);
                Response.TransmitFile(ImageURL);
                Response.End();
            }
        }
    }

    protected void BtnDownloadFile_Click(object sender, EventArgs e)
    {
        UpdateSelection();

        string bookid = Request.QueryString["bookid"];
        string userid = Convert.ToString(Session["userid"]);
        //string userid = "2";

        if (ViewState["Selection"] != null)
        {
            SelectedFile = (ArrayList)ViewState["Selection"];
        }

        CreateOnTheFlyZip(SelectedFile, bookid, userid);
    }

    protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        UpdateSelection();
        DownloadLnk.Visible = false;
    }
    
    protected void ListView1_PreRender(object sender, EventArgs e)
    {
        ShowSelected();

        if (CheckListEmpty())
        {
            BtnDownloadFile.Visible = false;
        }
    }

    protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbxSelectFile;

        for (int i = 0; i < ListView1.Items.Count; i++)
        {
            cbxSelectFile = (CheckBox)ListView1.Items[i].FindControl("cbxSelectFile");
            cbxSelectFile.Checked = true;
        }
    }

    private void UpdateSelection()
    {
        CheckBox cbxSelectFile;

        if (ViewState["Selection"] != null)
        {
            SelectedFile = (ArrayList)ViewState["Selection"];
        }

        for (int i = 0; i < ListView1.Items.Count; i++)
        {
            cbxSelectFile = (CheckBox)ListView1.Items[i].FindControl("cbxSelectFile");
            if (cbxSelectFile.Checked)
            {
                string filename = Convert.ToString(cbxSelectFile.Attributes["key"]);

                if (SelectedFile.IndexOf(filename) < 0)
                {
                    SelectedFile.Add(filename);
                }
            }
            else
            {
                string filename = Convert.ToString(cbxSelectFile.Attributes["key"]);

                if (SelectedFile.IndexOf(filename) >= 0)
                {
                    SelectedFile.Remove(filename);
                }
            }
        }

        if (SelectedFile.Count > 0)
        {
            ViewState["Selection"] = SelectedFile;
        }
    }

    private void ShowSelected()
    {
        CheckBox cbxSelectFile;

        if (ViewState["Selection"] != null)
        {
            SelectedFile = (ArrayList)ViewState["Selection"];
        }

        for (int i = 0; i < ListView1.Items.Count; i++)
        {
            cbxSelectFile = (CheckBox)ListView1.Items[i].FindControl("cbxSelectFile");
            
            string filename = Convert.ToString(cbxSelectFile.Attributes["key"]);

            if (SelectedFile.IndexOf(filename) >= 0)
            {
                cbxSelectFile.Checked = true;   
            }
        }
    }

    private void CreateZip(ArrayList SelectedFile, string bookid, string userid)
    {
        AddCounter(bookid, userid);

        ICSharpCode.SharpZipLib.Zip.ZipFile MyZip;
        string DownloadPath = Server.MapPath(String.Format("../E-library/{0}/{1}_{2}{3}{4}.Zip",
            bookid, userid, DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), DateTime.Now.ToString("yyyy")));
        MyZip = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(DownloadPath);
        
        foreach (string file in SelectedFile)
        {
            MyZip.BeginUpdate();
            string filePath = Server.MapPath( String.Format("../E-library/{0}/Pages/{1}", bookid, file));
            MyZip.Add(filePath, file);
            MyZip.CommitUpdate();
        }
       
        MyZip.Close();

        DownloadLnk.NavigateUrl = String.Format("../E-library/{0}/{1}_{2}{3}{4}.Zip",
            bookid, userid, DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), DateTime.Now.ToString("yyyy"));
        DownloadLnk.Visible = true;
    }

    private void CreateOnTheFlyZip(ArrayList SelectedFile, string bookid, string userid)
    {
        AddCounter(bookid, userid);

        string[] SelectedFileArray = new string[SelectedFile.Count];

        for (int i=0 ; i < SelectedFile.Count ; i++)
        {
            string filePath = Server.MapPath(String.Format("../E-library/{0}/Pages/{1}", bookid, SelectedFile[i]));
            SelectedFileArray[i] = filePath;
        }

        Response.Clear();
        Response.BufferOutput = false;

        string filename = string.Format("Book_{0}-{1}{2}{3}.zip",bookid,DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), DateTime.Now.ToString("yyyy"));

        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition","filename="+filename);
        using (Ionic.Zip.ZipFile Zip = new Ionic.Zip.ZipFile())
        {
            Zip.AddFiles(SelectedFileArray, "files");

            Zip.Save(Response.OutputStream);
        }
        Response.Close();
    }

    private void AddCounter(string bookid, string Userid)
    {
        string SQL = string.Format("INSERT INTO tb_counter(userid,bookid,load_date) VALUES ({0},{1},'{2}')",
            Userid,bookid,Util.GetToday());

        dbTool.RunCommand(SQL);
    }

    private bool CheckListEmpty()
    {
        if (ListView1.Items.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
