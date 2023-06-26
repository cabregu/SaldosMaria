<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSaldosMaria
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.DgvSaldos = New System.Windows.Forms.DataGridView()
        Me.TxtTotalMaria = New System.Windows.Forms.TextBox()
        Me.BtnDeposito = New System.Windows.Forms.Button()
        Me.DgvVeps = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DgvEmbGoogle = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DgvMariaCatalent = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgvdespachos = New System.Windows.Forms.DataGridView()
        CType(Me.DgvSaldos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvVeps, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvEmbGoogle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvMariaCatalent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvdespachos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DgvSaldos
        '
        Me.DgvSaldos.AllowUserToAddRows = False
        Me.DgvSaldos.AllowUserToDeleteRows = False
        Me.DgvSaldos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvSaldos.Location = New System.Drawing.Point(878, 22)
        Me.DgvSaldos.Name = "DgvSaldos"
        Me.DgvSaldos.ReadOnly = True
        Me.DgvSaldos.Size = New System.Drawing.Size(470, 268)
        Me.DgvSaldos.TabIndex = 0
        '
        'TxtTotalMaria
        '
        Me.TxtTotalMaria.Location = New System.Drawing.Point(1248, 298)
        Me.TxtTotalMaria.Name = "TxtTotalMaria"
        Me.TxtTotalMaria.Size = New System.Drawing.Size(100, 20)
        Me.TxtTotalMaria.TabIndex = 2
        '
        'BtnDeposito
        '
        Me.BtnDeposito.Location = New System.Drawing.Point(878, 296)
        Me.BtnDeposito.Name = "BtnDeposito"
        Me.BtnDeposito.Size = New System.Drawing.Size(130, 23)
        Me.BtnDeposito.TabIndex = 3
        Me.BtnDeposito.Text = "Agregar Deposito"
        Me.BtnDeposito.UseVisualStyleBackColor = True
        '
        'DgvVeps
        '
        Me.DgvVeps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvVeps.Location = New System.Drawing.Point(12, 229)
        Me.DgvVeps.Name = "DgvVeps"
        Me.DgvVeps.Size = New System.Drawing.Size(654, 188)
        Me.DgvVeps.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1283, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Saldos"
        '
        'DgvEmbGoogle
        '
        Me.DgvEmbGoogle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvEmbGoogle.Location = New System.Drawing.Point(12, 22)
        Me.DgvEmbGoogle.Name = "DgvEmbGoogle"
        Me.DgvEmbGoogle.Size = New System.Drawing.Size(657, 188)
        Me.DgvEmbGoogle.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 213)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Veps"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "embgoogle"
        '
        'DgvMariaCatalent
        '
        Me.DgvMariaCatalent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvMariaCatalent.Location = New System.Drawing.Point(12, 436)
        Me.DgvMariaCatalent.Name = "DgvMariaCatalent"
        Me.DgvMariaCatalent.Size = New System.Drawing.Size(657, 259)
        Me.DgvMariaCatalent.TabIndex = 30
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 420)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 13)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Planilla saldos maria"
        '
        'dgvdespachos
        '
        Me.dgvdespachos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvdespachos.Location = New System.Drawing.Point(691, 436)
        Me.dgvdespachos.Name = "dgvdespachos"
        Me.dgvdespachos.Size = New System.Drawing.Size(657, 259)
        Me.dgvdespachos.TabIndex = 32
        '
        'FrmSaldosMaria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 707)
        Me.Controls.Add(Me.dgvdespachos)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DgvMariaCatalent)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DgvEmbGoogle)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DgvVeps)
        Me.Controls.Add(Me.BtnDeposito)
        Me.Controls.Add(Me.TxtTotalMaria)
        Me.Controls.Add(Me.DgvSaldos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmSaldosMaria"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SALDOS MARIA"
        CType(Me.DgvSaldos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvVeps, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvEmbGoogle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvMariaCatalent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvdespachos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DgvSaldos As System.Windows.Forms.DataGridView
    Friend WithEvents TxtTotalMaria As System.Windows.Forms.TextBox
    Friend WithEvents BtnDeposito As System.Windows.Forms.Button
    Friend WithEvents DgvVeps As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents DgvEmbGoogle As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DgvMariaCatalent As DataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents dgvdespachos As DataGridView
End Class
