import {createBottomTabNavigator} from "@react-navigation/bottom-tabs"
import React from "react"
import {RestaurantList} from "../restaurant-list"

const Tabs = createBottomTabNavigator()

export const UserScreen = () => (
  <Tabs.Navigator initialRouteName="RestaurantList">
    <Tabs.Screen name="RestaurantList" component={RestaurantList} />
  </Tabs.Navigator>
)
