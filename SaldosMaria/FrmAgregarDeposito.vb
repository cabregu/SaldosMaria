Imports System.Data.OleDb

Public Class FrmAgregarDeposito

    Dim RutaDepositos As String

    Private Sub Seleccionar()
        Try
            Dim openFD As New OpenFileDialog()
            With openFD
                .Title = "Seleccionar archivos"
                .Filter = "Todos los archivos (*.xls)|*.xls"
                .Multiselect = False
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    RutaDepositos = .FileName
                Else
                    openFD.Dispose()
                End If

                Insertardesdexls()


            End With
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Insertardesdexls()
        Dim dt As New DataTable
        'Try
        Dim strconn As String
        strconn = "Provider=Microsoft.Jet.Oledb.4.0; data source= " + RutaDepositos + ";Extended properties=""Excel 8.0;hdr=yes;imex=1"""
        Dim mconn As New OleDbConnection(strconn)
        Dim ad As New OleDbDataAdapter("Select * from [" & "DEPOSITOS" & "$]", mconn)
        mconn.Open()
        ad.Fill(dt)
        mconn.Close()

        For Each drw As DataRow In dt.Rows
            Dim fecha As Date = drw(0).ToString

            DgvDepositos.Rows.Add(fecha.ToShortDateString, drw(1).ToString, drw(2).ToString)
        Next

    End Sub

    Private Sub Importar()

        For Each dw As DataGridViewRow In DgvDepositos.Rows

            CargarDepositos(dw.Cells("FechaDeposito").Value, dw.Cells("NroDeposito").Value, dw.Cells("ImporteDeposito").Value)

   

        Next
        MsgBox("OK")
        Me.Close()

    End Sub



    Private Sub CargarDepositos(ByVal Fecha As String, ByVal Nrodeposito As String, ByVal Deposito As String)

        Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\SALDOMARIA\BaseSaldos.mdb")
        Dim CMd2 As New OleDbCommand("INSERT INTO Base (fecha, importe, saldo, cliente, Nrodeposito) VALUES (" & "'" & Fecha & "'" & ", " & "'" & Deposito & "'" & ", " & "'" & Deposito & "', 'CATALENT' " & ", " & Nrodeposito & ")", cn)
        cn.Open()
        CMd2.ExecuteNonQuery()
        cn.Close()

    End Sub

    Private Sub BtnCargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCargar.Click
        Seleccionar()
    End Sub

    Private Sub BtnImportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImportar.Click
        Importar()
        FrmSaldosMaria.CARGARDEPOSITOS()
    End Sub
End Class