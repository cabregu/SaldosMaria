Imports System.Data.OleDb
Imports System.IO



Public Class FrmSaldosMaria
    Public TablaDT As DataTable = New DataTable
    '

    Private Sub SaldosMaria_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CARGARDEPOSITOS()
        crearcopia()
        If MessageBox.Show("Cargar Datos de Ret", "Cargar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            iniciar()
        End If


    End Sub
    Public Function CARGARDEPOSITOS()
        If DgvSaldos.Rows.Count > 0 Then
            DgvSaldos.Rows.Clear()
        End If
        Try
            Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\SALDOMARIA\BaseSaldos.mdb")
            Dim cm As New OleDbCommand("SELECT * FROM BASE where SALDO>0.00", cn)
            Dim da As New OleDbDataAdapter(cm)
            Dim ds As New DataSet()
            da.Fill(ds, "BASE")

            For Each dr As DataRow In ds.Tables("BASE").Rows
                Dim Fech As Date = dr("FECHA").ToString
                Dim imported As Decimal = dr("IMPORTE").ToString
                Dim saldod As Decimal = dr("SALDO").ToString

                DgvSaldos.Rows.Add(Fech.ToShortDateString, dr("Nrodeposito").ToString, imported, saldod)
            Next
            cn.Close()

            Dim imported2 As Decimal = "0.00"
            For Each dr As DataRow In ds.Tables("BASE").Rows
                imported2 = imported2 + dr("saldo").ToString
            Next
            TxtTotalMaria.Text = imported2


        Catch ex As Exception
            MessageBox.Show(ex.ToString & "verifique su conexion al server")
        End Try
        Return Nothing
    End Function

    Private Sub BtnDeposito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeposito.Click
        FrmAgregarDeposito.Show()

    End Sub
    Private Sub BtnInforme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInforme.Click
        FrmInforme.Show()
    End Sub
    Private Sub Seleccionar()
        Try
            Dim openFD As New OpenFileDialog()
            With openFD
                .Title = "Seleccionar archivos"
                .Filter = "Todos los archivos (*.xls)|*.xls"
                .Multiselect = False
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    txtPath.Text = .FileName
                Else
                    openFD.Dispose()
                End If

            End With
        Catch ex As Exception

        End Try


    End Sub
    Private Sub Insertardesdexls()
        Dim dt As New DataTable

        '
        'Try
        Dim strconn As String
        strconn = "Provider=Microsoft.Jet.Oledb.4.0; data source= " + txtPath.Text + ";Extended properties=""Excel 8.0;hdr=yes;imex=1"""
        Dim mconn As New OleDbConnection(strconn)
        Dim ad As New OleDbDataAdapter("Select * from [" & "VEP" & "$]", mconn)
        mconn.Open()
        ad.Fill(dt)
        mconn.Close()

        For Each drw As DataRow In dt.Rows
            DgvVep.Rows.Add(drw(0).ToString, "", "", drw(1).ToString, drw(2).ToString)

        Next

        If DgvVep.RowCount > 0 Then
            For Each drwvep As DataGridViewRow In DgvVep.Rows
                For Each drwsaldo As DataGridViewRow In DgvSaldos.Rows
                    If drwvep.Cells("NroVep").Value = drwsaldo.Cells("NroDeposito").Value Then
                        If drwsaldo.Cells("Saldo").Value >= drwvep.Cells("ImporteAfectado").Value Then
                            drwvep.DefaultCellStyle.BackColor = Color.Green
                        Else
                            drwvep.DefaultCellStyle.BackColor = Color.Red
                        End If
                    End If
                Next
            Next
        End If



        For Each drwDG As DataGridViewRow In DgvVep.Rows
            For Each DrwDesp As DataRow In TablaDT.Rows
                If DrwDesp("Cliente").ToString.Contains(drwDG.Cells("Referencia").Value) Then
                    drwDG.Cells("Despacho").Value = DrwDesp("Despacho").ToString
                    drwDG.Cells("FechaOf").Value = DrwDesp("FechaOf").ToString
                End If

            Next
        Next




    End Sub
    Private Sub BtnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeleccionar.Click
        Seleccionar()
        Insertardesdexls()


    End Sub

    Private Sub CargarPlanillaCatalent()


        Dim Dt As New DataTable
        Dim strconn As String
        strconn = "Provider=Microsoft.Jet.Oledb.4.0; data source= " + "\\Aleph-server-pc\server nuevo\Pc52\MARIA CATALENT.xls" + ";Extended properties=""Excel 8.0;hdr=yes;imex=1"""
        Dim mconn As New OleDbConnection(strconn)
        Dim ad As New OleDbDataAdapter("Select * from [" & "CT" & "$]", mconn)
        mconn.Open()
        ad.Fill(Dt)
        mconn.Close()


        For Each drw As DataGridViewRow In DgvVep.Rows
            For Each dr As DataRow In Dt.Rows


                If drw.Cells("Referencia").Value = dr("Emb").ToString Then
                    Dim Fechad As Date = dr("Fecha").ToString
                    drw.Cells("Despacho").Value = dr("NroDespacho").ToString
                    drw.Cells("FechaOf").Value = Fechad.ToShortDateString
                End If
            Next
        Next



    End Sub
    Public Function descargarsaldo(ByVal Iddeposito As Integer, ByVal Saldo1 As String, ByVal Monto1 As String) As Boolean

        Dim Saldo As Decimal = Saldo1
        Dim Monto As Decimal = Monto1

        Dim result As String = Saldo - Monto
        result = Replace(result, ".", ",")

        If Monto <= Saldo Then

            Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\SALDOMARIA\BaseSaldos.mdb")
            Dim Sql As String = "Update BASE set SALDO='" & result & "' where iddeposito = " & Iddeposito & ""

            Dim cm1 As New OleDbCommand(Sql, cn)
            cn.Open()
            cm1.ExecuteNonQuery()
            cn.Close()
            Me.Dispose()
            Me.Close()


        Else
            MsgBox("No puede Usar mas que el saldo, verifique importe")

        End If


    End Function



    Private Sub iniciar()


        TablaDT.Columns.Add("Despacho")
        TablaDT.Columns.Add("Tc")
        TablaDT.Columns.Add("Total")
        TablaDT.Columns.Add("FechaOf")
        TablaDT.Columns.Add("Cliente")

        Dim RutasList As New ArrayList

        RutasList.Add("\\Aleph-server-pc\out")
        RutasList.Add("\\aleph-server-pc\DUXBACK\RETs\OUT")

        Dim FechaUltimos60 As Date = Now.ToShortDateString
        FechaUltimos60 = FechaUltimos60.AddDays(-200)




        For Each RutaPpal As String In RutasList


            If Directory.Exists(RutaPpal) = True Then
                Dim di As New DirectoryInfo(RutaPpal)
                Dim ficheros() As FileInfo = di.GetFiles("*.RET")
                '*********************************************************
                For Each f As FileInfo In ficheros
                    Dim Archivo As String = f.Name
                    Dim Inicial As String = f.Name
                    Dim Regist As String = f.Name
                    Regist = Regist.Substring(1, 16)
                    Inicial = Inicial.Substring(0, 1)
                    If Not Regist.Contains("SIMI") Then
                        If Not Regist.Contains("EC") Then
                            Dim DatosCreacion As Date = f.CreationTime
                            If DatosCreacion > FechaUltimos60 Then
                                If Inicial = "C" Then
                                    Dim dst As New DataSet
                                    dst = New DataSet
                                    dst.ReadXml(RutaPpal & "\C" & Regist & ".RET")


                                    For Each drw As DataRow In dst.Tables("CARATULA").Rows
                                        Dim Fechaofi As Date = drw.Item("FECHA_OFICIALIZACION").ToString()
                                        If dst.Tables(0).Columns.Contains("COMPRADOR_VENDEDOR").ToString Then
                                            'If Not drw.Item("COMPRADOR_VENDEDOR").ToString.Contains("TI-") Then
                                            If Not drw.Item("COMPRADOR_VENDEDOR").ToString.Contains("OR-") Then
                                                If Not drw.Item("COMPRADOR_VENDEDOR").ToString.Contains("PD-") Then
                                                    If Not drw.Item("COMPRADOR_VENDEDOR").ToString.Contains("DB-") Then
                                                        Dim Importe As String
                                                        Dim TipoDeCambio As String

                                                        Importe = drw("COTIZACION_PAGOS")
                                                        TipoDeCambio = dst.Tables("LIQUIDACION").Rows(0)("TOTAL_A_PAGAR").ToString
                                                        Importe = Importe.Replace(".", ",")
                                                        TipoDeCambio = TipoDeCambio.Replace(".", ",")

                                                        TablaDT.Rows.Add(Regist, Importe, TipoDeCambio, Fechaofi.ToShortDateString, drw.Item("COMPRADOR_VENDEDOR").ToString)
                                                    End If
                                                End If
                                            End If
                                            'End If

                                        Else

                                            Dim Importe As String
                                            Dim TipoDeCambio As String

                                            Importe = drw("COTIZACION_PAGOS")
                                            TipoDeCambio = dst.Tables("LIQUIDACION").Rows(0)("TOTAL_A_PAGAR").ToString
                                            Importe = Importe.Replace(".", ",")
                                            TipoDeCambio = TipoDeCambio.Replace(".", ",")

                                            TablaDT.Rows.Add(Regist, Importe, TipoDeCambio, Fechaofi.ToShortDateString, "")
                                        End If

                                    Next
                                End If
                            End If
                        End If
                    End If
                Next


                MsgBox("OK")
                DgvExcel.DataSource = TablaDT


            Else
                MessageBox.Show("El directorio " & RutaPpal & " No Existe")
            End If
        Next

    End Sub


    Private Sub crearcopia()

        If File.Exists("C:\SALDOMARIA\BaseSaldos.mdb") = True Then
            Dim Fecha As Date = Now.ToShortDateString
            Dim FechaTx As String = Fecha
            FechaTx = FechaTx.Replace("/", "_")

            If Not File.Exists("C:\SALDOMARIA\BaseSaldos" & FechaTx & ".mdb") Then
                File.Copy("C:\SALDOMARIA\BaseSaldos.mdb", "C:\SALDOMARIA\BaseSaldos" & FechaTx & ".mdb")
                MsgBox("Backup Creado")
            Else
                'MsgBox("Ya existe backup")

            End If
        End If
    End Sub
 
   
    Private Sub BtnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcel.Click
        'Agregar referencia Microsoft.Office.Interop.Excel()
        'Creamos las variables
        Dim exApp As New Microsoft.Office.Interop.Excel.Application
        Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
        Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

        Try
            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()
            exHoja.Cells.NumberFormat = "@"
            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DgvExcel.ColumnCount
            Dim NRow As Integer = DgvExcel.RowCount


            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exHoja.Cells.Item(1, i) = DgvExcel.Columns(i - 1).Name.ToString
                'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
            Next

            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    exHoja.Cells.Item(Fila + 2, Col + 1) = DgvExcel.Rows(Fila).Cells(Col).Value
                Next
            Next
            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se
            'ajuste al texto

            'exHoja.Rows.Item(5).Font.Bold = 1
            'exHoja.Rows.Item(5).HorizontalAlignment = 3
            exHoja.Columns.AutoFit()



            'Aplicación visible
            exApp.Application.Visible = True

            exHoja = Nothing
            exLibro = Nothing
            exApp = Nothing

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")

        End Try
    End Sub

 
    Private Sub BtnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnActualizar.Click
        For Each drw As DataGridViewRow In DgvVep.Rows
            Dim fech As Date = drw.Cells("FechaOf").Value
            Dim ImporteSaldo As String = "0"

            For Each drwsaldo As DataGridViewRow In DgvSaldos.Rows
                If drw.Cells("NroVep").Value = drwsaldo.Cells("NroDeposito").Value Then
                    ImporteSaldo = drwsaldo.Cells("saldo").Value
                End If
            Next
            ActualizarVep(fech, drw.Cells("Despacho").Value, drw.Cells("NroVep").Value, drw.Cells("ImporteAfectado").Value, ImporteSaldo)
            Me.CARGARDEPOSITOS()
        Next
        DgvVep.Rows.Clear()


    End Sub


    Private Sub ActualizarVep(ByVal FechaOficializacion As Date, ByVal Despacho As String, ByVal Iddeposito As String, ByVal Usado As String, ByVal Saldo1 As String)
        'Try
        Dim Saldo As Decimal = "0,00"
        Dim Monto As Decimal = "0,00"
        Saldo = Saldo1
        Monto = Usado

        If Monto <= Saldo Then
            Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\SALDOMARIA\BaseSaldos.mdb")
            Dim CMd2 As New OleDbCommand("INSERT INTO DespachosOficializados (FechaOficializacion, Numerodespacho, DepositoUsado, Descontado) VALUES (" & "'" & FechaOficializacion & "', '" & Despacho & "'" & ", " & "'" & Iddeposito & "'" & ", " & "'" & Usado & "'" & ")", cn)
            cn.Open()
            CMd2.ExecuteNonQuery()
            cn.Close()


            Dim result As String = Saldo - Monto
            result = Replace(result, ".", ",")
            Dim Sql As String = "Update BASE set SALDO='" & result & "' where Nrodeposito = '" & Iddeposito & "'"
            Dim cm1 As New OleDbCommand(Sql, cn)
            cn.Open()
            cm1.ExecuteNonQuery()
            cn.Close()
            'Me.Dispose()


            'FrmSaldosMaria.CARGARDEPOSITOS()

        Else
            MsgBox("No puede Usar mas que el saldo despacho : " & Despacho & " - " & " - deposito  : " & Iddeposito & " - Usar :  " & Usado & " - Saldo : " & Saldo1)
        End If

    End Sub




    'Private Sub DgvVep_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvVep.CellDoubleClick
    '    Dim N As String = DgvVep.SelectedCells(0).RowIndex.ToString
    '    Dim IMPORTE As String = DgvVep.Rows(N).Cells("ImporteAfectado").Value
    '    Dim NROVEP1 As String = DgvVep.Rows(N).Cells("Nrovep").Value
    '    Dim FECHA As String = DgvVep.Rows(N).Cells("FechaOf").Value
    '    Dim DESPACHO1 As String = DgvVep.Rows(N).Cells("Despacho").Value



    '    FrmAsignarDespacho.TxtSaldo.Text = IMPORTE
    '    FrmAsignarDespacho.TxtIddeposito.Text = NROVEP1
    '    FrmAsignarDespacho.DtpFechaOficializacion.Value = FECHA
    '    FrmAsignarDespacho.TxtDespacho.Text = DESPACHO1


    '    FrmAsignarDespacho.Show()


    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DgvVep.RowCount > 0 Then
            CargarPlanillaCatalent()
        Else
            MsgBox("Primero carga el excel")
        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FrmLeerImagen.Show()

    End Sub
End Class
