<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAsignarDespacho
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
        Me.TxtSaldo = New System.Windows.Forms.TextBox
        Me.TxtUsado = New System.Windows.Forms.TextBox
        Me.LblSaldo = New System.Windows.Forms.Label
        Me.LblDespacho = New System.Windows.Forms.Label
        Me.Asignar = New System.Windows.Forms.Button
        Me.TxtIddeposito = New System.Windows.Forms.TextBox
        Me.LblUsado = New System.Windows.Forms.Label
        Me.TxtDespacho = New System.Windows.Forms.MaskedTextBox
        Me.DtpFechaOficializacion = New System.Windows.Forms.DateTimePicker
        Me.LblFechaOf = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TxtSaldo
        '
        Me.TxtSaldo.Enabled = False
        Me.TxtSaldo.Location = New System.Drawing.Point(74, 12)
        Me.TxtSaldo.Name = "TxtSaldo"
        Me.TxtSaldo.Size = New System.Drawing.Size(181, 20)
        Me.TxtSaldo.TabIndex = 0
        '
        'TxtUsado
        '
        Me.TxtUsado.Location = New System.Drawing.Point(75, 125)
        Me.TxtUsado.Name = "TxtUsado"
        Me.TxtUsado.Size = New System.Drawing.Size(181, 20)
        Me.TxtUsado.TabIndex = 1
        '
        'LblSaldo
        '
        Me.LblSaldo.AutoSize = True
        Me.LblSaldo.Location = New System.Drawing.Point(12, 15)
        Me.LblSaldo.Name = "LblSaldo"
        Me.LblSaldo.Size = New System.Drawing.Size(34, 13)
        Me.LblSaldo.TabIndex = 2
        Me.LblSaldo.Text = "Saldo"
        '
        'LblDespacho
        '
        Me.LblDespacho.AutoSize = True
        Me.LblDespacho.Location = New System.Drawing.Point(12, 97)
        Me.LblDespacho.Name = "LblDespacho"
        Me.LblDespacho.Size = New System.Drawing.Size(56, 13)
        Me.LblDespacho.TabIndex = 3
        Me.LblDespacho.Text = "Despacho"
        '
        'Asignar
        '
        Me.Asignar.Location = New System.Drawing.Point(268, 125)
        Me.Asignar.Name = "Asignar"
        Me.Asignar.Size = New System.Drawing.Size(78, 21)
        Me.Asignar.TabIndex = 4
        Me.Asignar.Text = "Asignar"
        Me.Asignar.UseVisualStyleBackColor = True
        '
        'TxtIddeposito
        '
        Me.TxtIddeposito.Enabled = False
        Me.TxtIddeposito.Location = New System.Drawing.Point(280, 12)
        Me.TxtIddeposito.Name = "TxtIddeposito"
        Me.TxtIddeposito.Size = New System.Drawing.Size(60, 20)
        Me.TxtIddeposito.TabIndex = 5
        '
        'LblUsado
        '
        Me.LblUsado.AutoSize = True
        Me.LblUsado.Location = New System.Drawing.Point(13, 132)
        Me.LblUsado.Name = "LblUsado"
        Me.LblUsado.Size = New System.Drawing.Size(56, 13)
        Me.LblUsado.TabIndex = 7
        Me.LblUsado.Text = "Descontar"
        '
        'TxtDespacho
        '
        Me.TxtDespacho.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDespacho.Location = New System.Drawing.Point(74, 93)
        Me.TxtDespacho.Mask = "00000LLA0000000L"
        Me.TxtDespacho.Name = "TxtDespacho"
        Me.TxtDespacho.Size = New System.Drawing.Size(181, 24)
        Me.TxtDespacho.TabIndex = 40
        '
        'DtpFechaOficializacion
        '
        Me.DtpFechaOficializacion.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtpFechaOficializacion.Location = New System.Drawing.Point(121, 50)
        Me.DtpFechaOficializacion.Name = "DtpFechaOficializacion"
        Me.DtpFechaOficializacion.Size = New System.Drawing.Size(134, 20)
        Me.DtpFechaOficializacion.TabIndex = 41
        '
        'LblFechaOf
        '
        Me.LblFechaOf.AutoSize = True
        Me.LblFechaOf.Location = New System.Drawing.Point(13, 56)
        Me.LblFechaOf.Name = "LblFechaOf"
        Me.LblFechaOf.Size = New System.Drawing.Size(102, 13)
        Me.LblFechaOf.TabIndex = 42
        Me.LblFechaOf.Text = "Fecha Oficializacion"
        '
        'FrmAsignarDespacho
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 160)
        Me.Controls.Add(Me.LblFechaOf)
        Me.Controls.Add(Me.DtpFechaOficializacion)
        Me.Controls.Add(Me.TxtDespacho)
        Me.Controls.Add(Me.LblUsado)
        Me.Controls.Add(Me.TxtIddeposito)
        Me.Controls.Add(Me.Asignar)
        Me.Controls.Add(Me.LblDespacho)
        Me.Controls.Add(Me.LblSaldo)
        Me.Controls.Add(Me.TxtUsado)
        Me.Controls.Add(Me.TxtSaldo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "FrmAsignarDespacho"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Asignar Despacho"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtSaldo As System.Windows.Forms.TextBox
    Friend WithEvents TxtUsado As System.Windows.Forms.TextBox
    Friend WithEvents LblSaldo As System.Windows.Forms.Label
    Friend WithEvents LblDespacho As System.Windows.Forms.Label
    Friend WithEvents Asignar As System.Windows.Forms.Button
    Friend WithEvents TxtIddeposito As System.Windows.Forms.TextBox
    Friend WithEvents LblUsado As System.Windows.Forms.Label
    Friend WithEvents TxtDespacho As System.Windows.Forms.MaskedTextBox
    Friend WithEvents DtpFechaOficializacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents LblFechaOf As System.Windows.Forms.Label
End Class
