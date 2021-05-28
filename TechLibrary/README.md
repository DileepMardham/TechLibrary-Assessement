# TechLibrary


### GitHub Repository 
[https://github.com/generic-widget-factory/TechLibraryExercise] 

### Prequisites
Visual Studio 2019 (.net core framework 3.x) - [Community Edition Download](https://visualstudio.microsoft.com/downloads/) 


### Solution Setup Instructions 
1. Clone the github repository locally 
2. Open **TechLibrary.sln**
3. Right Click the project “TechLibrary.Web”, select “Open Command Prompt Here” 
4. `$ npm install`   *(install the vue js client npm packages)* 
5. `$ npm run serve` *(run the client on localhost)*
6. **F5** in Visual Studio to launch the API project in Debug mode 
7. Select “Yes” to the dialog prompt to “trust the ASP.NET Core SSL certificate” 


### Coding Exercise Tasks 
Out of the box, the current solution uses a SQL Lite data source. The **BooksController** returns all books in the data store. Please complete the tasks below. 

#### Task: Pagination 
Return 10 results per page/request at the API and add pagination controls to the result set in the client UI 

**Added Pagination using "b-pagination" and paging through the api methods in the back ground. Created a class to pass the parameters to filter and paginate the results in the backend.**

#### Task: Search  
Create a search box control to filter/search the data store fields title and descr int the client UI, and the supporting functionality in the API 

**Added a search box and when keywords are entered it will add the search terms to book parameters which is used in turn for searching book results.**

#### Task: Edit Book 
On the book detail page, introduce a toggle into the client UI to toggle from read-only mode to edit mode.  Provide functionality in the API to support book detail edits/updates. 

**Reused the details page to act as the edit page. The detail page felt unneccesary as all the information regarding the book is on the homepage. This will save the book detials along with the thumbnail url which has a preview on the page.**

#### Bonus Task: Add New Book 
Create functionality in the client UI to add a new book to the data store and update the API to save a new book detail item. 

**Added a component to add books to the DB which has all the fields and also added a delete button to the edit page to include hte delete functionality.

Added some test cases to confirm that the REST API calls are going through. I would have added more functionality and made it more streamlined if I had a bit more time with the project.

#### Instructions

Please switch to the master branch after cloning the repo.

https://github.com/generic-widget-factory/TechLibraryExercise/tree/master

**

### Considerations
This development exercise is intended to be a ~ 4-hour exercise (+/-).  Even though the current scope does not represent an enterprise application, please approach your application design with supporting such a production application in mind. 


Agile teams strive for maintainable, quality code that will be evaluated at build time in a Continuous Integration pipeline – please apply all applicable techniques and practices to this project towards these goals. 

If you find areas that you would choose to refactor, please do so and note your thought process behind the refactoring.
