# Capsacin events sharp

This project is a proof of concept for the effectiveness of C# in a given use case. 
The application is made with the .Net framework, uses EF and sqlite.
Authentication is simple and not safe for production, but serves it's purpose for this given usecase.

## Use case

A Certain team wants to organise events. 
Because of personal responsibilities, it is not always possible to get all the 
members to attend these leisure activities. To help the organisers of the event plan, 
they want an application in which this can be organised and communication can occur. 
The event organiser wants to be able to create an event with a description and register 
the location where it will be. They also want to be able to attach a picture 
and files to the event. This way the attendees can easily find the 
address and relevant information. Besides this the event organiser wants 
the attendees to be able to respond to the event and register the dates they 
can attend. Once all of the availability is registered by the attendees, the organiser 
wants to see the dates that suit all of the attendees. Next to this the attendees 
and organisers want to be able to discuss the event within the system and leave comments under the event. 

## How to run

The application is not dockerized for now. You can still run the application locally or within gitpod.

To do this you will need to run a few commands:

```
dotnet dev-certs https 
dotnet restore
dotnet tool install --global dotnet-ef
```

and then you will be able to create the db if it is not already present:

```
dotnet ef database update
```

and finally run the api:

```
dotnet run
```

## Playing around

I use Postman to test api's a lot, because it allows you to import the openapi/swagger 
documentation and generates all the requests for you. This makes life a bit easier i.m.h.o.
The swagger documentation is still in the making and needs some more information but all the 
requests with payload are already defined. You can find it on `http://localhost:5199/swagger`

Start with the register and login endpoint. This will give you the required cookies you need on each request.
Then you can create events with a post on the `event` endpoint and add a picture in a string format. 
Then you can add other attendees to an event with the `attendees` endpoint on an event.
Finally you can leave comments and availibility dates on the `react` endpoint.

Elaborate tutorial will follow later ;)

## Feedback?

If you have any feedback regarding the code feel free to create an issue :D
