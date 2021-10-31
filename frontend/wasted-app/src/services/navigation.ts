import {ComponentType} from "react"
import {Navigation, Options} from "react-native-navigation"
import {Colors} from "react-native-ui-lib"
import {screenNames} from "../screenNames"
import {RestaurantLoginOwnProps} from "../screens/restaurant-login/interfaces"
import {RestaurantRegistrationOwnProps} from "../screens/restaurant-login/restaurant-registration/interfaces"
import {FoodListOwnProps} from "../screens/user/restaurants/food-list/interfaces"
import {RestaurantListOwnProps} from "../screens/user/restaurants/interfaces"

const navigateTo = (currentComponentId: string, componentName: string, props: any = {}, screenTitle: string) => {
  return Navigation.push(currentComponentId, {
    component: {
      name: componentName,
      options: {
        topBar: {
          title: {
            text: screenTitle
          }
        }
      },
      passProps: {...props}
    }
  })
}

const showModal = (componentName: string, props: any = {}, screenTitle: string) => {
  return Navigation.showModal({
    stack: {
      children: [
        {
          component: {
            name: componentName,
            options: {
              topBar: {
                title: {
                  text: screenTitle
                }
              }
            },
            passProps: {...props}
          }
        }
      ]
    }
  })
}

export const addOptions = <Props>(component: ComponentType<Props>, options: (props: Props) => Options) => {
  // @ts-expect-error
  component.options = options
}

export const navigateToRestaurantList = (componentId: string, props: RestaurantListOwnProps) =>
  navigateTo(componentId, screenNames.USER_RESTAURANTS, props, "Restaurant List")

export const navigateToRestaurantLogin = (componentId: string, props: RestaurantLoginOwnProps) =>
  navigateTo(componentId, screenNames.RESTAURANT_LOGIN, props, "Login")

export const navigateToRestaurantRegistration = (componentId: string, props: RestaurantRegistrationOwnProps) =>
  navigateTo(componentId, screenNames.RESTAURANT_REGISTRATION, props, "Registration")

export const navigateToFoodList = (componentId: string, props: FoodListOwnProps) =>
  navigateTo(componentId, screenNames.USER_FOOD_LIST, props, "Food List")

export const setRestaurantRoot = props =>
  Navigation.setRoot({
    root: {
      bottomTabs: {
        id: "RESTAURANT_BOTTOM_TABS",
        children: [
          {
            stack: {
              id: "RESTAURANT_FOOD_TAB",
              children: [
                {
                  component: {
                    name: screenNames.RESTAURANT_FOOD
                  }
                }
              ],
              options: {
                bottomTab: {
                  icon: require("../../assets/food-25x25.png"),
                  text: "Food",
                  fontSize: 13,
                  selectedIconColor: Colors.black,
                  iconColor: Colors.grey30,
                  selectedTextColor: Colors.black,
                  textColor: Colors.grey30
                }
              }
            }
          },
          {
            stack: {
              id: "RESTAURANT_PROFILE_TAB",
              children: [
                {
                  component: {
                    name: screenNames.RESTAURANT_PROFILE
                  }
                }
              ],
              options: {
                bottomTab: {
                  icon: require("../../assets/profile-30x30.png"),
                  text: "Profile",
                  fontSize: 13,
                  selectedIconColor: Colors.black,
                  iconColor: Colors.grey30,
                  selectedTextColor: Colors.black,
                  textColor: Colors.grey30
                }
              }
            }
          }
        ]
      }
    }
  })

export const setUserRoot = props =>
  Navigation.setRoot({
    root: {
      bottomTabs: {
        id: "USER_BOTTOM_TABS",
        children: [
          {
            stack: {
              id: "USER_HOME_TAB",
              children: [
                {
                  component: {
                    name: screenNames.USER_HOME
                  }
                }
              ],
              options: {
                bottomTab: {
                  icon: require("../../assets/food-25x25.png"),
                  text: "Home",
                  fontSize: 13,
                  selectedIconColor: Colors.black,
                  iconColor: Colors.grey30,
                  selectedTextColor: Colors.black,
                  textColor: Colors.grey30
                }
              }
            }
          },
          {
            stack: {
              id: "USER_FOOD_TAB",
              children: [
                {
                  component: {
                    name: screenNames.USER_FOOD
                  }
                }
              ],
              options: {
                bottomTab: {
                  icon: require("../../assets/profile-30x30.png"),
                  text: "Food",
                  fontSize: 13,
                  selectedIconColor: Colors.black,
                  iconColor: Colors.grey30,
                  selectedTextColor: Colors.black,
                  textColor: Colors.grey30
                }
              }
            }
          },
          {
            stack: {
              id: "USER_RESTAURANTS_TAB",
              children: [
                {
                  component: {
                    name: screenNames.USER_RESTAURANTS
                  }
                }
              ],
              options: {
                bottomTab: {
                  icon: require("../../assets/food-25x25.png"),
                  text: "Restaurants",
                  fontSize: 13,
                  selectedIconColor: Colors.black,
                  iconColor: Colors.grey30,
                  selectedTextColor: Colors.black,
                  textColor: Colors.grey30
                }
              }
            }
          }
        ]
      }
    }
  })
