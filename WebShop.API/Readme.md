///////////////////////////////
//////Migrations commands//////
///////////////////////////////
To add a new migration folder enter this into Package Manager Console:
Add-Migration "The entity you want" - for example: Add-Migration User 

Then Update it using this command:
Update-Database

This will take the data that we have build in the file called "WebShopContext.cs"
And insurt it into a new folder called "Migrations".

We will have to do alot of change to the database will the API is being done.
There for the easiest thing to will be deleting the folder "Migrations" add then
make a new one width the commands shown above.