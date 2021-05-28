# SnowChat
Simple NATS client and API

## Nats Console Client And API written in C#
this solution contain two main projects

## System Dependencies
- NATS server with JStream enabled running locally at localhost:4222
- .Net5

### getting nats server locally
The easiest way to get a nats server running locally with JStream enabled
is using docker, run this command

`docker run -d -p 4222:4222 nats -js`

### The console client
to run the project create folder somewhere in your computer
clone the git repo into it then run this command

- `dotnet run --project SnowChat.Konsol CLIENTNAME`
- replace the CLIENTNAME with you desired client name, leave it empty and it will be populated
with a random GUID.
  
open another terminal in the same directory and repeat the last two steps to get another client running

start writing text on either terminal and it will be send to all other online clients! DA
be mindful, empty values will be discarded, *quit* will quit the client.

### The API

in the same directory open a terminal and type 
- `dotnet run --project SnowChat.API`
- open your web browser and surf to url printed on the terminal output,
    probably it's http://localhost:5000/swagger
    the API is pretty straight forward, one get endpoint to get all messages
    and one post to send a new message.
  ### Notes
  these stuff skipped for brevity
    - security 
    - tests
    - best practices
      