# discovering ORM with NHibernate
## Overall reasons behind the scene
NHibernate aims to be a complete solution to the problem of managing persistent data when working with relation databases and domain model classes.  
Q: Does NHibernate/FleuntHibernate only work with SQL, what about NoSql approach.  
A: Seems that it only support traditional SQL db providers. https://nhibernate.info/doc/nhibernate-features  

So the main point of NHibernate (or any other ORM) is to decresse the hard work that you will do when dealing with application-database logic, leaving you free to concentrate on the business problems.  

We can observe two paradigms: *object-oriented language* and *relational database*. They are indeed use different programming paradigms.  
The issues are reffered to as the _paradigm mismatch_ (in our case between OOP and DB design).  

So, you can than ask. If we have paradigm mismatch why we can't develop something that will reflect/mirror database and easy to work with? The main argument here is that persistence should not hinder your ability to design entities that correctly represents what you are manipulating!
### Alternatives and why ORM
Alternatively, you can use SqlCommand (raw ADO.NET) objects and manually write and execute SQL to build DataSets.
Doing so will quickly become tedious; you want to work at a slightly higher level of abstraction so you can focus on solving business problems rather than worring about data access concerns.  

In case you wanna learn more about other approaches to data access - Martin Fowler's _Patterns of Enterprise Application Architecture_.  

When working with ORM the approach which we take is to write classes - _or business entities_ that can be loaded and saved from the db.  
**Unlike DataSets, these classes aren't designed to mirror the structure of a relational database (such as rows and columns)**. Instead, they are concerned with solving the business problem at hand.  

**Together, such classes typically represent the object-oriented _domain model_**.  

Instead of working with the rows and columns of a DataTable, the business logic interacts with this object-oriented domain model and its runtime realization _as a graph of interconnected objects_.  
The business logic is never executed in the db (as a SQL stored procedures); its implemented in .NET.  

If app is simple its ok to use ADO.NET DataSet, but when the app grow (domain model increase) it is easier to maintain and develop with ORM.  
### NHibernate. Unit of work and conversations
When users work on applications, they perform distinct unitary operations.  
These operations can be reffered to as _conversations (or business transactions or application transactions)._  
For instance, buying a product is a conversation.  
We all know how hard it can be to make sure that many related operations performed by the user are treated as if they were a single bigger business transaction (a unit).  
**Unit = business transaction**
#### The Unit of Work pattern
Imagine that you are involved in a complex conversation (units of work) involving many updates and deletes. If you have to manually track which entities to save or delete, while making sure you load entity only once, things can quickly become difficult.  
NHibernate follows the Unit of Work pattern to solve this problem and make it easy to implement the conversations.
You can simply create entities and associate them with NHibernate; then, NHibernate keeps track of all loading and saving of changes only when required.  
At the end of the transaction, NHibernate figures out and applies all changes in their correct order.