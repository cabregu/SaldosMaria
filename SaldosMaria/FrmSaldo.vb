﻿Imports System.Data.OleDb
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
        LeerDoscPoUbicacionArchivoVeps()
        LeerDoscPoUbicacionArchivoEmbarques()
        CargarArchivoExcel()
        Obtenerdatosparagrid()

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
            DataTable.Columns.Add("EMBARQUE", GetType(String))
            DataTable.Columns.Add("IMPORTE", GetType(Double))
            DataTable.Columns.Add("N°VEP", GetType(String))
            DataTable.Columns.Add("USUARIO", GetType(String))

            ' ...

            ' Agregar filas al DataTable
            If values IsNot Nothing AndAlso values.Count > 0 Then
                For Each row In values
                    ' Crear una nueva fila en el DataTable
                    Dim newRow = DataTable.NewRow()

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




            InsertarRegistrosEnSQLite(filteredDataTable)



        Else

            ' No hay suficientes hojas de cálculo en el archivo
            MessageBox.Show("El archivo no contiene al menos tres hojas de cálculo.")
        End If
    End Sub
    Private Sub LeerDoscPoUbicacionArchivoEmbarques()
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


            ImportarASQLiteEmbarquesgoogle(filteredDataTable)


        Else
            ' No hay suficientes hojas de cálculo en el archivo
            MessageBox.Show("El archivo no contiene al menos tres hojas de cálculo.")

        End If
    End Sub

    Public Sub CargarArchivoExcel()
        Dim archivoExcel As String = "\\aleph-server-pc\SERVER NUEVO\Pc52\MARIA CATALENT.xls"
        Dim cadenaConexion As String = "Provider=Microsoft.ACE.OleDb.12.0;Data Source=" & archivoExcel & ";Extended Properties='Excel 8.0;HDR=YES;'"
        Dim añoActual As Integer = DateTime.Now.Year
        Dim consultaSQL As String = "SELECT emb, Fecha, pagos FROM [CT$] WHERE emb IS NOT NULL AND YEAR(Fecha) = " & añoActual

        If Not VerificarRutaArchivo(archivoExcel) Then
            archivoExcel = SeleccionarArchivoExcel()
            If archivoExcel Is Nothing Then
                Return
            End If
        End If

        cadenaConexion = "Provider=Microsoft.ACE.OleDb.12.0;Data Source=" & archivoExcel & ";Extended Properties='Excel 8.0;HDR=YES;'"

        Using conexion As New OleDbConnection(cadenaConexion)
            Try
                conexion.Open()
                Using adaptador As New OleDbDataAdapter(consultaSQL, conexion)
                    Dim dt As New DataTable()
                    adaptador.Fill(dt)

                    ActualizarBaseDatos(dt)
                End Using
            Catch ex As Exception
                ' Manejar cualquier excepción
                MessageBox.Show("Error al cargar el archivo de Excel: " & ex.Message)
            Finally
                ' Cerrar la conexión
                conexion.Close()
            End Try
        End Using
    End Sub

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


    '********  Insertar Veps  ********

    Private Sub InsertarRegistrosEnSQLite(dataTable As DataTable)
        ' Ruta y nombre de la base de datos SQLite
        Dim dbPath As String = "C:\Users\Cristian\Source\Repos\SaldosMaria\SaldosMaria\SaldosMaria.db"

        ' Cadena de conexión a la base de datos SQLite
        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        ' Crear la conexión a la base de datos SQLite
        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            ' Verificar y insertar los registros
            For Each row As DataRow In dataTable.Rows
                If Not ExisteRegistroSQLite(connection, row) Then
                    InsertarRegistroSQLite(connection, row)
                End If
            Next
        End Using
    End Sub
    Private Function ExisteRegistroSQLite(connection As SQLiteConnection, row As DataRow) As Boolean
        ' Sentencia SQL para buscar un registro en SQLite
        Dim selectQuery As String = "SELECT COUNT(*) FROM Veps WHERE FECHA = @fecha AND EMBARQUE = @embarque AND IMPORTE = @importe AND NVEP = @nvep AND USUARIO = @usuario;"

        ' Crear el comando SQL con los parámetros
        Using command As New SQLiteCommand(selectQuery, connection)
            command.Parameters.AddWithValue("@fecha", row("FECHA").ToString())
            command.Parameters.AddWithValue("@embarque", row("EMBARQUE").ToString())
            command.Parameters.AddWithValue("@importe", row("IMPORTE").ToString().Replace(",", "."))
            command.Parameters.AddWithValue("@nvep", row("N°VEP").ToString())
            command.Parameters.AddWithValue("@usuario", row("USUARIO").ToString())

            ' Ejecutar la consulta y obtener el resultado
            Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())

            ' Si el resultado es mayor a 0, significa que el registro ya existe
            Return result > 0
        End Using
    End Function
    Private Sub InsertarRegistroSQLite(connection As SQLiteConnection, row As DataRow)
        ' Sentencia SQL para insertar un registro en SQLite
        Dim insertQuery As String = "INSERT INTO Veps (FECHA, EMBARQUE, IMPORTE, NVEP, USUARIO) VALUES (@fecha, @embarque, @importe, @nvep, @usuario);"

        ' Crear el comando SQL con los parámetros
        Using command As New SQLiteCommand(insertQuery, connection)
            command.Parameters.AddWithValue("@fecha", row("FECHA").ToString())
            command.Parameters.AddWithValue("@embarque", row("EMBARQUE").ToString())
            command.Parameters.AddWithValue("@importe", row("IMPORTE").ToString().Replace(",", "."))
            command.Parameters.AddWithValue("@nvep", row("N°VEP").ToString())
            command.Parameters.AddWithValue("@usuario", row("USUARIO").ToString())

            ' Ejecutar el comando SQL para insertar el registro
            command.ExecuteNonQuery()
        End Using
    End Sub

    '************      Insertar planilla Google   *************


    Public Sub ImportarASQLiteEmbarquesgoogle(dataTable As DataTable)
        ' Ruta de la base de datos SQLite
        Dim rutaBaseDatos As String = "C:\Users\Cristian\Source\Repos\SaldosMaria\SaldosMaria\SaldosMaria.db"

        ' Cadena de conexión a la base de datos SQLite
        Dim connectionString As String = "Data Source=" & rutaBaseDatos & ";Version=3;"

        ' Conexión a la base de datos SQLite
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()

            ' Insertar registros en la base de datos
            For Each row As DataRow In dataTable.Rows
                Dim emb As String = row("Embarque").ToString()
                Dim despacho As String = row("N° De Despacho").ToString()

                ' Verificar si el registro ya existe en la base de datos
                Using cmd As New SQLiteCommand(conn)
                    cmd.CommandText = "SELECT COUNT(*) FROM embarquesgoogle WHERE Embarque = @Embarque AND NroDespacho = @Despacho"
                    cmd.Parameters.AddWithValue("@Embarque", emb)
                    cmd.Parameters.AddWithValue("@Despacho", despacho)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If count = 0 Then
                        ' Insertar el registro si no existe
                        cmd.CommandText = "INSERT INTO embarquesgoogle (Embarque, NroDespacho) VALUES (@Embarque, @Despacho)"
                        cmd.ExecuteNonQuery()
                    End If
                End Using
            Next

            ' Cerrar la conexión
            conn.Close()
        End Using
    End Sub
    Public Sub ActualizarBaseDatos(ByVal dt As DataTable)
        Dim cadenaConexion As String = "Data Source=C:\Users\Cristian\Source\Repos\SaldosMaria\SaldosMaria\SaldosMaria.db;Version=3;"
        Dim añoActual As Integer = DateTime.Now.Year

        Using conexion As New SQLiteConnection(cadenaConexion)
            Try
                conexion.Open()

                For Each fila As DataRow In dt.Rows
                    Dim emb As String = fila("emb").ToString()
                    Dim fecha As DateTime = Convert.ToDateTime(fila("Fecha")).Date ' Obtener solo la fecha sin la parte de la hora
                    Dim pagos As Decimal = Convert.ToDecimal(fila("pagos"))

                    ' Verificar si el valor de "emb" ya existe en la base de datos
                    Dim existe As Boolean = False
                    Dim verificarSQL As String = "SELECT emb FROM mariacatalent WHERE emb = @emb"
                    Using verificarCmd As New SQLiteCommand(verificarSQL, conexion)
                        verificarCmd.Parameters.AddWithValue("@emb", emb)
                        Dim resultado As Object = verificarCmd.ExecuteScalar()
                        If resultado IsNot Nothing Then
                            existe = True
                        End If
                    End Using

                    If Not existe Then
                        ' Insertar un nuevo registro
                        Dim insertarSQL As String = "INSERT INTO mariacatalent (emb, Fecha, pagos) VALUES (@emb, @fecha, @pagos)"
                        Using insertarCmd As New SQLiteCommand(insertarSQL, conexion)
                            insertarCmd.Parameters.AddWithValue("@emb", emb)
                            insertarCmd.Parameters.AddWithValue("@fecha", fecha.ToString("yyyy-MM-dd")) ' Formatear la fecha como "yyyy-MM-dd"
                            insertarCmd.Parameters.AddWithValue("@pagos", Decimal.Round(pagos, 2))
                            insertarCmd.ExecuteNonQuery()
                        End Using
                    End If
                Next fila
            Catch ex As Exception
                ' Manejar cualquier excepción
                MessageBox.Show("Error al actualizar la base de datos: " & ex.Message)
            Finally
                ' Cerrar la conexión
                conexion.Close()
            End Try
        End Using
    End Sub

    Private Function Obtenerdatosparagrid()


        DgvEmbGoogle.DataSource = ObtenerDataTableEmbarquesgoogle()
        DgvBase.DataSource = ObtenerBase()
        dgvdespachos.DataSource = ObtenerDespachos()
        DgvVeps.DataSource = ObtenerVeps()

    End Function
    Public Function ObtenerDataTableEmbarquesgoogle() As DataTable
        Dim dataTable As New DataTable()

        ' Ruta de la base de datos SQLite
        Dim rutaBaseDatos As String = "C:\Users\Cristian\Source\Repos\SaldosMaria\SaldosMaria\SaldosMaria.db"

        ' Cadena de conexión a la base de datos SQLite
        Dim connectionString As String = "Data Source=" & rutaBaseDatos & ";Version=3;"

        ' Conexión a la base de datos SQLite
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()

            Dim selectQuery As String = "SELECT embarquesgoogle.*,mariacatalent.fecha ,mariacatalent.pagos AS PagosTotal, veps.importe AS ImporteVeps, (mariacatalent.pagos - veps.importe) AS diferencia " &
                                    "FROM (embarquesgoogle " &
                                    "INNER JOIN mariacatalent ON embarquesgoogle.embarque = mariacatalent.emb) " &
                                    "INNER JOIN veps ON embarquesgoogle.embarque = veps.embarque " &
                                    "WHERE mariacatalent.fecha >= '2023-01-01' AND embarquesgoogle.NroDespacho NOT IN (SELECT Numerodespacho FROM DespachosOficializados) " &
                                    "GROUP BY embarquesgoogle.embarque"





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

    Public Function ObtenerBase() As DataTable
        Dim dataTable As New DataTable()

        ' Ruta de la base de datos SQLite
        Dim rutaBaseDatos As String = "C:\Users\Cristian\Source\Repos\SaldosMaria\SaldosMaria\SaldosMaria.db"

        ' Cadena de conexión a la base de datos SQLite
        Dim connectionString As String = "Data Source=" & rutaBaseDatos & ";Version=3;"

        ' Conexión a la base de datos SQLite
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()

            ' Consulta para obtener los registros de la tabla "base" con fechas posteriores a "2023-02-01"
            ' y un campo adicional "diferencia", que es la resta del importe de "base" y la suma de importes de "veps" para el mismo "nvep"
            ' También se obtiene la sumatoria de importes de "veps" para el mismo "nvep"
            Dim selectQuery As String = "SELECT b.*, (b.importe - COALESCE(v.total_importes, 0)) AS diferencia, COALESCE(v.total_importes, 0) AS sumatoria " &
                                    "FROM base AS b " &
                                    "LEFT JOIN (SELECT nvep, SUM(importe) AS total_importes FROM veps GROUP BY nvep) AS v ON b.nvep = v.nvep " &
                                    "WHERE b.fecha > '2023-01-01'"

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

    Public Function ObtenerDespachos() As DataTable
        Dim dataTable As New DataTable()

        ' Ruta de la base de datos SQLite
        Dim rutaBaseDatos As String = "C:\Users\Cristian\Source\Repos\SaldosMaria\SaldosMaria\SaldosMaria.db"

        ' Cadena de conexión a la base de datos SQLite
        Dim connectionString As String = "Data Source=" & rutaBaseDatos & ";Version=3;"

        ' Conexión a la base de datos SQLite
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()

            ' Consulta para obtener los registros de la tabla "base" con un campo adicional "sumatoria" y "diferencia"
            ' que involucra los datos de la tabla "DespachosOficializados", con filtro de fecha desde "2023-01-01"
            Dim selectQuery As String = "SELECT base.*, " &
                                        "SUM(DespachosOficializados.Descontado) AS sumatoria, " &
                                        "(base.importe - SUM(DespachosOficializados.Descontado)) AS diferencia " &
                                        "FROM base " &
                                        "LEFT JOIN DespachosOficializados ON base.nvep = DespachosOficializados.DepositoUsado " &
                                        "WHERE base.fecha >= '2023-01-01' " &
                                        "GROUP BY base.nvep"

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


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        For Each filago As DataGridViewRow In DgvEmbGoogle.Rows
            If filago.Cells("diferencia").Value = "0" Then
                Dim Embarquego As String = filago.Cells("Embarque").Value

                For Each filavep As DataGridViewRow In DgvVeps.Rows
                    If filavep.Cells("embarque").Value = Embarquego Then
                        Dim NroVep As String = filavep.Cells("NVEP").Value
                        Dim ImporteVep As Decimal = filavep.Cells("IMPORTE").Value



                        For Each filabase As DataGridViewRow In DgvBase.Rows

                            If filabase.Cells("NVEP").Value = NroVep Then
                                Dim ImporteSaldo As Decimal = filabase.Cells("Saldo").Value

                                MsgBox("SaldoMaria " & ImporteSaldo & "importevep " & ImporteVep)

                                If ImporteSaldo <= ImporteVep Then
                                    filavep.DefaultCellStyle.BackColor = Color.Yellow
                                Else
                                    filavep.DefaultCellStyle.BackColor = Color.Green
                                End If
                            End If

                        Next
                    End If
                Next
            End If
        Next


    End Sub


End Class
