import Geocoder from "react-native-geocoding"
import {Navigation} from "react-native-navigation"
import {GOOGLE_MAPS_API_KEY} from "./credentials"
import {screenNames} from "./src/screenNames"
import {withProvider} from "./src/screens"
import {CustomerLogin} from "./src/screens/customer-login"
import {CustomerRegistration} from "./src/screens/customer-login/customer-registration"
import {FoodInfo} from "./src/screens/food-info"
import {Home} from "./src/screens/home"
import {RestaurantInfo} from "./src/screens/restaurant-info"
import {RestaurantLogin} from "./src/screens/restaurant-login"
import {RestaurantRegistration} from "./src/screens/restaurant-login/restaurant-registration"
import {AddFood} from "./src/screens/restaurant/addFood"
import {Food} from "./src/screens/restaurant/food"
import {Profile} from "./src/screens/restaurant/profile"
import {Drawer as CustomerDrawer} from "./src/screens/user/drawer/drawer"
import {Food as CustomerFood} from "./src/screens/user/food"
import {Home as CustomerHome} from "./src/screens/user/home"
import {Profile as CustomerProfile} from "./src/screens/user/profile"
import {Reservations as CustomerReservations} from "./src/screens/user/reservations"
import {RestaurantList} from "./src/screens/user/restaurants"
import {FoodList} from "./src/screens/user/restaurants/food-list"

Geocoder.init(GOOGLE_MAPS_API_KEY)

Navigation.registerComponent(screenNames.HOME_SCREEN, () => withProvider(Home))
Navigation.registerComponent(screenNames.CUSTOMER_RESTAURANTS, () => withProvider(RestaurantList))
Navigation.registerComponent(screenNames.CUSTOMER_FOOD, () => withProvider(FoodList))
Navigation.registerComponent(screenNames.CUSTOMER_HOME, () => withProvider(CustomerHome))
Navigation.registerComponent(screenNames.CUSTOMER_FOOD, () => withProvider(CustomerFood))
Navigation.registerComponent(screenNames.RESTAURANT_LOGIN, () => withProvider(RestaurantLogin))
Navigation.registerComponent(screenNames.RESTAURANT_REGISTRATION, () => withProvider(RestaurantRegistration))
Navigation.registerComponent(screenNames.RESTAURANT_FOOD, () => withProvider(Food))
Navigation.registerComponent(screenNames.RESTAURANT_PROFILE, () => withProvider(Profile))
Navigation.registerComponent(screenNames.FOOD_INFO, () => withProvider(FoodInfo))
Navigation.registerComponent(screenNames.RESTAURANT_INFO, () => withProvider(RestaurantInfo))
Navigation.registerComponent(screenNames.RESTAURANT_ADD_FOOD, () => withProvider(AddFood))
Navigation.registerComponent(screenNames.CUSTOMER_RESERVATIONS, () => withProvider(CustomerReservations))
Navigation.registerComponent(screenNames.CUSTOMER_DRAWER, () => withProvider(CustomerDrawer))
Navigation.registerComponent(screenNames.CUSTOMER_LOGIN, () => withProvider(CustomerLogin))
Navigation.registerComponent(screenNames.CUSTOMER_REGISTRATION, () => withProvider(CustomerRegistration))
Navigation.registerComponent(screenNames.CUSTOMER_PROFILE, () => withProvider(CustomerProfile))

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
