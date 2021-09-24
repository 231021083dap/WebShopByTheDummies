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

////////////////////////////
//////Angular commands//////
////////////////////////////
To start Angular client in VS Code open a terminal and insert the following:
npm start

This will serve you the localhost:4200 in the background.

To make a new page with html, css(scss) and typescript insert the following:
ng g c "and the folders name" --skipTests=true 

(like so: ng g c Frontpage --skipTests=true)

We use --skipTests=true to avoid the .spec.ts files.