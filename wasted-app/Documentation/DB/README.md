# Documentation DB

## Tables
* TypesOfFood: represents all the types of food. There is a default type
for unclassified meals.
    * idTypeOfFood: identifier.
    * name: name of the type.
* Restaurants: represents the restaurants registered in the app.
    * idRestaurant: identifier.
    * name: name of the restaurant.
    * longitude: longitude of the restaurant's ubication.
    * latitude: latitude of the restaurant's ubication.
    * password: password for the login. It can be changed and it will be encrypted.
    * mail: mail to identify the user.
* Foods: represents the shipments. A restaurant can upload a new meal (food) so that a user can see the restaurant's ubication and pick it up. 
    * isReserved: if the meal has already been reserved by another user. (future versions)
    * name: name of the meal.
    * price: price of the meal.
    * createdAt: when the restaurant uploaded the meal to the platform.
    * idRestaurant: extern key to identify the restaurant.
    * idTypeOfFood: extern key to identify the type of food.

## Entity-Relationship Diagram
A restaurant can register and login to the app with its email and password. Then, it can upload food, which will be visible to the users. 
A food can only exists if the restaurant uploaded it, and can only be owned by a restaurant.
A meal has one type of food to allow the user filter.

![E-R diagram](E-R.drawio.png)