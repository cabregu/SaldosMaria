<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAgregarDeposito
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.DgvDepositos = New System.Windows.Forms.DataGridView
        Me.FechaDeposito = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NroDeposito = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ImporteDeposito = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BtnCargar = New System.Windows.Forms.Button
        Me.BtnImportar = New System.Windows.Forms.Button
        CType(Me.DgvDepositos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DgvDepositos
        '
        Me.DgvDepositos.AllowUserToAddRows = False
        Me.DgvDepositos.AllowUserToDeleteRows = False
        Me.DgvDepositos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvDepositos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FechaDeposito, Me.NroDeposito, Me.ImporteDeposito})
        Me.DgvDepositos.Location = New System.Drawing.Point(12, 12)
        Me.DgvDepositos.Name = "DgvDepositos"
        Me.DgvDepositos.ReadOnly = True
        Me.DgvDepositos.Size = New System.Drawing.Size(359, 221)
        Me.DgvDepositos.TabIndex = 15
        '
        'FechaDeposito
        '
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.FechaDeposito.DefaultCellStyle = DataGridViewCellStyle1
        Me.FechaDeposito.HeaderText = "FechaDeposito"
        Me.FechaDeposito.Name = "FechaDeposito"
        Me.FechaDeposito.ReadOnly = True
        '
        'NroDeposito
        '
        Me.NroDeposito.HeaderText = "NroDeposito"
        Me.NroDeposito.Name = "NroDeposito"
        Me.NroDeposito.ReadOnly = True
        '
        'ImporteDeposito
        '
        Me.ImporteDeposito.HeaderText = "ImporteDeposito"
        Me.ImporteDeposito.Name = "ImporteDeposito"
        Me.ImporteDeposito.ReadOnly = True
        '
        'BtnCargar
        '
        Me.BtnCargar.Location = New System.Drawing.Point(377, 12)
        Me.BtnCargar.Name = "BtnCargar"
        Me.BtnCargar.Size = New System.Drawing.Size(75, 23)
        Me.BtnCargar.TabIndex = 16
        Me.BtnCargar.Text = "Cargar"
        Me.BtnCargar.UseVisualStyleBackColor = True
        '
        'BtnImportar
        '
        Me.BtnImportar.Location = New System.Drawing.Point(378, 209)
        Me.BtnImportar.Name = "BtnImportar"
        Me.BtnImportar.Size = New System.Drawing.Size(75, 23)
        Me.BtnImportar.TabIndex = 17
        Me.BtnImportar.Text = "Importar"
        Me.BtnImportar.UseVisualStyleBackColor = True
        '
        'FrmAgregarDeposito
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(472, 253)
        Me.Controls.Add(Me.BtnImportar)
        Me.Controls.Add(Me.BtnCargar)
        Me.Controls.Add(Me.DgvDepositos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmAgregarDeposito"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agregar Deposito"
        CType(Me.DgvDepositos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DgvDepositos As System.Windows.Forms.DataGridView
    Friend WithEvents BtnCargar As System.Windows.Forms.Button
    Friend WithEvents FechaDeposito As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NroDeposito As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImporteDeposito As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnImportar As System.Windows.Forms.Button
End Class
