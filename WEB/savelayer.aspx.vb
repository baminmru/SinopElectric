Imports System.Data
Imports Newtonsoft.Json
Imports System.IO

Partial Class savelayer
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub
        Dim jj As JOut
        Dim cm As CMConnector
        cm = New CMConnector()
        Dim dt As DataTable
        If Not cm.Init() Then

            dt = New DataTable
            jj = New JOut
            jj.success = "false"
            jj.data = dt
            jj.msg = "Error"
            Response.Clear()
            Response.Write(JsonConvert.SerializeObject(jj))
            Response.End()
            Exit Sub
        End If
		
		
		
		Dim P As String
        Dim L As String
        
        
        P = Request.QueryString("P") & ""
        L = Request.QueryString("L") & ""

		File.AppendAllText( "C:\bami\SINOP\SAVE\" & P & ".json",L & vbcrlf & vbcrlf)
        
        Response.Clear()
        Response.Write("OK")
        Response.End()



    End Sub


End Class
