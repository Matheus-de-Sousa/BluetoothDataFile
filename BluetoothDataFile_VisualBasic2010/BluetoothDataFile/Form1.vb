Imports System.IO 'Namespace para acessar o StreamReader e StreamWriter
Imports System.Threading 'NameSpace para acessar o método "Thread.sleep
Imports System.Text 'NameSpace para acessar os recursos de encoding
Public Class BluetoothDataFile
    Dim stringfile As String = "" ' String que armazena o arquivo que será enviado para o arduino
    Dim Sending As Boolean = False ' Indica se o computador está enviando um arquivo
    Dim ReceiveFileSize As UInt64 = 0 ' Tamanho do arquivo que será recebido pelo computador
    Dim Bytes_received As UInt64 = 0 ' Bytes recebidos pelo computador do arduino
    Dim Receiving As Boolean = False ' Indica se o computador está recebendo um arquivo
    Dim bytes_sent As Integer = 0 ' Bytes enviados do computador ao arduino
    Dim StrBufferentrada As String = "" ' Variável que atmazena os dados recebidos pela porta serial
    Dim SaveFilePath As String = "" ' Caminho onde deve ser armazenado o arquivo que está sendo enviado para o computador
    Delegate Sub SerialDelegado(ByVal NewSerialData As String) ' Delegado para lidar com a leitura e tratamento dos dados recebidos pela serial
    Private Sub Serial_updt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Serial_updt.Click
        'Ao clicar no botão "atualizar serial" esse evento é acionado e ele se encarrega de buscar o nome
        'das portas seriais do computador e colocá-los em uma combo box para o usuário escolher a qual
        'porta deseja se conectar
        Try
            Serial_cbo.Items.Clear()
            For Each serialname As String In My.Computer.Ports.SerialPortNames ' Busca o nome das portas
                Serial_cbo.Items.Add(serialname) ' Adiciona na combobox
            Next
            If Serial_cbo.Items.Count > 0 Then
                Serial_cbo.SelectedIndex = 0
                Connect_btn.Enabled = True ' Habilita o botão conectar a serial
            Else
                MessageBox.Show("Nenhuma porta serial foi encontrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Connect_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Connect_btn.Click
        'Ao clicar no botão "Conectar a serial" esse evento é acionado e ele se encarrega de conectar-se na porta serial
        'selecionada pelo usuário na combo box e de se desconectar da porta serial caso já esteja conectado 
        If Connect_btn.Text = "Conectar a serial" Then
            Try
                Bth_Serial.PortName = Serial_cbo.SelectedItem
                Bth_Serial.BaudRate = 38400
                Bth_Serial.Open()
                Bth_Serial.Encoding = System.Text.Encoding.GetEncoding(1252) 'Altera a codificação da serial para um padrão ASCII EXTENDED
                If Bth_Serial.IsOpen Then
                    MessageBox.Show("Porta serial aberta com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Connect_btn.Text = "Desconectar da serial"
                    'Habilita botões do formulário
                    ReceiveFile_btn.Enabled = True
                    Btn_send.Enabled = True
                    Search_btn.Enabled = True
                Else
                    MessageBox.Show("Falha ao abrir a porta serial", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ConnectSerialerror As Exception
                MessageBox.Show(ConnectSerialerror.Message, "Erro na conexão serial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            Bth_Serial.Close()
            Connect_btn.Text = "Conectar a serial"
            'Desabilita botões do formulário
            ReceiveFile_btn.Enabled = False
            Btn_send.Enabled = False
            Search_btn.Enabled = False
        End If
    End Sub
    Private Sub Search_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search_btn.Click
        'Ao clicar no botão "..." do formulário esse evento é acionado e ele se encarrega de abrir um File Browser para
        'que o usuário selecione o arquivo que deseja enviar para o Sdcard via bluetooth, após o arquivo ser selecionado
        'o caminho onde ele se encontra é copiado para a barra de texto que está no formulário
        Data_FileBrowser.ShowDialog()
        File_txt.Text = Data_FileBrowser.FileName
    End Sub
    Private Sub Btn_send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_send.Click
        'Ao clicar no botão "enviar" do formulário esse evento é acionado e ele se encarrega de abrir um StreamReader para
        'ler um arquivo que terá todo o seu conteúdo armazenado na variável "stringfile" e esse evento também inicia o envio
        'do arquivo começando com o envio do nome e tamanho do arquivo que será enviado
        Dim bthFile As New StreamReader(File_txt.Text, System.Text.Encoding.GetEncoding(1252)) 'Abre StreamReader com codificação ASCII EXTENDED
        stringfile = ""
        stringfile = bthFile.ReadToEnd() ' lê e armazena todo o StreamReader na String
        bthFile.Close()
        File_bar.Visible = True ' Torna a barra de progresso de envio do arquivo visível
        ReceiveFile_btn.Enabled = False
        Search_btn.Enabled = False
        Btn_send.Enabled = False
        Connect_btn.Enabled = False
        Serial_updt.Enabled = False
        ChooseFileBox.Enabled = False
        Sending = True 'Entra no modo de envio de arquivo
        bytes_sent = 0 'Zera a variável que contém a quantidade de bytes enviados
        File_bar.Value = 0
        Bth_Serial.DiscardOutBuffer()
        Bth_Serial.WriteLine("#name:" & Data_FileBrowser.SafeFileName & "size:" & CStr(stringfile.Length)) 'envia o nome e tamanho do arquivo para o arduino
    End Sub
    Private Sub SerialDataSub(ByVal NewData As String)
        'Essa sub é chamada quando a Serialport recebe algum dado e ela tem a função de tratar esse daddo e executar alguns comandos dependendo do dado que foi recebido pela serial
        StrBufferentrada = StrBufferentrada & NewData ' Adiciona o dado lido na serial a String que serve para verificar qual rotina deve ser executada em resposta ao dado recebido
        If Sending And StrBufferentrada.Contains("*") And Not Receiving Then ' Checa se o programa está no modo de envio de arquivo e se o arduino já leu os dados enviados pelo programa
            ThreadTaskFile() 'Função encarregada de enviar os dados contidos em um determinado arquivo para o arduino
            StrBufferentrada = ""
        End If
        If StrBufferentrada.Contains("HeaderEnd") And Not Receiving And Not Sending Then 'Verifica se o arduino terminou de enviar o tamanho do arquivo que ele enviara para o computador
            If StrBufferentrada.Contains("Filesize") Then
                Dim FileSizeString = StrBufferentrada.Substring(StrBufferentrada.IndexOf("Filesize"))
                ReceiveFileSize = Val(FileSizeString.Substring(FileSizeString.IndexOf("Filesize") + 8, FileSizeString.IndexOf("HeaderEnd") - (FileSizeString.IndexOf("Filesize") + 8))) 'Busca e armazena na variável o tamanho do arquivo
                StrBufferentrada = FileSizeString.Substring(FileSizeString.IndexOf("HeaderEnd") + 9) ' Apaga os dados já tratados da string
                Receiving = True
                FinishReceiving.Enabled = True
            End If
        End If
        If Receiving And Not Sending Then ' Verifica se o programa está no modo de recebimento de arquivo
            Try
                If Bytes_received < ReceiveFileSize Then ' Checa se a quantidade de bytes recebidos é menor que o tamanho do arquivo que está sendo enviado
                    Dim SaveFile As New StreamWriter(SaveFilePath, True, System.Text.Encoding.GetEncoding(1252)) ' Abre arquivo para salvar os dados recebidos do arduino
                    SaveFile.Write(StrBufferentrada)
                    SaveFile.Close()
                    Bytes_received = Bytes_received + StrBufferentrada.Length ' Atualiza a quantidade de bytes recebidos
                    StrBufferentrada = ""
                    ' Atualiza a barra de progresso de recebimento do arquivo
                    If (Bytes_received / ReceiveFileSize) * 10000 - FileReceiveBar.Value >= 1 Then
                        FileReceiveBar.Value = (Bytes_received / ReceiveFileSize) * 10000
                    End If
                    'Evita perda de dados no fim do envio do arquivo
                    If Bytes_received + Bth_Serial.BytesToRead >= ReceiveFileSize And Bth_Serial.BytesToRead > 0 Then
                        Dim LastData As New StreamWriter(SaveFilePath, True, System.Text.Encoding.GetEncoding(1252))
                        Dim lastDatastr As String = Bth_Serial.ReadExisting()
                        LastData.Write(lastDatastr)
                        LastData.Close()
                        Bytes_received = Bytes_received + lastDatastr.Length
                    End If
                End If
                If Bytes_received >= ReceiveFileSize Then 'Verifica se o envio do arquivo chegou ao fim e encerra o recebimento do arquivo
                    Receiving = False
                    FileReceiveBar.Value = FileReceiveBar.Maximum
                    Bytes_received = 0
                    ReceiveFileSize = 0
                    StrBufferentrada = ""
                    ChooseFileBox.Items.Clear()
                    SaveFilePath = ""
                    MessageBox.Show("Arquivo Recebido com sucesso", "Transferência finalizada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    FileReceiveBar.Value = 0
                    FinishReceiving.Enabled = False
                    ReceiveFile_btn.Enabled = True
                    Search_btn.Enabled = True
                    Btn_send.Enabled = True
                    Connect_btn.Enabled = True
                    Serial_updt.Enabled = True
                    ChooseFileBox.Enabled = True
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        While StrBufferentrada.Contains(vbLf) And Not Receiving And Not Sending ' Loop para ler a lista de arquivos que estão armazenados no SDcard
            If StrBufferentrada.Contains("isFile:") Then
                Dim NewItem = StrBufferentrada.Substring(StrBufferentrada.IndexOf("isFile:"))
                If NewItem.Contains(vbLf) Then
                    ChooseFileBox.Items.Add(NewItem.Substring(NewItem.IndexOf("isFile:") + 7, NewItem.IndexOf(vbLf) - (NewItem.IndexOf("isFile:") + 7))) ' Adiciona a listbox um item com o nome do arquivo que está no SDcard
                    StrBufferentrada = StrBufferentrada.Substring(0, StrBufferentrada.IndexOf("isFile:")) & NewItem.Substring(NewItem.IndexOf(vbLf) + 1)
                End If
            End If
            If Bth_Serial.BytesToRead > 0 Then
                StrBufferentrada = StrBufferentrada & Bth_Serial.ReadExisting()
            End If
        End While
    End Sub
    Private Sub Bth_Serial_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles Bth_Serial.DataReceived
        'Evento acionado ao receber algum dado pela porta serial, esse evento é encarregado de ler o dado recebido
        'e executar uma Sub, através de um delegado, para tratar esses dados
        Try
            Dim Serialdelegate As New SerialDelegado(AddressOf SerialDataSub)
            Dim SerialData As String = Bth_Serial.ReadExisting
            MyBase.Invoke(Serialdelegate, SerialData)
        Catch ex As Exception ' Tratamento de erros com a conexão serial
            MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ReceiveFile_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceiveFile_btn.Click
        'Ao clicar no botão "Receber arquivo" esse evento é acionado e ele se encarrega de solicitar para o arduino
        'a lista de arquivos contidos na raiz do SDcard e preparar o programa para o usuário escolher o arquivo que
        'ele deseja receber
        Try
            MessageBox.Show("Escolha o arquivo que deseja Receber", "Receber Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Bth_Serial.DiscardOutBuffer()
            Bth_Serial.WriteLine("ReadFile") ' Solicitação ao arduino da lista de arquivos no SDcard
            ChooseFileBox.Items.Clear()
            SaveFilePath = ""
            ReceiveFile_btn.Enabled = False
            Search_btn.Enabled = False
            Btn_send.Enabled = False
            Connect_btn.Enabled = False
            Serial_updt.Enabled = False
            ChooseFileBox.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ThreadTaskFile()
        'Subrotina que tem a função  de enviar os dados de um determinado arquivo para o arduino, conforme o arquivo
        'que o usuário escolheu para ser enviado
        Try
            Dim packdata As Integer = 0
            While bytes_sent < stringfile.Length And packdata < 15 'Loop para enviar um pacote de 15 em 15 bytes para o arduino até o fim do arquivo
                'Atualiza a barra de progresso de envio do arquivo
                If (bytes_sent / stringfile.Length) * 100 - File_bar.Value >= 1 Then
                    File_bar.Value = (bytes_sent / stringfile.Length) * 100
                End If
                'Envia um byte
                Bth_Serial.DiscardInBuffer()
                Bth_Serial.Write(stringfile(bytes_sent))
                bytes_sent = bytes_sent + 1
                packdata = packdata + 1
                If bytes_sent >= stringfile.Length Then 'Checa se o envio do arquivo chegou ao fim, se sim informa ao usuário que o envio do arquivo foi concluído e sai do modo de envio de arquivo
                    Data_FileBrowser.Reset()
                    Thread.Sleep(500)
                    stringfile = ""
                    File_bar.Value = 0
                    File_bar.Visible = False
                    bytes_sent = 0
                    File_txt.Text = ""
                    Sending = False
                    ReceiveFile_btn.Enabled = True
                    Search_btn.Enabled = True
                    Btn_send.Enabled = True
                    ChooseFileBox.Enabled = True
                    Connect_btn.Enabled = True
                    Serial_updt.Enabled = True
                    MessageBox.Show("Processo finalizado com sucesso", "Processo finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ChooseFileBox_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ChooseFileBox.MouseDoubleClick
        'Evento acionado ao dar um clique duplo na listbox no formulário, esse evento é encarregado de ler qual é o item selecionado
        'no listbox e enviar o nome desse item para o arduino como uma solicitação de recebimento de arquivo
        If Receiving = False Then 'Checa se o programa não esta no modo de recebimeno de arquivo
            If MessageBox.Show("Deseja mesmo receber esse arquivo?", "Confirmar escolha", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = MsgBoxResult.Yes Then 'Pergunta ao usuário se ele realmente quer receber aquele arquivo
                SaveFilePath = ""
                SaveFolderBrowser.ShowDialog() ' Abre uma Save Folder Browser
                SaveFilePath = SaveFolderBrowser.SelectedPath 'Atribui o caminho escolhido pelo usuário a String "SaveFilePath"
                SaveFilePath = SaveFilePath & "/" & ChooseFileBox.Text 'Adiciona o nome do arquivo selecionado na listbox à string
                Bth_Serial.DiscardOutBuffer()
                Bth_Serial.DiscardInBuffer()
                Bth_Serial.WriteLine("RequestFile:" & ChooseFileBox.Text) 'Envia uma solicitação de recebimento de arquivo com o nome do arquivo a receber para o arduino
                ChooseFileBox.Enabled = False
            End If
        End If
    End Sub
    Private Sub FinishReceiving_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinishReceiving.Tick
        'Evento Timer que é acionado a cada 500ms quando o programa está recebendo um arquivo para
        'garantir que no final do recebimento do arquivo não ocorra perda de dados
        Try
            If Receiving And Not Sending Then
                If Bytes_received + Bth_Serial.BytesToRead >= ReceiveFileSize And Bth_Serial.BytesToRead > 0 Then
                    Dim LastData As New StreamWriter(SaveFilePath, True, System.Text.Encoding.GetEncoding(1252))
                    Dim lastDatastr As String = Bth_Serial.ReadExisting()
                    LastData.Write(lastDatastr)
                    LastData.Close()
                    Bytes_received = Bytes_received + lastDatastr.Length
                End If
                If Bytes_received >= ReceiveFileSize Then
                    Receiving = False
                    FileReceiveBar.Value = FileReceiveBar.Maximum
                    Bytes_received = 0
                    ReceiveFileSize = 0
                    StrBufferentrada = ""
                    ChooseFileBox.Items.Clear()
                    SaveFilePath = ""
                    MessageBox.Show("Arquivo Recebido com sucesso", "Transferência finalizada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    FileReceiveBar.Value = 0
                    FinishReceiving.Enabled = False
                    ReceiveFile_btn.Enabled = True
                    Search_btn.Enabled = True
                    Btn_send.Enabled = True
                    ChooseFileBox.Enabled = True
                    Connect_btn.Enabled = True
                    Serial_updt.Enabled = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
