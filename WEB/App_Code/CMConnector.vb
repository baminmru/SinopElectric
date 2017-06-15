Imports Oracle.ManagedDataAccess.Client

Imports System.Data
Imports System.Xml
Imports System.IO

Public Class CMConnector


    Private Function GetMyDir() As String
        Dim s As String
        s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        s = s.Substring(6)
        Return s
    End Function
    Private connection As OracleConnection
    Public Function dbconnect() As OracleConnection
        Return connection
    End Function
    Public LogPath As String = ""
    Public LibPath As String = ""
    Public TableName As String = ""

    Public Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function

    Public Function Init() As Boolean


        Dim builder As New OracleConnectionStringBuilder
        Dim xml As XmlDocument
        xml = New XmlDocument
        'xml.Load(GetMyDir() + "\config.xml")
        xml.Load("D:\bami\projects\szt\SINOP\WEB\App_Data\config.xml")

        Dim node As XmlElement
        Dim nl As XmlNodeList
        nl = xml.GetElementsByTagName("Config")
        node = nl.Item(0)


        builder.DataSource = node.Attributes.GetNamedItem("Oracle").Value
        builder.UserID = node.Attributes.GetNamedItem("UserID").Value
        builder.Password = node.Attributes.GetNamedItem("Password").Value
        LogPath = node.Attributes.GetNamedItem("logpath").Value
     


        connection = New OracleConnection()

        connection.ConnectionString = builder.ConnectionString
        Try
            SyncLock connection
                connection.Open()
            End SyncLock
            If connection.State <> ConnectionState.Open Then
                Console.WriteLine("Ошибка соединения")
                Return False
            End If

        Catch ex As Exception

            Return False
        End Try
        Return True
    End Function

    Public Sub QueryExec(ByVal s As String)
        Try
            Dim cmd As OracleCommand
            cmd = New OracleCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = s
            cmd.Connection = dbconnect()

            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function QuerySelect(ByVal s As String) As DataTable
        Dim cmd As OracleCommand
        cmd = New OracleCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = s
        cmd.Connection = dbconnect()
        Dim dt As DataTable
        Dim da As OracleDataAdapter
        dt = New DataTable
        da = New OracleDataAdapter
        Try
            da.SelectCommand = cmd
            da.Fill(dt)
        Catch ex As Exception
            Throw ex
        End Try
      
        Return dt
    End Function

    Public Sub New()
        Init()
    End Sub


    Public Sub Log(ByVal msg As String)
        File.AppendAllText(LogPath & "log_" & DateTime.Now.ToString("yyyyMMdd") & ".txt", vbCrLf & DateTime.Now.ToString() & " " & msg)
    End Sub


    Public Function GetOutputTab() As DataTable
        Dim dt2 As DataTable
        dt2 = New DataTable
        dt2.Columns.Add("ID")
        dt2.Columns.Add("COLOR")
        dt2.Columns.Add("BLINK")
        dt2.Columns.Add("INFO")
        Return dt2
    End Function

    Public Function IsBitSet(ByVal V As UShort, bit As Byte) As Boolean

        If bit >= 0 And bit <= 15 Then
            If (V And (1 << bit)) <> 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Sub GetCommonParams(dt2 As DataTable)
        Dim dr As DataRow

        Dim ddd As DateTime
        ddd = DateTime.Now
        ''''''''''''''''''
        dr = dt2.NewRow
        dr("ID") = "P1"
        dr("COLOR") = ""
        dr("BLINK") = "NO"
        dr("INFO") = "Y:" & ddd.Year.ToString()
        dt2.Rows.Add(dr)


        dr = dt2.NewRow
        dr("ID") = "P2"
        dr("COLOR") = ""
        dr("BLINK") = "NO"
        dr("INFO") = "M:" & ddd.Month.ToString()
        dt2.Rows.Add(dr)

		dr = dt2.NewRow
        dr("ID") = "P3"
        dr("COLOR") = ""
        dr("BLINK") = "NO"
        dr("INFO") = "D:" & ddd.Day.ToString()
        dt2.Rows.Add(dr)

        dr = dt2.NewRow
        dr("ID") = "P4"
        dr("COLOR") = ""
        dr("BLINK") = "NO"
        dr("INFO") = "H:" & ddd.Hour.ToString()
        dt2.Rows.Add(dr)



        dr = dt2.NewRow
        dr("ID") = "P5"
        dr("COLOR") = ""
        dr("BLINK") = "NO"
        dr("INFO") = "Min:" & ddd.Minute.ToString()
        dt2.Rows.Add(dr)
		
		dr = dt2.NewRow
        dr("ID") = "P6"
        dr("COLOR") = ""
        dr("BLINK") = "NO"
        dr("INFO") = "S:" & ddd.Second.ToString()
        dt2.Rows.Add(dr)

        dr = dt2.NewRow
        dr("ID") = "P7"
        dr("COLOR") = ""
        dr("BLINK") = "NO"
        dr("INFO") = "mSEC:" & ddd.Millisecond.ToString()
        dt2.Rows.Add(dr)
		
	
		
		dr = dt2.NewRow
        dr("ID") = "P8"
        dr("COLOR") = ""
        dr("BLINK") = "NO"
        dr("INFO") = ""
        dt2.Rows.Add(dr)
		
		dr = dt2.NewRow
        dr("ID") = "P9"
        dr("COLOR") = ""
        dr("BLINK") = "NO"
        dr("INFO") = ""
        dt2.Rows.Add(dr)

		dr = dt2.NewRow
        dr("ID") = "P0"
        dr("COLOR") = ""
        dr("BLINK") = "NO"
        dr("INFO") = ""
        dt2.Rows.Add(dr)
        '''''''''''''''''
    End Sub


End Class
