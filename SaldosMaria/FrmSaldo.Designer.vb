<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSaldosMaria
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DgvSaldos = New System.Windows.Forms.DataGridView()
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nrodeposito = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Importe = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TxtTotalMaria = New System.Windows.Forms.TextBox()
        Me.BtnDeposito = New System.Windows.Forms.Button()
        Me.BtnInforme = New System.Windows.Forms.Button()
        Me.DgvVep = New System.Windows.Forms.DataGridView()
        Me.Referencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Despacho = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaOf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NroVep = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImporteAfectado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnSeleccionar = New System.Windows.Forms.Button()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.DgvExcel = New System.Windows.Forms.DataGridView()
        Me.BtnActualizar = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.DgvSaldos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvVep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DgvSaldos
        '
        Me.DgvSaldos.AllowUserToAddRows = False
        Me.DgvSaldos.AllowUserToDeleteRows = False
        Me.DgvSaldos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvSaldos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Fecha, Me.Nrodeposito, Me.Importe, Me.Saldo, Me.Id})
        Me.DgvSaldos.Location = New System.Drawing.Point(593, 54)
        Me.DgvSaldos.Name = "DgvSaldos"
        Me.DgvSaldos.ReadOnly = True
        Me.DgvSaldos.Size = New System.Drawing.Size(470, 563)
        Me.DgvSaldos.TabIndex = 0
        '
        'Fecha
        '
        Me.Fecha.HeaderText = "Fecha"
        Me.Fecha.Name = "Fecha"
        Me.Fecha.ReadOnly = True
        '
        'Nrodeposito
        '
        Me.Nrodeposito.HeaderText = "NroDeposito"
        Me.Nrodeposito.Name = "Nrodeposito"
        Me.Nrodeposito.ReadOnly = True
        '
        'Importe
        '
        Me.Importe.HeaderText = "Importe"
        Me.Importe.Name = "Importe"
        Me.Importe.ReadOnly = True
        '
        'Saldo
        '
        Me.Saldo.HeaderText = "Saldo"
        Me.Saldo.Name = "Saldo"
        Me.Saldo.ReadOnly = True
        '
        'Id
        '
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Visible = False
        '
        'TxtTotalMaria
        '
        Me.TxtTotalMaria.Location = New System.Drawing.Point(968, 625)
        Me.TxtTotalMaria.Name = "TxtTotalMaria"
        Me.TxtTotalMaria.Size = New System.Drawing.Size(100, 20)
        Me.TxtTotalMaria.TabIndex = 2
        '
        'BtnDeposito
        '
        Me.BtnDeposito.Location = New System.Drawing.Point(718, 623)
        Me.BtnDeposito.Name = "BtnDeposito"
        Me.BtnDeposito.Size = New System.Drawing.Size(130, 23)
        Me.BtnDeposito.TabIndex = 3
        Me.BtnDeposito.Text = "Agregar Deposito"
        Me.BtnDeposito.UseVisualStyleBackColor = True
        '
        'BtnInforme
        '
        Me.BtnInforme.Location = New System.Drawing.Point(12, 622)
        Me.BtnInforme.Name = "BtnInforme"
        Me.BtnInforme.Size = New System.Drawing.Size(75, 23)
        Me.BtnInforme.TabIndex = 4
        Me.BtnInforme.Text = "Informe"
        Me.BtnInforme.UseVisualStyleBackColor = True
        '
        'DgvVep
        '
        Me.DgvVep.AllowUserToAddRows = False
        Me.DgvVep.AllowUserToDeleteRows = False
        Me.DgvVep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvVep.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Referencia, Me.Despacho, Me.FechaOf, Me.NroVep, Me.ImporteAfectado})
        Me.DgvVep.Location = New System.Drawing.Point(12, 54)
        Me.DgvVep.Name = "DgvVep"
        Me.DgvVep.Size = New System.Drawing.Size(552, 562)
        Me.DgvVep.TabIndex = 5
        '
        'Referencia
        '
        Me.Referencia.HeaderText = "Referencia"
        Me.Referencia.Name = "Referencia"
        '
        'Despacho
        '
        Me.Despacho.HeaderText = "Despacho"
        Me.Despacho.Name = "Despacho"
        '
        'FechaOf
        '
        Me.FechaOf.HeaderText = "FechaOf"
        Me.FechaOf.Name = "FechaOf"
        '
        'NroVep
        '
        Me.NroVep.HeaderText = "NroVep"
        Me.NroVep.Name = "NroVep"
        '
        'ImporteAfectado
        '
        Me.ImporteAfectado.HeaderText = "ImporteAfectado"
        Me.ImporteAfectado.Name = "ImporteAfectado"
        '
        'BtnSeleccionar
        '
        Me.BtnSeleccionar.Location = New System.Drawing.Point(327, 15)
        Me.BtnSeleccionar.Name = "BtnSeleccionar"
        Me.BtnSeleccionar.Size = New System.Drawing.Size(36, 20)
        Me.BtnSeleccionar.TabIndex = 7
        Me.BtnSeleccionar.Text = "..."
        Me.BtnSeleccionar.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(12, 14)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(309, 20)
        Me.txtPath.TabIndex = 8
        '
        'BtnExcel
        '
        Me.BtnExcel.Location = New System.Drawing.Point(215, 625)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(195, 23)
        Me.BtnExcel.TabIndex = 12
        Me.BtnExcel.Text = "BAJAR EXCEL CE DESPACHOS"
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'DgvExcel
        '
        Me.DgvExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvExcel.Location = New System.Drawing.Point(1031, 54)
        Me.DgvExcel.Name = "DgvExcel"
        Me.DgvExcel.Size = New System.Drawing.Size(32, 150)
        Me.DgvExcel.TabIndex = 13
        Me.DgvExcel.Visible = False
        '
        'BtnActualizar
        '
        Me.BtnActualizar.Location = New System.Drawing.Point(494, 622)
        Me.BtnActualizar.Name = "BtnActualizar"
        Me.BtnActualizar.Size = New System.Drawing.Size(75, 23)
        Me.BtnActualizar.TabIndex = 14
        Me.BtnActualizar.Text = "Actualizar"
        Me.BtnActualizar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(444, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 23)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Cargar desde Planilla"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(593, 11)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "Imagen"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FrmSaldosMaria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1075, 660)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BtnActualizar)
        Me.Controls.Add(Me.DgvExcel)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.BtnSeleccionar)
        Me.Controls.Add(Me.DgvVep)
        Me.Controls.Add(Me.BtnInforme)
        Me.Controls.Add(Me.BtnDeposito)
        Me.Controls.Add(Me.TxtTotalMaria)
        Me.Controls.Add(Me.DgvSaldos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "FrmSaldosMaria"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SALDOS MARIA"
        CType(Me.DgvSaldos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvVep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvExcel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DgvSaldos As System.Windows.Forms.DataGridView
    Friend WithEvents TxtTotalMaria As System.Windows.Forms.TextBox
    Friend WithEvents BtnDeposito As System.Windows.Forms.Button
    Friend WithEvents BtnInforme As System.Windows.Forms.Button
    Friend WithEvents DgvVep As System.Windows.Forms.DataGridView
    Friend WithEvents Referencia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Despacho As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaOf As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NroVep As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImporteAfectado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnSeleccionar As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents Fecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nrodeposito As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Importe As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents DgvExcel As System.Windows.Forms.DataGridView
    Friend WithEvents BtnActualizar As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As Button
End Class
