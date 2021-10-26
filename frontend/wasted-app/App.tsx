import {createBottomTabNavigator} from "@react-navigation/bottom-tabs"
import {NavigationContainer} from "@react-navigation/native"
import {createNativeStackNavigator} from "@react-navigation/native-stack"
import React from "react"
import {Colors} from "react-native-ui-lib"
import Icon from "react-native-vector-icons/FontAwesome"
import {FoodList} from "./src/screens/food-list"
import {Home} from "./src/screens/home"
import {RestaurantList} from "./src/screens/restaurant-list"
import {RestaurantLoginRegistration} from "./src/screens/restaurant-login-register"
import {RestaurantRegistration} from "./src/screens/restaurant-login-register/restaurant-registration"

const HomeStack = createNativeStackNavigator()
const LoginStack = createNativeStackNavigator()
const Tabs = createBottomTabNavigator()

const HomeScreenStack = () => (
  <HomeStack.Navigator initialRouteName="RestaurantList">
    <HomeStack.Screen name="Home" component={Home} />
    <HomeStack.Screen name="RestaurantList" component={RestaurantList} />
    <HomeStack.Screen name="FoodList" component={FoodList} />
  </HomeStack.Navigator>
)

const LoginScreenStack = () => (
  <LoginStack.Navigator initialRouteName="Restaurant Login">
    <LoginStack.Screen name="Restaurant Login" component={RestaurantLoginRegistration} />
    <LoginStack.Screen name="Restaurant Registration" component={RestaurantRegistration} />
  </LoginStack.Navigator>
)

export default function App() {
  return (
    <NavigationContainer>
      <Tabs.Navigator>
        <Tabs.Screen
          name="Home"
          component={HomeScreenStack}
          options={{
            headerShown: false,
            tabBarIcon: ({focused}) => <Icon name="home" size={30} color={focused ? Colors.black : Colors.grey40} />
          }}
        />
        <Tabs.Screen
          name="Login"
          component={LoginScreenStack}
          options={{
            headerShown: false,
            tabBarIcon: ({focused}) => <Icon name="sign-in" size={30} color={focused ? Colors.black : Colors.grey40} />
          }}
        />
      </Tabs.Navigator>
    </NavigationContainer>
  )
}
