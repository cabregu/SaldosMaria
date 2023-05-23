Imports System.Data.SQLite
Imports System.IO
Imports System.Threading
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports Google.Apis.Sheets.v4



Public Class FrmSaldosMaria

    Private Sub BtnDeposito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeposito.Click
        FrmAgregarDeposito.Show()

    End Sub

    Private Sub SaldosMaria_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CrearCopia()
        CARGARDEPOSITOS()
        LeerDoscPoUbicacionArchivoVeps()
        LeerDoscPoUbicacionArchivoEmbarques()

    End Sub






    Public Function CARGARDEPOSITOS()
        If DgvSaldos.Rows.Count > 0 Then
            DgvSaldos.Rows.Clear()
        End If
        Try
            Dim cn As New SQLiteConnection("Data Source=C:\Users\Cristian\source\repos\SaldosMaria\SaldosMaria\SaldosMaria.db;Version=3;")
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
    Private Sub LeerDoscPoUbicacionArchivoVeps()

        Dim DataTable As New DataTable



        ' Autenticarse con las credenciales descargadas
        Dim credential As UserCredential
        Using stream As New FileStream("C:\Users\Cristian\Source\Repos\SaldosMaria\SaldosMaria\credencial.json", FileMode.Open, FileAccess.Read)
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                {"https://www.googleapis.com/auth/spreadsheets.readonly"},
                "cabregu@alephargentina.com.ar",
                CancellationToken.None
            ).Result
        End Using

        ' Crear el servicio de Google Sheets
        Dim service = New SheetsService(New BaseClientService.Initializer() With {
            .HttpClientInitializer = credential,
            .ApplicationName = "SALDOS"
        })

        ' ID de la hoja de cálculo y rango de la hoja a leer
        Dim spreadsheetId = "1Ak1I4oGmhnsQ-62OHNjwjynmIM60oCpbb5x2nWAljrc"
        Dim spreadsheet = service.Spreadsheets.Get(spreadsheetId).Execute()
        Dim sheets = spreadsheet.Sheets

        ' Verificar si hay al menos tres hojas de cálculo en el archivo
        If sheets.Count >= 3 Then
            Dim sheetIndex = 2
            ' Construir el rango de la tercera hoja utilizando el índice
            Dim range = sheets(sheetIndex).Properties.Title & "!B1:F"

            ' Leer los valores de la hoja de cálculo
            Dim request = service.Spreadsheets.Values.Get(spreadsheetId, range)
            Dim response = request.Execute()
            Dim values = response.Values

            ' Crear un DataTable y agregar los registros


            DataTable.Columns.Add("FECHA", GetType(String))
            dataTable.Columns.Add("EMBARQUE", GetType(String))
            dataTable.Columns.Add("IMPORTE", GetType(Double))
            dataTable.Columns.Add("N°VEP", GetType(String))
            dataTable.Columns.Add("USUARIO", GetType(String))

            ' ...

            ' Agregar filas al DataTable
            If values IsNot Nothing AndAlso values.Count > 0 Then
                For Each row In values
                    ' Crear una nueva fila en el DataTable
                    Dim newRow = dataTable.NewRow()

                    ' Agregar los valores de la fila de la hoja de cálculo a la fila del DataTable
                    For i = 0 To row.Count - 1
                        If i = 2 Then
                            ' Eliminar el signo de pesos y los separadores de miles del valor de "IMPORTE"
                            Dim importe As String = row(i).ToString()
                            importe = importe.Replace("$", "").Replace(".", "")

                            ' Validar el formato del valor de "IMPORTE"
                            Dim importeValido As Double
                            If Double.TryParse(importe, importeValido) Then
                                newRow(i) = importeValido
                            Else
                                newRow(i) = 0 ' Opcionalmente, puedes asignar un valor predeterminado para los formatos incorrectos
                                ' También puedes mostrar un mensaje de error o realizar alguna acción adicional
                            End If
                        Else
                            newRow(i) = row(i).ToString()
                        End If
                    Next

                    ' Agregar la fila al DataTable
                    dataTable.Rows.Add(newRow)
                Next
            End If

            ' ...


            If dataTable.Rows.Count >= 2 Then
                ' Eliminar las primeras dos filas
                dataTable.Rows.RemoveAt(0)
                dataTable.Rows.RemoveAt(0)
            End If



            Dim embarqueAnterior As String = ""
            ' Recorrer cada fila de la tabla
            For i As Integer = 0 To dataTable.Rows.Count - 1
                Dim embarqueActual As String = dataTable.Rows(i)("EMBARQUE").ToString()

                ' Completar el campo de embarque vacío con el valor de la fila anterior
                If String.IsNullOrEmpty(embarqueActual) Then
                    dataTable.Rows(i)("EMBARQUE") = embarqueAnterior
                Else
                    embarqueAnterior = embarqueActual
                End If
            Next i


            Dim fechaAnterior As String = ""
            ' Recorrer cada fila de la tabla
            For i As Integer = 0 To dataTable.Rows.Count - 1
                Dim fechaActual As String = dataTable.Rows(i)("FECHA").ToString()
                ' Completar el campo de fecha vacío con el valor de la fila anterior
                If String.IsNullOrEmpty(fechaActual) Then
                    dataTable.Rows(i)("FECHA") = fechaAnterior
                Else
                    fechaAnterior = fechaActual
                End If
            Next i


            DataGridView1.DataSource = dataTable

        Else

            ' No hay suficientes hojas de cálculo en el archivo
            MessageBox.Show("El archivo no contiene al menos tres hojas de cálculo.")
        End If
    End Sub
    Private Sub LeerDoscPoUbicacionArchivoEmbarques()

        ' Autenticarse con las credenciales descargadas
        Dim credential As UserCredential
        Using stream As New FileStream("C:\Users\Cristian\Source\Repos\SaldosMaria\SaldosMaria\credencial.json", FileMode.Open, FileAccess.Read)
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            GoogleClientSecrets.Load(stream).Secrets,
            {"https://www.googleapis.com/auth/spreadsheets.readonly"},
            "cabregu@alephargentina.com.ar",
            CancellationToken.None
        ).Result
        End Using

        ' Crear el servicio de Google Sheets
        Dim service = New SheetsService(New BaseClientService.Initializer() With {
        .HttpClientInitializer = credential,
        .ApplicationName = "EmbarquesMaria"
    })

        ' ID de la hoja de cálculo y rango de la hoja a leer
        Dim spreadsheetId = "18SnWT5YaalHTlKU-sGZopvRGXiLCYJUyD4IQX9i9Bm4"
        Dim spreadsheet = service.Spreadsheets.Get(spreadsheetId).Execute()
        Dim sheets = spreadsheet.Sheets
        Dim sheetIndex = 0

        Dim range = sheets(sheetIndex).Properties.Title & "!A1:J"
        Dim request = service.Spreadsheets.Values.Get(spreadsheetId, range)
        Dim response = request.Execute()
        Dim values = response.Values

        ' Crear DataTable y agregar columnas
        Dim dataTable As New DataTable()

        If values IsNot Nothing AndAlso values.Count > 0 Then
            Dim headerRow = values(0)

            For Each header In headerRow
                dataTable.Columns.Add(header.ToString())
            Next
        End If

        ' Obtener los últimos 200 registros
        Dim startIndex As Integer = Math.Max(values.Count - 200, 1)

        ' Recorrer las filas restantes y agregar los datos al DataTable
        For rowIndex As Integer = startIndex To values.Count - 1
            Dim rowValues = values(rowIndex)

            ' Verificar si los campos "emb" y "nrodespacho" están vacíos
            Dim embValue = If(rowValues.Count > 0, rowValues(0).ToString(), "")
            Dim nrodespachoValue = If(rowValues.Count > 1, rowValues(1).ToString(), "")

            If Not String.IsNullOrEmpty(embValue) OrElse Not String.IsNullOrEmpty(nrodespachoValue) Then
                Dim dataRow = dataTable.NewRow()

                For columnIndex As Integer = 0 To rowValues.Count - 1
                    dataRow(columnIndex) = rowValues(columnIndex)
                Next

                dataTable.Rows.Add(dataRow)
            End If
        Next

        ' Eliminar las columnas de los campos "DepLec", "Depositos", "PagosLe" y "saldos"
        dataTable.Columns.Remove("DepLec")
        dataTable.Columns.Remove("Depositos")
        dataTable.Columns.Remove("PagosLe")
        dataTable.Columns.Remove("saldo")

        DataGridView2.DataSource = dataTable


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


End Class
