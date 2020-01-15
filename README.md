# Retail.Product.API

This API is an end-to-end PoC for a products API, aggregating data from another API as well as a nosql database.

Accepted Actions:
	HTTP GET will obtain title and price information from aggregated sources
	HTTP PUT will save price information to a local Redis nosql database.
	
	
Tech used:

ServiceStack: (C#) https://servicestack.net/
Lightweight API framework used for the API layer, using the manager/repository pattern with applicable dependency injection.

Redis: https://redis.io/
For the purposes of this proof of concept, Redis is running within a Docker container locally to the API.

StackExchange.Redis: https://github.com/StackExchange/StackExchange.Redis
I've used this package before for easy interaction with Redis and found it to be very easy to work with, even easier than ServiceStack's own Redis client.
