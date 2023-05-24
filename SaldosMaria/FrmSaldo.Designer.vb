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
        Me.DgvVeps = New System.Windows.Forms.DataGridView()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.DgvPlanillaxlsEmb = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DgvEmbGoogle = New System.Windows.Forms.DataGridView()
        CType(Me.DgvSaldos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvVeps, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvPlanillaxlsEmb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvEmbGoogle, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.DgvSaldos.Size = New System.Drawing.Size(470, 268)
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
        Me.TxtTotalMaria.Location = New System.Drawing.Point(963, 330)
        Me.TxtTotalMaria.Name = "TxtTotalMaria"
        Me.TxtTotalMaria.Size = New System.Drawing.Size(100, 20)
        Me.TxtTotalMaria.TabIndex = 2
        '
        'BtnDeposito
        '
        Me.BtnDeposito.Location = New System.Drawing.Point(593, 328)
        Me.BtnDeposito.Name = "BtnDeposito"
        Me.BtnDeposito.Size = New System.Drawing.Size(130, 23)
        Me.BtnDeposito.TabIndex = 3
        Me.BtnDeposito.Text = "Agregar Deposito"
        Me.BtnDeposito.UseVisualStyleBackColor = True
        '
        'DgvVeps
        '
        Me.DgvVeps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvVeps.Location = New System.Drawing.Point(2, 54)
        Me.DgvVeps.Name = "DgvVeps"
        Me.DgvVeps.Size = New System.Drawing.Size(585, 268)
        Me.DgvVeps.TabIndex = 17
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(988, 658)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 22
        Me.Button3.Text = "XLS"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'DgvPlanillaxlsEmb
        '
        Me.DgvPlanillaxlsEmb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvPlanillaxlsEmb.Location = New System.Drawing.Point(270, 436)
        Me.DgvPlanillaxlsEmb.Name = "DgvPlanillaxlsEmb"
        Me.DgvPlanillaxlsEmb.Size = New System.Drawing.Size(265, 259)
        Me.DgvPlanillaxlsEmb.TabIndex = 23
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "planilla veps google"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(998, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Saldos"
        '
        'DgvEmbGoogle
        '
        Me.DgvEmbGoogle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvEmbGoogle.Location = New System.Drawing.Point(12, 436)
        Me.DgvEmbGoogle.Name = "DgvEmbGoogle"
        Me.DgvEmbGoogle.Size = New System.Drawing.Size(252, 259)
        Me.DgvEmbGoogle.TabIndex = 27
        '
        'FrmSaldosMaria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1360, 749)
        Me.Controls.Add(Me.DgvEmbGoogle)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DgvPlanillaxlsEmb)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.DgvVeps)
        Me.Controls.Add(Me.BtnDeposito)
        Me.Controls.Add(Me.TxtTotalMaria)
        Me.Controls.Add(Me.DgvSaldos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "FrmSaldosMaria"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SALDOS MARIA"
        CType(Me.DgvSaldos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvVeps, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvPlanillaxlsEmb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvEmbGoogle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DgvSaldos As System.Windows.Forms.DataGridView
    Friend WithEvents TxtTotalMaria As System.Windows.Forms.TextBox
    Friend WithEvents BtnDeposito As System.Windows.Forms.Button
    Friend WithEvents Fecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nrodeposito As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Importe As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DgvVeps As DataGridView
    Friend WithEvents Button3 As Button
    Friend WithEvents DgvPlanillaxlsEmb As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents DgvEmbGoogle As DataGridView
End Class
