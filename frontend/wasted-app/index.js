import Geocoder from "react-native-geocoding"
import {Navigation} from "react-native-navigation"
import {GOOGLE_MAPS_API_KEY} from "./credentials"
import {screenNames} from "./src/screenNames"
import {withProvider} from "./src/screens"
import {FoodInfo} from "./src/screens/food-info"
import {Home} from "./src/screens/home"
import {RestaurantInfo} from "./src/screens/restaurant-info"
import {RestaurantLogin} from "./src/screens/restaurant-login"
import {RestaurantRegistration} from "./src/screens/restaurant-login/restaurant-registration"
import {AddFood} from "./src/screens/restaurant/addFood"
import {Food} from "./src/screens/restaurant/food"
import {Profile} from "./src/screens/restaurant/profile"
import {UserLogin} from "./src/screens/user-login"
import {UserRegistration} from "./src/screens/user-login/user-registration"
import {Food as UserFood} from "./src/screens/user/food"
import {Home as UserHome} from "./src/screens/user/home"
import {RestaurantList} from "./src/screens/user/restaurants"
import {FoodList} from "./src/screens/user/restaurants/food-list"

Geocoder.init(GOOGLE_MAPS_API_KEY)

Navigation.registerComponent(screenNames.HOME_SCREEN, () => withProvider(Home))
Navigation.registerComponent(screenNames.USER_RESTAURANTS, () => withProvider(RestaurantList))
Navigation.registerComponent(screenNames.USER_FOOD_LIST, () => withProvider(FoodList))
Navigation.registerComponent(screenNames.USER_HOME, () => withProvider(UserHome))
Navigation.registerComponent(screenNames.USER_FOOD, () => withProvider(UserFood))
Navigation.registerComponent(screenNames.RESTAURANT_LOGIN, () => withProvider(RestaurantLogin))
Navigation.registerComponent(screenNames.RESTAURANT_REGISTRATION, () => withProvider(RestaurantRegistration))
Navigation.registerComponent(screenNames.RESTAURANT_FOOD, () => withProvider(Food))
Navigation.registerComponent(screenNames.RESTAURANT_PROFILE, () => withProvider(Profile))
Navigation.registerComponent(screenNames.FOOD_INFO, () => withProvider(FoodInfo))
Navigation.registerComponent(screenNames.RESTAURANT_INFO, () => withProvider(RestaurantInfo))
Navigation.registerComponent(screenNames.RESTAURANT_ADD_FOOD, () => withProvider(AddFood))
Navigation.registerComponent(screenNames.USER_LOGIN, () => withProvider(UserLogin))
Navigation.registerComponent(screenNames.USER_REGISTRATION, () => withProvider(UserRegistration))

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
