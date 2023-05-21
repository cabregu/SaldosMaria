Imports System.Data.OleDb



Public Class FrmInforme
    Dim TablaDT As New DataTable

    Private Sub FrmInforme_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CARGARINFORME()
    End Sub


    Public Function CARGARINFORME()

        If DgbInforme.Rows.Count > 0 Then
            DgbInforme.Rows.Clear()
        End If
        'Try
        Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\SALDOMARIA\BaseSaldos.mdb")
        Dim cm As New OleDbCommand("SELECT * FROM Base", cn)
        Dim da As New OleDbDataAdapter(cm)
        Dim ds As New DataSet()
        da.Fill(ds, "BASE")


        Dim cn1 As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\SALDOMARIA\BaseSaldos.mdb")
        Dim cm1 As New OleDbCommand("SELECT * FROM DespachosOficializados", cn1)
        Dim da1 As New OleDbDataAdapter(cm1)
        Dim ds1 As New DataSet()
        da1.Fill(ds, "DespachosOficializados")

        Dim FECHALTE As Date = Now.ToShortDateString
        FECHALTE = FECHALTE.AddDays(-270)


        For Each dr As DataRow In ds.Tables("BASE").Rows

            Dim FechaDep As Date = dr("Fecha").ToString
            If FechaDep > FECHALTE Then


                DgbInforme.Rows.Add("", FechaDep.ToShortDateString, "", "", dr("importe").ToString, "", dr("saldo").ToString)

                For Each dr2 As DataRow In ds.Tables("DespachosOficializados").Rows

                    If dr("NroDeposito").ToString = dr2("DepositoUsado").ToString Then
                        Dim FechDesp As Date = dr2("FechaOficializacion").ToString
                        Dim Referenciapordespacho As String = ""
                        For Each drw As DataRow In TablaDT.Rows

                            If dr2("Numerodespacho").ToString = drw("Despacho").ToString Then
                                Referenciapordespacho = drw("Cliente").ToString

                                Try
                                    If Len(Referenciapordespacho) > 10 Then

                                        Referenciapordespacho = Referenciapordespacho.Substring(Referenciapordespacho.LastIndexOf("CT-") - 2).ToString

                                    End If
                                Catch ex As Exception

                                End Try


                            End If
                        Next

                        DgbInforme.Rows.Add(Referenciapordespacho, FechDesp.ToShortDateString, dr2("Numerodespacho").ToString, dr2("DepositoUsado").ToString, "", dr2("Descontado").ToString)
                    End If
                Next

            End If
        Next


        'Catch ex As Exception
        '    MessageBox.Show("verifique su conexion al server")
        'End Try
        Return Nothing
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnXls.Click
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
            Dim NCol As Integer = DgbInforme.ColumnCount
            Dim NRow As Integer = DgbInforme.RowCount


            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exHoja.Cells.Item(1, i) = DgbInforme.Columns(i - 1).Name.ToString
                'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
            Next

            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    exHoja.Cells.Item(Fila + 2, Col + 1) = DgbInforme.Rows(Fila).Cells(Col).Value
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



End Class