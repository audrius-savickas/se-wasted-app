import Geocoder from "react-native-geocoding"
import {Navigation} from "react-native-navigation"
import {GOOGLE_MAPS_API_KEY} from "./credentials"
import {screenNames} from "./src/screenNames"
import {FoodInfo} from "./src/screens/food-info"
import {Home} from "./src/screens/home"
import {RestaurantInfo} from "./src/screens/restaurant-info"
import {RestaurantLogin} from "./src/screens/restaurant-login"
import {RestaurantRegistration} from "./src/screens/restaurant-login/restaurant-registration"
import { AddFood } from "./src/screens/restaurant/addFood"
import {Food} from "./src/screens/restaurant/food"
import {Profile} from "./src/screens/restaurant/profile"
import {Food as UserFood} from "./src/screens/user/food"
import {Home as UserHome} from "./src/screens/user/home"
import {RestaurantList} from "./src/screens/user/restaurants"
import {FoodList} from "./src/screens/user/restaurants/food-list"

Geocoder.init(GOOGLE_MAPS_API_KEY)

Navigation.registerComponent(screenNames.HOME_SCREEN, () => Home)
Navigation.registerComponent(screenNames.USER_RESTAURANTS, () => RestaurantList)
Navigation.registerComponent(screenNames.USER_FOOD_LIST, () => FoodList)
Navigation.registerComponent(screenNames.USER_HOME, () => UserHome)
Navigation.registerComponent(screenNames.USER_FOOD, () => UserFood)
Navigation.registerComponent(screenNames.RESTAURANT_LOGIN, () => RestaurantLogin)
Navigation.registerComponent(screenNames.RESTAURANT_REGISTRATION, () => RestaurantRegistration)
Navigation.registerComponent(screenNames.RESTAURANT_FOOD, () => Food)
Navigation.registerComponent(screenNames.RESTAURANT_PROFILE, () => Profile)
Navigation.registerComponent(screenNames.FOOD_INFO, () => FoodInfo)
Navigation.registerComponent(screenNames.RESTAURANT_INFO, () => RestaurantInfo)
Navigation.registerComponent(screenNames.RESTAURANT_ADD_FOOD, () => AddFood)

Navigation.events().registerAppLaunchedListener(() => {
  Navigation.setRoot({
    root: {
      stack: {
        children: [
          {
            component: {
              name: screenNames.HOME_SCREEN
            }
          }
        ]
      }
    }
  })
})
