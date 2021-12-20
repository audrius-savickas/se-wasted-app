import {ComponentType} from "react"
import {Navigation, Options, OptionsModalPresentationStyle} from "react-native-navigation"
import {Assets, Colors} from "react-native-ui-lib"
import {screenNames} from "../screenNames"
import {FoodInfoProps} from "../screens/food-info/interfaces"
import {RestaurantInfoProps} from "../screens/restaurant-info/interfaces"
import {RestaurantLoginOwnProps} from "../screens/restaurant-login/interfaces"
import {RestaurantRegistrationOwnProps} from "../screens/restaurant-login/restaurant-registration/interfaces"
import {CustomerInfoProps} from "../screens/restaurant/customer-info/interfaces"
import {RestaurantProfileProps} from "../screens/restaurant/profile/interfaces"
import {CustomerProfileProps} from "../screens/user/profile/interfaces"
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
                },
                leftButtons: [
                  {
                    id: "DISMISS",
                    icon: Assets.icons.x
                  }
                ]
              },
              modalPresentationStyle: OptionsModalPresentationStyle.fullScreen
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

export const showFoodInfoModal = (props: FoodInfoProps) => showModal(screenNames.FOOD_INFO, props, "Food Info")

export const showCustomerProfileModal = (props: CustomerProfileProps) =>
  showModal(screenNames.CUSTOMER_PROFILE, props, "Your Profile")

export const showCustomerInfoModal = (props: CustomerInfoProps) =>
  showModal(screenNames.RESTAURANT_CUSTOMER_INFO, props, "Customer Info")

export const showRestaurantProfileModal = (props: RestaurantProfileProps) =>
  showModal(screenNames.RESTAURANT_PROFILE, props, "Your Restaurant")

export const navigateToFoodInfo = (componentId: string, props: FoodInfoProps) =>
  navigateTo(componentId, screenNames.FOOD_INFO, props, "Food Info")

export const navigateToRestaurantInfo = (componentId: string, props: RestaurantInfoProps) =>
  navigateTo(componentId, screenNames.RESTAURANT_INFO, props, "Restaurant Info")

export const navigateToRestaurantList = (componentId: string, props: RestaurantListOwnProps) =>
  navigateTo(componentId, screenNames.CUSTOMER_RESTAURANTS, props, "Restaurant List")

export const navigateToRestaurantLogin = (componentId: string, props: RestaurantLoginOwnProps) =>
  navigateTo(componentId, screenNames.RESTAURANT_LOGIN, props, "Login")

export const navigateToRestaurantRegistration = (componentId: string, props: RestaurantRegistrationOwnProps) =>
  navigateTo(componentId, screenNames.RESTAURANT_REGISTRATION, props, "Registration")

export const navigateToFoodList = (componentId: string, props: FoodListOwnProps) =>
  navigateTo(componentId, screenNames.CUSTOMER_FOOD_LIST, props, "Food List")

export const navigateToCustomerLogin = (componentId: string) =>
  navigateTo(componentId, screenNames.CUSTOMER_LOGIN, undefined, "User Login")

export const navigateToCustomerRegistration = (componentId: string) =>
  navigateTo(componentId, screenNames.CUSTOMER_REGISTRATION, undefined, "User Registration")

export const setRestaurantRoot = () =>
  Navigation.setRoot({
    root: {
      sideMenu: {
        left: {
          component: {
            name: screenNames.RESTAURANT_DRAWER
          }
        },
        center: {
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
                      icon: require("../../assets/food-30x30.png"),
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
                  id: "RESTAURANT_ADD_FOOD",
                  children: [
                    {
                      component: {
                        name: screenNames.RESTAURANT_ADD_FOOD
                      }
                    }
                  ],
                  options: {
                    bottomTab: {
                      icon: require("../../assets/add.png"),
                      text: "New meal",
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
                  id: "RESTAURANT_RESERVATIONS_TAB",
                  children: [
                    {
                      component: {
                        name: screenNames.RESTAURANT_RESERVATIONS
                      }
                    }
                  ],
                  options: {
                    bottomTab: {
                      icon: require("../../assets/time-left-25x25.png"),
                      text: "Reservations",
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
      }
    }
  })

export const setUserRoot = () =>
  Navigation.setRoot({
    root: {
      sideMenu: {
        left: {
          component: {
            name: screenNames.CUSTOMER_DRAWER
          }
        },
        center: {
          bottomTabs: {
            id: "USER_BOTTOM_TABS",
            options: {
              bottomTabs: {
                tabsAttachMode: "onSwitchToTab"
              },
              sideMenu: {
                openGestureMode: "entireScreen"
              }
            },
            children: [
              {
                stack: {
                  id: "USER_HOME_TAB",
                  children: [
                    {
                      component: {
                        name: screenNames.CUSTOMER_HOME
                      }
                    }
                  ],
                  options: {
                    bottomTab: {
                      icon: require("../../assets/home-25x25.png"),
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
                        name: screenNames.CUSTOMER_FOOD
                      }
                    }
                  ],
                  options: {
                    bottomTab: {
                      icon: require("../../assets/food-30x30.png"),
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
                        name: screenNames.CUSTOMER_RESTAURANTS
                      }
                    }
                  ],
                  options: {
                    bottomTab: {
                      icon: require("../../assets/restaurant-25x25.png"),
                      text: "Restaurants",
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
                  id: "USER_RESERVATIONS_TAB",
                  children: [
                    {
                      component: {
                        name: screenNames.CUSTOMER_RESERVATIONS
                      }
                    }
                  ],
                  options: {
                    bottomTab: {
                      icon: require("../../assets/time-left-25x25.png"),
                      text: "Reservations",
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
      }
    }
  })

export const setHomeRoot = () =>
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
