# First part

## Objetives

## Demo

## Requirements

1. Creating and using your own class, struct and enum (with flag(s), preferably).

	• Coords.cs - struct\
	• Distances.cs - enum 

2. Property (standard, indexed, auto-implemented) usage in struct and class.

	• We primarily used auto-implemented properties because it is easier than writing them manually. There are lots of examples throughout the code like Credetials.cs.\
	• We didn't use indexed properties.

3. Named and optional argument usage.

	• Optional argument usage can be found in Food.cs we can give an optional argument date but if it is not provided then the current date is used.\
	• We didn't use named arguments because there is not a lot of optional arguments in our classes therefore there is no need for named argument usage.
	
4. Extension method usage.

	• Custom extension methods are created in ListOfFood.cs and are used in RestaurantViewFoodControl.cs line 40 and line 45.

5. Reading from file.

	• Reading from file can be seen in BaseRepository.cs on line 35.

6. Generic type usage.

	• Generic type usage can be found in in the interface IBaseRepository.cs or IAuthService.cs.

7. Regex.

	• Regex is used in Password.cs to validate if a password contains certain characters.

8. Widening and narrowing type conversions.

	• Narrowing and widening type conversions can be found in the CoordsHelper.cs class narrowing on line 16 and widening on line 37.

9. Putting data to collection, iterating through it the right way.

	• Collections are used and iterated in FoodUtilities.cs for example line 28.

10. LINQ to Objects usage (methods and queries), including groupJoin.

	• Query can be found in RestaurantFoodControl.cs on line 37 LINQ usage can be found in FoodUtilities.cs line 15 and 43.

11. Implementing some of the standard .NET interfaces (IEnumerable, IComparable, IComparer, IEquatable, IEnumerator, etc.)

	• We implement IComparer in FoodCheaperFirst.cs so we could compare and sort out own objects and IEquatable in BaseDto.cs so we could find if a list contains our object.