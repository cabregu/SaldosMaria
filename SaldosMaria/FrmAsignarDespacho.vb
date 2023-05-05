Imports System.Data.OleDb


Public Class FrmAsignarDespacho

 

    'Function SoloNumeros(ByVal Keyascii As Short) As Short
    '    If InStr("1234567890,", Chr(Keyascii)) = 0 Then
    '        SoloNumeros = 0
    '    Else
    '        SoloNumeros = Keyascii
    '    End If
    '    Select Case Keyascii
    '        Case 8
    '            SoloNumeros = Keyascii
    '        Case 13
    '            SoloNumeros = Keyascii
    '    End Select
    'End Function
    'Private Sub TxtDespacho_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDespacho.LostFocus

    '    'Try
    '    '    DtpFechaOficializacion.Value = ObtenerfechaOfi(TxtDespacho.Text)
    '    'Catch ex As Exception
    '    'End Try


    'End Sub


    Private Sub ActualizarVep(ByVal FechaOficializacion As Date, ByVal Despacho As String, ByVal Iddeposito As String, ByVal Usado As String)
        'Try
        Dim Saldo As Decimal = "0,00"
        Dim Monto As Decimal = "0,00"
        Saldo = TxtSaldo.Text
        Monto = TxtUsado.Text

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
            Me.Dispose()
            Me.Close()

            'FrmSaldosMaria.CARGARDEPOSITOS()

        Else
            MsgBox("No puede Usar mas que el saldo, verifique importe")
        End If

    End Sub




    Private Sub Asignar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Asignar.Click



    End Sub
End Class