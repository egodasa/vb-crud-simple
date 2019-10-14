Public Class Form1
    Dim db As New Db()

    Private ReadOnly query_sql As String = "SELECT id_barang, nm_barang AS 'Nama Barang', harga AS 'Harga Barang' FROM barang"
    Private Sub ResetForm()
        Tnm_barang.Clear()
        Tharga.Clear()
        Tharga.Focus()
    End Sub
    Private Sub TampilData()
        DGcontoh.DataSource = db.JalankanDanAmbilSql(query_sql)
    End Sub
    Private Sub TambahData()
        Dim data_sql As String = "'" & Tnm_barang.Text & "', '" & Tharga.Text & "'"
        Dim sql As String = "INSERT INTO barang (nm_barang, harga) VALUES (" & data_sql & ")"
        db.JalankanSql(sql)
        If db.ApakahError() Then
            MessageBox.Show(db.AmbilPesanError())
        End If
        ResetForm()
        TampilData()
    End Sub
    Private Sub EditData(ByVal id_barang As String)
        Dim data_sql As String = " SET nm_barang = '" & Tnm_barang.Text & "', harga = '" & Tharga.Text & "' "
        Dim sql As String = "UPDATE barang" & data_sql & "WHERE id_barang = " & id_barang
        db.JalankanSql(sql)
        If db.ApakahError() Then
            MessageBox.Show(db.AmbilPesanError())
        End If
        ResetForm()
        TampilData()
    End Sub
    Private Sub DeleteData(ByVal id_barang As String)
        Dim sql As String = "DELETE FROM barang WHERE id_barang = " & id_barang
        db.JalankanSql(sql)
        If db.ApakahError() Then
            MessageBox.Show(db.AmbilPesanError())
        Else
            Call TampilData()
        End If
        TampilData()
    End Sub
    Private Sub DetailData(ByVal id_barang As String)
        MessageBox.Show(id_barang)
        Dim data As DataTable = db.JalankanDanAmbilSql(query_sql & " WHERE id_barang = " & id_barang)
        If db.ApakahError() Then
            MessageBox.Show(db.AmbilPesanError())
        Else
            Tnm_barang.Text = data.Rows(0).Item("Nama Barang")
            Tharga.Text = data.Rows(0).Item("Harga Barang")
        End If

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DGcontoh.DataSource = db.JalankanDanAmbilSql(query_sql)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call TambahData()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim id_barang As String = DGcontoh.Rows(DGcontoh.CurrentRow.Index).Cells("id_barang").Value
        Call EditData(id_barang)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim id_barang As String = DGcontoh.Rows(DGcontoh.CurrentRow.Index).Cells("id_barang").Value
        Call DeleteData(id_barang)
    End Sub

    Private Sub DGcontoh_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGcontoh.CellContentDoubleClick
        Call DetailData(DGcontoh.Rows(DGcontoh.CurrentRow.Index).Cells("id_barang").Value)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Close()
    End Sub
End Class
