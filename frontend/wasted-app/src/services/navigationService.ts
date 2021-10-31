import {ComponentType} from "react"
import {Navigation, Options} from "react-native-navigation"
import {screenNames} from "../screenNames"
import {FoodListProps} from "../screens/food-list/interfaces"
import {RestaurantListOwnProps} from "../screens/restaurant-list/interfaces"
import {RestaurantLoginOwnProps} from "../screens/restaurant-login/interfaces"

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
  navigateTo(componentId, screenNames.RESTAURANT_LIST, props, "Restaurant List")

export const navigateToRestaurantLogin = (componentId: string, props: RestaurantLoginOwnProps) =>
  navigateTo(componentId, screenNames.RESTAURANT_LOGIN, props, "Restaurant Login")

export const navigateToRestaurantRegistration = (componentId: string, props: RestaurantListOwnProps) =>
  navigateTo(componentId, screenNames.RESTAURANT_REGISTRATION, props, "Restaurant Registration")

export const navigateToFoodList = (componentId: string, props: FoodListProps) =>
  navigateTo(componentId, screenNames.FOOD_LIST, props, "Food List")
