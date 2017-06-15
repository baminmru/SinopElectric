Imports System.Data
Imports Newtonsoft.Json


Partial Class sinop1
    Inherits System.Web.UI.Page


    Private Function IsBitSet(ByVal V As UShort, bit As Byte) As Boolean

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


       
        Dim dt2 As DataTable
        dt2 = cm.GetOutputTab
       ' cm.GetCommonParams(dt2)

		
		Dim dr As DataRow
        Dim v As UShort
        Dim T1 As UShort
		Dim T2 As UShort
		Dim M1 As UShort
		Dim M2 As UShort
		Dim P1 As UShort
		Dim P2 As UShort 
		Dim G1 As UShort


        '''''''''''''''''''''''''''''''  "test"
		on error resume next
		
		dim dd as date
		dd =Date.Now
		
        dt = cm.QuerySelect(" SELECT * FROM datacurr WHERE id_bd=600 and id_ptype =1 AND dcounter >SYSDATE - 1/24 order BY dcounter desc")
        If dt.Rows.Count > 0 Then
            If Not (TypeOf (dt.Rows(0)("T1")) Is DBNull) Then
                T1 = CType(dt.Rows(0)("T1") mod 32767, UShort)
            Else
                T1 = 0
            End If

            If Not (TypeOf (dt.Rows(0)("T2")) Is DBNull) Then
                T2 = CType(dt.Rows(0)("T2") mod 32767, UShort)
            Else
                T2 = 0
            End If
			
			
			If Not (TypeOf (dt.Rows(0)("V1")) Is DBNull) Then
                P1 = CType(dt.Rows(0)("V1") mod 32767, UShort)
            Else
                P1 = 0
            End If
			
			
			If Not (TypeOf (dt.Rows(0)("V2")) Is DBNull) Then
                P2 = CType(dt.Rows(0)("V2") mod 32767, UShort)
            Else
                P2 = 0
            End If
			
			If Not (TypeOf (dt.Rows(0)("M1")) Is DBNull) Then
                M1 = CType(dt.Rows(0)("M1") mod 32767, UShort)
            Else
                M1 = 0
            End If
			
			
			If Not (TypeOf (dt.Rows(0)("M2")) Is DBNull) Then
                M2 = CType(dt.Rows(0)("M2") mod 32767, UShort)
            Else
                M2 = 0
            End If
			
			If Not (TypeOf (dt.Rows(0)("G1")) Is DBNull) Then
                G1 = CType(dt.Rows(0)("G1") mod 32767, UShort)
            Else
                G1 = 0
            End If
		else
			jj = New JOut
			jj.success = "true"
			jj.data = dt2
			jj.msg = "ERROR-no DATA"
			Response.Clear()
			Response.Write(JsonConvert.SerializeObject(jj))
			Response.End()
			return
        End If




        
        dr = dt2.NewRow
        dr("ID") = "1AB"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

      
		If cm.IsBitSet(T1, 0) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Вкл."
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
	
        dt2.Rows.Add(dr)


		dr = dt2.NewRow
        dr("ID") = "2AB"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(T1, 1) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)


		
		dr = dt2.NewRow
        dr("ID") = "3AB"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(T1, 2) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		
		
		dr = dt2.NewRow
        dr("ID") = "4AB"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(T2, 0) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "GRAY"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		
		dr = dt2.NewRow
        dr("ID") = "5AB"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(T2, 1) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "GRAY"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		
		dr = dt2.NewRow
        dr("ID") = "6AB"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(T2, 2) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "GRAY"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		
		dr = dt2.NewRow
        dr("ID") = "7AB"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(M1, 0) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		
		
		dr = dt2.NewRow
        dr("ID") = "ABP1"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(M1, 1) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		
		dr = dt2.NewRow
        dr("ID") = "ABP3"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(M1, 2) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		dr = dt2.NewRow
        dr("ID") = "ABP4"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(M2, 0) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		dr = dt2.NewRow
        dr("ID") = "ABP5"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(M2, 1) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		dr = dt2.NewRow
        dr("ID") = "ABP6"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(M2, 2) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		
		dr = dt2.NewRow
        dr("ID") = "ABP7"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(G1, 0) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		dr = dt2.NewRow
        dr("ID") = "ABP8"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(G1, 1) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		
		'''''''''''' ADES
		
		   dr = dt2.NewRow
        dr("ID") = "ADES1"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

      
		If cm.IsBitSet(T1, 0) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Вкл."
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
	
        dt2.Rows.Add(dr)


		dr = dt2.NewRow
        dr("ID") = "ADES2"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(T1, 1) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)


		
		dr = dt2.NewRow
        dr("ID") = "ADES3"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(T1, 2) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)
		
		
		dr = dt2.NewRow
        dr("ID") = "ADES2"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(T1, 1) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Работа"
		Else
			dr("COLOR") = "YELLOW"
			dr("INFO") = "Отключен"
		End If
        dt2.Rows.Add(dr)


		''''''''''''
		dr = dt2.NewRow
        dr("ID") = "P1"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(T1, 2) Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Есть напряжение"
		Else
			dr("COLOR") = "RED"
			dr("INFO") = "Нет напряжения"
		End If
        dt2.Rows.Add(dr)
		
		
		dr = dt2.NewRow
        dr("ID") = "P2"
        dr("COLOR") = "-"
        dr("BLINK") = "NO"
        dr("INFO") = "-"

        If cm.IsBitSet(T1, 2)=0 Then
			dr("COLOR") = "GREEN"
			dr("INFO") = "Есть напряжение"
		Else
			dr("COLOR") = "RED"
			dr("INFO") = "Нет напряжения"
		End If
        dt2.Rows.Add(dr)
		
		

        jj = New JOut
        jj.success = "true"
        jj.data = dt2
        jj.msg = "OK"
        Response.Clear()
        Response.Write(JsonConvert.SerializeObject(jj))
        Response.End()



    End Sub


End Class
