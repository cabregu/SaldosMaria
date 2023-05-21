Imports System.Data.OleDb
Imports System.Data.SQLite
Imports System.IO

Public Class FrmSaldosMaria
    Dim TablaDT As New DataTable

    Private Sub SaldosMaria_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CrearCopia()
        CARGARDEPOSITOS()

    End Sub
    Private Sub CrearCopia()

        Dim originalPath As String = "C:\SaldosMaria\SaldosMaria.db"
        Dim backupFolderPath As String = "C:\SaldosMaria\Backup"

        If File.Exists(originalPath) Then
            Dim fecha As Date = Now
            Dim fechaTx As String = fecha.ToString("dd-MM-yyyy")
            Dim backupPath As String = Path.Combine(backupFolderPath, $"SaldosMaria_{fechaTx}.db")

            If Not File.Exists(backupPath) Then
                Try
                    File.Copy(originalPath, backupPath)
                    MsgBox("Copia de seguridad creada correctamente.", MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox("Error al crear la copia de seguridad: " & ex.Message, MsgBoxStyle.Exclamation)
                End Try
            Else
                'MsgBox("Ya existe una copia de seguridad para la fecha actual.", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("No se encontró el archivo de base de datos original.", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Public Function CARGARDEPOSITOS()
        If DgvSaldos.Rows.Count > 0 Then
            DgvSaldos.Rows.Clear()
        End If
        Try
            Dim cn As New SQLiteConnection("Data Source=C:\SaldosMaria\SaldosMaria.db;Version=3;")
            Dim cm As New SQLiteCommand("SELECT * FROM BASE WHERE SALDO > 0.00", cn)
            Dim da As New SQLiteDataAdapter(cm)
            Dim ds As New DataSet()
            da.Fill(ds, "BASE")

            For Each dr As DataRow In ds.Tables("BASE").Rows
                Dim Fech As Date = Convert.ToDateTime(dr("FECHA"))
                Dim imported As Decimal = Convert.ToDecimal(dr("IMPORTE"))
                Dim saldod As Decimal = Convert.ToDecimal(dr("SALDO"))

                DgvSaldos.Rows.Add(Fech.ToShortDateString, dr("Nrodeposito").ToString, imported, saldod)
            Next
            cn.Close()

            Dim imported2 As Decimal = 0.00
            For Each dr As DataRow In ds.Tables("BASE").Rows
                imported2 = imported2 + Convert.ToDecimal(dr("saldo"))
            Next
            TxtTotalMaria.Text = imported2.ToString()

        Catch ex As Exception
            MessageBox.Show(ex.ToString & " Verifique su conexión a la base de datos.")
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
                    Insertardesdexls(.FileName)
                Else
                    openFD.Dispose()
                End If

            End With
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Insertardesdexls(ByVal ruta As String)
        Dim dt As New DataTable
        Dim strconn As String
        strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ruta & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1"""
        Dim mconn As New OleDbConnection(strconn)
        Dim ad As New OleDbDataAdapter("SELECT * FROM [" & "vep" & "$]", mconn)
        mconn.Open()
        ad.Fill(dt)
        mconn.Close()

        Dgvdat.DataSource = dt


        'If DgvVep.RowCount > 0 Then
        '    For Each drwvep As DataGridViewRow In DgvVep.Rows
        '        For Each drwsaldo As DataGridViewRow In DgvSaldos.Rows
        '            If drwvep.Cells("NroVep").Value = drwsaldo.Cells("NroDeposito").Value Then
        '                If drwsaldo.Cells("Saldo").Value >= drwvep.Cells("ImporteAfectado").Value Then
        '                    drwvep.DefaultCellStyle.BackColor = Color.Green
        '                Else
        '                    drwvep.DefaultCellStyle.BackColor = Color.Red
        '                End If
        '            End If
        '        Next
        '    Next
        'End If



        'For Each drwDG As DataGridViewRow In DgvVep.Rows
        '    For Each DrwDesp As DataRow In TablaDT.Rows
        '        If DrwDesp("Cliente").ToString.Contains(drwDG.Cells("Referencia").Value) Then
        '            drwDG.Cells("Despacho").Value = DrwDesp("Despacho").ToString
        '            drwDG.Cells("FechaOf").Value = DrwDesp("FechaOf").ToString
        '        End If

        '    Next
        'Next




    End Sub
    Private Sub BtnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeleccionar.Click
        Seleccionar()

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


        For Each drw As DataGridViewRow In Dgvdat.Rows
            For Each dr As DataRow In Dt.Rows


                If drw.Cells("Referencia").Value = dr("Emb").ToString Then
                    Try
                        Dim Fechad As Date = dr("Fecha").ToString
                        drw.Cells("Despacho").Value = dr("NroDespacho").ToString
                        drw.Cells("FechaOf").Value = Fechad.ToShortDateString
                    Catch ex As Exception

                    End Try

                End If
            Next
        Next



    End Sub
    Public Function descargarsaldo(ByVal Iddeposito As Integer, ByVal Saldo1 As String, ByVal Monto1 As String) As Boolean
        Dim Saldo As Decimal = Convert.ToDecimal(Saldo1)
        Dim Monto As Decimal = Convert.ToDecimal(Monto1)

        Dim result As Decimal = Saldo - Monto

        If Monto <= Saldo Then
            Try
                Dim cn As New SQLiteConnection("Data Source=C:\SALDOMARIA\BaseSaldos.db;Version=3;")
                Dim Sql As String = "UPDATE BASE SET SALDO = @result WHERE iddeposito = @Iddeposito"

                Dim cm1 As New SQLiteCommand(Sql, cn)
                cm1.Parameters.AddWithValue("@result", result)
                cm1.Parameters.AddWithValue("@Iddeposito", Iddeposito)

                cn.Open()
                cm1.ExecuteNonQuery()
                cn.Close()

                Me.Dispose()
                Me.Close()

            Catch ex As Exception
                MessageBox.Show(ex.ToString & " Verifique su conexión a la base de datos.")
            End Try
        Else
            MsgBox("No puede usar más que el saldo, verifique el importe")
        End If

        Return True
    End Function
    Private Sub BtnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnActualizar.Click
        For Each drw As DataGridViewRow In Dgvdat.Rows
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
        Dgvdat.Rows.Clear()



    End Sub
    Private Sub ActualizarVep(ByVal FechaOficializacion As Date, ByVal Despacho As String, ByVal Iddeposito As String, ByVal Usado As String, ByVal Saldo1 As String)
        Dim Saldo As Decimal = 0.0
        Dim Monto As Decimal = 0.0
        Decimal.TryParse(Saldo1, Saldo)
        Decimal.TryParse(Usado, Monto)

        If Monto <= Saldo Then
            Dim connectionString As String = "Data Source=C:\SALDOMARIA\BaseSaldos.db"
            Using cn As New SQLiteConnection(connectionString)
                cn.Open()

                ' Insertar registro en la tabla DespachosOficializados
                Dim insertQuery As String = "INSERT INTO DespachosOficializados (FechaOficializacion, Numerodespacho, DepositoUsado, Descontado) VALUES (@FechaOficializacion, @Despacho, @Iddeposito, @Usado)"
                Using cmdInsert As New SQLiteCommand(insertQuery, cn)
                    cmdInsert.Parameters.AddWithValue("@FechaOficializacion", FechaOficializacion)
                    cmdInsert.Parameters.AddWithValue("@Despacho", Despacho)
                    cmdInsert.Parameters.AddWithValue("@Iddeposito", Iddeposito)
                    cmdInsert.Parameters.AddWithValue("@Usado", Usado)
                    cmdInsert.ExecuteNonQuery()
                End Using

                ' Actualizar el saldo en la tabla BASE
                Dim result As Decimal = Saldo - Monto
                Dim updateQuery As String = "UPDATE BASE SET SALDO = @Saldo WHERE Nrodeposito = @Iddeposito"
                Using cmdUpdate As New SQLiteCommand(updateQuery, cn)
                    cmdUpdate.Parameters.AddWithValue("@Saldo", result)
                    cmdUpdate.Parameters.AddWithValue("@Iddeposito", Iddeposito)
                    cmdUpdate.ExecuteNonQuery()
                End Using
            End Using

            'FrmSaldosMaria.CARGARDEPOSITOS()
        Else
            MsgBox("No puede usar más que el saldo del despacho: " & Despacho & " - deposito: " & Iddeposito & " - Usar: " & Usado & " - Saldo: " & Saldo1)
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Dgvdat.RowCount > 0 Then
            CargarPlanillaCatalent()
        Else
            MsgBox("Primero carga el excel")
        End If


    End Sub





End Class
