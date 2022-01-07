# ImplementazioneAES

Implementazione basata su CLI della crittografia AES.

[Specifica AES](https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf)

### Crittazione

Dato un file `file.txt` e una chiave provvista tramite terminale, il programma lo crittetà e restituirà un file `file.txt.aes` contenente il file crittato

```clear text -> cipher key -> cipher text```

### Decrittazione

dato un file `file.txt.aes` e la sua chiave provvista tramite terminale, il programma restituirà il file in chiaro `file.txt`.  
Il programma cerca automaticamente il file con all'interno la chiave, ma si potra specificare la chiave a parte.

```cipher text -> cipher key -> clear text```
