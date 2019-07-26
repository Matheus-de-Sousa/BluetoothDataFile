#include <SD.h> // Biblioteca para lidar com o módulo SD
#include <SPI.h> //Biblioteca para lidar com a comunicação SPI
bool SendMode = false; // Variável que indica se o arduino está enviando um arquivo 
void setup() {
  pinMode(6, OUTPUT); // Pino Slave Select do Módulo SD
  Serial.begin(38400); // Iniciliaza comunicação serial com baudrate de 38400bps
  Serial.setTimeout(50); // Altera o tempo máximo de espera de leitura da serial para 50ms
  while(!SD.begin(6)); // Testa se o SDcard está funcionando corretamente, quando ele estiver, inicializa a conexão com o módulo SD 
}
void loop() {
  if (Serial.available()) {
    if(SendMode){ // Checa se o arduino está enviando um arquivo
      String SendCmd = Serial.readStringUntil('\n'); // Lê os dados recebidos pela serial até encontrar o caractere de quebra de linha 
      if(SendCmd.indexOf("\n") != -1) SendCmd.replace("\n","");
      if(SendCmd.indexOf("RequestFile:") !=-1){ // Verifica se houve um pedido de envio de arquivo
        SendCmd.replace("RequestFile:","");
        File SendFile = SD.open(SendCmd); // Abre o arquivo solicitado pelo computador
        if(SendFile){ // Verifica se o arquivo foi aberto com sucesso
          SendFile.seek(0); // Vai para o primeiro byte do arquivo
          //---------- Envio do tamanho do arquivo para o computador ------------
          Serial.print(String("Filesize")); 
          Serial.print(SendFile.size());
          Serial.print(String("HeaderEnd"));
          //--------------------- Envio do Arquivo ------------------------------
          unsigned long SizeFile = 0; // Variável para contar a quantidade de bytes do arquivo que foram enviados
          delay(500);
          while(SizeFile < SendFile.size()){ // Loop para enviar o arquivo byte por byte
            if(Serial.availableForWrite()>0){
              byte data = SendFile.read();
              Serial.write(data);
              SizeFile++;
            }
          }
          SendFile.close(); // Fecha o arquivo
        }
        SendMode = false; // Sai do modo de envio de arquivos
      }
    }
    if(!SendMode){ // Checa se o arduino não está enviando um arquivo
       String DataReceived = Serial.readStringUntil('\n');
       if(DataReceived.indexOf("\n") != -1) DataReceived.replace("\n","");
       if(DataReceived.indexOf("ReadFile") != -1){ // Verifica se o computador solicitou a lista de arquivos na raiz do SDcard
        SendMode = true; // Entra no modo de envio de arquivos
        File SendFile = SD.open("/"); // Abre a raiz do SDcard 
        SendFile.rewindDirectory(); // retorna´para a raiz ou diretório atual
        File ListItems = SendFile.openNextFile(); // Avança um arquivo ou diretório na raiz e o abre
        while(ListItems){ // Enquanto houverem arquivos ou diretórios a serem percorridos
          if(!ListItems.isDirectory())Serial.print(String("isFile:") + String(ListItems.name()) + String("\n")); // Verifica se a instância atual da classe File é um arquivo e se for envia o nome dele para o computador
          ListItems.close();
          ListItems = SendFile.openNextFile();
        }
        SendFile.rewindDirectory();
        SendFile.close();
        ListItems.close();
      }
      else if(DataReceived.indexOf("#") !=-1){ // Verifica se o computador vai iniciar o envio de um arquivo para o arduino
        DataReceived.replace("#","");
        String ReceiveFileName = DataReceived.substring(DataReceived.indexOf("name:")+5,DataReceived.indexOf("size:")); // Busca na String DataReceived e armazena na String ReceiveFileName o nome do arquivo que será enviado
        long ReceiveFileSize = (DataReceived.substring(DataReceived.indexOf("size:")+5)).toInt(); // Busca na String DataReceived e armazena na variável ReceiveFileSize o tamanho do arquivo que será enviado
        unsigned long bytesReceived = 0; // Quantidade de bytes recebidos do computador
        DataReceived="";
        File ReceiveFile = SD.open(ReceiveFileName,FILE_WRITE); // Abre um arquivo de escrita no SDcard
        if(ReceiveFile){ // Checa se o arquivo foi aberto corretamente
        //------- Recebimento do arquivo -----------------------------
          while(bytesReceived < ReceiveFileSize){ 
            Serial.print("*"); // Caractere enviado ao computador para indicar que o pacote de dados já foi lido
            while(!Serial.available()){ // Aguarda o envio de um novo pacote de dados
              Serial.print("*");
              delayMicroseconds(100);
            }
            //------Leitura do pacote de dados ----------------------- 
            while(Serial.available()){ 
              byte databuffer = Serial.read();
              ReceiveFile.write(databuffer);
              bytesReceived++;
            }
          }
          ReceiveFile.close(); // Fecha o arquivo
        }
        ReceiveFileName = ""; // limpa a variável com o nome do arquivo
        ReceiveFileSize = 0; // Zera a variável com o tamanho do arquivo
      }
    }
  }
}
