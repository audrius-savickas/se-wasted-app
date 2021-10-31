import {Navigation} from "react-native-navigation"
import {screenNames} from "./src/screenNames"
import {FoodList} from "./src/screens/food-list"
import {Home} from "./src/screens/home"
import {RestaurantList} from "./src/screens/restaurant-list"
import {RestaurantLogin} from "./src/screens/restaurant-login"
import {RestaurantRegistration} from "./src/screens/restaurant-login/restaurant-registration"
import {Food} from "./src/screens/restaurant/food"
import {Profile} from "./src/screens/restaurant/profile"

Navigation.registerComponent(screenNames.HOME_SCREEN, () => Home)
Navigation.registerComponent(screenNames.RESTAURANT_LIST, () => RestaurantList)
Navigation.registerComponent(screenNames.RESTAURANT_LOGIN, () => RestaurantLogin)
Navigation.registerComponent(screenNames.RESTAURANT_REGISTRATION, () => RestaurantRegistration)
Navigation.registerComponent(screenNames.FOOD_LIST, () => FoodList)
Navigation.registerComponent(screenNames.RESTAURANT_FOOD, () => Food)
Navigation.registerComponent(screenNames.RESTAURANT_PROFILE, () => Profile)

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
