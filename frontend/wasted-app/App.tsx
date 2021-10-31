import {createBottomTabNavigator} from "@react-navigation/bottom-tabs"
import {NavigationContainer} from "@react-navigation/native"
import {createNativeStackNavigator} from "@react-navigation/native-stack"
import React from "react"
import {Colors, Text, View} from "react-native-ui-lib"
import Icon from "react-native-vector-icons/FontAwesome"
import {FoodList} from "./src/screens/food-list"
import {Home} from "./src/screens/home"
import {RestaurantList} from "./src/screens/restaurant-list"
import {RestaurantLoginRegistration} from "./src/screens/restaurant-login-register"
import {RestaurantRegistration} from "./src/screens/restaurant-login-register/restaurant-registration"
import {Food} from "./src/screens/restaurant/food/food"

const Stack = createNativeStackNavigator()
const UserHomeStack = createNativeStackNavigator()
const RestaurantHomeStack = createNativeStackNavigator()
const UserTabs = createBottomTabNavigator()
const RestaurantTabs = createBottomTabNavigator()

const UserHomeScreenStack = () => (
  <UserHomeStack.Navigator>
    <UserHomeStack.Screen
      name="RestaurantList"
      component={RestaurantList}
      options={{header: () => <Text>Hello</Text>}}
    />
    <UserHomeStack.Screen name="FoodList" component={FoodList} />
  </UserHomeStack.Navigator>
)

const RestaurantHomeScreenStack = () => (
  <RestaurantHomeStack.Navigator>
    <RestaurantHomeStack.Screen
      name="Restaurant Login"
      component={RestaurantLoginRegistration}
      options={{headerShown: false}}
    />
    <RestaurantHomeStack.Screen name="Restaurant Registration" component={RestaurantRegistration} />
  </RestaurantHomeStack.Navigator>
)

const UserScreenTabs = () => (
  <UserTabs.Navigator>
    <UserTabs.Screen
      name="Home"
      component={UserHomeScreenStack}
      options={{
        headerShown: false,
        tabBarIcon: ({focused}) => <Icon name="home" size={30} color={focused ? Colors.black : Colors.grey40} />
      }}
    />
  </UserTabs.Navigator>
)

const RestaurantScreenTabs = () => (
  <RestaurantTabs.Navigator>
    <RestaurantTabs.Screen
      name="Profile"
      component={RestaurantHomeScreenStack}
      options={{
        headerShown: false,
        tabBarIcon: ({focused}) => <Icon name="sign-in" size={30} color={focused ? Colors.black : Colors.grey40} />
      }}
    />
    <RestaurantTabs.Screen
      name="Food"
      component={Food}
      options={{
        headerShown: false,
        tabBarIcon: ({focused}) => <Icon name="sign-in" size={30} color={focused ? Colors.black : Colors.grey40} />
      }}
    />
  </RestaurantTabs.Navigator>
)

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Home">
        <Stack.Screen name="Home" component={Home} />
        <Stack.Screen name="UserScreenTabs" component={UserScreenTabs} />
        <Stack.Screen name="RestaurantScreenTabs" component={RestaurantScreenTabs} />
      </Stack.Navigator>
    </NavigationContainer>
  )
}
