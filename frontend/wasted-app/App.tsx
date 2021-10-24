import {NavigationContainer} from "@react-navigation/native"
import {createNativeStackNavigator} from "@react-navigation/native-stack"
import React from "react"
import {FoodList} from "./src/screens/food-list"
import {Home} from "./src/screens/home"
import {RestaurantList} from "./src/screens/restaurant-list"
import {RestaurantLoginRegistration} from "./src/screens/restaurant-login-register"
import {RestaurantRegistration} from "./src/screens/restaurant-login-register/restaurant-registration"

const Stack = createNativeStackNavigator()

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Home">
        <Stack.Screen name="Home" component={Home} />
        <Stack.Screen name="RestaurantLoginRegistration" component={RestaurantLoginRegistration} />
        <Stack.Screen name="RestaurantList" component={RestaurantList} />
        <Stack.Screen name="RestaurantRegistration" component={RestaurantRegistration} />
        <Stack.Screen name="FoodList" component={FoodList} />
      </Stack.Navigator>
    </NavigationContainer>
  )
}
