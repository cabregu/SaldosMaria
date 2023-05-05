<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInforme
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
        Me.DgbInforme = New System.Windows.Forms.DataGridView()
        Me.BtnXls = New System.Windows.Forms.Button()
        Me.Referencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Datos = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Vep = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Importe = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImporteDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgbInforme, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DgbInforme
        '
        Me.DgbInforme.AllowUserToAddRows = False
        Me.DgbInforme.AllowUserToDeleteRows = False
        Me.DgbInforme.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgbInforme.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Referencia, Me.Fecha, Me.Datos, Me.Vep, Me.Importe, Me.ImporteDesc, Me.Saldo})
        Me.DgbInforme.Location = New System.Drawing.Point(12, 12)
        Me.DgbInforme.Name = "DgbInforme"
        Me.DgbInforme.ReadOnly = True
        Me.DgbInforme.Size = New System.Drawing.Size(758, 296)
        Me.DgbInforme.TabIndex = 0
        '
        'BtnXls
        '
        Me.BtnXls.Location = New System.Drawing.Point(12, 314)
        Me.BtnXls.Name = "BtnXls"
        Me.BtnXls.Size = New System.Drawing.Size(75, 23)
        Me.BtnXls.TabIndex = 1
        Me.BtnXls.Text = "Xls"
        Me.BtnXls.UseVisualStyleBackColor = True
        '
        'Referencia
        '
        Me.Referencia.HeaderText = "Referencia"
        Me.Referencia.Name = "Referencia"
        Me.Referencia.ReadOnly = True
        '
        'Fecha
        '
        Me.Fecha.HeaderText = "Fecha"
        Me.Fecha.Name = "Fecha"
        Me.Fecha.ReadOnly = True
        '
        'Datos
        '
        Me.Datos.HeaderText = "Datos"
        Me.Datos.Name = "Datos"
        Me.Datos.ReadOnly = True
        '
        'Vep
        '
        Me.Vep.HeaderText = "Vep"
        Me.Vep.Name = "Vep"
        Me.Vep.ReadOnly = True
        '
        'Importe
        '
        Me.Importe.HeaderText = "Importe"
        Me.Importe.Name = "Importe"
        Me.Importe.ReadOnly = True
        '
        'ImporteDesc
        '
        Me.ImporteDesc.HeaderText = "ImporteDesc"
        Me.ImporteDesc.Name = "ImporteDesc"
        Me.ImporteDesc.ReadOnly = True
        '
        'Saldo
        '
        Me.Saldo.HeaderText = "Saldo"
        Me.Saldo.Name = "Saldo"
        Me.Saldo.ReadOnly = True
        '
        'FrmInforme
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(808, 361)
        Me.Controls.Add(Me.BtnXls)
        Me.Controls.Add(Me.DgbInforme)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmInforme"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmInforme"
        CType(Me.DgbInforme, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DgbInforme As System.Windows.Forms.DataGridView
    Friend WithEvents BtnXls As System.Windows.Forms.Button
    Friend WithEvents Referencia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Datos As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Vep As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Importe As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImporteDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
