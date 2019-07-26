# BluetoothDataFile 
Projeto com o objetivo de enviar e receber arquivos em um computador, via bluetooth, de um cartão SD conectado a um arduino, utilizando Arduino UNO, Visual Basic, Módulo Sdcard, comunicação serial e protocolo SPI.

## Lista de componentes
* Arduino UNO
* Módulo Bluetooth HC-05
* Resistores de 1000, 100 e 2200 ohms (um de cada) 
* Módulo Sdcard
## Funcionamento
Este projeto está dividido em duas partes que são: o programa do Visual Basic e o do arduino. O programa do Visual Basic, através da comunicação serial,
se encarrega de permitir ao usuário se conectar com o módulo bluetooth HC-05 que está conectado a serial do arduino, possibilitando uma comunicação sem fio entre 
o computador e o arduino, Após isso o usuário escolhe se deseja enviar ou receber um arquivo do Sdcard e o Visual Basic lida com a comunicação com o arduino para 
informar o que o usuário deseja, em resposta, o arduino com o módulo sdcard que utiliza o protocolo SPI, acessa o cartão SD e lê ou salva um arquivo nele, 
permitindo também que o usuário veja quais arquivos existem na raíz do sdcard, no caso dele optar por receber um arquivo.

## Como usar 
Abra o aplicativo do Visual Basic na pasta BluetoothDataFile_VisualBasic2010\BluetoothDataFile\bin\Debug\BluetoothDataFile.exe, depois de aberto clique
no botão "atualizar serial", selecione pela combo box a COM em que está emparelhado o módulo bluetooth e clique em "conectar a serial". Com isso basta escolher entre
receber ou enviar um arquivo para o cartão SD.

**Receber arquivo:** Para receber um arquivo clique no botão "receber arquivo" e aguarde a caixa em branco abaixo do botão mostrar as opções de arquivo para receber da raíz do SD,
escolha um arquivo com um clique duplo no nome dele, depois disso aparecerá uma mensagem perguntando se realmente deseja receber aquele arquivo, clique em "sim" e
selecone um local para salvá-lo na nova janela que se abriu, agora basta aguardar que o recebimento seja concluído, monitorando a barra de progresso e esperando pela
mensagem de conclusão de transferência. 

**Enviar arquivo:** Para enviar um arquivo clique no botão de busca de arquivos ao lado da caixa de texto no formulário, selecione o arquivo que deseja enviar e clique no botão 
"enviar", após isso basta esperar a barra de progresso chegar ao fim e a mensagem de conclusão de envio aparecer.

**Obs:** O nome dos arquivos que serão recebidos e enviados devem ter no máximo 8 caracteres e mais 3 caracteres para a extensão, caso o contrário a transferência não será
bem sucedida.

## Bugs e limitações
Um dos bugs que podem ocorrrer é o de no recebimento do arquivo dados serem perdidos, o que fará com que o Visual Basic não consiga terminar a 
transferência e ela não seja bem sucedida, além disso as velocidades de envio e recebimento de arquivos são consideravelmente baixas (baud rate da COM serial é de 38400bps, por causa da perda de dados em velocidades maiores), principalmente a de envio,
isso se deve as limitações nas bibliotecas que estão sendo utilizadas no arduino e no hardware do módulo bluetooth e da serial do arduino.
