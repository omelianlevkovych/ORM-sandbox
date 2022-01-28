# Understanding architecture
The first thing you have to learn about NHibernate in order to use it are the programming interfaces.  

![basic-architecture](https://github.com/omelianlevkovych/ORM-sandbox/blob/main/NHibernate/notes/images/understand-architecture.jpg)


We can approximately clasify this interfaces:
* To perform basic CRUD and query operations. These interfaces are the main point of dependency of app business/control logic on NHibernate.  
They include _ISession, ITransaction, IQuery_ and _ICriteria_.
* Interfaces which used by app infrastructure code to configure NHibernate, most importantly the _Configuration_ class.
* Callback interfaces that allow the app to react to events occuring inside NHibernate, such as _IInterceptor, ILifecycle_ and _IValidatable_.
* Interfaces that allow extension of NHibernate mapping functionality, such as _IUserType,
ICompositeUserType_ and _IIdentifierGenerator_. These interfaces can be implemented by app infrastructure code (if necessary).

NHibernate makes use of existing .NET APIs, including ADO.NET and its _ITransaction_ API.
## The core interfaces
Five core interfaces which we can use to store and retrieve persistent objects and control transactions.
### ISession interface
It exposes NHibernate's methods for finding, saving, updating and deleting objects (CRUD).  
An instance of ISession is **lightweight and not expensive to create and destroy**.  
This is important bacause your app will need to create and destroy sessions all the time, perhaps on every ASP.NET request.  
NHibernate sessions are **not thread safe** and should by design be used by only one thread at a time.  
The NH notion of a _session_ is something between _connection_ and _transaction_.  
It might be easier to think of a session as a collection (cache) of loaded objects relating to a single unit of work. NH can detect changes to the objects in this unit of work.  
We sometimes call _ISession_ a _persistence manager_ bacause it's also the interface for persistence-related operations such as storing and retrieving objects.
### ISessionFactory interface
The app get ISession instances from ISessionFactory.  
**The ISessionFactory is not lightweight!**.  
It is intended to be shared among many app threads. There is typically a single instance of ISessionFactory for the whole app - created on startup, for instance.  
In case your app accesses multiple dbs usning NHibernate, you'll need SessionFactory per each.  
The SessionFactory caches generated SQL statements and other mapping metadata that NHibernate uses at runtime.
### Configuration class (interface)
Basically used to configure NH.  
The app uses a Configuration instance to specify the location of mapping docs and to specify NHibernate-specific properties before creating the ISessionFactory.
### ITransaction interface
The ITransaction interface is an optional API.  
NH apps may choose not to use this interface, instead managing transactions in their own infrastructure code.  
An NH ITransaction abstracts application code from the underlying transaction implementation - which might be an ADO.NET transaction or any kind of manual transaction - allowing the app to control transaction boundaries via a consistent API. This helps to keep NH applications portable between different kind of execution envs and containers.  
By the way, I have started reading this book just because of such case, I do need some simple lightweight solution to support transaction between NHibernate and Dapper.  
### IQuery and ICriteria
IQuery gives you powerful ways to perform queries against the db while controlling how the query is executed.  
Queries are written in HQL or in your native db dialect (mysql, mssql, etc).  
IQuery is lightweight and can't be used outside of ISession that created it.  
The ICriteria is similar; it allows us create and execute object-oriented criteria queries.
## A bit more advanced features (APIs, interfaces)
### Callback interfaces
Callback interfaces allow the app to recieve a notification when something interesting happens to an object - for instance, when it loaded, deleted or updated.  
The ILifecycle and IValidatable interfaces let a persistent object react to events relating to its own persistent lifecycle.  
### Types
An HN _Type_ maps the .NET type to db column type.
### Lower-level interfaces
You can extend basic implementation by using custom implementation for:
* Primary-key generation (IIdentifierGenerator)
* SQL dialect support (Dialect abstract class)
* Caching strategies (ICache and ICacheProvider)
* ADO.NET connection management (IConnectionProvider)
* Transaction management (ITransactionFactory and ITransaction)
* ORM strategies (IClassPersister)
* Property-access strategies (IPropertyAccessor)
* Proxy creation (IProxyFactory)
