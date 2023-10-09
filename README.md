# CS295N-Example-BookReviews-DotNet6
Book Review example from LCC-CIT/CS295N-Example-BookReviews migrated to ASP.NET 6.0 MVC from ASP.NET Core MVC 3.1. The migration process is described in https://lcc-cit.github.io/CS295N-CourseMaterials/Notes/UpgradeMvcAppToDotNeT6.html

This is the example code for CS295N, Web Development 1: ASP.NET

 ## Branchs
- Not added yet:

  - 1-EmptySite  
    Just an empty ASP.NET Core site created from the Visual Studio project template. The home view has been customized.

  - 2-SkeletalSite  
    Controllers and views for the skeleton of a site were added: BookController, ReviewController, and associated views.

- 3-Data  
  An imput form was added so a book reveiw can be entered by a user. It is just eched back to the Review page.

 - 7-RepositoryAndUnitTests  
     The repository pattern is implemented to facilitate unit testing of controller methods.  
       A ReviewRepository was added and the ReviewController was refactored to use it. A FakeReviewRepository was added and unit tests were written that use it.
- 8A-SeedData  
  Code to seed the database with some initial book reviews was added.
- 8B-LinqFiltering  
  Added code to filter reviews by book title or reviewer.
