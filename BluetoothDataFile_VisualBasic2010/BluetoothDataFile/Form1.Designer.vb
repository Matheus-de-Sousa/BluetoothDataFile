<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BluetoothDataFile
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.FileReceiveBar = New System.Windows.Forms.ProgressBar()
        Me.ChooseFileBox = New System.Windows.Forms.ListBox()
        Me.File_bar = New System.Windows.Forms.ProgressBar()
        Me.Serial_updt = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Connect_btn = New System.Windows.Forms.Button()
        Me.Serial_cbo = New System.Windows.Forms.ComboBox()
        Me.ReceiveFile_btn = New System.Windows.Forms.Button()
        Me.Search_btn = New System.Windows.Forms.Button()
        Me.Btn_send = New System.Windows.Forms.Button()
        Me.File_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FinishReceiving = New System.Windows.Forms.Timer(Me.components)
        Me.SaveFolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.Bth_Serial = New System.IO.Ports.SerialPort(Me.components)
        Me.Data_FileBrowser = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'FileReceiveBar
        '
        Me.FileReceiveBar.Location = New System.Drawing.Point(120, 133)
        Me.FileReceiveBar.Maximum = 10000
        Me.FileReceiveBar.Name = "FileReceiveBar"
        Me.FileReceiveBar.Size = New System.Drawing.Size(293, 23)
        Me.FileReceiveBar.Step = 1
        Me.FileReceiveBar.TabIndex = 6
        '
        'ChooseFileBox
        '
        Me.ChooseFileBox.AccessibleDescription = ""
        Me.ChooseFileBox.FormattingEnabled = True
        Me.ChooseFileBox.Location = New System.Drawing.Point(120, 162)
        Me.ChooseFileBox.Name = "ChooseFileBox"
        Me.ChooseFileBox.Size = New System.Drawing.Size(293, 251)
        Me.ChooseFileBox.TabIndex = 5
        '
        'File_bar
        '
        Me.File_bar.Location = New System.Drawing.Point(419, 104)
        Me.File_bar.Name = "File_bar"
        Me.File_bar.Size = New System.Drawing.Size(191, 23)
        Me.File_bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.File_bar.TabIndex = 47
        Me.File_bar.Visible = False
        '
        'Serial_updt
        '
        Me.Serial_updt.Location = New System.Drawing.Point(379, 37)
        Me.Serial_updt.Name = "Serial_updt"
        Me.Serial_updt.Size = New System.Drawing.Size(126, 23)
        Me.Serial_updt.TabIndex = 46
        Me.Serial_updt.Text = "Atualizar serial"
        Me.Serial_updt.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 16)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "Portas Seriais"
        '
        'Connect_btn
        '
        Me.Connect_btn.Enabled = False
        Me.Connect_btn.Location = New System.Drawing.Point(208, 37)
        Me.Connect_btn.Name = "Connect_btn"
        Me.Connect_btn.Size = New System.Drawing.Size(165, 23)
        Me.Connect_btn.TabIndex = 44
        Me.Connect_btn.Text = "Conectar a serial"
        Me.Connect_btn.UseVisualStyleBackColor = True
        '
        'Serial_cbo
        '
        Me.Serial_cbo.BackColor = System.Drawing.SystemColors.Window
        Me.Serial_cbo.Cursor = System.Windows.Forms.Cursors.Default
        Me.Serial_cbo.FormattingEnabled = True
        Me.Serial_cbo.Location = New System.Drawing.Point(120, 39)
        Me.Serial_cbo.Name = "Serial_cbo"
        Me.Serial_cbo.Size = New System.Drawing.Size(82, 21)
        Me.Serial_cbo.TabIndex = 43
        '
        'ReceiveFile_btn
        '
        Me.ReceiveFile_btn.Enabled = False
        Me.ReceiveFile_btn.Location = New System.Drawing.Point(120, 103)
        Me.ReceiveFile_btn.Name = "ReceiveFile_btn"
        Me.ReceiveFile_btn.Size = New System.Drawing.Size(293, 23)
        Me.ReceiveFile_btn.TabIndex = 39
        Me.ReceiveFile_btn.Text = "Receber Arquivo"
        Me.ReceiveFile_btn.UseVisualStyleBackColor = True
        '
        'Search_btn
        '
        Me.Search_btn.Enabled = False
        Me.Search_btn.Location = New System.Drawing.Point(472, 74)
        Me.Search_btn.Name = "Search_btn"
        Me.Search_btn.Size = New System.Drawing.Size(36, 23)
        Me.Search_btn.TabIndex = 32
        Me.Search_btn.Text = "..."
        Me.Search_btn.UseVisualStyleBackColor = True
        '
        'Btn_send
        '
        Me.Btn_send.Enabled = False
        Me.Btn_send.Location = New System.Drawing.Point(514, 74)
        Me.Btn_send.Name = "Btn_send"
        Me.Btn_send.Size = New System.Drawing.Size(96, 23)
        Me.Btn_send.TabIndex = 29
        Me.Btn_send.Text = "Enviar"
        Me.Btn_send.UseVisualStyleBackColor = True
        '
        'File_txt
        '
        Me.File_txt.BackColor = System.Drawing.SystemColors.Window
        Me.File_txt.Location = New System.Drawing.Point(120, 77)
        Me.File_txt.Name = "File_txt"
        Me.File_txt.Size = New System.Drawing.Size(345, 20)
        Me.File_txt.TabIndex = 28
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 16)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Enviar Arquivo"
        '
        'FinishReceiving
        '
        Me.FinishReceiving.Interval = 500
        '
        'Bth_Serial
        '
        Me.Bth_Serial.BaudRate = 38400
        Me.Bth_Serial.ReadBufferSize = 5000
        Me.Bth_Serial.WriteBufferSize = 5000
        '
        'Data_FileBrowser
        '
        Me.Data_FileBrowser.FileName = "OpenFileDialog1"
        '
        'BluetoothDataFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(620, 423)
        Me.Controls.Add(Me.FileReceiveBar)
        Me.Controls.Add(Me.File_bar)
        Me.Controls.Add(Me.ReceiveFile_btn)
        Me.Controls.Add(Me.ChooseFileBox)
        Me.Controls.Add(Me.Serial_updt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Connect_btn)
        Me.Controls.Add(Me.Serial_cbo)
        Me.Controls.Add(Me.Search_btn)
        Me.Controls.Add(Me.Btn_send)
        Me.Controls.Add(Me.File_txt)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "BluetoothDataFile"
        Me.Text = "BluetoothDataFile"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FileReceiveBar As System.Windows.Forms.ProgressBar
    Public WithEvents ChooseFileBox As System.Windows.Forms.ListBox
    Friend WithEvents File_bar As System.Windows.Forms.ProgressBar
    Friend WithEvents Serial_updt As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Connect_btn As System.Windows.Forms.Button
    Friend WithEvents Serial_cbo As System.Windows.Forms.ComboBox
    Friend WithEvents ReceiveFile_btn As System.Windows.Forms.Button
    Friend WithEvents Search_btn As System.Windows.Forms.Button
    Friend WithEvents Btn_send As System.Windows.Forms.Button
    Friend WithEvents File_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FinishReceiving As System.Windows.Forms.Timer
    Friend WithEvents SaveFolderBrowser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Bth_Serial As System.IO.Ports.SerialPort
    Friend WithEvents Data_FileBrowser As System.Windows.Forms.OpenFileDialog

End Class
