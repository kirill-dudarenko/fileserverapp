**fileserverapp**</br></br>
Source code is splitted on 5 project modules:</br></br>
**Common**  class library contain abstractions related to given tech task.</br></br>
**FileSystem** class library contain concrete implementation of file system operations.</br></br>
**fileserverapp** represent RESTful WebApi of serverside filesystem operations.</br></br>
**FileServerClient** is a single page AJAX application provided to expose various extendable operations, such as display and modify server file system  entries.</br></br>
**FileServerService.UnitTests** provided to test the file system operations layer in terms of unit tests.</br></br>

deployment

In order to make the solution work you need to perform the following:</br></br>
  **Local IIS Deployment**</br>
  create a folder on a local drive such as "c:\apps" and Publish fileserverapp project to it 
  ![изображение](https://user-images.githubusercontent.com/100361551/155582259-c428c604-2413-4234-8f92-e14b447b6a83.png)</br>
  Next, publish WebApi by WebDeploy method of Visual Studio with the settings as follows:</br>
  ![изображение](https://user-images.githubusercontent.com/100361551/155582989-3b3b1386-43cd-4c49-9edc-520e297b762d.png)</br>
  Hit "Publish" button.</br>
    
  You can ensure api work by accessing http://localhost:8080/resources  endpoint or using Swagger UI (http://localhost:8080/swagger)
  
  Host the client part using same steps as above</br>
  ![изображение](https://user-images.githubusercontent.com/100361551/155656383-23c357b1-681d-41af-8e74-bacd69168863.png)
</br></br>
![изображение](https://user-images.githubusercontent.com/100361551/155656593-d2d20dcc-8925-49a0-8120-e93b853634eb.png)</br>
</br>

Query web site by accessing http://localhost:8081/index.html</br></br></br>

Once navigated to a tree folder/file node, the actions panell will appear above the tree</br>
Folder actions:
![изображение](https://user-images.githubusercontent.com/100361551/155657207-7b5b1fe9-c9b6-4ded-bfd0-5f2f4d17c24e.png)</br>
</br></br>
File actions:
![изображение](https://user-images.githubusercontent.com/100361551/155657363-0d2ebf63-0b8f-4350-85d5-c16cd667d644.png)
</br></br></br>

**Design solution**

The object model being built around the abstract UnifiedResource class that holds the collection of available actions.</br>
Action is an IAction implementation that contain generic instructions of how to work with a certain action.</br>
The concrete action implementation have a precise instructions of what it does.</br>
</br>

In order to create the resource we need to identify it by its location and type. This is delegated to a IResourceFactory.Create method</br>
that knows what needs to be created.</br>

So, if you need a new resource with a specific unlisted actions you need to do following:</br>
1. Make a descendant of IAction interface that will hold the action information and implement it.</br>
2. Implement abstract UnifiedResource class and add the action implementation to the resource's available actions collection.</br>
3. Change the ResourceFactory.Create method to create your resources.</br>

</br></br>
**Database resource data persistence**</br>

The central part of the database desing should be a table that holds the resource nestings.</br>
It should contain parent resource id and ids of the resources beneath it.</br>
There are should be tables that holds various resource data and all of them should reference children resource id from the central table.</br>
In this case the new resource can be added by addition of new table to the database.</br>

