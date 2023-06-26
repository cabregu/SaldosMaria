Imports System.Data.OleDb
Imports System.Data.SQLite
Imports System.Globalization
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

        SetDefaultDateFormat()
        CARGARDEPOSITOS()

        'traer de planilla mochi los veps cargados sin guardar
        LeerVepsDesdeGoogleymostrarendgv()

        'traer de planilla mochi los embarques cargados desde el 10909 y guardarlos en sqlite
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

            Dim dataTable As DataTable = ds.Tables("BASE")

            DgvSaldos.DataSource = dataTable

            cn.Close()

            Dim imported2 As Decimal = 0.00
            For Each row As DataRow In dataTable.Rows
                imported2 = imported2 + Convert.ToDecimal(row("SALDO"))
            Next

            TxtTotalMaria.Text = imported2.ToString()

        Catch ex As Exception
            MessageBox.Show(ex.ToString & " Verifique su conexión a la base de datos.")
        End Try
        Return Nothing
    End Function

    Private Function LeerVepsDesdeGoogleymostrarendgv()
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
            DataTable.Columns.Add("EMBARQUE", GetType(String))
            DataTable.Columns.Add("IMPORTE", GetType(String))
            DataTable.Columns.Add("N°VEP", GetType(String))
            DataTable.Columns.Add("USUARIO", GetType(String))


            If values IsNot Nothing AndAlso values.Count > 0 Then
                For Each row In values
                    Dim newRow = DataTable.NewRow()

                    For i = 0 To row.Count - 1
                        newRow(i) = row(i).ToString()
                    Next

                    DataTable.Rows.Add(newRow)
                Next

            End If

            ' ...


            If DataTable.Rows.Count >= 2 Then
                ' Eliminar las primeras dos filas
                DataTable.Rows.RemoveAt(0)
                DataTable.Rows.RemoveAt(0)
            End If
            Dim embarqueAnterior As String = ""
            ' Recorrer cada fila de la tabla
            For i As Integer = 0 To DataTable.Rows.Count - 1
                Dim embarqueActual As String = DataTable.Rows(i)("EMBARQUE").ToString()

                ' Completar el campo de embarque vacío con el valor de la fila anterior
                If String.IsNullOrEmpty(embarqueActual) Then
                    DataTable.Rows(i)("EMBARQUE") = embarqueAnterior
                Else
                    embarqueAnterior = embarqueActual
                End If
            Next i


            Dim fechaAnterior As String = ""
            ' Recorrer cada fila de la tabla
            For i As Integer = 0 To DataTable.Rows.Count - 1
                Dim fechaActual As String = DataTable.Rows(i)("FECHA").ToString()
                ' Completar el campo de fecha vacío con el valor de la fila anterior
                If String.IsNullOrEmpty(fechaActual) Then
                    DataTable.Rows(i)("FECHA") = fechaAnterior
                Else
                    fechaAnterior = fechaActual
                End If
            Next i


            ' Filtrar los registros del DataTable a partir del índice
            Dim filteredDataTable As DataTable = DataTable.Clone()
            For rowIndex As Integer = 0 To DataTable.Rows.Count - 1
                If DataTable.Rows(rowIndex)("EMBARQUE").ToString().StartsWith("CT-10919") Then
                    For i As Integer = rowIndex To DataTable.Rows.Count - 1
                        Dim rowValues As Object() = DataTable.Rows(i).ItemArray
                        filteredDataTable.Rows.Add(rowValues)
                    Next
                    Exit For
                End If
            Next

            For Each row As DataRow In filteredDataTable.Rows
                Dim fechaString As String = row("FECHA").ToString()
                fechaString = fechaString.Replace("ene", "jan")
                fechaString = fechaString.Replace("abr", "apr")

                Dim fecha As DateTime
                If DateTime.TryParse(fechaString, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, fecha) Then
                    row("FECHA") = fecha.ToString("yyyy-MM-dd")
                End If
            Next



            For Each row As DataRow In filteredDataTable.Rows
                Dim importeString As String = row("IMPORTE").ToString()
                importeString = importeString.Replace(".", "")
                importeString = importeString.Replace("$", "")
                row("IMPORTE") = importeString
            Next


            DgvVeps.DataSource = filteredDataTable


        Else

            ' No hay suficientes hojas de cálculo en el archivo
            MessageBox.Show("El archivo no contiene al menos tres hojas de cálculo.")
        End If
    End Function


    Private Function LeerDoscPoUbicacionArchivoEmbarques()
        Dim dataTable As New DataTable

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
        If sheets.Count >= 1 Then
            Dim sheetIndex = 0
            ' Construir el rango de la tercera hoja utilizando el índice
            Dim range = sheets(sheetIndex).Properties.Title & "!B1:L"

            ' Leer los valores de la hoja de cálculo
            Dim request = service.Spreadsheets.Values.Get(spreadsheetId, range)
            Dim response = request.Execute()
            Dim values = response.Values

            If values IsNot Nothing AndAlso values.Count > 0 Then
                ' Obtener los nombres de la primera fila
                Dim headers As List(Of String) = values(0).Select(Function(cell) cell.ToString()).ToList()

                ' Agregar las columnas al DataTable
                For Each header In headers
                    dataTable.Columns.Add(header)
                Next

                ' Agregar los datos al DataTable
                For rowIndex As Integer = 1 To values.Count - 1
                    Dim rowValues As List(Of String) = values(rowIndex).Select(Function(cell) cell.ToString()).ToList()
                    dataTable.Rows.Add(rowValues.ToArray())
                Next
            End If

            Dim t As Integer = dataTable.Rows.Count - 1
            While t >= 0
                For Each columna As DataColumn In dataTable.Columns
                    If dataTable.Rows(t)(columna.ColumnName).ToString().Contains("EMBARQUE") Or dataTable.Rows(t)(columna.ColumnName).ToString().Contains("PD-") Then
                        dataTable.Rows.RemoveAt(t)
                        Exit For
                    End If
                Next
                t -= 1
            End While



            ' Filtrar los registros del DataTable a partir del índice
            Dim filteredDataTable As DataTable = dataTable.Clone()
            For rowIndex As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(rowIndex)("embarque").ToString().StartsWith("CT-10919") Then
                    For i As Integer = rowIndex To dataTable.Rows.Count - 1
                        Dim rowValues As Object() = dataTable.Rows(i).ItemArray
                        filteredDataTable.Rows.Add(rowValues)
                    Next
                    Exit For
                End If
            Next

            For Each row As DataRow In filteredDataTable.Rows
                Dim fechaString As String = row("OFI").ToString()
                fechaString = fechaString.Replace("ene", "jan")
                fechaString = fechaString.Replace("abr", "apr")

                Dim fecha As DateTime
                If DateTime.TryParse(fechaString, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, fecha) Then
                    row("OFI") = fecha.ToString("yyyy-MM-dd")
                End If
            Next


            Dim filteredView As New DataView(filteredDataTable)
            Dim filteredTable As DataTable = filteredView.ToTable(False, "embarque", "ofi", "n° de despacho")

            Dim DTPlanillaCatalent As New DataTable
            DTPlanillaCatalent = ObtenerArchivoExcelPlanillaMariaCatalent()


            filteredTable.Columns.Add("Pago")
            For Each drwfil As DataRow In filteredTable.Rows
                For Each drwcat In DTPlanillaCatalent.Rows
                    If drwfil("EMBARQUE").ToString = drwcat("emb").ToString Then
                        Dim pagoValue As Double
                        If Double.TryParse(drwcat("pagos").ToString, pagoValue) Then
                            drwfil("Pago") = pagoValue.ToString("F2")
                        End If
                    End If
                Next
            Next

            DgvEmbGoogle.DataSource = filteredTable


        Else
            ' No hay suficientes hojas de cálculo en el archivo
            MessageBox.Show("El archivo no contiene al menos tres hojas de cálculo.")

        End If
    End Function






    Public Shared Function ObtenerArchivoExcelPlanillaMariaCatalent() As DataTable
        Dim archivoExcel As String = "\\aleph-server-pc\SERVER NUEVO\Pc52\MARIA CATALENT.xls"
        Dim cadenaConexion As String = "Provider=Microsoft.ACE.OleDb.12.0;Data Source=" & archivoExcel & ";Extended Properties='Excel 8.0;HDR=YES;'"
        Dim añoActual As Integer = DateTime.Now.Year
        Dim consultaSQL As String = "SELECT emb, Fecha, pagos, NroDespacho FROM [CT$] WHERE emb IS NOT NULL AND YEAR(Fecha) = " & añoActual


        Using conexion As New OleDbConnection(cadenaConexion)
            Try

                conexion.Open()

                Using comando As New OleDbCommand(consultaSQL, conexion)

                    Using adaptador As New OleDbDataAdapter(comando)
                        Dim dtPlanillaCatalent As New DataTable()
                        adaptador.Fill(dtPlanillaCatalent)


                        Return dtPlanillaCatalent

                    End Using
                End Using
            Catch ex As Exception

                Console.WriteLine("Error al cargar el archivo Excel: " & ex.Message)
            Finally

                conexion.Close()
            End Try
        End Using
    End Function


    Private Function VerificarRutaArchivo(ByVal rutaArchivo As String) As Boolean
        Return File.Exists(rutaArchivo)
    End Function

    Private Function SeleccionarArchivoExcel() As String
        Dim dialogoArchivo As New OpenFileDialog()
        dialogoArchivo.Filter = "Archivos de Excel|*.xls;*.xlsx"
        dialogoArchivo.Title = "Seleccionar archivo de Excel"

        If dialogoArchivo.ShowDialog() = DialogResult.OK Then
            Return dialogoArchivo.FileName
        Else
            Return Nothing
        End If
    End Function


    Public Function ObtenerVeps() As DataTable
        Dim dataTable As New DataTable()

        ' Ruta de la base de datos SQLite
        Dim rutaBaseDatos As String = "C:\Users\Cristian\Source\Repos\SaldosMaria\SaldosMaria\SaldosMaria.db"

        ' Cadena de conexión a la base de datos SQLite
        Dim connectionString As String = "Data Source=" & rutaBaseDatos & ";Version=3;"

        ' Conexión a la base de datos SQLite
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()

            Dim selectQuery As String = "SELECT Veps.fecha, Veps.embarque, embarquesgoogle.NroDespacho, Veps.importe, Veps.nvep " &
                                    "FROM Veps " &
                                    "INNER JOIN embarquesgoogle ON Veps.embarque = embarquesgoogle.embarque " &
                                    "WHERE embarquesgoogle.NroDespacho NOT IN (SELECT Numerodespacho FROM DespachosOficializados)"

            Using cmd As New SQLiteCommand(selectQuery, conn)
                ' Crear un adaptador de datos para ejecutar la consulta y llenar el DataTable
                Using adapter As New SQLiteDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

            ' Cerrar la conexión
            conn.Close()
        End Using

        Return dataTable
    End Function

    Private Sub SetDefaultDateFormat()
        Dim cultureInfo As New CultureInfo("en-US") ' Puedes ajustar el código de idioma según tus necesidades
        cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd"
        cultureInfo.DateTimeFormat.DateSeparator = "-"

        System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo
    End Sub

    Public Function ActualizarSaldo(ByVal numeroVEP As String, ByVal importeDescontar As Decimal) As Boolean
        Try
            Dim cn As New SQLiteConnection("Data Source=C:\Users\Cristian\source\repos\SaldosMaria\SaldosMaria\SaldosMaria.db;Version=3;")
            cn.Open()

            ' Verificar el saldo antes de la actualización
            Dim verificarSaldo As New SQLiteCommand("SELECT SALDO FROM BASE WHERE NVEP = @numeroVEP", cn)
            verificarSaldo.Parameters.AddWithValue("@numeroVEP", numeroVEP)
            Dim saldoActual As Decimal = CDec(verificarSaldo.ExecuteScalar())

            If saldoActual < importeDescontar Then
                cn.Close()
                Return False
            End If

            Dim cm As New SQLiteCommand("UPDATE BASE SET SALDO = SALDO - @importe WHERE NVEP = @numeroVEP", cn)
            cm.Parameters.AddWithValue("@importe", importeDescontar)
            cm.Parameters.AddWithValue("@numeroVEP", numeroVEP)
            cm.ExecuteNonQuery()

            cn.Close()

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.ToString & " Verifique su conexión a la base de datos.")
            Return False
        End Try
    End Function




End Class
