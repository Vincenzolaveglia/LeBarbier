# LeBarbier

LeBarbier è un'applicazione web per la gestione delle prenotazioni di un salone.

## Descrizione

## 🔍 Caratteristiche Principali

Responsive Design: LeBarbier è progettato con un design responsive che si adatta automaticamente a diverse dimensioni di schermo e dispositivi. Questo assicura un'esperienza utente ottimale sia su desktop che su dispositivi mobili.

## 🚀 Tecnologie Utilizzate

 - Piattaforma: ASP.NET MVC Framework
 - Gestione dei Dati: Entity Framework, SQL Server

## 📦 Installazione e Setup

1. Clona il repository: `git clone https://github.com/Vincenzolaveglia/LeBarbier`
2. Apri il progetto in Visual Studio
3. Configura il database nel file `Web.config`
4. Esegui l'applicazione in modalità debug o pubblicala su un server web

## Configurazione

- Assicurati di configurare correttamente la connessione al database nel file `Web.config`.
- Puoi personalizzare il layout e lo stile dell'applicazione modificando i file CSS e le viste Razor.
- Per funzionare, va aggiunto al database nella tabella "Roles", nel campo "TypeRole" (specificamente scritti nel seguente modo):
     - Admin
     - User 
- L'admin NON si può registrare. Va aggiunto manualmente al database nella tabella "Users", mettendogli come id nel campo "RoleId" quello corrispondente all'id nella tabella "Roles".
(Puoi trovare tutto nel file `Creazione DB`)

## Contributi

Sii libero di contribuire al progetto mediante suggerimenti, segnalazioni di bug o richieste di nuove funzionalità. Segui le linee guida di GitHub per la collaborazione.

## 📱 Contatti

Il progetto LeBarbier è stato sviluppato da Laveglia Vincenzo.

[GitHub](https://github.com/Vincenzolaveglia) 🐙 - [LinkedIn](https://www.linkedin.com/in/vincenzo-laveglia-404baa2ab/) 💼
