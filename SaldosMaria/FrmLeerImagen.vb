Imports IronOcr

Public Class FrmLeerImagen
    Private startPoint As Point
    Private endPoint As Point
    Private isDragging As Boolean = False

    Private Sub FrmLeerImagen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim bmp As Bitmap = Image.FromFile("C:\Users\Cristian\Desktop\TEMP\4.jpg")
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.Image = bmp
        PictureBox1.Size = bmp.Size
    End Sub
    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        startPoint = e.Location
        isDragging = True
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If isDragging Then
            endPoint = e.Location
            PictureBox1.Invalidate()
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        isDragging = False
        Dim rect As New Rectangle(
            Math.Min(startPoint.X, endPoint.X),
            Math.Min(startPoint.Y, endPoint.Y),
            Math.Abs(startPoint.X - endPoint.X),
            Math.Abs(startPoint.Y - endPoint.Y))
        Dim bmp As Bitmap = PictureBox1.Image
        Dim croppedBmp As Bitmap = bmp.Clone(rect, bmp.PixelFormat)
        PictureBox1.Image = croppedBmp
        PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        If isDragging Then
            Dim rect As New Rectangle(
                Math.Min(startPoint.X, endPoint.X),
                Math.Min(startPoint.Y, endPoint.Y),
                Math.Abs(startPoint.X - endPoint.X),
                Math.Abs(startPoint.Y - endPoint.Y))
            e.Graphics.DrawRectangle(Pens.Red, rect)
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Obtener la imagen actual en el PictureBox
        Dim bmp As Bitmap = PictureBox1.Image

        ' Crear un objeto IronOcr OCR
        Dim Ocr = New AutoOcr()

        ' Leer el texto de la imagen
        Dim Resultado = Ocr.Read(bmp)

        ' Mostrar el texto en un cuadro de texto
        MsgBox(Resultado.ToString)

    End Sub

End Class
