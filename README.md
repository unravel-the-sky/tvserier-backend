# tvserier-backend
payex oppgave - backend bits

Here is my results for the part coding challenge. it's not 100% complete and there are a bunch of things that i'd have liked to do differently if i had more time and energy.. but i'm still happy with the results.

Backend decisions:
- i chose dotnetcore cause it was the technology that was asked :)
- this was by far the most difficult part of the task, since i haven't really worked with backend or dotnetcore before.
- i chose to use mongodb cause it looked somewhat simpler, and EF looked a bit scary for the short time span
- the main logic is inside the TvShowsService.cs file, and model that is in sync with mongodb is under Models/TvShowsModel.cs file
- i spent the most amount of time trying to serialize/deserialize json data, been working with JS for way too long to remember those bits, but i think it worked
- i have created data models for return types, and tried to minimize the payload for api calls, cause i believe it's important to fetch as least data as possible (knowing by experience)
- the code is not very clean right now and there are a couple of methods/classes that are not in use but i don't want to send the code too late (and also shall sleep)
- i can describe the code more if you'd like to proceed later on :) 
- PS: i didn't find a way to NOT upload the pdp and dll files, although i have set up the .gitignore file correctly. at least it seems.. so it looks a bit rookie but oh well..

HOW TO RUN THE PROJECT:
- first install mongodb on your machine: https://docs.mongodb.com/v3.2/administration/install-community/
- then: Open a command shell. Run the following command to connect to MongoDB on default port 27017. Remember to replace <data_directory_path> with the directory you chose in the previous step. 
  mongod --dbpath <data_directory_path>
- then: open another terminal and type mongo
- then: start the tvserier-backend project (it will use the mongodb database)
- IMPORTANT: because CORS requirements, when running the frontend part, please go and modify the Startup.cs file:
            app.UseCors(
                    options => options.WithOrigins("http://192.168.1.121:8080").AllowAnyMethod()
                );
        
        the 'http://192.168.1.121:8080' shall be cha

Sadan